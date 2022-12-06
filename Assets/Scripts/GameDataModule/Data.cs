using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

public class Data:IData
{
    public event Action<IData> OnDataRemove;
    public uint Id { get;  set; }
    public uint InstanceId { get; set; }
    public IDataTable TableModel { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }
    public void OnRemoveData()
    {
        OnDataRemove?.Invoke(this);
    }
}
