using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

//�þ� Ŭ����
public interface ISight
{
    [JsonProperty]
    public float VisibleDistance { get; set; }
    [JsonProperty]
    public string[] VisibleLayerName { get; set; }

    //�þ߿� ���̴� ��� ������Ʈ ��ȯ
    public Collider[] ReturnVisibleObjects(Transform transform);
}
