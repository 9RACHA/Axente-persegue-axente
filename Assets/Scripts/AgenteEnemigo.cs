using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgenteEnemigo : MonoBehaviour
{
    public Transform objetivo;
    NavMeshAgent agente;

    // AgentState estado;
    public Transform retorno;
    public float distanciaAlJugador;

    public Vector3 diferencia;

    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    /*
    Quaternion origen = Quaternion.Euler(10f,0f,0f);
    Quaternion destino = Quaternion.Euler(300f,0f,0f);

    Quaternion diferencia = destino * Quaternion.Inverse(origen);

    this.diferencia = diferencia.eulerAngles;
    */
    
    }

    // Update is called once per frame
    void FixedUpdate()  //Hacer un switch que contenga los estados
    {
            // bool inRangeAndVisible = IsRangeAndVisible();
     //agente.destination = objetivo.position;
    RaycastHit hit;
     //float distanceToPlayer = Vector3.Distance(PlayerPrefs.position, transform.position);
    //Vector3 directionToTarget = (player.position - transform.position).normalized;
    Vector3 directionToTarget = (objetivo.position - transform.position).normalized;
    if (Physics.Raycast(transform.position + Vector3.up, directionToTarget, out hit, distanciaAlJugador, -1)){ //QueryTrigger.Ignore
    } 
        Detectado();
    }

/*    bool IsRangeAndVisible(){

    }
*/
    //O axente persegue se o target está dentro dunha distancia determinada en 360º 
    public bool Detectado(){
        if(distanciaAlJugador >= 360){
            agente.destination = retorno.position;
            Debug.Log("Jugador No Detectado");
            return false;
        } else {
            agente.destination = objetivo.position;
            Debug.Log("Jugador Detectado");
            return true;
        }
    }
    
       //STOP
       //FOLLOWING
       //RETURN
     // if(inrange()) que sea bool
              //  Target = player.position;
                // else
               // target = init.position;       
}

/*
    public enum EstadoAgente {
                   Idle,
                   Chasing,
                   Returning
               }
*/