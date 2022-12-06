using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

//시야 클래스
public interface ISight
{
    [JsonProperty]
    public float VisibleDistance { get; set; }
    [JsonProperty]
    public string[] VisibleLayerName { get; set; }

    //시야에 보이는 모든 오브젝트 반환
    public Collider[] ReturnVisibleObjects(Transform transform);
}
