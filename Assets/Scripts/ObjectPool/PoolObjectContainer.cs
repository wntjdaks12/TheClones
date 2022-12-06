using System.Collections.Generic;

public class PoolObjectContainer
{
    private static PoolObjectContainer instance;
    public static PoolObjectContainer Instance { get => instance==null?instance=new PoolObjectContainer():instance; }

    Dictionary<string, PoolObject> poolObjects = new Dictionary<string, PoolObject>();

    public static T CreatePoolableObject<T>(string path) where T:PoolableObject
    {
        if (!Instance.poolObjects.ContainsKey(path))
            Instance.poolObjects.Add(path, new PoolObject());

        Instance.poolObjects.TryGetValue(path, out PoolObject poolObject);


        return poolObject.GetPoolableObject<T>(path);
    }
}
