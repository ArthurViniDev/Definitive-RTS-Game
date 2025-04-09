using UnityEngine;

public class UnitFarm : MonoBehaviour
{
    [SerializeField] private LayerMask resourceLayer;
    [SerializeField] private Vector3 sphereOffset;

    [SerializeField] private int woodsCollected;
    [SerializeField] private int stonesCollected;

    //private UnitFarmState farmState = new UnitFarmState();
    public UnitFarmState farmState;

    public virtual void Update()
    {
        HandleFarmState();
    }

    protected virtual void HandleFarmState()
    {
        
    }

    public void CollectWoods(int totalResources) => woodsCollected += totalResources;
    public void CollectStones(int totalResources) => stonesCollected += totalResources;
}
