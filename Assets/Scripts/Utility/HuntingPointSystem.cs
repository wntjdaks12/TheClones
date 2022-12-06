using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[System.Serializable]
public class HuntingPointSystem
{
    [SerializeField] private List<Transform> huntingPoints;

    public MapModel mapModel = new MapModel();

    [SerializeField] private int speed;

    public bool IsStopped { get; private set; }

    public void Init()
    {
        mapModel.ObserveEveryValueChanged(x => x.CurrentIndex)
            .Subscribe(_ => StartMove());
    }

    public void Move()
    {
        var cameraTs = Camera.main.transform;

        if (mapModel.CurrentIndex == 0)
        {
            cameraTs.position = Vector3.Lerp(cameraTs.position, huntingPoints[0].position, Time.deltaTime * speed);
            cameraTs.rotation = Quaternion.Lerp(cameraTs.rotation, huntingPoints[0].rotation, Time.deltaTime * speed);
        }
        else if (mapModel.CurrentIndex == 1)
        {
            cameraTs.position = Vector3.Lerp(cameraTs.position, huntingPoints[1].position, Time.deltaTime * speed);
            cameraTs.rotation = Quaternion.Lerp(cameraTs.rotation, huntingPoints[1].rotation, Time.deltaTime * speed);
        }
        else if (mapModel.CurrentIndex == 2)
        {
            cameraTs.position = Vector3.Lerp(cameraTs.position, huntingPoints[2].position, Time.deltaTime * speed);
            cameraTs.rotation = Quaternion.Lerp(cameraTs.rotation, huntingPoints[2].rotation, Time.deltaTime * speed);
        }
    }

    public void StartMove()
    {
        IsStopped = false;
    }

    public void StopMove()
    {
        IsStopped = true;
    }
}
