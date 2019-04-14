using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMage : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        pm.AddPattern("attack", 3.0f, 1.0f, 0.1f);
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
