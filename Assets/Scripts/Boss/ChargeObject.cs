using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //other.gameObject.GetComponent<Rigidbody2D>().drag = 5.0f;
            other.gameObject.GetComponent<Actor>().TakeDamage(10.0f);
            EffectManager.Instance.PlayEffectOnPosition("blast", other.transform.position, 1.0f);
        }
    }

}
