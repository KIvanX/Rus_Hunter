using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Config")]
public class CameraConfig : ScriptableObject
{
    public float turnSmooth;
    public float pivotSpeed;
    public float xRotateSpeed;
    public float yRotateSpeed;
    public float minAngle;
    public float maxAngle;
    public float normalX;
    public float normalY;
    public float normalZ;
    public float aimX;
    public float aimZ;
}
