using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameBarObject : EntityObject
{
    [SerializeField] private TextMeshProUGUI nameTMP;

    private GameApplication gameApplication;

    private void Awake()
    {
        gameApplication = FindObjectOfType<GameApplication>();
    }

    public override void Init(Entity entity)
    {
        base.Init(entity);
        Debug.Log(entity.Subject.Id);
        nameTMP.text = gameApplication.App.GameModel.PresetDataModel.ReturnData<Name>(nameof(Name), entity.Subject.Id).LanguageKR;
    }

    private void LateUpdate()
    {
        var nameBar = Entity as NameBar;

        var actor = nameBar.Subject as Actor;

        transform.position = Camera.main.WorldToScreenPoint(actor.NameBarTransform.position);
    }
}
