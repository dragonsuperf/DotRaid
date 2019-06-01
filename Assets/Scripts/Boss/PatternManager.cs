using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    Queue<Pattern> patternQueue = new Queue<Pattern>();
    Animator ani = null;
    Pattern currentPattern;
    bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForInit());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause) return;

        if(currentPattern == null)
            if (!GetNextPattern())
                return;

        currentPattern.Update(Time.deltaTime);

        if (!currentPattern.IsRunning) // 현재 패턴이 끝났다면
        {
            EndPattern();
            GetNextPattern();
        }
    }

    bool GetNextPattern()
    {
        if (patternQueue.Count > 0)
        {
            currentPattern = patternQueue.Dequeue(); // 다음 패턴을 꺼내옴
            StartPattern();
            return true;
        }
        else
        {
            //ani.SetBool(currentPattern.Name, false);
            currentPattern = null;
            return false;
        }
    }

    void StartPattern()
    { 
        ani.SetBool(currentPattern.Name, true);
        ani.speed = currentPattern.Speed;
        //Debug.Log("Play : " + currentPattern.Name);
    }

    public void EndPattern()
    {
        currentPattern.IsRunning = false;
        ani.SetBool(currentPattern.Name, false);
        
        ani.speed = 1.0f;
        StartCoroutine(WaitForCD(currentPattern));
        //Debug.Log("End : " + currentPattern.Name);
    }

    public void AddPattern(string name, float duration, float cd)
    {
        StartCoroutine(WaitForCD(new Pattern(name, duration, cd)));
    }

    public void AddPattern(string name, float duration, float cd, float speed)
    {
        StartCoroutine(WaitForCD(new Pattern(name, duration, cd, speed)));
        //patternQueue.Enqueue(new Pattern(name, duration, cd, speed));
    }

    public void SkipCurrentPattern()
    {
        if (currentPattern != null)
        {
            EndPattern();
            GetNextPattern();
        }
    }

    public int GetPatternCount()
    {
        return patternQueue.Count;
    }

    public string GetCurrentPattern()
    {
        return currentPattern.Name;
    }

    public void SetAnimator(Animator ani)
    {
        this.ani = ani;
    }

    public bool IsRunning()
    {
        if (currentPattern == null) return false;
        return currentPattern.IsRunning;
    }

    IEnumerator WaitForInit()
    {
        while(ani == null)
        {
            yield return null;
        }
    }

    IEnumerator WaitForCD(Pattern p)
    {
        yield return new WaitForSeconds(p.Cooldown);

        p.Reset();
        patternQueue.Enqueue(p);
    }
    
    IEnumerator WaitForCD(Pattern p, float cd)
    {
        yield return new WaitForSeconds(cd);

        p.Reset();
        patternQueue.Enqueue(p);
    }

    IEnumerator CheckPatternState()
    {
        while (!ani.GetNextAnimatorStateInfo(0).IsName(currentPattern.Name))
        {
            //전환 중
            yield return null;
        }

        while (ani.GetNextAnimatorStateInfo(0).normalizedTime < 0.95f)
        {
            //플레이 중
            yield return null;
        }

        //플레이 후
    }

    public void Pause()
    {
        isPause = true;
        ani.speed = 0;
    }

    public void Resume()
    {
        isPause = false;
        ani.speed = currentPattern != null ? currentPattern.Speed : 1;
    }
}
