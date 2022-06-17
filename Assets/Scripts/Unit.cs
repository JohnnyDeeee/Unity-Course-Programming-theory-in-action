using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : Entity
{
    private NavMeshAgent _navAgent;

    protected bool hasPath;

    protected virtual void Start()
    {
        Debug.Log("Unit Start()");
        _navAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        hasPath = _navAgent.hasPath;

        Debug.DrawLine(transform.position, _navAgent.destination); // DEBUG
    }

    protected void MoveToPoint(Vector3 point)
    {
        _navAgent.SetDestination(point);
    }

    protected void Attack(Transform target)
    {
        throw new System.NotImplementedException();
    }
}
