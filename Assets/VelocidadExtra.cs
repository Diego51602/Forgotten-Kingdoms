using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocidadExtra : MonoBehaviour
{
private bool canBeCollected = false;

    [SerializeField] private float aumentoDeVelocidad; // Ajusta el aumento de velocidad deseado

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el jugador ha colisionado con el objeto
        if (other.CompareTag("Player"))
        {
            canBeCollected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el jugador ha salido de la zona de interacción con el objeto
        if (other.CompareTag("Player"))
        {
            canBeCollected = false;
        }
    }

    void Update()
    {
        // Verificar si se puede recoger el objeto y si se presiona la tecla 'E'
        if (canBeCollected && Input.GetKeyDown(KeyCode.E))
        {
            // Obtener el componente de MovimientoJugador del jugador
            MovimientoJugador movimientoJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoJugador>();

            if (movimientoJugador != null)
            {
                // Aumentar la velocidad del jugador
                movimientoJugador.AumentarVelocidad(aumentoDeVelocidad);

                // Destruir este objeto recolectable
                Destroy(gameObject);
            }
        }
    }
}
