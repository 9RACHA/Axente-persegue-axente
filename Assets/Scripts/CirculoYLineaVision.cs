using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CirculoYLineaVision : MonoBehaviour
{
    //tag P.4.1.2
    //2.O axente persegue se o target está dentro dunha distancia determinada en 360º e ten liña de visión (se un obxecto se interpón entre eles deixa de perseguilo)

    public Transform objetivo; //A quien persigue
    NavMeshAgent agente; //El jugador enemigo
    public float rango; //Hasta donde detectará 36
    public Transform retorno; //A donde volvera el enemigo
    public float radio; //De deteccion, entre 2 y 3 varia por defecto
    
    
    void Start()
    {
         agente = GetComponent<NavMeshAgent>();
         objetivo = GameObject.FindGameObjectWithTag("Jugador").transform;
         retorno = GameObject.FindGameObjectWithTag("Retorno").transform;
         
         
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 targetDirection = (objetivo.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(objetivo.position, transform.position);
        if(Physics.Raycast(transform.position + Vector3.up, targetDirection, out hit, radio, -1)){ 
            if(hit.transform.tag == "Muro"){ //Si el enemigo no consigue ver al jugador
                agente.destination = transform.position;
            } else{
                SigoJugador();
            }
        }
    }
    public bool SigoJugador(){
        float distanceToTarget = Vector3.Distance(objetivo.position, transform.position);
        if(distanceToTarget < radio){
            agente.destination = objetivo.position;
            return true;
        } else{
            agente.destination = retorno.position;
            return false;
        }
    }
}
