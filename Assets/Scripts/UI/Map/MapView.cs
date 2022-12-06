using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : GameView
{
    private MapModel mapModel;

    public MapModel MapModel 
    {
        get => mapModel ??= new MapModel();
    }
}
