using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    public Transform controladorDisparo;
    public float distanciaLinea;
    public LayerMask capaJugador;
    public bool jugadorEnRango;
    public float tiempoEntreDisparo;
    public float tiempoUltimoDisparo;
    public GameObject balaEnemigo;
    public float tiempoEsperaDisparo;

    private void Update()
    {
        // Raycast hacia la derecha
        RaycastHit2D jugadorEnRangoDerecha = Physics2D.Raycast(controladorDisparo.position, transform.right, distanciaLinea, capaJugador);

        // Raycast hacia la izquierda
        RaycastHit2D jugadorEnRangoIzquierda = Physics2D.Raycast(controladorDisparo.position, -transform.right, distanciaLinea, capaJugador);

        // Verificar si el jugador está en rango en alguna dirección y si ha pasado suficiente tiempo desde el último disparo
        if ((jugadorEnRangoDerecha || jugadorEnRangoIzquierda) && Time.time > tiempoUltimoDisparo + tiempoEsperaDisparo)
        {
            // Determinar dirección de disparo según la posición del jugador
            Vector2 direccionDisparo = (jugadorEnRangoDerecha ? transform.right : -transform.right);

            // Disparar en la dirección calculada
            DispararEnDireccion(direccionDisparo);

            // Actualizar el tiempo del último disparo al momento actual
            tiempoUltimoDisparo = Time.time;
        }
    }

    private void DispararEnDireccion(Vector2 direccionDisparo)
    {
        // Calcular la rotación para apuntar hacia la dirección del disparo
        float angle = Mathf.Atan2(direccionDisparo.y, direccionDisparo.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Instanciar la bala con la rotación correcta
        Instantiate(balaEnemigo, controladorDisparo.position, rotation);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * distanciaLinea);
    }
}
