using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEnemigo : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float velocidadDeMovimiento;
    public LayerMask capaAbajo;
    public LayerMask capaEnfrente;
    public float distanciaAbajo;
    public float distanciaEnfrente;
    public Transform controladorAbajo;
    public Transform controladorEnfrente;
    public bool informacionAbajo;
    public bool informacionEnfrente;
    public bool mirandoALaDerecha = true;

    private void Update()
    {
        // Establecer la velocidad de movimiento basada en la dirección de mirada
        rb2D.velocity = new Vector2(velocidadDeMovimiento, rb2D.velocity.y);

        // Determinar la dirección del raycast basada en la dirección de mirada del objeto
        Vector2 direccionRaycast = mirandoALaDerecha ? Vector2.right : Vector2.left;

        // Realizar el raycast hacia el controlador de enfrente
        informacionEnfrente = Physics2D.Raycast(controladorEnfrente.position, direccionRaycast, distanciaEnfrente, capaEnfrente);

        // Realizar el raycast hacia el controlador de abajo
        informacionAbajo = Physics2D.Raycast(controladorAbajo.position, Vector2.down, distanciaAbajo, capaAbajo);


        if (!informacionAbajo || informacionEnfrente)
        {
            Girar();
        }
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
        Gizmos.DrawLine(controladorEnfrente.transform.position, controladorEnfrente.transform.position + transform.right * distanciaEnfrente);
    }
}
