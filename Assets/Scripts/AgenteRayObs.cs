using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgenteRayObs : MonoBehaviour
{
    //tag P.4.1.2
    //2.O axente persegue se o target está dentro dunha distancia determinada en 360º e ten liña de visión (se un obxecto se interpón entre eles deixa de perseguilo)

    public Transform jugador; //Jugador 1
    public Transform casa; //Casa o retorno, Objetivo 2.1
    public float distanciaRay; // Proyecta un rayo
    
    NavMeshAgent agentePerseguidor; //Jugador 2
    RaycastHit hit; //Referencia al Raycast

    void Start()
    {
        agentePerseguidor = GetComponent<NavMeshAgent>(); //Acceder al componente NavMeshAgent
    }
    
    void FixedUpdate()
    {
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,-1, QueryTriggerInteraction.Ignore) )
        { //Si accedo a las fisicas del raycast mirando hacia delante
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white); //Se dibuja un rayo blanco hacia delante          
        }
        else //Sino
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distanciaRay, Color.black); //Se dibuja uno negro 
        }

        DistanciaJugadorRay(); //Se crea un nuevo metodo para calcular la distancia del jugador

    }

    private void DistanciaJugadorRay(){
        float distanciaToJugador = Vector3.Distance(jugador.position, transform.position); //calcular la distancia al jugador
        if(distanciaToJugador < distanciaRay && hit.collider.tag == "Jugador"){ //Si la distancia al jugador es menor a la distancia del ray Y ademas contiene el tag Jugador
            agentePerseguidor.destination = jugador.position; //el agente perseguidor ira a la posicion del jugador
        } else { //sino
            agentePerseguidor.destination = casa.position; //el agente perseguidor ira al punto inicial casa
        } 
    }
}