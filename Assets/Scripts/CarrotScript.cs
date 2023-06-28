using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotScript : MonoBehaviour
{
    public float amplitude = 2;
    public float speed = 1.5f;
    private Animator anim;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    private PolygonCollider2D collider;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        collider = gameObject.GetComponent<PolygonCollider2D>();
    }

    void FixedUpdate()
    {
        Vector3 p = transform.position;
        p.y += amplitude * Mathf.Cos(Time.time * speed);
        transform.position = p;
    }

    public void PickUp()
    {
        collider.enabled = false;
        anim.Play("CarrotPickUp");
        anim.SetTrigger("PickedUp");
        Destroy(gameObject,0.7f);
    }
}
