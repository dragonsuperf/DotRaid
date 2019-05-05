using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float speed = 1.0f;
    protected float liftTime = 0.0f;
    protected Vector2 targetVec;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Set(float lifeTime, float speed, Vector2 pos, Vector2 target)
    {
        this.speed = speed;
        transform.position = pos;
        gameObject.SetActive(true);
        StartCoroutine(WaitForDisable(lifeTime));

        Vector3 vectorToTarget = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);


        GetComponent<Rigidbody2D>().AddForce((target - (Vector2)transform.position).normalized * speed);
    }

    public void Set(float lifeTime, float speed, Vector2 pos, Vector2 target, float extraAngle)
    {
        this.speed = speed;
        transform.position = pos;
        gameObject.SetActive(true);
        StartCoroutine(WaitForDisable(lifeTime));
        
        Vector3 vectorToTarget = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90 + extraAngle;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

        Vector2 normal = Quaternion.Euler(0, 0, extraAngle) * (target - (Vector2)transform.position).normalized;

        GetComponent<Rigidbody2D>().AddForce(normal * speed);
    }

    IEnumerator WaitForDisable(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
    }
}
