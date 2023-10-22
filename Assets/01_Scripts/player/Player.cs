using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocity = 5, forceJump = 5, estabilidad = 100, estabilidadtotal = 100;
    public Image estabilidadUI;
    public bool canJump = true, cantdoo = false, cantShoot = true;
    public door doo;
    void Start()
    {
        if(General.door && !General.casa)
            transform.position = General.pos;
        rb = gameObject.GetComponent<Rigidbody2D>();
        estabilidad = General.estabilidadactual;
        estabilidadUI.fillAmount = estabilidad/ estabilidadtotal;
    }
    
    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        perdida();
        Active();
    }
    public void Active()
    {
        if (Input.GetKeyDown(KeyCode.E) && cantdoo)
        {
            General.estabilidadactual = estabilidad;
            General.actualMap = doo.gameObject.GetComponent<door>().ecenario;
            General.door = true;
            General.casa = !General.casa;
            if (General.casa)
                General.pos = doo.gameObject.transform.position;
            SceneManager.LoadScene(doo.gameObject.GetComponent<door>().ecenario);
        }
    }
    public void perdida()
    {
        estabilidad -= Time.deltaTime;
        estabilidadUI.fillAmount = estabilidad / estabilidadtotal;
        if (estabilidad<=0)
        {
            SceneManager.LoadScene(General.actualMap);
            General.estabilidadactual = 100;
        }
    }
    public void Movement()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * velocity, rb.velocity.y);
        if (rb.velocity.x < -0.1)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (rb.velocity.x > 0.1)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump) 
            rb.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
    }
    public void TakeEstavilidad(float est)
    {
        estabilidad += est;
        if (estabilidad > 100)
            estabilidad = 100;
        if (estabilidad < 0)
        {

            Debug.Log("muerto");
            estabilidad = 0;
            estabilidadUI.fillAmount = estabilidad / estabilidadtotal;
            General.estabilidadactual = 100;

            SceneManager.LoadScene(General.actualMap);
        }
        estabilidadUI.fillAmount = estabilidad / estabilidadtotal;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            canJump = true;
        }
        
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            canJump = false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<door>())
        {
            doo = collision.gameObject.GetComponent<door>();
            cantdoo = true;
        }
        if (collision.gameObject.CompareTag("Energy"))
        {
            TakeEstavilidad(5);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("key"))
        {
            General.bloqueado = false;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<door>())
        {
            doo = null;
            cantdoo = false;
        }
    }
}
