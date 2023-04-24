using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private HuntingPointSystem huntingPointSystem;

    [SerializeField] private new Camera camera;

    // 줌 거리 값
    private float distance;

    private void Start()
    {
        Zoom();

        Drag();
    }

    // 카메라 줌 인 아웃을 결정합니다. 
    private void Zoom()
    {
        // 거리 값을 체크합니다.
        this.UpdateAsObservable()
            .Where(_ => !EventSystem.current.IsPointerOverGameObject())
            .Subscribe(pos => distance = Input.GetAxis("Mouse ScrollWheel") * -1 * 10);

        // 거리 값이 변할 경우 줌 인 아웃을 합니다.
        this.ObserveEveryValueChanged(_ => distance)
            .Subscribe(_ => Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView + distance, 20, 80));
    }

    /*
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
    */

    // 카메라를 드래그합니다.
    private void Drag()
    {
        var downStream = camera.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject());
        var upStream = camera.UpdateAsObservable().Where(_ => Input.GetMouseButtonUp(0));
        var dragStream = camera.UpdateAsObservable()
            .SkipUntil(downStream)
            .TakeUntil(upStream)
            .Select(_ => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")))
            .RepeatUntilDestroy(camera);

        dragStream.Subscribe(hitInfo =>
        {
            camera.transform.position -= new Vector3(hitInfo.x, 0, hitInfo.y);
        });
    }
}
