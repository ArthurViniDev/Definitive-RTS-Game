using UnityEngine;

public class UnitWalkState : IUnitState
{
    private bool enterState = false;
    public void EnterState(Animator unitAnimator)
    {
        if (enterState) return;
        //Debug.Log("Unit is walking");
        unitAnimator.SetBool("isWalking", true);
        enterState = true;
    }

    public void UpdateState(Animator unitAnimator)
    {
        //Debug.Log("Unit is walking");
    }

    public void ExitState(Animator unitAnimator)
    {
        if (!enterState) return;
        //Debug.Log("Unit is not walking");
        unitAnimator.SetBool("isWalking", false);
        enterState = false;
    }
}
