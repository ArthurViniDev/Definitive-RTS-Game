using System.Collections;
using UnityEngine;

public class UnitFarmState : MonoBehaviour, IUnitState
{
    private bool enterState = false;
    private bool isFarming = false;
    private int totalResources;

    public ResourceType currentResourceType;
    private UnitFarm unitFarm;
    void Awake()
    {
        unitFarm = GetComponentInParent<UnitFarm>();
    }
    public void EnterState(Animator farmAnimator, Collider[] resourceGameObject)
    {
        if (enterState) return;
        Debug.Log("Unit is farming-------------------");
        StartCoroutine(Farm(resourceGameObject));
        farmAnimator.SetBool("isFarming", true);
        enterState = true;
        currentResourceType = resourceGameObject[0].GetComponent<ResourceManager>().resourceType;
    }

    public void UpdateState(string currentStateName)
    {
        isFarming = currentStateName == "Unit is farming";
    }

    private IEnumerator Farm(Collider[] resourceGameObject)
    {
        int resourceCount = resourceGameObject[0].GetComponent<ResourceManager>().resourceCount;
        do
        {
            yield return new WaitForSeconds(1.35f);
            if (!isFarming) yield break;
            totalResources++;
            resourceCount--;
            if (resourceGameObject == null) yield break;
            TakeResource(resourceGameObject);
        }
        while (resourceCount > 0);
        yield break;
    }

    public void TakeResource(Collider[] resourceGameObject)
    {
        var resourceManager = resourceGameObject[0].GetComponent<ResourceManager>();
        resourceManager.TakeResource(1);
    }

    public void ExitState(Animator farmAnimator)
    {
        isFarming = false;
        StopCoroutine(Farm(null));
        farmAnimator.SetBool("isFarming", false);
        if (enterState == false) return;
        switch (currentResourceType)
        {
            case ResourceType.Wood:
                unitFarm.CollectWoods(totalResources);
                break;
            case ResourceType.Stone:
                unitFarm.CollectStones(totalResources);
                break;
        }
        totalResources = 0;
        enterState = false;
    }
}
