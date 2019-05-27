using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMage : Enemy
{
    Projectile proj;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        proj = Resources.Load<Projectile>("Prefabs/Enemy/TestProjectile");
        pm.AddPattern("attack", 3.0f, 1.0f, 0.1f);
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    
    void ThrowThreeProjectile()
    {
        for (int i = 0; i < 3; i++)
        {
            Projectile p = Instantiate(proj, transform);
            p.Set(10.0f, 300.0f, transform.position, currentTarget.position, (-15.0f + (15.0f * i)), false);
        }
    }
}
