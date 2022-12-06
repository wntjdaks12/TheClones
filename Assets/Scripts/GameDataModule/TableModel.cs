using System.Collections.Generic;
using System.Linq;
using System;

public interface IDataTable
{
    public string TableName { get; set; }

    public void AddData(uint id, IData data);
    public void AddData(IData data);
    public bool ExitData(uint id);
    public IData GetData(uint id);
    public void RemoveData(uint id);
    public IData[] GetDatas();
}
public class TableModel: IDataTable
{
    //uint CurrentInstanceId { get; set; }
    //
    //   현재 인스턴스 ID GET SET 퍼블릭으로 바꿈
    //                  |      
    //                  V
    public uint CurrentInstanceId { get; set; }

    public TableModel(string tableName, Dictionary<uint, IData> dataContainer)
    {
        TableName = tableName;
        DataContainer = dataContainer;
    }

    public string TableName { get; set; }

    Dictionary<uint, IData> DataContainer { get; set; }

    public void AddData(uint id,IData data)
    {
        data.InstanceId = ++CurrentInstanceId;
        DataContainer.Add(id, data);
    }

    public void AddData(IData data)
    {
        data.InstanceId = ++CurrentInstanceId;
        DataContainer.Add(CurrentInstanceId, data);
    }
    public bool ExitData(uint id)
    {
       DataContainer.TryGetValue(id, out IData data);
        if (data == null)
            return false;
        return true;
    }
    public IData GetData(uint id)
    {
        return DataContainer[id];
    }
    public void RemoveData(uint id)
    {
        DataContainer.Remove(id);
    }
    public IData[] GetDatas()
    {
        return DataContainer.Values.Select(x=>x).ToArray();
    }
}
