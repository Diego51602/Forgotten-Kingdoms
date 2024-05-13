using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [Header("Movimiento")]
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velocidadDeMovimiento;
    [Range(0, 0.3f)][SerializeField] private float suavizadoDeMovimiento;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;

    [Header("Rebote")]

    [SerializeField] private float velocidadRebote;

    [Header("Arma")]
    public Transform armaPuntoDeAgarre; // Punto de agarre del arma en el jugador
    private GameObject armaActual; // Referencia al arma actualmente en uso

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;

        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }

        // Verificar si se presiona la tecla "E" para recoger un arma
        if (Input.GetKeyDown(KeyCode.E) && armaActual == null)
        {
            RecogerArma(); // Llamar al método para recoger un arma
        }

        // Verificar si se presiona la tecla "F" para usar un arma
        if (Input.GetKeyDown(KeyCode.F) && armaActual != null)
        {
            UsarArma(); // Llamar al método para usar el arma
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);

        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);

        salto = false;
    }

    public void Rebote()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, velocidadRebote);
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        if (enSuelo && saltar)
        {
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void RecogerArma()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f); // Colisionadores cercanos

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Arma")) // Verificar si es un arma (asumiendo que tiene el tag "Arma")
            {
                // Desactivar el collider del arma para evitar interacciones adicionales
                collider.enabled = false;

                // Obtener referencia al GameObject del arma
                armaActual = collider.gameObject;

                // Desactivar la física del arma (Rigidbody2D y Collider2D)
                Rigidbody2D rbArma = armaActual.GetComponent<Rigidbody2D>();
                if (rbArma != null)
                {
                    rbArma.simulated = false;
                }
                Collider2D colliderArma = armaActual.GetComponent<Collider2D>();
                if (colliderArma != null)
                {
                    colliderArma.enabled = false;
                }

                // Asignar el arma al punto de agarre del jugador
                armaActual.transform.parent = armaPuntoDeAgarre;
                armaActual.transform.localPosition = Vector3.zero;
                armaActual.transform.localRotation = Quaternion.identity; // Restaurar la rotación inicial

                // Mantener el tamaño original del arma (pequeño)
                armaActual.transform.localScale = new Vector3(0.3883206f, 0.456985f, 0.5669f); // Ajustar la escala al tamaño original

                // Alinear la rotación del arma según la orientación del jugador
                Vector3 localScale = transform.localScale;
                if (localScale.x < 0f) // El jugador está mirando a la izquierda
                {
                    armaActual.transform.localScale = new Vector3(-1f, 1f, 1f); // Invertir la escala en X
                }

                // Activar el GameObject del arma para que sea visible
                armaActual.SetActive(true);

                // Salir del bucle una vez que se recoge un arma
                break;
            }
        }
    }




    private void UsarArma()
    {
        // Aquí puedes agregar la lógica para usar el arma
        // Por ejemplo, lanzar proyectiles desde el arma, activar efectos visuales, etc.
        Debug.Log("¡Usando el arma!");

        // Lanzar energía desde el arma (llamando a un método en el script del arma, por ejemplo)
        ArmaController arma = armaActual.GetComponent<ArmaController>();
        if (arma != null)
        {
            arma.LanzarEnergia();
        }
    }

    public void AumentarVelocidad(float aumento)
    {
        velocidadDeMovimiento += aumento;
        Debug.Log("Velocidad aumentada a: " + velocidadDeMovimiento);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
