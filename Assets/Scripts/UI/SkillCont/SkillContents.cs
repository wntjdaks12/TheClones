using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillContents : MonoBehaviour
{
    [Header("TOGGLE BUTTON")]
    [SerializeField] private Button button;

    [Header("CLON SLOT")]
    [SerializeField] private Transform parent;
    [SerializeField] private ClonSlot clonSlot;

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
        Init();
    }

    public void Init()
    {
        var player = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo;

        var count = player.clonInfos.Count;

        for (int i = 0; i < count; i++)
        {
            var clonInfo = player.clonInfos[i];
            Instantiate(clonSlot, parent).Init(clonInfo.clonId, clonInfo.skillId);
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
