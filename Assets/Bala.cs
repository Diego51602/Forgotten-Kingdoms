using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisión detectada con: " + other.tag);

        if (other.CompareTag("JefeFinal"))
        {
            // Intenta imprimir el GameObject detectado para verificar si es el Jefe Final
            Debug.Log("Colisión con JefeFinal: " + other.gameObject.name);

            JefeFinal jefeFinal = other.GetComponent<JefeFinal>();
            if (jefeFinal != null)
            {
                jefeFinal.TomarDaño(1);
                Debug.Log("El JefeFinal ha recibido daño.");
            }

            Destroy(gameObject);
            Debug.Log("La bala se ha destruido.");
        }
        else if (other.CompareTag("Enemigo"))
        {
            enemigoDaño enemigoDamage = other.GetComponent<enemigoDaño>();

            if (enemigoDamage != null)
            {
                enemigoDamage.Golpe();
                Debug.Log("Otro tipo de enemigo ha recibido un golpe.");
            }

            Destroy(gameObject);
            Debug.Log("La bala se ha destruido.");
        }
    }


}
