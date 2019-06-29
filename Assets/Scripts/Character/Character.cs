using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

/*
[Serializable]
public struct CharacterStats
{
    public string job;
    public string synergy;
    public bool isSelect;

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
*/

/// <summary>
/// 스킬을 만드는 타이밍
/// </summary>
public class SkillStateData
{
    public bool hasAnimation = false; // 애니메이션 특성 동작에 스킬을 발사해야 할 수 있어서 임시로 만들어둠
    public eSkillState skillState = eSkillState.None;
    public Action skillMakeCallback = null;

    public void Clear()
    {
        hasAnimation = false;
        skillState = eSkillState.None;
        skillMakeCallback = null;
    }
}

/*
public enum CharacterState
{
    idle, attack, move, cast, chase, count, dead
}
*/

public class Character : Actor
{
    [Header("Use Skill")]
    public eSkill skill_first;
    public eSkill skill_second;
    [Space(15)]

    protected Animator ani;
    EffectManager effectmanager;
    DungeonManager dungeonManager;
    InputListener _inputListener;
    AStarManager aStarManager;
    float distance;

    GameManager gameManager;
    public GameObject boss;
    public List<Character> characters;
    protected int _idx = 0; //캐릭터 고유 index (게임매니저의 인덱스랑 싱크가 맞아야 함)
    public int IDX { get { return _idx; } private set { } }
    public void SetIDX(int val) { _idx = val; }

    public GameObject point;
    private GameObject aStarTarget;
    List<Node> pathNode = new List<Node>();

    private bool isCollidingWithPlayer;
    private bool isCollidingWithEnemy;
    private GameObject arrowSelector;
    private GameObject arrowStart;
    private GameObject arrowEnd;
    private GameObject selectiveObject;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Quaternion arrowAngle;
    private AStarPathfinding aStarPathfinding;  

    public Vector3 curPosition;
    //public CharacterStats stat;
    //public ActorState charState;
    private Vector2 startPosition;

    private LineRenderer line;
    public GameObject lineProto;

    //이걸로 스킬 만드는 타이밍 조절
    public SkillStateData skillStateData = new SkillStateData();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        GetStat();
        ani = this.GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _inputListener = GameManager.Instance.GetComponent<InputListener>();
        aStarManager = GameObject.Find("MapGrid").GetComponent<AStarManager>();
        aStarTarget = new GameObject(); // empty GameObject for Astar pathfinding
        state = ActorState.idle;

        boss = gameManager.GetBoss();
        characters = gameManager.GetChars();

        effectmanager = EffectManager.Instance;
        dungeonManager = DungeonManager.Instance;

        aStarPathfinding = GetComponent<AStarPathfinding>(); 
        aStarPathfinding.grid = aStarManager.AStarGrid;
        
        selectiveObject = transform.Find("isSelect").gameObject;

        StartCoroutine(AttackMotion(1 / this.stat.attackSpeed));

        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (currentTarget != null)
        {
            distance = Vector2.Distance(currentTarget.transform.position, transform.position);
        }
        if (selectiveObject)
        {
            selectiveObject.SetActive(stat.isSelect); // Select된 캐릭터 밑 원
        }
        
        MoveAndAttack();
        Moving();
        Attacking();
        CharFlipping();
        OrderLayer();
        Death();
        

        if (this.ani.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            ani.SetBool("attack", false);
        }

        //----- 스킬 부르는 예시 캐스팅 바 다 차거나 애니메이션 끝날 때 이렇게하면 됨
        if (skillStateData.skillState == eSkillState.NonTarget_Cast)
        {
            Debug.Log("캐스트 끝났을 때 시점");
            skillStateData.skillMakeCallback.SafeInvoke();
            skillStateData.Clear();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) //목적지 근처 Colliding 판정위하여
    {
        if(collision.collider.tag == "Player")
        {
            isCollidingWithPlayer = true;
        }

        if(collision.collider.tag == "Enemy")
        {
            isCollidingWithEnemy = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) //목적지 근처 Colliding 판정위하여
    {
        if (collision.collider.tag == "Player")
        {
            isCollidingWithPlayer = false;
        }
        if(collision.collider.tag == "Enemy")
        {
            isCollidingWithEnemy = false;
        }
    }

    void Death()
    {
        if(stat.hp < 0)
        {
            state = ActorState.dead; //인풋제외 위하여 따로 처리
        }
        if(state == ActorState.dead)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("tomb");
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<Animator>().enabled = false;
        }

    }

    Transform GetClosestEnemy(List<Enemy> enemies) // 가장 가까운 캐릭터를 찾음
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        for (int i = 0; i < enemies.Count; i++)
        {
            float dist = Vector3.Distance(enemies[i].transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = enemies[i].transform;
                minDist = dist;
            }
        }
        return tMin;
    }


    void MoveAndAttack()
    {
        if (_inputListener.selectedUnits.Contains(this))
        {
            if(state != ActorState.dead) // 안죽은 캐릭터만
            {
                if (Input.GetMouseButtonUp(1))
                {
                    state = ActorState.move;
                    screenPoint = Camera.main.WorldToScreenPoint(transform.position);
                    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                    curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

                    aStarTarget.transform.position = curPosition;
                    pathNode = aStarPathfinding.FindPath(transform.position, curPosition); //찾은 길 노드배열
                }

                if (Input.GetKeyUp(KeyCode.S))
                {
                    state = ActorState.idle;
                }

                if (Input.GetKeyUp(KeyCode.A))
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }


    IEnumerator Attack()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0)); // wait for mouse button

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray2D ray = new Ray2D(wp, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider == null) // 어택땅
        {
            List<Enemy> enemies = new List<Enemy>();
            enemies = dungeonManager.EnemiesGroup[dungeonManager.GetCurrentDungeonRoom()];
            currentTarget = GetClosestEnemy(enemies);

            if (enemies.Count == 0)
            {
                curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                state = ActorState.move;
            }

        }
        else if (hit.collider.tag == "Enemy") // 강제어택
        {
            currentTarget = hit.collider.transform;
        }
    }


    //---------------------캐릭터이동--------------------------//
    void Moving()
    {
        if (state == ActorState.move)
        {
            transform.position = Vector2.MoveTowards(transform.position, aStarPathfinding.WorldPointFromNode(pathNode[0]), stat.moveSpeed * Time.deltaTime);
            if(pathNode != null)
            {
                if (transform.position == aStarPathfinding.WorldPointFromNode(pathNode[0]))
                {
                    pathNode.RemoveAt(0);
                }
            }

            currentTarget = null; /////////////////수정해야함 ... 이거때문에 이동할때 공격하지않음
            ani.SetBool("walk", true);

            if (pathNode.Count == 0)
            {
                state = ActorState.idle;
                ani.SetBool("walk", false);
                curPosition = transform.position;
            }

            if (Vector2.Distance(transform.position, curPosition) < 2 && isCollidingWithPlayer) // 목적지 근처에서 Player끼리 Colliding 중이면 멈춤
            {
                state = ActorState.idle;
                ani.SetBool("walk", false);
                curPosition = transform.position;
            }

            if (isCollidingWithEnemy || isCollidingWithPlayer) // 콜리더 충돌중이면 새로운 길 찾음
            {
                //pathNode[0].isSolid = true;
                pathNode = aStarPathfinding.FindPath(transform.position, curPosition);
            }

        }

        if (state == ActorState.chase)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, stat.moveSpeed * Time.deltaTime);
            ani.SetBool("walk", true);
        }
    }

    public void Attacking()
    {
        if (currentTarget != null)
        {
            if (stat.attackRangeRadius > distance) //in attack range
            {
                state = ActorState.attack;
            }
            /*
            else if (charState != CharacterState.hold && charState != CharacterState.move && stat.awareRangeRadius > distance) //in aware range
            {
                charState = CharacterState.chase;
            }
            */
            else
            {
                state = ActorState.chase;
            }
        }
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

        if (state == ActorState.move)
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

    //캐릭터 겹칠때 레이어순서
    public void OrderLayer()
    {
        characters = characters.OrderBy(obj => obj.transform.position.y).ToList();
        for(int i = 0; i<characters.Count; i++)
        {
            characters[i].GetComponent<SpriteRenderer>().sortingOrder = 10 - i;
        }
    }

    private void NormalAttack()
    {
        if (currentTarget != null)
        {
            effectmanager.BlastOnPosition(currentTarget.position, 0.7f);
            currentTarget.gameObject.GetComponent<Enemy>().TakeDamage(this.CharPhysicDamage);
        }
        else
            Debug.Log("attack fail");
    }

    public IEnumerator AttackMotion(float attackCycle)
    {

        if (state == ActorState.attack)
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
            if (this.name.Contains(data[i]["name"].ToString()))
            {
                //this.stat.level = (int)data[i]["level"];
                this.stat.hp = (int)data[i]["max_hp"];
                //this.stat.armor = (int)data[i]["dp"];
                //this.stat.attack = (int)data[i]["atk"];
                this.stat.attackSpeed = (float)data[i]["atkSpeed"];
                this.stat.attackRangeRadius = (int)data[i]["attackRange"];
                this.stat.awareRangeRadius = (int)data[i]["awareRange"];
                this.stat.moveSpeed = (int)data[i]["moveSpeed"];
            }
        }    
    }
}
