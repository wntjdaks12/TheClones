using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CameraController : MonoBehaviour
{
    [SerializeField] private HuntingPointSystem huntingPointSystem;

    // �� �Ÿ� ��
    private float distance;

    private void Start()
    {
     //   Drag();

      //  Zoom();

        //MoveHuntingPoint();
    }

   /* // ī�޶� �巡�� �մϴ�.
    private void Drag()
    {
        // ī�޶� �ش� ��ġ�� �巡���Ͽ� �̵��մϴ�.
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

    // ī�޶� �� �� �ƿ��� �����մϴ�. 
    private void Zoom()
    {
        // �Ÿ� ���� üũ�մϴ�.
        this.UpdateAsObservable()
            .Subscribe(pos => distance = Input.GetAxis("Mouse ScrollWheel") * -1 * 10);

        // �Ÿ� ���� ���� ��� �� �� �ƿ��� �մϴ�.
        this.ObserveEveryValueChanged(_ => distance)
            .Subscribe(_ => Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + distance, 7, 14));
    }

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
}
