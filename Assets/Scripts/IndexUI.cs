using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IndexUI : MonoBehaviour
{
    public GameObject codexPanel;
    public Button closeButton;
    public Transform gridContainer;
    public GameObject fishCardPrefab;
    public TextMeshProUGUI progressText;
    public CanvasGroup codexCanvasGroup;
    public float fadeDuration = 0.25f;
    public KeyCode toggleKey = KeyCode.I;

    [Header("Player Control")]
    public PlayerMovement playerMovement;
    public PlayerCam playerCam;

    private List<FishCardUI> spawnedCards = new List<FishCardUI>();
    private bool isOpen = false;
    private Coroutine fadeCoroutine;

    void Start()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseCodex);

        codexPanel.SetActive(false);
        if (codexCanvasGroup != null)
            codexCanvasGroup.alpha = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            ToggleCodex();
    }

    public void ToggleCodex()
    {
        if (isOpen) CloseCodex();
        else OpenCodex();
    }

    public void OpenCodex()
    {
        isOpen = true;
        codexPanel.SetActive(true);
        PopulateGrid();
        UpdateProgress();

        SetPlayerMovement(false);

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        if (codexCanvasGroup != null)
            fadeCoroutine = StartCoroutine(FadeTo(1f));
    }

    public void CloseCodex()
    {
        isOpen = false;
        SetPlayerMovement(true);

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        if (codexCanvasGroup != null)
            fadeCoroutine = StartCoroutine(FadeTo(0f, deactivateAfter: true));
        else
            codexPanel.SetActive(false);
    }

    void SetPlayerMovement(bool enabled)
    {
        if (playerMovement != null)
            playerMovement.canMove = enabled;

        if (playerCam != null)
            playerCam.canLook = enabled;

        Cursor.lockState = enabled ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !enabled;
    }



    void PopulateGrid()
    {
        foreach (Transform child in gridContainer)
            Destroy(child.gameObject);
        spawnedCards.Clear();

        var manager = IndexManager.Instance;
        if (manager == null)
        {
            return;
        }

        foreach (var fish in manager.allFishItems)
        {
            var cardObj = Instantiate(fishCardPrefab, gridContainer);
            var cardUI = cardObj.GetComponent<FishCardUI>();
            if (cardUI != null)
            {
                bool caught = manager.IsCaught(fish);
                cardUI.Setup(fish, caught);
                spawnedCards.Add(cardUI);
            }
        }
    }

    void UpdateProgress()
    {
        var manager = IndexManager.Instance;
        if (manager == null || progressText == null)
        {
            return;
        }
        progressText.text = $"{manager.GetCaughtCount()} / {manager.GetTotalCount()} pescados";
    }

    public void RefreshIfOpen()
    {
        if (isOpen)
        {
            PopulateGrid();
            UpdateProgress();
        }
    }

    IEnumerator FadeTo(float targetAlpha, bool deactivateAfter = false)
    {
        float startAlpha = codexCanvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            codexCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }

        codexCanvasGroup.alpha = targetAlpha;
        if (deactivateAfter) codexPanel.SetActive(false);
    }
}