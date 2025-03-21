using UnityEngine;
using UnityEngine.AI;

public class Unit : UnitCombatManager
{
    [SerializeField] private GameObject unitSelectedMark;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask resourceMask;
    [SerializeField] private Vector3 sphereOffset;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator unitAnimator;

    public GameObject enemyTarget;

    public string currentStateName = "none";

    IUnitState walkState;
    IUnitState combatState;

    UnitCombatState classCombatState;

    public bool endAttackState;

    void Awake()
    {
        walkState = new UnitWalkState();
        combatState = new UnitCombatState();
        agent = GetComponent<NavMeshAgent>();
        unitAnimator = GetComponent<Animator>();
        classCombatState = combatState as UnitCombatState;
    }

    private void Update()
    {
        bool isUnitSelected = UnitManager.instance.selectedUnits.Contains(this);
        unitSelectedMark.SetActive(isUnitSelected);
        HandleWalkState();

        Collider[] collidingEnemies = IsCollidingWithEnemy();
        HandleAttackState(collidingEnemies);
        UnitStateMachine(collidingEnemies);
    }

    private void UnitStateMachine(Collider[] collidingEnemies)
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

    void EndAttack()
    {
        endAttackState = !endAttackState;
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

    private void HandleWalkState()
    {
        if (agent.velocity.magnitude > 0)
        {
            currentStateName = "Unit is walking";
            walkState.EnterState(unitAnimator);
        }
        else
        {
            currentStateName = "none";
            walkState.ExitState(unitAnimator);
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
}
