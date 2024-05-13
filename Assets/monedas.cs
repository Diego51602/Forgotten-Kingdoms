using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monedas : MonoBehaviour
{
    public int valor = 1;
    public GameManager gameManager;

    [SerializeField] private AudioClip audio1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameManager.SumarPuntos(valor);
            ControladorSonidos.Instance.EjecutarSonido(audio1);
            Destroy(this.gameObject);
        }
    }
}
