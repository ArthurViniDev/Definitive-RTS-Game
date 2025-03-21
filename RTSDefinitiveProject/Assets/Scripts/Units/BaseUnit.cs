using UnityEngine;
using UnityEngine.AI;

public class BaseUnit : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator unitAnimator;

    public IUnitState walkState;
    public GameObject selectedMark;


    public string currentStateName = "none";
    public int life = 100;
    public int damage = 10;

    void Awake()
    {
        unitAnimator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        walkState = new UnitWalkState();
    }

    public virtual void Update()
    {
        bool isUnitSelected = NewUnitManager.instance.selectedUnits.Contains(this);
        selectedMark.SetActive(isUnitSelected);

        HandleWalkState();
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
}
