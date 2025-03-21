using System.Collections.Generic;
using UnityEngine;

public enum UnitType { Spearman }

public class NewUnitManager : MonoBehaviour
{
    [SerializeField] public List<BaseUnit> selectedUnits = new List<BaseUnit>();
    [SerializeField] public List<BaseUnit> units = new List<BaseUnit>();

    [SerializeField] private LayerMask targetsLayer;
    [SerializeField] private LayerMask unitsLayer;

    [SerializeField] private GameObject unitDestiantionMark;

    [HideInInspector] public bool canInteractWithUnit = true;

    public static NewUnitManager instance;

    private void Awake()
    {
        instance = this;
        units.AddRange(FindObjectsOfType<BaseUnit>());
    }
    void Update()
    {
        if (!canInteractWithUnit) return;
        if (selectedUnits.Count > 0) UnitSetDestination();

        UnitSelect();
    }

    private void UnitSelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetButtonDown("Fire1") && !Input.GetButton("Fire3"))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, unitsLayer))
            {
                var unitInfo = hit.collider.GetComponent<BaseUnit>();
                if (selectedUnits.Contains(unitInfo))
                {
                    selectedUnits.Remove(unitInfo);
                    Debug.Log("unit removed");
                }
                else
                {
                    Debug.Log("Unit selected");
                    selectedUnits.Clear();
                    selectedUnits.Add(unitInfo);
                }
            }
            else
            {
                selectedUnits.Clear();
            }
        }
        else if (Input.GetButton("Fire3") && Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, unitsLayer))
            {
                var unitInfo = hit.collider.GetComponent<BaseUnit>();
                Debug.Log("Unit multi selected");
                if (selectedUnits.Contains(unitInfo))
                    selectedUnits.Remove(unitInfo);
                else
                    selectedUnits.Add(unitInfo);
            }
        }
    }

    private void UnitSetDestination()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetButtonDown("Fire2") && canInteractWithUnit)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetsLayer))
            {
                // if (hit.collider.gameObject.layer == 8)
                // {
                //     foreach (BaseUnit unit in selectedUnits)
                //     {
                //         unit.enemyTarget = hit.collider.gameObject;
                //     }
                // }
                // else
                // {
                //     foreach (Unit unit in selectedUnits)
                //     {
                //         unit.enemyTarget = null;
                //     }
                // }
                foreach (BaseUnit unit in selectedUnits)
                {
                    unit.agent.SetDestination(hit.point);
                    Instantiate(unitDestiantionMark, hit.point, Quaternion.identity);
                }
            }
        }
    }

    public void TrainTrops(UnitType unitType, GameObject unitPrefab, Transform spawnPoint)
    {
        switch (unitType)
        {
            case UnitType.Spearman:
                Instantiate(unitPrefab, spawnPoint.position, spawnPoint.rotation);
                break;
        }
    }
}
