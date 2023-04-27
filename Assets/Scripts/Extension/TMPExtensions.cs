using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UniRx.Triggers;

public static class TMPExtensions
{

    /// <summary>
    /// 타이핑 효과 코루틴을 적용합니다.
    /// </summary>
    /// <param name="textTMP">적용할 TMP</param>
    /// <param name="str">타이핑할 문자열</param>
    /// <param name="typingSpeed">타이핑 속도</param>
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
    /// 알파 효과를 적용합니다.
    /// </summary>
    /// <param name="textTMP">적용할 TMP</param>
    /// <param name="alphaSpeed">투명값 속도 </param>
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
