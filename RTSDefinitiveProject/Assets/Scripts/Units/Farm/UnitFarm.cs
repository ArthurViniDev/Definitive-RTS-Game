using UnityEngine;

public class UnitFarm : BaseUnit
{
    [SerializeField] private LayerMask resourceLayer;
    [SerializeField] private Vector3 sphereOffset;

    [SerializeField] private int woodsCollected;
    [SerializeField] private int stonesCollected;

    //private UnitFarmState farmState = new UnitFarmState();
    public UnitFarmState farmState;

    public override void Update()
    {
        base.Update();

        HandleFarmState();

        switch (currentStateName)
        {
            case "Unit is walking":
                walkState.UpdateState(unitAnimator);
                break;
            case "Unit is farming":
                farmState.UpdateState(this.currentStateName);
                break;
            default: break;
        }
    }

    private void HandleFarmState()
    {
        Collider[] resourcesCollidingCount = IsCollidingWithResource();
        if (resourcesCollidingCount.Length > 0)
        {
            currentStateName = "Unit is farming";
            farmState.EnterState(unitAnimator, IsCollidingWithResource());
        }
        else
        {
            farmState.ExitState(unitAnimator);
        }
    }

    private Collider[] IsCollidingWithResource()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + sphereOffset, 1f, resourceLayer);
        return colliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position + sphereOffset, 1f);
    }

    public void CollectWoods(int totalResources) => woodsCollected += totalResources;
    public void CollectStones(int totalResources) => stonesCollected += totalResources;
}
