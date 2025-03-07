using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
   
    private int rotatespeed=50;
    public  Text keyText;
    public  GameObject Key›mage;
    public GameObject door1;
   
    public GameObject door3;


    private void Start()
    {
        door1.SetActive(false);
       
    }

    void Update()
    {
        rotate();
        Key›mage.SetActive(false);
        keyText.gameObject.SetActive(false);
      
    }



    void rotate()
    {
        transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Character"))
        {
            Destroy(this.gameObject);
            keyText.text += "1";
            keyText.gameObject.SetActive(true);
            Key›mage.SetActive(true);
            door1.SetActive(true);
        }

        

    }

   


}
