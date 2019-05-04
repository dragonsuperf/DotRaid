using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[Serializable]
public struct CharacterStats
{
    public string job;
    public string synergy;

    public bool agro; //탱커 어그로 획득 여부

    public int level;
    public int hp;
    public int armor;
    public int attack;
    public float attackSpeed;
    public int attackRangeRadius;
    public int awareRangeRadius;
    public int moveSpeed;
    public int CastSpeed;
}


public enum CharacterState
{
    idle, attack, move, cast, chase, count
}

public class Character : MonoBehaviour
{
    [Header("Use Skill")]
    public eSkill skill_first;
    public eSkill skill_second;
    [Space(15)]

    protected Animator ani;
    EffectManager effectmanager;
    float distance;

    GameManager gameManager;
    public GameObject boss;
    public Character[] characters;
   
    Transform currentTarget;
    
    public GameObject point;

    private float startPointX;
    private float startPointY;
    private float charPointX;
    private float charPointY;
    private GameObject arrowSelector;
    private GameObject arrowStart;
    private GameObject arrowEnd;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Quaternion arrowAngle;

    public Vector3 curPosition;
    public CharacterStats stat;
    public CharacterState charState;
    private Vector2 startPosition;

    private LineRenderer line;
    private int curStart = 0, curEnd = 1;
    public GameObject lineProto;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        GetStat();
        ani = this.GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        charState = CharacterState.idle;
        boss = gameManager.GetBoss();
        characters = gameManager.GetChars();
        effectmanager = gameManager.effectManager;
        
        line = transform.GetComponent<LineRenderer>();
        arrowSelector = Resources.Load("Prefabs/arrowSelector") as GameObject;
        arrowEnd = Resources.Load("Prefabs/arrowHead") as GameObject;

        arrowStart = Instantiate(arrowSelector, new Vector2(0, 0), Quaternion.identity);
        point = Instantiate(arrowEnd, new Vector2(0, 0), Quaternion.identity);
        arrowStart.SetActive(false);
        point.SetActive(false);

        

        StartCoroutine(AttackMotion(1 / this.stat.attackSpeed));
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        distance = Vector2.Distance(boss.transform.position, transform.position);
        Moving();
        Attack();
        CharFlipping();

        if (this.ani.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            ani.SetBool("attack", false);
        }
    }

    void OnMouseDown()
    {
        if (this.GetComponent<CircleCollider2D>()){
            //움직이는 중이면 멈춤
            if (charState != CharacterState.idle) charState = CharacterState.idle;

            //마우스 다운 캐릭터 발 밑 좌표 x, y
            startPointX = this.transform.gameObject.GetComponent<CapsuleCollider2D>().transform.position.x;
            startPointY = this.transform.gameObject.GetComponent<CapsuleCollider2D>().transform.position.y - (float)0.9;

            screenPoint = Camera.main.WorldToScreenPoint(transform.position);

            //화살표 시작
            arrowStart.SetActive(true);
            arrowStart.transform.position = new Vector2(startPointX, startPointY);
            startPosition = Vector2.zero;
            StartLine();
        }
        

    }

    private void OnMouseDrag()
    {
        //움직이는 중이면 멈춤
        if (charState != CharacterState.idle) charState = CharacterState.idle;

        //마우스 다운 캐릭터 중앙 좌표 x, y
        charPointX = this.transform.gameObject.GetComponent<CircleCollider2D>().transform.position.x;
        charPointY = this.transform.gameObject.GetComponent<CircleCollider2D>().transform.position.y;

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        //마우스 회전각
        arrowAngle = Quaternion.Euler(0, 0, GetAngle(new Vector3(charPointX, charPointY, screenPoint.z),
            Camera.main.ScreenToWorldPoint(curScreenPoint)) + 180);

        //화살표 끝
        point.SetActive(true);
        point.transform.rotation = arrowAngle;
        point.transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint);
        

        //화살표 몸통(라인렌더러)
        Vector3 pointPos = new Vector2(point.transform.position.x , point.transform.position.y);
        line.SetPosition(curEnd, pointPos);
        line.sortingOrder = 1;
        line.sortingLayerName = "UI";
    }

    private void OnMouseUp()
    {
        Debug.Log("Clicked");
        charState = CharacterState.move;

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        
        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

        //화살표 삭제
        arrowStart.SetActive(false);
        point.SetActive(false);
        EndLine();
    }

    public static float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    //타겟기준 좌우판정
    public void CharFlipping()
    {
        if (currentTarget)
        {
            if (currentTarget.position.x < this.transform.position.x)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (charState == CharacterState.move)
        {
            if(curPosition.x < this.transform.position.x)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
 
    public void Attack()
    {

        if (charState != CharacterState.move && stat.attackRangeRadius > distance) //in attack range
        {
            charState = CharacterState.attack;
        }

        else if (charState != CharacterState.move && stat.awareRangeRadius > distance) //in aware range
        {
            charState = CharacterState.chase;
        }

    }
 
    //---------------------캐릭터이동--------------------------//
    void Moving()
    {
        if (charState == CharacterState.move)
        {
            transform.position = Vector2.MoveTowards(transform.position, curPosition, stat.moveSpeed * Time.deltaTime);
            currentTarget = null;
            ani.SetBool("walk", true);

            if (transform.position == curPosition) //강제 이동 끝나면 idle상태
            {
                charState = CharacterState.idle;
                ani.SetBool("walk", false);
            }
        }

        if (charState == CharacterState.chase)
        {
            transform.position = Vector2.MoveTowards(transform.position, boss.transform.position, stat.moveSpeed * Time.deltaTime);
            ani.SetBool("walk", true);
        }

        if (charState == CharacterState.attack)
        {
            /*--------------------------------------수정 필요(쫄처리) ----------------------------------------*/
            currentTarget = boss.transform;
        }
    }
    
    private void StartLine()
    {
        line.enabled = true;
        line.SetWidth(0.5f, 0.5f);
        Vector3 pointPos = new Vector3(transform.position.x, transform.position.y, -0.1f);
        line.SetPosition(0, pointPos);
    }

    private void EndLine()
    {
        line.enabled = false;
        
    }

    private void NormalAttack()
    {
        if (currentTarget != null)
        {
            effectmanager.BlastOnPosition(currentTarget.position, 0.7f);
            currentTarget.gameObject.GetComponent<Enemy>().stat.hp -= this.stat.attack;
        }
        else
            Debug.Log("attack fail");
    }

    public IEnumerator AttackMotion(float attackCycle)
    {

        if (charState == CharacterState.attack)
        {
            ani.SetBool("attack", true);
            
        }
        else
        {
            ani.SetBool("attack", false);
        }

        yield return new WaitForSeconds(attackCycle);

        StartCoroutine(AttackMotion(attackCycle));

    }

    private void GetStat()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Tables/characterData");

        for(int i = 0; i < data.Count; i++)
        {
            if (data[i]["name"].Equals(this.name))
            {
                this.stat.level = (int)data[i]["level"];
                this.stat.hp = (int)data[i]["max_hp"];
                this.stat.armor = (int)data[i]["dp"];
                this.stat.attack = (int)data[i]["atk"];
                this.stat.attackSpeed = (float)data[i]["atkSpeed"];
                this.stat.attackRangeRadius = (int)data[i]["attackRange"];
                this.stat.awareRangeRadius = (int)data[i]["awareRange"];
                this.stat.moveSpeed = (int)data[i]["moveSpeed"];
            }
        }    
    }
}
