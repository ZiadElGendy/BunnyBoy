using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action OnCollected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player")))
        {
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
