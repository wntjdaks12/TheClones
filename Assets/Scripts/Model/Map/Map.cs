using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : Data
{
    public string Name { get; set; }

    public uint SceneId { get; set; }

    public List<Stage> Stage { get; set; }
}

public class Stage 
{
    public string Name { get; set; }
}
