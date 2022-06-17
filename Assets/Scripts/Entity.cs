using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int Health { get; protected set; }

    public void SubtractHealth(int amount)
    {
        if (Health - amount < 0)
        {
            // Make sure we don't go below 0
            Health = 0;

            OnDie();
        } else
        {
            Health -= amount;
        }
    }

    // TODO: Better name?
    protected void OnDie() { 
        //
    }
}
