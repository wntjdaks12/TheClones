using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;
using System.Linq;

public class ChoicePattern : MonoBehaviour, IDragHandler
{
    public Actor Target { get; set; }

    [Header("¹öÆ°")]
    [SerializeField] private Button handleButton;
    [SerializeField] private Button cancelButton, CheckButton;

    private MenuContents menuContents;

    private void Awake()
    {
        menuContents = GameObject.FindObjectOfType<MenuContents>();
    }

    private void Start()
    {
        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() =>
        {
            menuContents.ActiveAll();

            Target.OnRemoveData();

            Destroy(gameObject);
        });

        CheckButton.onClick.RemoveAllListeners();
        CheckButton.onClick.AddListener(() =>
        {
            var resDirVec = (Target.Transform.position - Camera.main.transform.position).normalized;

            var hitInfos = Physics.RaycastAll(Camera.main.transform.position, resDirVec * 1000);

            var a = hitInfos.Where(x => x.collider.GetComponent<GroundObject>() != null).ToArray();

            if (a.Length > 0)
            {
                Target.Transform.position = a[0].point;
                Target.Transform.GetComponent<CharacterAI>().Resume();

                menuContents.ActiveAll();

                Destroy(gameObject);
            }
        });
        
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                handleButton.transform.Rotate(0, 0, 0.25f);
            });

        this.LateUpdateAsObservable()
            .Subscribe(_ => transform.position = Camera.main.WorldToScreenPoint(Target.Transform.position));

        menuContents.DeactiveAll();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var mouseScrPos = Input.mousePosition; mouseScrPos.z = -Camera.main.transform.position.z;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScrPos);

        var resDirVec = (mouseWorldPos - Camera.main.transform.position).normalized;

        Target.Transform.position = Camera.main.transform.position + resDirVec * 10;
    }
}
