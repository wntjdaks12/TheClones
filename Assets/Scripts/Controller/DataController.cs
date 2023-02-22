using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DataController : GameController
{
    public event Action<IData> OnDataSpawn;

    //엔티티 생성
    public IData AddData(string tableId,uint id)
    {
        var gamePresetData = App.GameModel.PresetDataModel;
        var gameRuntimeDataModel = App.GameModel.RuntimeDataModel;
        Debug.Log(tableId + ", : " + id);
        var data = (IData)gamePresetData.ReturnData<IData>(tableId, id).Clone();

        OnDataSpawn?.Invoke(data);
        gameRuntimeDataModel.AddData(tableId,data);

        return data;
    }
    public void RemoveEntity(IData data)
    {
        var gameRuntimeDataModel = App.GameModel.RuntimeDataModel;
        gameRuntimeDataModel.RemoveData(data.TableModel.TableName, data.InstanceId);
    }
}
