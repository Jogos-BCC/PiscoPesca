using UnityEngine;

public class CatchTest : MonoBehaviour
{
    [SerializeField] private CatchRevealUI revealUI;
    public void UnlockFish(FishItem fishItem)
    {
        Debug.Log($"{fishItem.modelPrefab}");
        revealUI.ShowCatch(fishItem, fishItem.modelPrefab);
    }
}