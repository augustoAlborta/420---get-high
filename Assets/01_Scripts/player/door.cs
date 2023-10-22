using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{
    public string ecenario;
    public bool salida;
    private void Update()
    {
        if (salida)
        {
            if (!General.bloqueado)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !General.bloqueado && salida)
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
