using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CameraController : MonoBehaviour
{
    [SerializeField] private HuntingPointSystem huntingPointSystem;

    // 줌 거리 값
    private float distance;

    private void Start()
    {
     //   Drag();

        Zoom();

        MoveHuntingPoint();
    }

   /* // 카메라를 드래그 합니다.
    private void Drag()
    {
        // 카메라를 해당 위치로 드래그하여 이동합니다.
        this.UpdateAsObservable()
            .SkipUntil(this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0)))
            .Select(_ => new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y) - new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")))
            .TakeUntil(this.UpdateAsObservable().Where(_ => Input.GetMouseButtonUp(0)))
            .RepeatUntilDestroy(this)
            .Subscribe(pos => {
                Camera.main.transform.position = new Vector3(pos.x, pos.y, Camera.main.transform.position.z);
                huntingPointSystem.StopMove();
            });
    }*/

    // 카메라 줌 인 아웃을 결정합니다. 
    private void Zoom()
    {
        // 거리 값을 체크합니다.
        this.UpdateAsObservable()
            .Subscribe(pos => distance = Input.GetAxis("Mouse ScrollWheel") * -1 * 10);

        // 거리 값이 변할 경우 줌 인 아웃을 합니다.
        this.ObserveEveryValueChanged(_ => distance)
            .Subscribe(_ => Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + distance, 7, 14));
    }

    // 카메라를 사냥 포인트로 이동시킵니다.
    private void MoveHuntingPoint()
    {
        // 사냥 포인트 시스템을 초기화합니다.
        huntingPointSystem.Init();

        // 사냥 포인트 시스템 이동 여부를 결정합니다.
        this.UpdateAsObservable()
            .SkipWhile(_ => huntingPointSystem.IsStopped)
            .TakeUntil(this.UpdateAsObservable().Where(_ => huntingPointSystem.IsStopped))
            .RepeatUntilDestroy(this)
            .Subscribe(_ => huntingPointSystem.Move());
    }
}
