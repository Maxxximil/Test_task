using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    
    public NavMeshAgent Agent;

   
    private void Update()
    {
        Agent.SetDestination(GameController.Instance.FinishPoint.position);
    }
}
