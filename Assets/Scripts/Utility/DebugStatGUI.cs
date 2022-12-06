using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DebugStatGUI : MonoBehaviour
{
    public DataController entityController;
    public Entity SelectedActor { get; set; }
    public void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction,out RaycastHit hit, 1000.0f))
            {
                Debug.Log(hit.transform.gameObject.name);

                var actorObject = hit.transform.gameObject.GetComponent<CharacterObject>();
                if (actorObject == null)
                    return;

              //  SelectedActor = entityController.GetEntity<Entity>(actorObject.ModelInstanceId);
            }
        }
    }
    public void OnDrawGizmos()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000.0f, Color.red);
    }
    public void OnGUI()
    {
        //if (SelectedActor == null)
        //    return;

        //var stats = GamePresetDataModel.Instance.ReturnPresetData<StatData>("Stat", SelectedActor.Id).Stats;
        //GUIStyle style=new GUIStyle();
        //style.normal.textColor = Color.black;
        //for (int i = 0; i < stats.Count; i++)
        //{
        //    GUI.Label(new Rect(0, i*20, 100, 20), $"{stats[i].statType}:{stats[i].Value}", style);

        //}
    }
}
