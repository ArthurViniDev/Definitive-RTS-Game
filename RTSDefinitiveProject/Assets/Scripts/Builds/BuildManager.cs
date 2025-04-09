using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public UnitType unitType;

    [SerializeField] protected GameObject buildUI;
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Transform spawnPoint;

    [Header("Units Build Settings")]
    [SerializeField] private int unitsCount;

    public virtual void OpenBuildWindow()
    {
        buildUI.SetActive(true);
        NewUnitManager.instance.canInteractWithUnit = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !buildUI.activeSelf) OpenBuildWindow();
    }

    public virtual void CloseBuildWindow()
    {
        buildUI.SetActive(false);
        NewUnitManager.instance.canInteractWithUnit = true;
    }

    private void Update()
    {
        if (buildUI.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) CloseBuildWindow();
        }
    }
}
