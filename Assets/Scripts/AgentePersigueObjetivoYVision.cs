using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentePersigueObjetivoYVision : MonoBehaviour
{   
    //tag P.4.1.2
    //2.O axente persegue se o target está dentro dunha distancia determinada en 360º e ten liña de visión (se un obxecto se interpón entre eles deixa de perseguilo)

    public Transform objetivo;
    NavMeshAgent agente;
    public float rango;
    public float angulo;

    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToTarget = Vector3.Distance(objetivo.position, transform.position);
        Vector3 targetDirection = objetivo.position - transform.position;
        angulo = Vector3.Angle(targetDirection, transform.forward); //Angulo de vision del agente
        if (distanceToTarget >= rango){
            agente.destination = objetivo.position;
        }
        
    }
}
