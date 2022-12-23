using System;
using System.Collections.Generic;
using UniRx;

[Serializable]
public class PlayerInfo
{
    public string playerId;

    private ReactiveProperty<List<ClonInfo>> cloneInfos;
    public ReactiveProperty<List<ClonInfo>> CloneInfos {
        get
        {
            cloneInfos ??= new ReactiveProperty<List<ClonInfo>>();
            cloneInfos.Value ??= new List<ClonInfo>();

            return cloneInfos;
        }
    }
}

[Serializable]
public class ClonInfo
{
    public uint clonId;
    public uint skillId;
}
