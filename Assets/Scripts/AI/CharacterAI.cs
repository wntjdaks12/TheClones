using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.AI;
using System;
using System.Linq;

public class CharacterAI : ActorAI
{
    private NavMeshAgent navAgent;
    private NavMeshHit navHit;

    private bool isTestCool, isTestDel;
    private bool isTest;

    // ���� ����
    private bool isTrace;
    private bool isCreate;

    private IEnumerator traceAsync;

    public event Action traceStartEvent, traceStopEvent;
    // ------------------------------------------------------

    private new void Awake()
    {
        base.Awake();

        navAgent = GetComponent<NavMeshAgent>();    
    }

    private void Start()
    {
        StartCoroutine(RandomAsync());
    }

    public virtual void Update()
    {
        if (!navAgent.enabled) return;

        if (Entity is Character)
        {
            var character = Entity as Character;

            if (character.StateType == Actor.StateTypes.Death) return;

            if (character.StateType != Actor.StateTypes.Hit)
            {
                if (!isTestDel)
                {
                    if (isTest)
                    {
                        Move(character.MoveSpeed);
                        character.OnActorMove();
                    }
                    else
                    {
                        character.OnActorIdle();
                        navAgent.velocity = Vector3.zero;
                    }
                }
                else
                {
                    navAgent.ResetPath();
                }

                if (!isTestCool)
                {
                    if (Entity is ISight)
                    {
                        var sight = Entity as ISight;

                        var targetObjs = sight.ReturnVisibleObjects(transform);

                        if (targetObjs.Length > 0) StartCoroutine(TestRangeAsync());
                    }
                }
            }

            if (character.StateType == Actor.StateTypes.Hit)
            {
                if (traceAsync != null) StopCoroutine(traceAsync);

                traceAsync = TraceAsync();

                StartCoroutine(traceAsync);
            }
        }
    }

    public override void Init(Actor actor)
    {
        base.Init(actor);
        navAgent.Warp(transform.position);

        // �ش� ����Ʈ�� �ʱ�ȭ ��ŵ�ϴ�.
        transform.position = ResetNavHitPos(0f);

        // �ش� ��ġ�� �����ϸ� ���ο� ��ġ�� �����մϴ�.
        this.UpdateAsObservable()
            .Where(_ => Vector3.SqrMagnitude(new Vector3(navHit.position.x, transform.position.y, navHit.position.z) - transform.position) <= 0.1f)
            .Subscribe(_ => ResetNavHitPos(5f));

        // ���� ���� �ʱ�ȭ ��ŵ�ϴ�.
        isTrace = false;
        isCreate = false;

        traceStartEvent = null;
        traceStopEvent = null;
        // ----------------------------------
    }

    public void Init2(Actor actor)
    {
        base.Init(actor);

        Pause();

        ChoicePatternSystem.OnCreate(actor);

        // ���� ���� �ʱ�ȭ ��ŵ�ϴ�.
        isTrace = false;
        isCreate = false;

        traceStartEvent = null;
        traceStopEvent = null;
        // ----------------------------------
    }

    public IEnumerator RandomAsync()
    {
        while (true)
        {
            var rand = UnityEngine.Random.Range(0, 2);

            switch (rand)
            {
                case 0: isTest = true; break;
                case 1: isTest = false; break;
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 5.0f));
        }
    }

    /// <summary>
    /// �̵��� ��ġ�� �����մϴ�.
    /// </summary>
    /// <returns></returns>
    private Vector3 ResetNavHitPos(float Range)
    {
        var randPos = UnityEngine.Random.insideUnitSphere * Range;

        var navHitPos = randPos + transform.position; navHitPos.y = transform.position.y;

        NavMesh.SamplePosition(navHitPos, out navHit, 10f, 1);

        return navHit.position;
    }  

    /// <summary>
    /// �̵��� ��ŵ�ϴ�.
    /// </summary>
    /// <param name="speed">���ǵ�</param>
    public virtual void Move(float speed)
    {
        if (!navAgent) return;

        var character = Entity as Character;
        var visibleObjects = character.ReturnVisibleObjects(transform);

        Vector3 destPos;
        if (visibleObjects.Length == 0)
        {
            destPos = ReurnDestPos();
        }
        else
        {
            var visibleObject = visibleObjects[0];
            destPos = visibleObject.transform.position;
        }

        navAgent.speed = character.MoveSpeed;
        navAgent.SetDestination(destPos);
    }
    public virtual Vector3 ReurnDestPos()
    {
        return navHit.position;
    }

    private IEnumerator TestRangeAsync()
    {
        isTestCool = true;
        isTestDel = true;
        /*
        if (Entity is ICaster)
        {
            App.GameController.GetComponent<RangeController>().Spawn("Range", 100001, transform.position, Entity);
        }

        yield return new WaitForSeconds(1.5f);*/

        yield return new WaitForSeconds(0f);

        if (Entity is ISight)
        {
            var sight = Entity as ISight;

            var targetObjs = sight.ReturnVisibleObjects(transform);

            var character = Entity as Character;

            var skill = App.GameModel.PresetDataModel.ReturnData<DevSkill>(nameof(DevSkill), character.skillId[0]);

            var otherEntities = Physics.OverlapSphere(transform.position, skill.Range, LayerMask.GetMask(skill.LayerName)).Select(x => x.GetComponent<EntityObject>().Entity).ToArray();

            var spawnCount = 0;

            for (int i = 0; i < otherEntities.Length; i++)
            {
                if (skill.SpawnCount == -1 || spawnCount < skill.SpawnCount)
                {
                    var pos = Vector3.zero;
                    var rot = Quaternion.identity;
                    Entity parent = null;

                    switch (skill.SpawnType)
                    {
                        case DevSkill.SpawnTypes.CasterCenter: 
                            pos = Entity.Transform.position;
                            rot = Entity.Transform.rotation;
                            parent = Entity;
                            break;
                        case DevSkill.SpawnTypes.SubjectCenter:
                            pos = otherEntities[i].Transform.position;
                            rot = otherEntities[i].Transform.rotation;
                            parent = otherEntities[i];
                            break;
                    }

                    App.GameController.GetComponent<SkillController>().Spawn("DevSkill", character.skillId[0], Entity, pos, rot, parent);

                    ++spawnCount;
                }
            }
        }

        yield return new WaitForSeconds(1);

        isTestDel = false;

        yield return new WaitForSeconds(2);

        isTestCool = false;
    }

    /// <summary>
    /// �����մϴ�
    /// </summary>
    /// <returns></returns>
    public IEnumerator TraceAsync()
    {
        isTrace = true;

        if (!isCreate)
        {
            App.GameController.GetController<HeadBarController>().Spawn("HeadBar", 110001, Entity.Transform.position, GameObject.Find("WorldCanvas").transform, Entity);
            App.GameController.GetController<NameBarController>().Spawn("NameBar", 251, Entity.Transform.position, GameObject.Find("WorldCanvas").transform, Entity);

            isCreate = true;
        }

        yield return new WaitForSeconds(0.7f);

        isCreate = false;
        isTrace = false;

        traceStopEvent?.Invoke();
        traceStopEvent = null;
    }

    public void Resume()
    {
        Entity.Rigidbody.useGravity = true;
        navAgent.enabled = true;
    }

    public void Pause()
    {
        Entity.Rigidbody.useGravity = false;
        navAgent.enabled = false;
    }
}
