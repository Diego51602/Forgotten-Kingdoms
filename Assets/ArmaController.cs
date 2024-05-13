using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaController : MonoBehaviour
{
    public GameObject energiaPrefab; // Prefab del proyectil de energía
    public GameObject jugador;
    public Transform puntoLanzamiento; // Punto de origen del lanzamiento de energía
    public float fuerzaLanzamiento = 10f; // Fuerza de lanzamiento del proyectil de energía

    public void LanzarEnergia()
    {
        // Instanciar un proyectil de energía en el punto de lanzamiento
        GameObject energia = Instantiate(energiaPrefab, puntoLanzamiento.position, transform.rotation);

        // Obtener el componente Rigidbody2D del proyectil de energía
        Rigidbody2D rbEnergia = energia.GetComponent<Rigidbody2D>();

        if (rbEnergia != null)
        {
            // Obtener la dirección de disparo basada en la orientación del jugador
            Vector2 direccionDisparo = ObtenerDireccionDisparo();

            // Aplicar fuerza al proyectil de energía en la dirección calculada
            rbEnergia.AddForce(direccionDisparo * fuerzaLanzamiento, ForceMode2D.Impulse);
        }
    }

    private Vector2 ObtenerDireccionDisparo()
    {
        if (jugador != null)
        {
            // Obtener la escala local del objeto del jugador
            Vector2 escalaLocal = jugador.transform.localScale;

            // Determinar la dirección de disparo basada en la escala local
            if (escalaLocal.x > 0f)
            {
                // El jugador está mirando hacia la derecha, disparar hacia la derecha
                return transform.right;
            }
            else
            {
                // El jugador está mirando hacia la izquierda, disparar hacia la izquierda
                return -transform.right;
            }
        }
        else
        {
            Debug.LogError("La referencia al jugador no está establecida en ArmaController.");
            return Vector2.right; // Devuelve una dirección predeterminada (derecha) en caso de error
        }
    }





}
