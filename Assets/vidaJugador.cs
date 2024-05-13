using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vidaJugador : MonoBehaviour
{
    public float cantidadDeVida;

    public void TomarDaño(float daño)
    {
        cantidadDeVida -= daño;

        if (cantidadDeVida <= 0)
        {
            StartCoroutine(ReiniciarDespuesDeTiempo(0f));
        }

    }

    private IEnumerator ReiniciarDespuesDeTiempo(float retraso)
    {
        // Esperar un tiempo antes de reiniciar la escena
        yield return new WaitForSeconds(retraso);

        // Obtener el índice de la escena actual
        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;

        // Reiniciar la escena cargando la misma escena por su índice
        SceneManager.LoadScene(indiceEscenaActual);

        // Destruir el jugador después de reiniciar la escena
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el jugador ha entrado en un trigger que indica el vacío
        if (other.CompareTag("Vacio"))
        {
            // Iniciar la corutina para reiniciar la escena
            StartCoroutine(ReiniciarDespuesDeTiempo(0f));
        }
    }
}
