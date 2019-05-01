using UnityEngine;

public class Skill : MonoBehaviour
{
    //스킬 id임
    protected int _idx = 0;
    public int IDX { get { return _idx; } private set { } }
    
    /// <summary>
    /// 초기화임
    /// </summary>
    public virtual void OnSet(int idx)
    {
        _idx = idx;
    }
    /// <summary>
    /// 매니저에서 업데이트로 불림
    /// </summary>
    public virtual void OnUpdate() { }

    /// <summary>
    /// 마지막에 지워질 때 불림
    /// </summary>
    public virtual void OnRemove() { }
}
