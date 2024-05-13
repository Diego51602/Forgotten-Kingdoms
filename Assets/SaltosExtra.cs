using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltosExtra : MonoBehaviour
{
    private bool canBeCollected = false;

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
            // Obtener el componente de SaltoDoble del jugador
            SaltoDoble saltoDoble = GameObject.FindGameObjectWithTag("Player").GetComponent<SaltoDoble>();

            if (saltoDoble != null)
            {
                // Incrementar el número de saltos extra del jugador
                saltoDoble.AgregarSaltosExtra(2);

                // Destruir este objeto recolectable
                Destroy(gameObject);
            }
        }
    }


}
