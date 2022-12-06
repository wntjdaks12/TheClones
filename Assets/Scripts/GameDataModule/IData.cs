using System;
using Newtonsoft.Json;

//데이터의 가장 작은 단위
public interface IData : ICloneable
{
    public event Action<IData> OnDataRemove;

    public uint Id { get; set; }
    public uint InstanceId { get; set; }

    [JsonIgnore]
    IDataTable TableModel { get; set; }
}