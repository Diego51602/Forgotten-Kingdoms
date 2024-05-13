using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoDoble : MonoBehaviour
{

    private Rigidbody2D rb2D;
    

    [Header("Salto")]

    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private int saltosExtraRestantes;
    [SerializeField] private int saltosExtra;

    private bool enSuelo;
    private bool salto = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);

        if(enSuelo){
            saltosExtraRestantes = saltosExtra;
        }

        if(Input.GetButtonDown("Jump")){
            salto = true;
        }
    }

    private void FixedUpdate(){
        Movimiento(salto);
        salto = false;
    }

    private void Movimiento(bool salto){
        if(salto){
            if(enSuelo){
                Salto();
            }else{
                if(salto && saltosExtraRestantes > 0){
                    Salto();
                    saltosExtraRestantes -= 1;
                }
            }
        }
    }

    private void Salto(){
        //rb2D.AddForce(new Vector2(0f, fuerzaSalto));
        rb2D.velocity = new Vector2(0f, fuerzaSalto);
        salto = false;
    }

    public void AgregarSaltosExtra(int cantidad)
    {
        saltosExtra += cantidad;
        fuerzaSalto = 10;
        Debug.Log("Saltos extra restantes: " + saltosExtraRestantes);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
