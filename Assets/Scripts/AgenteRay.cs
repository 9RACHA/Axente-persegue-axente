using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgenteRay : MonoBehaviour
{  
     //tag P.4.1.1
    //1. O axente persegue se o target está dentro dunha distancia determinada en 360º (non importa que haxa obxectos entres eles, continúa a persecución)

    public Transform jugador; 
    NavMeshAgent perseguidor; 
    public float distanciaRay; //Si es menor o igual que 29 se dirijira hacia el jugador si es 30 o mayor hacia el punto de inicio
    public Transform puntoInicio;

    // Start is called before the first frame update
    void Start()
    {
        perseguidor = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        float distanceToJugador = Vector3.Distance(jugador.position, transform.position);

         if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distanciaRay, Color.black);
        }

        if(distanceToJugador < distanciaRay){
            perseguidor.destination = jugador.position;
        } else {
            perseguidor.destination = puntoInicio.position;
        }
    }
}