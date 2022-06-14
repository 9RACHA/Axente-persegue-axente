using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TresEstados : MonoBehaviour
{   
    //[tag P.4.1.4]
    //O axente persegue se o target está dentro dunha distancia determinada e nun ángulo de visión determinado Utilizando estados

    public Transform objetivo; //Jugador 1
    public Transform casa; //Retorno o punto inicial

    public float proximidadRadio; //Distancia del ray
    public float anguloVision; //Angulo de vision que tendra el agente

    NavMeshAgent agente; //Jugador 2
    AgentState estado; //Estado para los 3 tipos 
    RaycastHit hit; //Referencia al raycast
    
    
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>(); //agente accede al componente NavMeshAgent
        casa = GameObject.FindGameObjectWithTag("Retorno").transform; //acceder a la variable casa mediante un tag llamado Retorno
        objetivo = GameObject.FindGameObjectWithTag("Jugador").transform; //el objetivo sera igual a acceder al tag jugador 
        estado = AgentState.Idle; //estado del agente parado
        
    }

    void FixedUpdate() { //Cuando se tocan fisicas es mejor 
        Vector3 RayI = transform.forward * -30; //Vector del ray Izq igual a la posicion recto por menos 30
        Vector3 RayD = transform.forward * 30; //Vector del ray Der igual a la posicion de recto por 30
    
        Debug.DrawLine(transform.position, objetivo.transform.position, Color.black, 1f); //Dibuja una linea de color negro
        Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow, 1f); //Dibuja el raycast 
        Debug.DrawRay(transform.position, RayI * 10, Color.blue, 1f); //Diuja el raycast izquierdo
        Debug.DrawRay(transform.position, RayD * 10, Color.white, 1f); //Dibuja el raycast derecho
        
        float distanciaAlObjetivo = Vector3.Distance(objetivo.position, transform.position); //calculo distanciaAlObjetivo 
        if(Physics.Raycast(transform.position, objetivo.transform.position - transform.position, out hit, proximidadRadio)) { 

            Sigo();
        
        }
        
        switch (estado) { //switch permite crear varias opciones o casos para los diferentes estados
            case AgentState.Idle: //caso 1 : parado, esperando
            Debug.Log(estado); //Se ejecuta por consola el estado
                if(Sigo() == true) //Si sigo es verdadero
                    SetState(AgentState.Chasing); //actualiza el estado a persiguiendo
                break; //Finaliza el caso 1
            case AgentState.Chasing: //caso 2 : persiguiendo al jugador
            Debug.Log(estado); //Se ejecuta por consola el estado
                if(Sigo() == true) //Si sigo es verdadero
                    agente.destination = objetivo.position; //el destino de agente sera igual a la posicion del objetivo
                else //Sino
                    SetState(AgentState.Returning); //programa una actualización al objeto estado de un componente a volviendo
                break; //Finaliza el caso 2
            case AgentState.Returning: //caso 3 : volviendo a casa
            Debug.Log(estado); //Ejecuta por consola el estado
                if(Sigo() == true) //Si sigo es igual a verdadero
                    SetState(AgentState.Chasing); //programa una actualización al objeto estado de un componente a persiguiendo
                else if (agente.destination == casa.position) //Sino el destino del agente sera igual a la posicion de la casa
                    SetState(AgentState.Idle); //programa una actualización al objeto estado de un componente a parado
                break; //Finaliza el caso 3
        }
        
    }
    public void SetState(AgentState newState) { //programa una actualización al objeto estado de un componente
        if(newState != estado) { //Si el nuevo estado no es estado
            estado = newState; //estado sera el nuevo estado
            switch (newState){ //crea varias opciones para los diferentes estados
                case AgentState.Idle: //caso 1 estado del agente parado
                break; //Finaliza el caso 1
                case AgentState.Chasing: //caso 2 estado del agente pesiguiendo
                break; //Finaliza el caso 2
                case AgentState.Returning: //caso 3 estado del agente volviendo
                break; //Finaliza el caso 3
            }
        }
    }
    public bool Sigo(){ //bool para que devuelva verdadero o falso
        float distanciaAlObjetivo = Vector3.Distance(objetivo.position, transform.position);
        
        if(distanciaAlObjetivo < proximidadRadio && Veo() == true){ //Si la distancia al objetivo es menor a la proximidadDelRadio Y Ademas Veo es verdadero
                agente.destination = objetivo.position; //el destino de agente sera el objetivo jugador 1
                return true; //Devuelve verdadero
        }
        else { //Sino
            agente.destination = casa.position; //el destino del agente sera igual a la posicion de casa punto de inicio
            return false; //devuelve falso
        }
    }
    public bool Veo() { //bool para que devuelva verdadero o falso
        Vector3 direccionObjetivo = objetivo.transform.position - transform.position; //direccionObjetivo sera igual a la direccion de objetivo Jugador 1 - la posicion
        anguloVision = Vector3.Angle(transform.forward, direccionObjetivo); 
        
        if (anguloVision > -40 && anguloVision < 40) { //Si el angulo de vision es mayor a -40 Y ademas al angulo de vision menor a 40
            return true; //Devuelve verdadero
        }
        else if((hit.transform.tag == "Obstaculo")){ //Sino el raycast pasara por el tag Obstaculo
            Debug.Log("No veo"); //Se imprime por consola 
            agente.destination = casa.position; //el destino del agente sera igual al de casa o punto de incio
        } 
        return false; //Devuelve falso
    }
    
}
 public enum AgentState { //enum permite crear diferentes estados
    Idle, //Parado
    Chasing, //Persiguiendo
    Returning, //Volviendo
 }