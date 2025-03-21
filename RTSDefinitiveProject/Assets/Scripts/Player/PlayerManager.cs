using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public int troopsCount = 0;
    [SerializeField] public int populationLimit;
    [SerializeField] private TextMeshProUGUI troopsCountText;
    [SerializeField] private TextMeshProUGUI populationLimitText;
    void Start()
    {
        troopsCount += NewUnitManager.instance.units.Count;
    }

    void Update()
    {
        if (troopsCount != NewUnitManager.instance.units.Count) troopsCount = NewUnitManager.instance.units.Count;
        troopsCountText.text = "Current troops: " + troopsCount.ToString();
        populationLimitText.text = "Population limit: " + populationLimit.ToString();
    }
}
