using System;
using System.Collections.Generic;
using UniRx;

[Serializable]
public class PlayerInfo
{
    public string playerId;

    //public List<ClonInfo> cloneInfos;

    private ReactiveProperty<List<ClonInfo>> cloneInfosRP;
    public ReactiveProperty<List<ClonInfo>> CloneInfosRP {
        get
        {
            cloneInfosRP ??= new ReactiveProperty<List<ClonInfo>>();
            cloneInfosRP.Value ??= new List<ClonInfo>();

            return cloneInfosRP;
        }
    }
}

[Serializable]
public class ClonInfo
{
    public uint clonId;
    public uint skillId;
}
