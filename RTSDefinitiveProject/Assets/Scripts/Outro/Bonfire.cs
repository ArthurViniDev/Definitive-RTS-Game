using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private GameObject bonFireUI;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            bonFireUI.SetActive(true);
        }
    }
}
