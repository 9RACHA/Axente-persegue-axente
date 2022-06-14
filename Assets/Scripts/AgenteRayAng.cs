using UnityEngine;
using UnityEngine.AI;

public class AgenteRayAng : MonoBehaviour
{
    //3.O axente persegue se o target está dentro dunha distancia determinada e nun ángulo de visión determinado
    //tag P.4.1.3

    
    public Transform jugador; //Jugador 1
    public Transform casa; //Retorno o punto inicial

    public float distanciaRay; //Distancia del raycast
    public float anguloVision; //Angulo de vision que tendra el agente 

    Vector3 direccionToObjetivo; //Posicion de la direccion al objetivo

    NavMeshAgent agentePerseguidor; //Agente Jugador 2
    
    RaycastHit hit; //Referencia al raycast


    void Start()
    {
        agentePerseguidor = GetComponent<NavMeshAgent>(); //agentePerseguidor accede al componente NavMeshAgent
        anguloVision = Vector3.Angle(transform.forward, direccionToObjetivo); //anguloVision sera igual a un angulo de mirar hacia delante y la direccion al objetivo
        direccionToObjetivo = jugador.transform.position - transform.position; //Inicializar la variable direccionToObjetivo
    }
    
    void FixedUpdate()
    {
        
        Vector3 direccionToJugador = (jugador.position - transform.position).normalized; //Normalize, un vector mantiene la misma dirección pero su longitud es 1.0
        
        if (Physics.Raycast(transform.position, direccionToJugador, out hit, Mathf.Infinity,-1, QueryTriggerInteraction.Ignore) )
        { // Si accedo a las fisicas del raycast mirando hacia delante
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue); //Se dibuja un rayo blanco hacia delante                  
        }
        else //Sino
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distanciaRay, Color.yellow); //Se dibuja uno amarillo
        }

        EnRango(); //Metodo para calcular el si estoy en rango de vision      
    }

    
    private bool Sigo(){
        float distanciaToJugador = Vector3.Distance(jugador.position, transform.position);
        if(distanciaToJugador < distanciaRay && hit.collider.tag == "Jugador"){ //Si la distancia al jugador es menor a la distanciaRay Y el raycast hit toca el collider con el tag Jugador
            agentePerseguidor.destination = jugador.position; //el agentePerseguidor volvera al destino de la posicion del jugador
            return true; //devuelve verdadero
        } else { //Sino
            agentePerseguidor.destination = casa.position; //agentePersiguidor volvera al destino de la casa o punto de inicio
            return false; //Devuelve falso
        } 
    }

    private bool EnRango(){
         if(anguloVision > -30 && anguloVision < 30){ //Si anguloVision es mayor a -30 Y ademas menor que 30
            if (hit.collider.tag == "Jugador") { //Si golpeo el collider con el nombre Jugador
                Sigo(); //Ejecuto el metodo
                return true; //devulelvo verdadero
            } else { //Sino
                agentePerseguidor.destination = casa.position; //agentePerseguidor ira a la posicion de casa
                return false; //Devuelvo falso
            }
        }     
        return true; //Devuelvo verdadero fin del metodo
    }  
}