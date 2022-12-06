using System.Collections.Generic;

public interface IDataContainer
{
    bool ExitData(IDataTable tableModel, uint id);
    public IDataTable ReturnTable(string tableName);

    public T[] ReturnDatas<T>();

    public T ReturnData<T>(IDataTable tableModel, uint id) where T : IData;
    public T ReturnData<T>(string tableName, uint id) where T : IData;

    public T[] ReturnDatas<T>(string tableName) where T : IData;

    public void AddTable(string tableName);

    //인스턴스ID로들어감
    public void AddData(string tableName, IData data);

    public void AddData(string tableName, uint id, IData data);

    public void RemoveData(string tableName, uint id);
}
