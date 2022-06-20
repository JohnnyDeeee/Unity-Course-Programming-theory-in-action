using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private int _health;

    // ENCAPSULATION
    public int Health { get { return _health; } protected set { _health = value; } }

    // ABSTRACTION
    public void SubtractHealth(int amount)
    {
        if (_health - amount <= 0)
        {
            // Make sure we don't go below 0
            _health = 0;

            Die();
        } else
        {
            _health -= amount;
        }
    }

    // ABSTRACTION
    protected virtual void Die() {
        Destroy(gameObject);
    }
}
