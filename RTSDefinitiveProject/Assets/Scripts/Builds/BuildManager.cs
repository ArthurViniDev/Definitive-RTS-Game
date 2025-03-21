using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public UnitType unitType;

    [SerializeField] protected GameObject buildUI;
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Transform spawnPoint;

    private PlayerManager playerManager;

    public virtual void OpenBuildWindow()
    {
        buildUI.SetActive(true);
        NewUnitManager.instance.canInteractWithUnit = false;
    }

    public virtual void CloseBuildWindow()
    {
        buildUI.SetActive(false);
        NewUnitManager.instance.canInteractWithUnit = true;
    }

    public void CreateUnit()
    {
        if (playerManager.troopsCount >= playerManager.populationLimit) return;
        var unitCreated = Instantiate(unitPrefab, spawnPoint.position, spawnPoint.rotation);
        NewUnitManager.instance.units.Add(unitCreated.GetComponent<BaseUnit>());
    }

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        if (buildUI.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) CloseBuildWindow();
        }
    }
}
