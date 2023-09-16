using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Triggers;
using UniRx;
using System.Linq;
public class BossMessageContents : GameView
{
    [SerializeField] private Button MessageBtn;
    [SerializeField] private Image ArrowImg;

    public void Init(uint bossMonsterId)
    {
        gameObject.SetActive(true);

        MessageBtn.onClick.RemoveAllListeners();
        MessageBtn.onClick.AddListener(() => gameObject.SetActive(false));

        this.UpdateAsObservable()
            .Subscribe(x => 
            {
                var monster = App.GameModel.RuntimeDataModel.ReturnDatas<Monster>(nameof(Monster)).Where(x => x.Id == bossMonsterId).FirstOrDefault();

                var bossSrcPos = Camera.main.WorldToScreenPoint(monster.Transform.position);

                Vector2 v2 = new Vector2(bossSrcPos.x, bossSrcPos.y) - new Vector2(ArrowImg.transform.position.x, ArrowImg.transform.position.y);

                ArrowImg.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg - 90);
            });
    }
}
