using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Bomber : Unit
{
    private Animator _animator;
    private bool _isFighting;
    private bool _isDying;

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

        // TODO: State machine instead of these if's
        if(_isDying)
        {
            _animator.SetBool("die", true);
        } else
        {
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
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Entity>() == TargetEntity)
        {
            Attack(TargetEntity);
        }

        // TODO: If target is a knight we might want to blow up when were close to it instead of when we collide with it?
    }

    // POLYMORPHISM
    protected override void Attack(Entity entity)
    {
        _isFighting = true;

        base.Attack(entity);
    }

    private void AfterAttack()
    {
        _isFighting = false;
        base.Die(); // Explosion attack is a suicide attack
    }

    // POLYMORPHISM
    protected override void Die()
    {
        _isDying = true;
    }

    private void AfterDie()
    {
        _isDying = false;

        base.Die();
    }
}
