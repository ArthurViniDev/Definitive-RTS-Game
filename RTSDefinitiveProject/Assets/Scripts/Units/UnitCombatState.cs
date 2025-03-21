using UnityEngine;

public class UnitCombatState : IUnitState
{
    private bool enterState = false;
    private bool canAttack = true;

    public void EnterState(Animator unitAnimator)
    {
        if (enterState) return;
        Debug.Log("Unit is attacking");
        unitAnimator.SetBool("isWalking", false);
        unitAnimator.SetBool("isAttacking", true);
        enterState = true;
    }
    public void UpdateState(Collider[] targetGameObject, int unitDamage, bool endAttackState)
    {
        var enemyTargetComponent = targetGameObject[0].GetComponent<UnitCombatManager>();
        if (enemyTargetComponent.life <= 0) enemyTargetComponent.Die();
        else if (canAttack && endAttackState)
        {
            canAttack = false;
            enemyTargetComponent.TakeDamage(unitDamage);
        }
        else if (!canAttack && !endAttackState) canAttack = true;
    }

    public void ExitState(Animator unitAnimator)
    {
        if (!enterState) return;
        Debug.Log("Unit is not attacking");
        unitAnimator.SetBool("isAttacking", false);
        enterState = false;
    }
}
