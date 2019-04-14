using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEffect(float lifeTime , Vector2 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        StartCoroutine(WaitForDisable(lifeTime));
    }

    public void SetEffect(float lifeTime, string spriteName)
    {

    }

    IEnumerator WaitForDisable(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
    }
}
