using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    public GameObject[] effects;
    public GameObject blastObj;

    List<Effect> blastPool = new List<Effect>();
    Dictionary<string, List<Effect>> effectPool = new Dictionary<string, List<Effect>>();
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        blastObj = Instantiate(Resources.Load("Prefabs/Effects/blast") as GameObject);
        blastPool.Add(blastObj.GetComponent<Effect>());
        AddEffectToPool("blast", blastObj.GetComponent<Effect>(), 20);
        for (int i = 0; i < 10; i++)
            blastPool.Add(Instantiate(blastObj, transform).GetComponent<Effect>());
    }

    // Update is called once per frame
    void Update()
    {
    }

    Effect FindUseableObj(List<Effect> list)
    {
        return list.Find(e => !e.gameObject.activeSelf);
    }

    public void BlastOnPosition(Vector2 pos, float lifeTime)
    {
        Effect curEffect = FindUseableObj(blastPool);
        if (curEffect != null)
            curEffect.SetEffect(lifeTime, pos); 
    }

    public void PlayEffectOnPosition(string key, Vector2 pos, float lifeTime)
    {
        List<Effect> curList = new List<Effect>();
        effectPool.TryGetValue(key, out curList);
        if (curList.Count > 0)
        {
            Effect curEffect = FindUseableObj(curList);
            if (curEffect != null)
                curEffect.SetEffect(lifeTime, pos);
            else
                Debug.Log("사용 가능한 이펙트가 없습니다.");
        }
        else
            Debug.Log(key + "라는 키를 가진 Pool이 존재하지 않거나, 비어있습니다.");
    }

    public void PlayEffectOnPosition(string key, Vector2 pos, float lifeTime, Quaternion rotate)
    {
        List<Effect> curList = new List<Effect>();
        effectPool.TryGetValue(key, out curList);
        if (curList.Count > 0)
        {
            Effect curEffect = FindUseableObj(curList);

            if (curEffect != null)
            {
                curEffect.transform.localRotation = rotate;
                curEffect.SetEffect(lifeTime, pos);
            }
            else
                Debug.Log("사용 가능한 이펙트가 없습니다.");
        }
        else
            Debug.Log(key + "라는 키를 가진 Pool이 존재하지 않거나, 비어있습니다.");
    }

    public void AddEffectToPool(string key, Effect e) // 이펙트 등록
    {
        if (!effectPool.ContainsKey(key))
            effectPool.Add(key, new List<Effect>());
        effectPool[key].Add(Instantiate(e, transform));
    }

    public void AddEffectToPool(string key, Effect e, int howMany) // 이펙트 등록 여러개    
    {
        if (!effectPool.ContainsKey(key))
            effectPool.Add(key, new List<Effect>());

        for(int i = 0; i < howMany; i++) effectPool[key].Add(Instantiate(e, transform));
    }

    public IEnumerator DestroyWithFadeOut(GameObject obj)
    {
        Color tmp = obj.GetComponent<SpriteRenderer>().color;

        for (; tmp.a > 0; tmp.a -= 0.1f)
        {
            obj.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(obj);
        yield return new WaitForSeconds(0.1f);
    }
}
