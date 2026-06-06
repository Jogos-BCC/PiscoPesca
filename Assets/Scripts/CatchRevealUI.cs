using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatchRevealUI : MonoBehaviour
{
    public GameObject revealPanel;
    public TextMeshProUGUI fishNameText;
    public TextMeshProUGUI rarityText;
    public TextMeshProUGUI descriptionText;
    public Transform modelSpawnPoint;
    public float rotationSpeed = 90f;

    private GameObject currentModel;
    private FishItem currentFish;
    private bool isShowing = false;

    void Update()
    {
        if (isShowing && currentModel != null)
            currentModel.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        if (isShowing && Input.GetMouseButtonDown(0))
            HideReveal();
    }

    public void ShowCatch(FishItem fish, GameObject modelPrefab)
    {
        currentFish = fish;
        isShowing = true;
        revealPanel.SetActive(true);

        fishNameText.text = fish.fishName;
        rarityText.text = fish.rarity.ToString();
        descriptionText.text = fish.description;

        if (currentModel != null) Destroy(currentModel);
        currentModel = Instantiate(modelPrefab, modelSpawnPoint.position, Quaternion.identity);

        foreach (var renderer in currentModel.GetComponentsInChildren<Renderer>())
            renderer.gameObject.layer = LayerMask.NameToLayer("Showcase");
    }

    void HideReveal()
    {
        isShowing = false;
        revealPanel.SetActive(false);
        IndexManager.Instance.RegisterCatch(currentFish);
        FindObjectOfType<IndexUI>()?.RefreshIfOpen();

        if (currentModel != null) Destroy(currentModel);
    }
}