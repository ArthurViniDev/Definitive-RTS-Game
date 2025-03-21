using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] public List<Unit> units = new List<Unit>();
    [SerializeField] public List<Unit> selectedUnits = new List<Unit>();

    [SerializeField] private LayerMask targetsLayer;
    [SerializeField] private LayerMask unitsLayer;

    [SerializeField] private GameObject unitDestiantionMark;

    public static UnitManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach (Unit unit in FindObjectsOfType<Unit>())
            units.Add(unit);

    }
    void Update()
    {
        if (selectedUnits.Count > 0) UnitSetDestination();

        UnitSelect();
    }

    private void UnitSelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetButtonDown("Fire1") && !(Input.GetButton("Fire3")))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, unitsLayer))
            {
                var unitInfo = hit.collider.GetComponent<Unit>();
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
                var unitInfo = hit.collider.GetComponent<Unit>();
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
        if (Input.GetButtonDown("Fire2"))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetsLayer))
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    foreach (Unit unit in selectedUnits)
                    {
                        unit.enemyTarget = hit.collider.gameObject;
                    }
                }
                else
                {
                    foreach (Unit unit in selectedUnits)
                    {
                        unit.enemyTarget = null;
                    }
                }
                foreach (Unit unit in selectedUnits)
                {
                    unit.agent.SetDestination(hit.point);
                    Instantiate(unitDestiantionMark, hit.point, Quaternion.identity);
                }
            }
        }
    }
}
