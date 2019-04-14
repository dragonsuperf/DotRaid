using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Set(float lifeTime, Vector2 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        StartCoroutine(WaitForDisable(lifeTime));
    }

    IEnumerator WaitForDisable(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
    }
}
