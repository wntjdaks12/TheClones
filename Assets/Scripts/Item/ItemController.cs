using UnityEngine;

public class ItemController : GameController
{

    public GameDataContainerModel PresetDataModel { get; set; }

   /* public T GetItem<T>(uint itemId) where T:Item
    {
        return PresetDataModel.ReturnData<T>("Item", itemId);
    }
    protected override void Awake()
    {
        PresetDataModel = App.GameModel.PresetDataModel;

        //PresetDataModel.LoadData<ItemInfo>(nameof(ItemInfo), "JsonData/ItemInfo");
        //PresetDataModel.LoadData<ItemSaleInfo>(nameof(ItemSaleInfo), "JsonData/ItemSaleInfo");
        //PresetDataModel.LoadData<EquipItem>(nameof(EquipItem), "JsonData/EquipItem");

    }
    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            var item = GetItem<Item>(100);
            //App.GameModel.Inventory.AddSlot("OwnedEquipItem", equipedItem);
            App.GameModel.Inventory.AddSlot("EquipItem", item.Id);
            App.GameModel.UserDataModel.AddData("OwnedEquipItem", new SlotItem(item.Id));
            Debug.Log(App.GameModel.UserDataModel.ReturnDatas<SlotItem>().Length);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            App.GameModel.UserDataModel.SaveData("OwnedEquipItem", "Assets/Resources/JsonData/OwnedEquipItem.json");
        }
    }*/
}
