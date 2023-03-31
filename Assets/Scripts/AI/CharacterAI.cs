using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.AI;

public class CharacterAI : ActorAI
{
    private NavMeshAgent navAgent;
    private NavMeshHit navHit;

    private bool isTestBool;
    private new void Awake()
    {
        base.Awake();

        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        navAgent.Warp(transform.position);

        // �ش� ����Ʈ�� �ʱ�ȭ ��ŵ�ϴ�.
        transform.position = ResetNavHitPos(0f);

        // �ش� ��ġ�� �����ϸ� ���ο� ��ġ�� �����մϴ�.
        this.UpdateAsObservable()
            .Where(_ => Vector3.SqrMagnitude(new Vector3(navHit.position.x, transform.position.y, navHit.position.z) - transform.position) <= 0.1f)
            .Subscribe(_ => ResetNavHitPos(5f));
    }

    public virtual void Update()
    {
        if (Entity is Character)
        {
            var character = Entity as Character;
            if (!isTestBool) Move(character.MoveSpeed);
        }
    }

    /// <summary>
    /// �̵��� ��ġ�� �����մϴ�.
    /// </summary>
    /// <returns></returns>
    private Vector3 ResetNavHitPos(float Range)
    {
        var navHitPos = Random.insideUnitSphere * Range + transform.position;

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

            if(!isTestBool) StartCoroutine(TestRangeAsync());
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
        isTestBool = true;

        if (Entity is ICaster)
        {
            App.GameController.GetComponent<RangeController>().Spawn("Range", 100001, transform.position, Entity);
        }

        yield return new WaitForSeconds(1.5f);


            if (Entity is ISight)
            {
                var sight = Entity as ISight;

                var targetObjs = sight.ReturnVisibleObjects(transform);

            for (int i = 0; i < targetObjs.Length; i++)
            {
                if (Entity is ISubject)
                {
                    var entityObj = targetObjs[i].GetComponent<EntityObject>();

                    var entity = entityObj.Entity;
                    
                    if (entity is ISpell)
                    {
                        var spell = entity as ISpell;
                        spell.Subject = entity;

                        var character = Entity as Character;
                        App.GameController.GetComponent<SkillController>().Spawn("Skill", character.skillId[0], spell.Subject.Transform.position, spell);
                    }
                }
            }
            
        }

        yield return new WaitForSeconds(3);

        isTestBool = false;
    }
}
