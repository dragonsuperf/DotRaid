using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public struct SkillData
{
    public string name;
    public bool AOE;
    public string target;
    public int power;
    public float duration;
    public int mp;
    public float castSpeed;
    public float cooltime;
}


public class Skill : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    GameObject skillIcon;
    GameObject nullObject;
    Vector3 screenPoint;
    Vector3 offset;
    Canvas canvas;
    public Image cooldownImg;
    private bool isCooldown;
    public SkillData skillData;

    // Start is called before the first frame update
    void Start()
    {
        GetSkillData();
        cooldownImg = this.gameObject.GetComponent<Image>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        skillIcon = Instantiate(this.gameObject, this.gameObject.transform.position, Quaternion.identity, canvas.transform);
        
        skillIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isCooldown)
        {
            skillIcon.SetActive(true);

            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        }
        
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isCooldown)
        {
            skillIcon.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(point, Vector2.zero);
            RaycastHit2D hit = new RaycastHit2D();
            hit = Physics2D.Raycast(ray.origin, ray.direction);
            /*
            if (hit.collider.gameObject.tag == "Player")
            {
                nullObject = hit.collider.gameObject;
                nullObject.transform.localScale = new Vector3(4,4,1);
                Debug.Log("meet player");
            }

            */

            if (skillData.AOE)
            {
                //AOE 원 처리
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isCooldown)
        {
            skillIcon.SetActive(false);
            StartCoroutine(SkillCooldown(skillData.cooltime));
        }
        
    }
    
    public IEnumerator SkillCooldown(float cooldown)
    {
        while(cooldown > 1.0f)
        {
            isCooldown = true;
            cooldown -= Time.deltaTime;
            cooldownImg.fillAmount = 1.0f / cooldown;
            yield return new WaitForFixedUpdate();
            isCooldown = false;
        }
    }

    private void GetSkillData()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Tables/skillData");

        for (int i = 0; i < data.Count; i++)
        {
            if (data[i]["name"].Equals(this.name))
            {
                if (data[i]["AOE"].Equals("TRUE"))
                {
                    this.skillData.AOE = true;
                }
                else
                {
                    this.skillData.AOE = false;
                }
                this.skillData.target = (string)data[i]["target"];
                this.skillData.power = (int)data[i]["power"];
                this.skillData.duration = (int)data[i]["duration"];
                this.skillData.mp = (int)data[i]["mp"];
                this.skillData.castSpeed = (float)data[i]["castspeed"];
                this.skillData.cooltime = (float)data[i]["cooldown"];
            }
        }
    }
    
}
