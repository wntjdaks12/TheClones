using System;
using UnityEngine;

public class PoolableObject:MonoBehaviour,IData
{
    public PoolObject PoolObject { get; set; }
    public uint Id { get; set; }
    public uint InstanceId { get; set; }
    public IDataTable TableModel { get; set; }

    public event Action<IData> OnDataRemove;

    public object Clone()
    {
        return MemberwiseClone();
    }

    public void ReturnPoolableObject()
    {
        gameObject.SetActive(false);
        PoolObject.ReturnPoolableObject(this);
    }
}
