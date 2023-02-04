using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CloneSettingDetailContents : MonoBehaviour
{
    [Header("�ؽ�Ʈ"), SerializeField] private TextMeshProUGUI cloneNameTMP;
    [SerializeField] private TextMeshProUGUI cloneSkillTitle;

    public void Init(Clon clone)
    {
        cloneNameTMP.text = clone.nameKr;
        cloneSkillTitle.text = "���� ��ų";
    }
}
