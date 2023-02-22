using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameModel : Element
{
    //사전에 정의된  비 휘발성 데이터
    public GameDataContainerModel PresetDataModel
    {
        get => gamePresetDataModel ??= new GameDataContainerModel();
    }
    private GameDataContainerModel gamePresetDataModel;

    //런타임 중 할당되는 휘발성 데이터
    public GameDataContainerModel RuntimeDataModel
    {
        get => gameRuntimeDataModel ??= new GameDataContainerModel();
    }
    private GameDataContainerModel gameRuntimeDataModel;

    //유저 데이터 
    public GameDataContainerModel UserDataModel
    {
        get => userDataModel ??= new GameDataContainerModel();
    }
    private GameDataContainerModel userDataModel;


    public InventoryModel Inventory
    {
        get => inventory ??= new InventoryModel(App.GameModel.UserDataModel);
    }
    private InventoryModel inventory;

    protected override void Awake()
    {
        base.Awake();

        PresetDataModel.LoadData<StatData>(nameof(StatData), "JsonData/Stat");
        PresetDataModel.LoadData<Hero>(nameof(Hero), "JsonData/Hero");
        PresetDataModel.LoadData<Monster>(nameof(Monster), "JsonData/Monster");
        PresetDataModel.LoadData<Clon>(nameof(Clon), "JsonData/Clon");

        PresetDataModel.LoadData<Skill>(nameof(Skill), "JsonData/Skill");

        PresetDataModel.LoadData<ImageInfo>(nameof(ImageInfo), "JsonData/ImageInfo");

        PresetDataModel.LoadData<StaticEntity>(nameof(StaticEntity), "JsonData/StaticEntity");

        PresetDataModel.LoadData<DamageTMP>(nameof(DamageTMP), "JsonData/DamageTMP");

        PresetDataModel.LoadData<EquipItem>("Item", "JsonData/EquipItem");

        PresetDataModel.LoadData<Particle>(nameof(Particle), "JsonData/Particle");

        PresetDataModel.LoadData<EntityPrefabInfo>(nameof(EntityPrefabInfo), "JsonData/PrefabInfo");

        PresetDataModel.LoadData<Range>(nameof(Range), "JsonData/Range");

        PresetDataModel.LoadData<Map>(nameof(Map), "JsonData/Map");

        PresetDataModel.LoadData<HeadBar>(nameof(HeadBar), "JsonData/HeadBar");

        UserDataModel.LoadData<SlotItem>("OwnedEquipItem", "JsonData/OwnedEquipItem");
        //  UserDataModel.LoadData<SlotItem>("OwnedconsumableItem", "JsonData/Inventroy");

        PresetDataModel.LoadData<IAP>(nameof(IAP), "JsonData/IAP");

        PresetDataModel.LoadData<Tooltip>(nameof(Tooltip), "JsonData/Tooltip");

        PresetDataModel.LoadData<DropItemData>(nameof(DropItemData), "JsonData/DropItemData");
        PresetDataModel.LoadData<DropItem>(nameof(DropItem), "JsonData/DropItem");
    }
}
