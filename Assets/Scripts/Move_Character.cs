using System.Collections;
using System.Collections.Generic;
using DoorScript;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;
public class Move_Character : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;
    private bool rightmove = true;
    public float maxJumpHeight;
    public float jumpForce;
    public Animator anim;
    public GameObject WinPanel;

    private GameObject gate;

    public Transform player;
    public GameObject door2;
    public GameObject door3;
    public GameObject healthBar;
    private int maxhealth = 100;
    private bool istouching = false;
    public GameObject FinishPanel;
    public GameObject[] enemys;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isidle", true);
        gate = GameObject.Find("Gate");

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rightmove = false;
            movego();
            anim.SetBool("isrun", true);
            anim.SetBool("isidle", false);
            anim.SetBool("isattack", false);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            rightmove = true;
            movego();
            anim.SetBool("isrun", true);
            anim.SetBool("isidle", false);
            anim.SetBool("isattack", false);
        }

        if (Input.GetMouseButtonDown(0))
        {

            anim.SetBool("isattack", true);
            anim.SetBool("isidle", false);

        }



        if (Input.GetKeyDown(KeyCode.W) && rb.velocity.y == 0) // Yalnızca yerdeyken zıpla
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("isjump", true);
            anim.SetBool("isrun", false);
            anim.SetBool("isidle", false);
            anim.SetBool("isattack", false);
        }

        // Karakter yere temas ediyorsa zıplama animasyonunu kapat
        if (rb.velocity.y == 0)
        {
            anim.SetBool("isjump", false);

        }

        // Eğer hareket yoksa idle animasyonuna geç
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && rb.velocity.y == 0)
        {
            anim.SetBool("isidle", true);
            anim.SetBool("isrun", false);
            anim.SetBool("isjump", false);


        }





    }

    void movego()
    {
        Vector2 move = new Vector2((rightmove ? 1 : -1) * speed, 0f);
        transform.Translate(move * Time.deltaTime);

        // Karakterin yönünü ayarla (flip x)
        Vector3 scale = transform.localScale;
        scale.x = rightmove ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }




    void OnCollisionEnter2D(Collision2D other)
    {

        foreach (GameObject enemy in enemys)
        {
            if (other.gameObject == enemy)
            {
                maxhealth -= 10;

                float healthScale = Mathf.Clamp01((float)maxhealth / 100f);
                healthBar.transform.localScale = new Vector3(healthScale, healthBar.transform.localScale.y, 1f);

                Debug.Log("Düşmana çarptın! Yeni Can: " + maxhealth);


                if (maxhealth <= 0)
                {
                    FinishPanel.SetActive(true);
                }                                      //BEN HİLALE AŞIĞIIIIM KALP KALP KALP❤❤❤❤❤❤❤❤❤❤❤❤❤❤❤❤❤❤
            }

        }

        if (other.gameObject.CompareTag("Mermi"))
        {
            maxhealth -= 10;

            float healthScale = Mathf.Clamp01((float)maxhealth / 100f);
            healthBar.transform.localScale = new Vector3(healthScale, healthBar.transform.localScale.y, 1f);

            Debug.Log("Mermi isabet etti! Yeni Can: " + maxhealth);

            if (maxhealth <= 0)
            {
                
                FinishPanel.SetActive(true);
            }

            Destroy(other.gameObject);
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door1"))
        {
            maxhealth = 100;

            transform.position = door2.transform.position;

        }

        if (other.gameObject.CompareTag("Door2"))
        {
            maxhealth = 100;

            transform.position = door3.transform.position;

        }

        if (other.gameObject.CompareTag("heart"))
        {
            maxhealth = 100;


            healthBar.transform.localScale = new Vector3(1.28f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);


            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("TouchDoor"))
        {
            gate.transform.position = new Vector3(gate.transform.position.x, 46f, gate.transform.position.z);
        }

        if (other.gameObject.CompareTag("Door3"))
        {
            WinPanel.SetActive(true);

            Debug.Log("Temas Gerçekleşti");
            Destroy(other.gameObject);
            //wintextanim.Play("WinText");

            SceneManager.LoadScene("MENU");

        }
    }


   
}
