using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// INHERITANCE
public class Unit : Entity
{
    private NavMeshAgent _navAgent;

    protected bool hasPath;

    // ENCAPSULATION
    public Vector3 TargetPosition { get; private set; }
    public Entity TargetEntity { get; private set; }
    public bool HasTarget { get; private set; }
    
    [SerializeField]
    private int _attackDamage;

    protected virtual void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    // ABSTRACTION
    protected virtual void Update()
    {
        if(HasTarget && !TargetEntity) // Target probably died and got destroyed
        {
            ClearTarget();
        }

        if (HasTarget)
        {
            if(!(TargetEntity as Castle)) // Target is able to move
            {
                TargetPosition = TargetEntity.gameObject.transform.position; // Update target position to new position
            }
            MoveToPoint(TargetPosition);
        }

        Debug.DrawLine(transform.position, _navAgent.destination); // DEBUG
    }

    private void MoveToPoint(Vector3 point)
    {
        _navAgent.SetDestination(point);
    }

    public void SetTarget(Vector3 position, Entity entity)
    {
        TargetPosition = position;
        TargetEntity = entity;
        HasTarget = true;
    }

    protected void ClearTarget()
    {
        TargetPosition = Vector3.zero;
        TargetEntity = null;
        HasTarget = false;
    }

    // ABSTRACTION
    protected virtual void Attack(Entity target)
    {
        // Subtract health from target
        TargetEntity.SubtractHealth(_attackDamage);

        Debug.Log($"Attacked {TargetEntity}, health is now {TargetEntity.Health}");
    }

    // POLYMORPHISM
    protected override void Die()
    {
        Debug.Log($"{gameObject} is now dying!");

        base.Die();
    }

    private void OnDestroy()
    {
        ClearTarget();
    }
}
