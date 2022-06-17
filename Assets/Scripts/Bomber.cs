using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Unit
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(!hasPath)
        {
            MoveToPoint(
                new Vector3(
                    Random.Range(transform.position.x - 10, transform.position.x + 10), 
                    transform.position.y,
                    Random.Range(transform.position.z - 10, transform.position.z + 10)
                )
            );
        }
    }
}
