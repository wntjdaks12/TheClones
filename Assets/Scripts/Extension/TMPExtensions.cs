using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UniRx.Triggers;

public static class TMPExtensions
{

    /// <summary>
    /// Ÿ���� ȿ�� �ڷ�ƾ�� �����մϴ�.
    /// </summary>
    /// <param name="textTMP">������ TMP</param>
    /// <param name="str">Ÿ������ ���ڿ�</param>
    /// <param name="typingSpeed">Ÿ���� �ӵ�</param>
    /// <returns></returns>
    public static IEnumerator TypingAsync(this TextMeshProUGUI textTMP, string str, float typingSpeed)
    {
        var col = textTMP.color;

        for (int i = 0; i < str.Length; i++)
        {
            textTMP.text = str.Substring(0, i + 1);

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    /// <summary>
    /// ���� ȿ���� �����մϴ�.
    /// </summary>
    /// <param name="textTMP">������ TMP</param>
    /// <param name="alphaSpeed">���� �ӵ� </param>
    /// <returns></returns>
    public static IEnumerator AlphaAsync(this TextMeshProUGUI textTMP, float alphaSpeed)
    {
        var col = textTMP.color; col.a = 0;

        while (col.a < 1)
        {
            col.a += alphaSpeed;

            textTMP.color = col;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
