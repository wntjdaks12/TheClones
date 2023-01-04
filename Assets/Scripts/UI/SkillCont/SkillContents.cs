using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SkillContents : MonoBehaviour
{
    [Header("TOGGLE BUTTON")]
    [SerializeField] private Button button;

    [Header("CONTENTS")]
    [SerializeField] private PoolingScrollview poolingScrollview;

    private SkillContentsState state;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SkillContentsState"))
        {
            setState(SkillContentsShowState.getInstance());

            state.OnShow(this);
        }
        else
        {
            if (PlayerPrefs.GetInt("SkillContentsState") == 0)
            {
                setState(SkillContentsHideState.getInstance());

                state.OnHide(this);
            }
            else
            {
                setState(SkillContentsShowState.getInstance());

                state.OnShow(this);
            }
        }
    }

    public void setState(SkillContentsState state)
    {
        this.state = state;
    }

    private void Start()
    {
        var playerInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo;
        playerInfo.cloneInfosRP.ObserveAdd().Subscribe(_ => Init());

        Init();
    }

    public void Init()
    {
        var playerInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo;

        var cloneInfos = playerInfo.cloneInofs;

        if (cloneInfos != null)
        {
            poolingScrollview.Init(cloneInfos.Count);
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnPressedButton);
    }

    private void OnPressedButton()
    {
        if ((PlayerPrefs.GetInt("SkillContentsState") == 0))
        {
            state.OnShow(this);
        }
        else
        {
            state.OnHide(this);
        }
    }
}
