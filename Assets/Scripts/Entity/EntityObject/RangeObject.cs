using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeObject : EntityObject
{
    public void Init(Range range)
    {
        base.Init(range);

        transform.localScale = Vector3.zero;

        StartCoroutine(TestAsync());
    }

    private void Update()
    {
        var range = Entity as Range;

        var tempScale = Vector3.one * range.Radius * 2; tempScale.y = 0.9403151f;

        transform.localScale = Vector3.Lerp(transform.localScale, tempScale, Time.deltaTime * 15f);

        transform.position = range.Caster.Transform.position;
    }

    private IEnumerator TestAsync()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            OnRemoveEntity();
        }
    }
}
