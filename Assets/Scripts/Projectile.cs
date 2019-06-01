using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float speed = 1.0f;
    protected float lifeTime = 0.0f;
    protected Vector2 targetVec;
    protected CircleCollider2D collider;

    [SerializeField]
    private ProjectileStat stat;

    struct ProjectileStat
    {
        public float speed;
        public float lifeTime;
        public float damage;
        public Vector2 targetVec;
        public CircleCollider2D collider;
    }
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Set(float lifeTime, float speed, Vector2 pos, Vector2 target, bool forPlayer)
    {
        this.speed = speed;
        transform.position = pos;
        gameObject.SetActive(true);
        StartCoroutine(WaitForDisable(lifeTime));

        Vector3 vectorToTarget = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

        gameObject.tag = forPlayer ? "Player-Proj" : "Enemy-Proj";

        GetComponent<Rigidbody2D>().AddForce((target - (Vector2)transform.position).normalized * speed);
    }

    public void Set(float lifeTime, float speed, Vector2 pos, Vector2 target, float extraAngle, bool forPlayer)
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

        gameObject.tag = forPlayer ? "Player-Proj" : "Enemy-Proj";

        GetComponent<Rigidbody2D>().AddForce(normal * speed);
    }

    IEnumerator WaitForDisable(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string compareString = gameObject.tag == "Enemy-Proj" ? "Player" : "Enemy";

        if (collision.tag == compareString)
        {
            Actor t = collision.gameObject.GetComponent<Actor>();
            if (t == null)
            {
                Debug.Log("collision null");
                return;
            }
            t.TakeDamage(10);
            EffectManager.Instance.PlayEffectOnPosition("blast", transform.position, 0.5f);
            Destroy(gameObject);
        }
    }
}