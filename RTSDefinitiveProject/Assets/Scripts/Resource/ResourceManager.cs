using UnityEngine;

public enum ResourceType { Wood, Stone }

public class ResourceManager : MonoBehaviour
{
    public int resourceCount = 5;
    public ResourceType resourceType;

    public void TakeResource(int amount) => resourceCount -= amount;

    private void Update()
    {
        if (resourceCount <= 0) Destroy(gameObject);
    }
}
