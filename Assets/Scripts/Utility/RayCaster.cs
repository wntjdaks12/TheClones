using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class Raycaster
{
    public List<RaycastResult> ReturnUIRaycastResultList(GraphicRaycaster graphicRaycaster)
    {
        var ped = new PointerEventData(null);
        ped.position = Input.mousePosition;
        var results = new List<RaycastResult>();

        graphicRaycaster.Raycast(ped, results);

        return results;
    }
    public T[] ReturnUIRaycastResultList<T>(GraphicRaycaster graphicRaycaster)
    {
        var resultRaycastList = ReturnUIRaycastResultList(graphicRaycaster);

        var array = resultRaycastList.Select(x => x.gameObject.GetComponent<T>()).Where(x => x != null).ToArray();

        return array;
    }

    public T[] ReturnRaycastResultList<T>()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        var array = hits.Select(x => x.collider.GetComponentInChildren<T>()).Where(x => x != null).ToArray();
        
        if (array.Length == 0) return null;

        return array;
    }

    public RaycastHit[] ReturnRaycastResultList<T>(Vector3 _startPosition, Vector3 _direction, float _distance, Color _color, bool _isDrawing)
    {
        if (_isDrawing) Debug.DrawRay(_startPosition, _direction * _distance, _color);

        RaycastHit[] hitInfos = Physics.RaycastAll(_startPosition, _direction, _distance);

        return hitInfos.Where(x => x.collider.GetComponent<T>() != null).ToArray();
    }

    public RaycastHit[] ReturnScreenToWorldHits<T>()
    {
        var mouseScrPos = Input.mousePosition; mouseScrPos.z = -Camera.main.transform.position.z;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScrPos);

        var cameraSrcPos = Camera.main.transform.position;

        Vector3 vec1 = (mouseWorldPos - cameraSrcPos).normalized;
        Vector3 vec2 = (cameraSrcPos - mouseWorldPos).normalized;

        float dist = Vector3.Distance(cameraSrcPos, mouseWorldPos);

        return ReturnRaycastResultList<T>(mouseWorldPos + vec2 * dist, vec1, 100, new Color(1, 1, 1), true);
    }

    public RaycastHit GetRaycastHit()
    {
        RaycastHit[] hitInfos = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward * 1000);

        return hitInfos[0];
    }
}