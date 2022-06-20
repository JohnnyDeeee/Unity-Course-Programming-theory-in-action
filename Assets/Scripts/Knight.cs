using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Knight : Unit
{
    private Animator _animator;
    private bool _isFighting;

    // POLYMORPHISM
    protected override void Start()
    {
        base.Start();

        _animator = GetComponent<Animator>();
    }

    // POLYMORPHISM
    protected override void Update()
    {
        base.Update();

        if (_isFighting)
        {
            _animator.SetBool("attack", true);
        }
        else
        {
            _animator.SetBool("attack", false);
        }

        if (HasTarget && !_isFighting)
        {
            _animator.SetBool("run", true);
        }
        else
        {
            _animator.SetBool("run", false);

            // Find bomb that is closest and set it as target
            int layerMask = LayerMask.GetMask("Bombers");
            Collider[] hits = Physics.OverlapSphere(transform.position, Mathf.Infinity, layerMask);
            if (hits.Length > 0)
            {
                Collider hit = hits[Random.Range(0, hits.Length)]; // TODO: Prioritize the closest bomber, it should also not be a target of another knight
                Bomber bomber = hit.gameObject.GetComponent<Bomber>();
                SetTarget(bomber.transform.position, bomber);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Entity>() == TargetEntity)
        {
            Attack(TargetEntity);
        }
    }

    // POLYMORPHISM
    protected override void Attack(Entity target)
    {
        _isFighting = true;
        // Animator will call AfterAttack() when done
    }

    private void AfterAttack()
    {
        _isFighting = false;

        base.Attack(TargetEntity);
    }
}
