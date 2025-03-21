using UnityEngine;

public class UnitCombat : BaseUnit
{
    [SerializeField] private Vector3 sphereOffset;
    [SerializeField] private LayerMask enemyMask;

    public UnitCombatState classCombatState = new UnitCombatState();

    public bool endAttackState = false;

    public override void Update()
    {
        base.Update();

        HandleAttackState(IsCollidingWithEnemy());
        Collider[] collidingEnemies = IsCollidingWithEnemy();
        UnitCombatStateMachine(collidingEnemies);
    }

    private void UnitCombatStateMachine(Collider[] collidingEnemies)
    {
        switch (currentStateName)
        {
            case "Unit is walking":
                walkState.UpdateState(unitAnimator);
                break;
            case "Unit is attacking":
                classCombatState.UpdateState(collidingEnemies, this.damage, endAttackState);
                break;
            default: break;
        }
    }

    private void HandleAttackState(Collider[] collidingEnemies)
    {
        if (collidingEnemies.Length > 0)
        {
            currentStateName = "Unit is attacking";
            classCombatState.EnterState(unitAnimator);
        }
        else
        {
            if (currentStateName == "Unit is attacking")
                currentStateName = "none";
            classCombatState.ExitState(unitAnimator);
        }
    }

    private Collider[] IsCollidingWithEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + sphereOffset, 1f, enemyMask);
        return colliders;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + sphereOffset, 1f);
    }

    public void EndAttack()
    {
        endAttackState = !endAttackState;
    }
}
