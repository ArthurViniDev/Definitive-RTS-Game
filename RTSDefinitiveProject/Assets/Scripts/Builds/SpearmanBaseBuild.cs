using UnityEngine;

public class SpearmanBaseBuild : BuildManager
{
    private void OnMouseOver()
    {
        Debug.Log("OnMouseOver()");
        if (Input.GetMouseButtonDown(1) && buildUI.activeSelf == false)
        {
            OpenBuildWindow();
        }
        else if (Input.GetMouseButtonDown(1) && buildUI.activeSelf == true)
        {
            CloseBuildWindow();
        }
    }
}
