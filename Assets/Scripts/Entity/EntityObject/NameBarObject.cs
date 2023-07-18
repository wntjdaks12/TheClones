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

        nameTMP.text = gameApplication.App.GameModel.PresetDataModel.ReturnData<Name>(nameof(Name), entity.Subjects[0].Id).LanguageKR;
    }

    private void LateUpdate()
    {
        var nameBar = Entity as NameBar;

        var actor = nameBar.Subjects[0] as Actor;

        transform.position = Camera.main.WorldToScreenPoint(actor.NameBarTransform.position);
    }
}
