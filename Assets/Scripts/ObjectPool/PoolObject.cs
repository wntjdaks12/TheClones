using System.Collections.Generic;
using UnityEngine;

public class PoolObject
{
    Queue<PoolableObject> poolableObjects = new Queue<PoolableObject>();

    public T GetPoolableObject<T>(string path) where T:PoolableObject
    {
        if (poolableObjects.Count==0)
        {
            CreatePoolableObject<T>(path);
        }
        return poolableObjects.Dequeue() as T;
    }
    
    public void ReturnPoolableObject(PoolableObject poolableObject)
    {
        poolableObjects.Enqueue(poolableObject);
    }

    private void CreatePoolableObject<T>(string path) where T : PoolableObject
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        //var poolableObject = Resources.Load<T>(path);
        Debug.Log(path);
        var poolableObject = assetBundleManager.AssetBundleInfo.prefab.LoadAsset<GameObject>(path).GetComponent<T>();

        if (poolableObject == null)
            Debug.LogError($"{path}가 잘못됨.");

        var instance = UnityEngine.Object.Instantiate(poolableObject);
        instance.PoolObject = this;
        poolableObjects.Enqueue(instance);
    }
}
