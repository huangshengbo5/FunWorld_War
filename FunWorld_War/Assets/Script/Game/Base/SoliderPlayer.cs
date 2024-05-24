using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SoliderPlayer : BaseSolider
{
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        
    }

    public override void MoveToTarget(Vector3 targetPoint)
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        //navMeshAgent.destination = targetPoint;
        navMeshAgent.SetDestination(targetPoint);
    }

    public void Attack()
    {
        
    }
}