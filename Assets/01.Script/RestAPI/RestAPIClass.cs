using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RestAPIClass<T>
{
    public Action<T> OnComplete = null;

    public bool success;
    public string code;
    public string message;
    public string httpStatus;
    public T data;
}
