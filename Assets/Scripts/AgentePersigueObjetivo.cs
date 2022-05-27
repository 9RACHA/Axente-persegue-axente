using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentePersigueObjetivo : MonoBehaviour
{  
     //tag P.4.1.1
    //1. O axente persegue se o target está dentro dunha distancia determinada en 360º (non importa que haxa obxectos entres eles, continúa a persecución)
    public Transform objetivo;
    NavMeshAgent agente;
    public float rango;

    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(objetivo.position, transform.position);
        if (distanceToTarget <= rango){  //Menor o igual que 35 por defecto no lo reconoce
            agente.destination = objetivo.position;
        }
    }
}
