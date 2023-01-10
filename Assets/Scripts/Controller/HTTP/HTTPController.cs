using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTTPController : MonoBehaviour
{
    public T GetController<T>()
    {
        return gameObject.GetComponent<T>();
    }
}
