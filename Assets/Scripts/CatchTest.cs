using UnityEngine;

public class CatchTest : MonoBehaviour
{
    public FishItem fishItem;
    public GameObject fishModelPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            FindObjectOfType<CatchRevealUI>().ShowCatch(fishItem, fishModelPrefab);
    }
}