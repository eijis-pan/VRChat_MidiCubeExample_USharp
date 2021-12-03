
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FloatUSharp : UdonSharpBehaviour
{
    public Vector3 amplitude;
    public Vector3 speed;

    public void Update()
    {
        var sinTime = Mathf.Sin(Time.time);
        var x = sinTime * speed.x * amplitude.x;
        var y = sinTime * speed.y * amplitude.y;
        var z = sinTime * speed.z * amplitude.z;
        transform.localPosition = new Vector3(x, y, z);
    }
}
