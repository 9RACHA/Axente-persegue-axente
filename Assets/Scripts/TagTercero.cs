using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TagTercero : MonoBehaviour
{   
    //[tag P.4.1.3]
    //O axente persegue se o target está dentro dunha distancia determinada e nun ángulo de visión determinado 
    public Transform objetivo;
    public Transform casa;

    public float proximidadRadio; 
    public float anguloVision;

    NavMeshAgent agente;
    AgentState estado;
    RaycastHit hit;
    
    
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        casa = GameObject.FindGameObjectWithTag("Vuelvo").transform;
        objetivo = GameObject.FindGameObjectWithTag("Jugador").transform;
        estado = AgentState.Idle;
        
    }

    void FixedUpdate() {
        Vector3 RayI = transform.forward * -30;
        Vector3 RayD = transform.forward * 30;
    
        Debug.DrawLine(transform.position, objetivo.transform.position, Color.black, 1f);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow, 1f);
        Debug.DrawRay(transform.position, RayI * 10, Color.blue, 1f);
        Debug.DrawRay(transform.position, RayD * 10, Color.white, 1f);
        
        float distanciaAlObjetivo = Vector3.Distance(objetivo.position, transform.position);
        if(Physics.Raycast(transform.position, objetivo.transform.position - transform.position, out hit, proximidadRadio)) {

            Sigo();
             
        }
        
        switch (estado) {
            case AgentState.Idle: //caso 1 : parado, esperando
            Debug.Log(estado);
                if(Sigo() == true)
                    SetState(AgentState.Chasing);
                break;
            case AgentState.Chasing: //caso 2 : persiguiendo al jugador
            Debug.Log(estado);
                if(Sigo() == true)
                    agente.destination = objetivo.position;
                else 
                    SetState(AgentState.Returning);
                break;
            case AgentState.Returning: //caso 3 : volviendo a casa
            Debug.Log(estado);
                if(Sigo() == true)
                    SetState(AgentState.Chasing);
                else if (agente.destination == casa.position)
                    SetState(AgentState.Idle);
                break;        
        }
        
    }
    public void SetState(AgentState newState) {
        if(newState != estado) {
            estado = newState;
            switch (newState){
                case AgentState.Idle:
                break;
                case AgentState.Chasing:
                break;
                case AgentState.Returning:
                break;
            }
        }
    }
    public bool Sigo(){
        float distanciaAlObjetivo = Vector3.Distance(objetivo.position, transform.position);
        
        if(distanciaAlObjetivo < proximidadRadio && Veo() == true){
                agente.destination = objetivo.position;
                return true;
        }
        else {
            agente.destination = casa.position;
            return false;
        }
    }
    public bool Veo() {
        Vector3 direccionObjetivo = objetivo.transform.position - transform.position;
        anguloVision = Vector3.Angle(transform.forward, direccionObjetivo);
        
        if (anguloVision > -40 && anguloVision < 40) {
            return true;
        }
        else if((hit.transform.tag == "Obstaculo")){
            Debug.Log("No veo");
            agente.destination = casa.position;
        } 
        return false;
    }
    
}
 public enum AgentState {
    Idle, 
    Chasing, 
    Returning, 
 }