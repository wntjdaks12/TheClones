using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillContents : MonoBehaviour
{
    [SerializeField] private Button button;

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
