using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class DeteccionPorRadio : MonoBehaviour
{
    //3.O axente persegue se o target está dentro dunha distancia determinada e nun ángulo de visión determinado
    //tag P.4.1.3

    public GameObject jugador;
    public Transform casa;
    public LayerMask capa;
    
    NavMeshAgent agente;

    private Color colorActivo = Color.blue;
    private Color colorDesactivo = Color.black;

    Collider c;
    

    public float radio = 9;
    

    
    void Start()
    {
        this.agente = GetComponent<NavMeshAgent>();
        
        agente.destination = jugador.transform.position;
    }

    
    void Update()
    {
        if (RadioCheck()){
            this.agente.destination = jugador.transform.position;
        } else {
            this.agente.destination = casa.position;
        }
    }

    private bool RadioCheck(){
        RaycastHit[] JugadorEnArea = Physics.SphereCastAll(this.transform.position, radio, this.transform.forward, radio, capa);
            if (JugadorEnArea.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
    }
}