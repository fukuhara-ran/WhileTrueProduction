using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotatable
{
    Quaternion Rotation { get; set; }
    void Rotate(Vector3 axis, float angle);
}
