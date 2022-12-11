using System;
using System.Collections.Generic;

[Serializable]
public class PlayerInfo
{
    public string playerId;

    public List<ClonInfo> clonInfos = new List<ClonInfo>();
}

[Serializable]
public class ClonInfo
{
    public uint clonId;
    public uint skillId;
}
