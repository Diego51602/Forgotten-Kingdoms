using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEnemigo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private LayerMask capaSuelo; // Capa para el suelo
    [SerializeField] private float rangoDeVision; // Rango de visión del enemigo
    [SerializeField] private Transform jugador; // Referencia al transform del jugador
    private Rigidbody2D rb;
    private bool moviendoDerecha = true; // Indica la dirección de movimiento actual

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Verifica si hay suelo debajo del controlador de suelo
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia, capaSuelo);

        // Si no hay suelo, cambia de dirección
        if (!informacionSuelo)
        {
            CambiarDireccion();
        }

        // Mueve al enemigo en la dirección actual
        if (moviendoDerecha)
        {
            rb.velocity = new Vector2(velocidad, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);
        }

        // Verifica si el jugador está dentro del rango de visión del enemigo
        if (Vector2.Distance(transform.position, jugador.position) < rangoDeVision)
        {
            // Detiene al enemigo si ve al jugador
            Detener();
        }
    }

    void CambiarDireccion()
    {
        // Cambia la dirección de movimiento y la orientación del sprite
        moviendoDerecha = !moviendoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void Detener()
    {
        // Detiene al enemigo
        rb.velocity = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
        Gizmos.DrawWireSphere(transform.position, rangoDeVision);
    }
}




