/// <summary>
/// 포이즌 - 패시브
/// 뒤에서 공격할 시 추뎀 
/// </summary>
[SkillJobKind(SkillJobKindAttribute.eAttribute.Warrior)]
public class PoisonPassiveSkill : PassiveSkillBase
{
    private float preDamage = 0.0f;
    public override void OnUpdate()
    {
        base.OnUpdate();
        //CharPhysicDamage = preDamage;
        if ( hero.GetActorBack() ) // TODO 뒤일 때 데미지 증가, 아니면 되돌리기
        {

        }
    }
    //sudo targetObject.보는방향(flipX) != 나.보는방향 
    //나.stat.damage += 데미지좀 올린다
}
