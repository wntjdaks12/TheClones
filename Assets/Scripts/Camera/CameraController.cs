using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CameraController : MonoBehaviour
{
    [SerializeField] private HuntingPointSystem huntingPointSystem;

    [SerializeField] private new Camera camera;

    // �� �Ÿ� ��
    private float distance;

    private void Start()
    {
        Zoom();

        Drag();
    }

    // ī�޶� �� �� �ƿ��� �����մϴ�. 
    private void Zoom()
    {
        // �Ÿ� ���� üũ�մϴ�.
        this.UpdateAsObservable()
            .Subscribe(pos => distance = Input.GetAxis("Mouse ScrollWheel") * -1 * 10);

        // �Ÿ� ���� ���� ��� �� �� �ƿ��� �մϴ�.
        this.ObserveEveryValueChanged(_ => distance)
            .Subscribe(_ => Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView + distance, 20, 80));
    }

    /*
    // ī�޶� ��� ����Ʈ�� �̵���ŵ�ϴ�.
    private void MoveHuntingPoint()
    {
        // ��� ����Ʈ �ý����� �ʱ�ȭ�մϴ�.
        huntingPointSystem.Init();

        // ��� ����Ʈ �ý��� �̵� ���θ� �����մϴ�.
        this.UpdateAsObservable()
            .SkipWhile(_ => huntingPointSystem.IsStopped)
            .TakeUntil(this.UpdateAsObservable().Where(_ => huntingPointSystem.IsStopped))
            .RepeatUntilDestroy(this)
            .Subscribe(_ => huntingPointSystem.Move());
    }
    */

    // ī�޶� �巡���մϴ�.
    private void Drag()
    {
        var downStream = camera.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));
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
