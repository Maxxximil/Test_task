using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player player;
    
    
    public NavMeshAgent Agent;
    
    
    private void Awake()
    {
        player = this;    
    }

    


    private void Update()
    {
       Agent.SetDestination(GameController.Instance.FinishPoint.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            Debug.Log("Death");
        }
    }
}
