using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    GameManager gameManager;
    GameObject[] characters;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        characters = gameManager.GetChars();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    /*
    //도트힐 초당 30힐 7틱
    public IEnumerator Regeneration(Character[] obj, CharacterStats[] initStat,int count)
    {
        //7틱
        if (count == 7)
        {
            yield break;
        }

        for (int i = 0; i < characters.Length; i++)
        {
            //hp 최대량
            if(obj[i].stat.hp + 30 > initStat[i].hp)
            {
                obj[i].stat.hp = initStat[i].hp;
            }
            else
            {
                obj[i].stat.hp += 30;
            }
            
        }

        count ++;

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(Regeneration(obj,initStat, count));

    }

    public IEnumerator DefUp(Character obj)
    {
        obj.stat.armor += 10;
        yield return new WaitForSeconds(10.0f);
        obj.stat.armor -= 10;
    }

    public void Bash(EnemyStats stats, float cooldown)
    {
        stats.hp -= 100;
        cooldown = 5.0f;
    }

    public void FireBall(EnemyStats stats)
    {
        stats.hp -= 150;
    }

    public IEnumerator DamageUp(Character obj)
    {
        obj.stat.attack += 10;
        yield return new WaitForSeconds(10.0f);
        obj.stat.attack -= 10;
    }
    

    //최대HP 처리 필요
    public IEnumerator MinorHeal()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(point, Vector2.zero);
                RaycastHit2D hit = new RaycastHit2D();
                hit = Physics2D.Raycast(ray.origin, ray.direction);

                if(hit.collider.gameObject.tag == "Player")
                {
                    hit.transform.gameObject.GetComponent<Character>().stat.hp += 100;
                    yield break;
                }
                else
                {
                    yield break;
                }
                
            }
            yield return null;
        }
    }
    
    public IEnumerator MassCurse()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(point, Vector2.zero);
                RaycastHit2D hit = new RaycastHit2D();
                hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider.gameObject.tag != "Obstacle")
                {
                    if(Vector2.Distance(hit.transform.position, GameObject.FindWithTag("Enemy").transform.position) < 5)
                    {
                        GameObject.FindWithTag("Enemy").GetComponent<Boss>().stat.hp -= 10;
                    }
                    yield break;
                }
                else
                {
                    yield break;
                }

            }
            yield return null;
        }
    }
    */

}
