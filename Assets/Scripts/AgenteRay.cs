using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgenteRay : MonoBehaviour
{  
     //tag P.4.1.1
    //1. O axente persegue se o target está dentro dunha distancia determinada en 360º (non importa que haxa obxectos entres eles, continúa a persecución)

    public Transform jugador; //Jugador 1
    public Transform casa; //Casa a donde ira si no detecta al jugador
    NavMeshAgent agentePerseguidor; //El jugador que nos sigue
    public float distanciaRay; // La distancia que el raycast tendra
    

    // Start is called before the first frame update
    void Start()
    {
        agentePerseguidor = GetComponent<NavMeshAgent>();  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit; //Referencia al raycast

        float distanceToJugador = Vector3.Distance(jugador.position, transform.position); //calcular distancia hasta el jugador

         if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) //Si detecta al jugador
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue); //Se dibujara una linea de color azul
            //Debug.Log("Jugador detectado");
        }
        else //Sino detecta al jugador
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distanciaRay, Color.black); //Dibujara linea negra
           // Debug.Log("Jugador NO Detectado");
        }

        if(distanceToJugador < distanciaRay){ //Si la distancia al jugador es Menor que la distancia del raycast
            agentePerseguidor.destination = jugador.position; //el destino del agente perseguidor sera igual a la posicion del jugador 1
        } else { //Sino
            agentePerseguidor.destination = casa.position; //El destino del agente perseguidor sera la casa
        }
    }
}