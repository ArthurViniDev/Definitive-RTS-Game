using UnityEngine;

public class BonfireUI : MonoBehaviour
{
    [SerializeField] private GameObject bonFireUI;
    [SerializeField] private GameObject SpearmanPrefab;
    [SerializeField] private Transform bonfirePosition;
    public void CloseWindow() => this.gameObject.SetActive(false);

    public void TrainTroop()
    {
        NewUnitManager.instance.TrainTrops(UnitType.Spearman, SpearmanPrefab, bonfirePosition);
    }
}
