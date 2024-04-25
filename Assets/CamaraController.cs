using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{

    public GameObject Target;
    private Vector3 TargetPos;

    public float HaciaAdelante;

    public float Smoothing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);

        if(Target.transform.localScale.x == 1){ //Derecha
            TargetPos = new Vector3(TargetPos.x + HaciaAdelante, TargetPos.y, transform.position.z);
        }

        if(Target.transform.localScale.x == 1){ //Izquierda
            TargetPos = new Vector3(TargetPos.x - HaciaAdelante, TargetPos.y, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, TargetPos, Smoothing * Time.deltaTime);
    }
}
