using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cazadordepersonas : MonoBehaviour
{
    public GameObject jugador, bullet;
    public Transform fireShot;
    public bool detectado = false, atacar = true, pistolero = false;
    public float velocity = 5;
    public Rigidbody2D rb;
    public Animator anim;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!pistolero)
        {
            if (jugador.transform.position.x > transform.position.x)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);

            if (detectado && atacar)
            {
                anim.SetTrigger("atacar");
                rb.AddForce(Vector2.left * velocity, ForceMode2D.Impulse);
                atacar = false;
                Destroy(gameObject, 2);
            }
        }
        else
        {
            if (jugador.transform.position.x > transform.position.x)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);
            if (detectado && atacar)
            {
                anim.SetTrigger("atacar");
                rb.AddForce(Vector2.left * velocity, ForceMode2D.Impulse);
                atacar = false;
            }
        }
           
    }

    public void Shoot()
    {
        Instantiate(bullet, fireShot.position, fireShot.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            detectado = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            detectado = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().TakeEstavilidad(-10);
            Destroy(gameObject);
        }
    }
}
