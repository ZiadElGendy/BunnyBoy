using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour   
{
    public float amplitude = 2;
    public float speed = 1.5f;

    void FixedUpdate()
    {
        Vector3 p = transform.position;
        p.y += amplitude * Mathf.Cos(Time.time * speed);
        transform.position = p;
    }
}
