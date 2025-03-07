using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door2;
    public GameObject player;
    



    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)    
        {
            if (collision.gameObject)
            {

            }
        }
    }
}
