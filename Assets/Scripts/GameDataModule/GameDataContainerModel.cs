using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using System.Text;
using System.IO;

public class GameDataContainerModel: IDataContainer
{
    Dictionary<string, IDataTable> tables=new Dictionary<string, IDataTable>();

    public bool ExitData(IDataTable tableModel,uint id)
    {
        return tableModel.ExitData(id);
    }
    public IDataTable ReturnTable(string tableName) 
    {
        if (tables.ContainsKey(tableName))
            return tables[tableName];
        else
            return null;
    }
    public T[] ReturnDatas<T>() 
    {
        return tables.Values.Where(x => x.GetDatas().FirstOrDefault() is T)
            .SelectMany(x => x.GetDatas().Select(x=>(T)x)).ToArray();
    }
    public T ReturnData<T>(IDataTable tableModel, uint id) where T : IData
    {
        if(!ExitData(tableModel, id))
            Debug.LogError($"{id}에 해당하는 PresetData가 없습니다.");

        return (T)tableModel.GetData(id);
    }
    public  T ReturnData<T>(string tableName,uint id) where T: IData
    {
        tables.TryGetValue(tableName, out IDataTable tableModel);

        if(tableModel==null)
            Debug.LogError($"{tableName}에 해당하는 Table이없다");

        return ReturnData<T>(tableModel, id);
    }

    public T[] ReturnDatas<T>(string tableName) where T : IData
    {
        tables.TryGetValue(tableName, out IDataTable tableModel);

        if (tableModel == null)
            Debug.LogError($"{tableName}에 해당하는 Table이없다");

        return tableModel.GetDatas().Select(x=>(T)x).ToArray();
    }
    private void LoadDatas()
    {
        var jsonAssets = Resources.LoadAll<TextAsset>("JsonData");
        var types = GetType().Assembly.GetTypes();

        foreach (var jsonAsset in jsonAssets)
        {
            var JArray = GetJArray(jsonAsset.ToString());
            var jObjects = JArray.Children<JObject>().ToArray();
            var tokenNames = jObjects[0].Properties().Select(x => x.Name).ToArray();

            var type = types.Where(x => IsMatchedType(x, tokenNames)).Select(x => x).FirstOrDefault();
            var tableName = type.Name;
            Debug.Log(tableName);
            var instances = ParseJarrayTo(JArray, type);

            if (!tables.ContainsKey(tableName))
                tables.Add(tableName, new TableModel(tableName, new Dictionary<uint, IData>()));

            foreach (var instance in instances)
            {
                // Debug.Log($"{nameof(type) }");
                tables[tableName].AddData(instance.Id, instance);
            }

            //foreach (var jObject in jObjects)
            //{
            //    var tokenNames = jObject.Properties().Select(x => x.Name).ToArray();
            //    var type = types.Where(x => IsMatchedType(x, tokenNames)).Select(x => x).FirstOrDefault();
            //    ParseJarrayTo(JArray, type)
            //}

        }
    }
    public void AddTable(string tableName)
    {
        tables.Add(tableName, new TableModel(tableName, new Dictionary<uint, IData>()));
    }
    //인스턴스ID로들어감
    public void AddData(string tableName, IData data)
    {
        if (!tables.ContainsKey(tableName))
            AddTable(tableName);

        var table = ReturnTable(tableName);
        data.TableModel = table;
        tables[tableName].AddData(data);

    }
    public void AddData(string tableName,uint id,IData data)
    {
        if (!tables.ContainsKey(tableName))
            AddTable(tableName);

        var table = ReturnTable(tableName);
        data.Id = id;
        data.TableModel = table;
        tables[tableName].AddData(id, data);
    }

    public void RemoveData(string tableName,uint id)
    {
        tables[tableName].RemoveData(id);
    }
    public void LoadData<T>(string tableName, string path) where T : Data
    {
        var jsonAsset = Resources.Load<TextAsset>(path);
        if (jsonAsset == null)
            Debug.LogError($"{path}경로가 잘못됬거나 존재하지 않는 파일입니다.");

        var JArray = GetJArray(jsonAsset.ToString());
        var instances = ParseJarrayTo<T>(JArray);

        //if (!tables.ContainsKey(tableName))
        //    AddTable(tableName);

        foreach (var instance in instances)
        {
            AddData(tableName, instance.Id, instance);
           // tables[tableName].DataContainer.Add(instance.Id, instance);
        }
    }
    public void SaveData(string tableName,string path)
    {
        Debug.Log($"{tableName}데이터 저장 완료");
        StringBuilder sb=new StringBuilder();
        var datas = tables[tableName].GetDatas();

        var serializedDatas = datas.Select(x => JsonConvert.SerializeObject(x,x.GetType(),Formatting.Indented,null)).ToArray();
        var jobjects = serializedDatas.Select(x => JObject.Parse(x));
        JArray jarr = new JArray(jobjects);
        File.WriteAllText(path, jarr.ToString());
    }
    private bool IsMatchedType(Type type, string[] names)
    {
        var properties = type.GetProperties();

        foreach (var name in names)
        {
            var propertyName = properties.Where(x => x.Name.ToLower() == name.ToLower()).Select(x => x).FirstOrDefault();
            if (propertyName == null)
                return false;
        }
        return true;

    }

    private JArray GetJArray(string jsonText)
    {
        return JArray.Parse(jsonText);
    }
    private T[] ParseJarrayTo<T>(JArray jArray)
    {
        return jArray.Select(x => JsonConvert.DeserializeObject<T>(x.ToString())).ToArray();
    }
    private Entity[] ParseJarrayTo(JArray jArray, Type type)
    {
        return jArray.Select(x => (Entity)JsonConvert.DeserializeObject(x.ToString(), type)).ToArray();
    }
}