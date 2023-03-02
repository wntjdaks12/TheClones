using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Newtonsoft.Json;
/*
public interface IEquippable
{
    public enum EquipType{Hat,Weapon }
    EquipType equipType { get; }
    public void OnEquip();
    public void OnUnequip();
}
public interface IConsumable
{
    event Action<IConsumable> OnConsumableEvent;
    public void OnConsume();
}

public interface ISlot:IData
{
    int Count { get; set; }
}

public class InventoryModel
{
    public event Action<string ,ISlot> OnSlotAdd;

    public InventoryModel(IDataContainer dataContainer)
    {
        Init(dataContainer);
    }

    public Dictionary<string, SlotContainer> SlotContainers { get; private set; } = new Dictionary<string, SlotContainer>();
    void Init(IDataContainer dataContainer)
    {
        var tab = new SlotContainer
            (
            "EquipItem",
            1,
            dataContainer,
            false,
            false
            );
        SlotContainers.Add("EquipItem", tab);
    }
    public void AddSlot(string slotContainerName ,uint id)
    {
        SlotItem slot = new SlotItem(id);
        SlotContainers[slotContainerName].AddSlot(slot);

        OnSlotAdd?.Invoke(slotContainerName, slot);
    }
    public void AddSlot(string slotContainerName, ISlot slot)
    {
        SlotContainers[slotContainerName].AddSlot(slot);

        OnSlotAdd?.Invoke(slotContainerName, slot);
    }
}

public class SlotContainer
{
    IDataContainer DataContainer { get; }
    public SlotContainer(string tabName, int capacity, IDataContainer dataContainer, bool isEmpty, bool isFull)
    {
        TabName = tabName;
        Capacity = capacity;
        DataContainer = dataContainer;
        IsEmpty = isEmpty;
        IsFull = isFull;
    }

    public string TabName { get; }
    public int Capacity { get; private set; }
    public bool IsEmpty { get; }
    public bool IsFull { get; }

    public void AddSlot(ISlot slot)
    {
        if (IsFull)
            return;

      //  var slots = GetNotFullSlots(slot.Id);

        Debug.Log($"{slot.Id}추가");
        DataContainer.AddData(TabName,slot);
    }
    IEnumerable<ISlot> GetNotFullSlots(uint id)
    {
        return DataContainer.ReturnDatas<ISlot>().Where(x => x.Id == id)
            .Select(x=>x as ISlot)
            .OrderBy(x=>x.Count)
            .Select(x => x);
    }
}
public class SlotItem : Data, ISlot
{
    public SlotItem(uint id)
    {
        Id = id;
    }
    public int Count { get; set; }
}
//설치,장비,기타,소비
public abstract class Item : Data
{

    public enum ItemType { Equip, Consumable }
    public ItemType itmeType { get; set; }
    public string Name { get; set; }
    public Sprite ItemSprite{ get; }
}

public class ItemSaleInfo:Data
{
    public Currency.CurrencyType Currency { get; set; }
    public uint Price { get; set; }
}
public class ItemInfo : Data
{
    public string Description { get; set; }
}

public class Currency : Item
{
    public enum CurrencyType{Gold,Crystal }
}
public class EquipItem : Item, IEquippable, IUpgradable,IAbility, IUnlockable
{
    public int UpgradableCount { get; set; }
    public Ability Ability { get; set; } = new Ability();

    public uint AbilityOwnerInstanceId => InstanceId;

    public uint LockedTargetId => Id;

    public bool IsUnlocked { get; private set; }
    public IEquippable.EquipType equipType { get; private set; }

    public void OnEquip()
    {
        Debug.Log($"{Id}장착");
    }
    public void OnUnequip()
    {
        Debug.Log($"{Id}장착 해제");
    }

    public void OnUnlockSuccess(IUnlockable unlockable)
    {
        IsUnlocked = true;
    }

    public void OnUnlockFail(IUnlockable unlockable)
    {
        IsUnlocked = false;
    }
}

public class ConsumableItem : Item, IConsumable
{
    public event Action<IConsumable> OnConsumableEvent;

    public void OnConsume()
    {
        Debug.Log($"{Id}소모");
        OnConsumableEvent.Invoke(this);
    }
}
public interface IUpgradable
{
    public int UpgradableCount { get; set; }
}

public class UserUpgradeData : Data
{
    public int UpgradableCount { get; set; }
}
public class UpgradeModule
{
    public void Upgrade(IUpgradable upgradable)
    {
        upgradable.UpgradableCount--;
        Debug.Log("업그레이드 시도");
    }
}


public class DataInfo : Data
{ 
    public string Name { get; private set; }

    public string Description { get; private set; }

}

public interface IUnlockable
{
    uint LockedTargetId { get;}
    bool IsUnlocked { get; }
    void OnUnlockSuccess(IUnlockable unlockable);
    void OnUnlockFail(IUnlockable unlockable);

}

public class UnlockInfo : DataInfo
{
}

public class UnlockConditionInfo:Data
{
    public uint ConditionId { get; private set; }
}

public abstract class ConditionalAction
{
    public abstract bool IsSatisfyCondtion();
}
public class Condition
{ 

}

public class UnlockController:GameController
{
    Dictionary<uint, IUnlockable> LockedTargets { get; set; }

    Dictionary<uint,Func<bool>> Conditions { get; set; }

    public GameDataContainerModel PresetDataModel { get; set; }

    protected override void Awake()
    {
        PresetDataModel = App.GameModel.PresetDataModel;
    }
    public void Unlock(uint lockedTargetId)
    {
        var unlockConditionInfo=PresetDataModel.ReturnData<UnlockConditionInfo>(nameof(UnlockConditionInfo), lockedTargetId);

        var lockedTarget = LockedTargets[lockedTargetId];
     //   lockedTarget.Unlock(lockedTarget, Conditions[unlockConditionInfo.ConditionId]);
    }

    public void Unlock(IUnlockable lockedItem, Func<bool> IsUnclok)
    {
        if ((bool)IsUnclok?.Invoke())
        {
            Debug.Log($"{lockedItem.LockedTargetId}잠금해제!");
            lockedItem.OnUnlockSuccess(lockedItem);
        }
        else
        {
            Debug.Log($"{lockedItem.LockedTargetId}잠금 조건 미 달성!");
            lockedItem.OnUnlockFail(lockedItem);

        }
    }
}
*/