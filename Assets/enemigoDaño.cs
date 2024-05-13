using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoDaño : MonoBehaviour
{
    [SerializeField] private GameObject efecto;
    private Animator animator;
    [SerializeField] private AudioClip audio1;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

        private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            
            foreach (ContactPoint2D punto in other.contacts)
            {
                if (punto.normal.y <= -0.9)
                {
                    // Si la colisión es desde arriba, haz que el jugador rebote
                    animator.SetTrigger("Golpe");
                    other.gameObject.GetComponent<MovimientoJugador>().Rebote();
                }
                else
                {
                    // Si la colisión es desde los lados, haz que el jugador pierda vida
                    other.gameObject.GetComponent<vidaJugador>().TomarDaño(1);
                }
            }
        }
    }

    public void Golpe()
    {
        Instantiate(efecto, transform.position, transform.rotation);
        ControladorSonidos.Instance.EjecutarSonido(audio1);
        Destroy(gameObject);
    }
}
