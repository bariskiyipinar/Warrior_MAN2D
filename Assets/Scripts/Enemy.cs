using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform character; // Karakterin transformu
    public GameObject mermiPrefab; // Mermi objesi (Prefab olmalý)
    private float health = 3;
    private float speed = 5f; // Mermi hýzý
    private float fireRate = 2f; // Ateþ etme süresi
    private float nextFireTime = 0f; // Sonraki ateþ zamaný
    public Transform MermiOffset;

    private void Update()
    {
        float distance = Vector2.Distance(character.position, transform.position);

        if (distance <= 4 && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; 
            FireBullet(); 
        }
    }

    private void FireBullet()
    {
        GameObject mermi = Instantiate(mermiPrefab, MermiOffset.transform.position, Quaternion.identity);
        Rigidbody2D mermiRb = mermi.GetComponent<Rigidbody2D>();

        if (mermiRb != null)
        {
            Vector2 direction = (character.position - transform.position).normalized; //Karaktere doðru mermi atmasý düþmanýn
            mermiRb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
        }

        Destroy(mermi, 2f); 
    }


    private void OnCollisionEnter2D(Collision2D temas)
    {
        if (temas.gameObject.CompareTag("Character"))
        {
            health--;

            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
