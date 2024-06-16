using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{

    private float z;
    public float speedRotation = 100f;

    void FixedUpdate()
    {
        z += Time.deltaTime * speedRotation;
        if (z > 360f) z %= 360 ;
        transform.rotation = Quaternion.Euler(0,0,z);
    }
}
