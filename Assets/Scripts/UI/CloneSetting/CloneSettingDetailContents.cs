using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CloneSettingDetailContents : MonoBehaviour
{
    [Header("텍스트"), SerializeField] private TextMeshProUGUI cloneNameTMP;
    [SerializeField] private TextMeshProUGUI cloneSkillTitle;

    public void Init(Clon clone)
    {
        cloneNameTMP.text = clone.nameKr;
        cloneSkillTitle.text = "고유 스킬";
    }
}
