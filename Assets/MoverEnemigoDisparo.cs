using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEnemigoDisparo : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float velocidadDeMovimiento;
    public LayerMask capaAbajo;
    public LayerMask capaEnfrente;
    public LayerMask capaEnfrenteJugador;
    public float distanciaAbajo;
    public float distanciaEnfrente;
    public float distanciaEnfrenteJugador;
    public Transform controladorAbajo;
    public Transform controladorJugador;
    public Transform controladorEnfrente;
    public bool informacionAbajo;
    public bool informacionJugador;
    public bool informacionEnfrente;
    public bool mirandoALaDerecha = true;

    private void Update()
    {
        // Establecer la velocidad de movimiento basada en la dirección de mirada
        rb2D.velocity = new Vector2(velocidadDeMovimiento, rb2D.velocity.y);

        // Determinar la dirección del raycast basada en la dirección de mirada del objeto
        Vector2 direccionRaycast = mirandoALaDerecha ? Vector2.right : Vector2.left;

        // Realizar el raycast hacia el controlador de enfrente
        informacionJugador = Physics2D.Raycast(controladorJugador.position, direccionRaycast, distanciaEnfrenteJugador, capaEnfrenteJugador);
        informacionEnfrente = Physics2D.Raycast(controladorEnfrente.position, direccionRaycast, distanciaEnfrente, capaEnfrente);

        // Realizar el raycast hacia el controlador de abajo
        informacionAbajo = Physics2D.Raycast(controladorAbajo.position, Vector2.down, distanciaAbajo, capaAbajo);

        // Si detecta algo enfrente, detener el movimiento
        if (informacionJugador)
        {
            DetenerMovimiento();
        }
        else if (!informacionAbajo || informacionEnfrente)
        {
            Girar();
        }
        else
        {
            // Si no detecta nada enfrente y hay suelo abajo, continuar moviéndose
            ReanudarMovimiento();
        }
    }

    private void DetenerMovimiento()
    {
        // Detener el movimiento estableciendo la velocidad a cero
        rb2D.velocity = Vector2.zero;
    }

    private void ReanudarMovimiento()
    {
        // Reanudar el movimiento con la dirección adecuada basada en la dirección de mirada
        rb2D.velocity = new Vector2(velocidadDeMovimiento, rb2D.velocity.y);
    }

    private void Girar()
    {
        // Invertir la dirección de mirada
        mirandoALaDerecha = !mirandoALaDerecha;

        // Invertir la escala en el eje X para reflejar el objeto horizontalmente
        Vector3 nuevaEscala = transform.localScale;
        nuevaEscala.x *= -1;
        transform.localScale = nuevaEscala;

        // Invertir la dirección de movimiento
        velocidadDeMovimiento *= -1;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorAbajo.transform.position, controladorAbajo.transform.position + transform.up * -1 * distanciaAbajo);
        Gizmos.DrawLine(controladorJugador.transform.position, controladorJugador.transform.position + transform.right * distanciaEnfrenteJugador);
        Gizmos.DrawLine(controladorEnfrente.transform.position, controladorEnfrente.transform.position + transform.right * distanciaEnfrente);
    }
}
