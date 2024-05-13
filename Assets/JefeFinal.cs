using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFinal : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    public Rigidbody2D rb2D;

    public Transform jugador;
    private bool mirandoDerecha = true;

    [Header("Vida")]
    [SerializeField] private float vida;
    [SerializeField] public BarraDeVida barraDeVida;

    [Header("Movimiento hacia el jugador")]
    [SerializeField] private float velocidadMovimiento;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        barraDeVida.InicializarBarraDeVida(vida);
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        barraDeVida.gameObject.SetActive(false);
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;

        barraDeVida.CambiarVidaActual(vida);

        if (vida <= 0)
        {
            animator.SetTrigger("Muerte");
        }
    }

    private void Muerte()
    {
        Destroy(gameObject);
        barraDeVida.gameObject.SetActive(false);
    }

    void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("DistanciaJugador", distanciaJugador);
        if (EsVisibleParaJugador())
        {
            MoverHaciaJugador();
            MostrarBarraDeVida();
        }
        else
        {
            OcultarBarraDeVida();
        }
    }

    bool EsVisibleParaJugador()
    {
        return Vector2.Distance(transform.position, jugador.position) < 60f;
    }

    void MoverHaciaJugador()
    {
        // Calcular la dirección hacia el jugador
        Vector2 direccion = (jugador.position - transform.position).normalized;

        // Mover el jefe final en la dirección calculada
        rb2D.velocity = new Vector2(direccion.x * velocidadMovimiento, rb2D.velocity.y);

        // Determinar la orientación del jefe final
        if ((direccion.x < 0 && mirandoDerecha) || (direccion.x > 0 && !mirandoDerecha))
        {
            // Voltear al jefe final en la dirección opuesta
            Voltear();
        }
    }

    void Voltear()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1; // Invertir la escala en el eje x para voltear al jefe
        transform.localScale = escala;
    }




    public void MirarJugador()
    {
        if (jugador.position.x < transform.position.x && mirandoDerecha)
        {
            // Cambiar la dirección hacia la izquierda
            mirandoDerecha = false;
            transform.eulerAngles = new Vector3(0, 180f, 0); // Rotar hacia la izquierda (180 grados)
        }
        else if (jugador.position.x > transform.position.x && !mirandoDerecha)
        {
            // Cambiar la dirección hacia la derecha
            mirandoDerecha = true;
            transform.eulerAngles = new Vector3(0, 0, 0); // Rotar hacia la derecha (0 grados)
        }

    }

    void MostrarBarraDeVida()
    {
        // Mostrar la barra de vida si el jugador está dentro del rango de visión
        barraDeVida.gameObject.SetActive(true);
    }

    void OcultarBarraDeVida()
    {
        // Ocultar la barra de vida si el jugador está fuera del rango de visión
        barraDeVida.gameObject.SetActive(false);
    }


    public void Ataque()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);
        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<vidaJugador>().TomarDaño(dañoAtaque);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }
}
