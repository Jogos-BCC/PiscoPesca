using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishCardUI : MonoBehaviour
{
    public Image fishImage;
    public Image rarityBorder;
    public TextMeshProUGUI fishNameText;
    public TextMeshProUGUI rarityText;
    public GameObject unknownOverlay;
    public GameObject caughtBadge;

    public Color commonColor = new Color(0.7f, 0.7f, 0.7f);
    public Color uncommonColor = new Color(0.3f, 0.8f, 0.3f);
    public Color rareColor = new Color(0.3f, 0.5f, 1f);
    public Color epicColor = new Color(0.7f, 0.2f, 1f);
    public Color legendaryColor = new Color(1f, 0.7f, 0.1f);

    private FishItem fishData;
    private bool isCaught;

    public void Setup(FishItem fish, bool caught)
    {
        fishData = fish;
        isCaught = caught;

        if (isCaught)
            ShowCaught();
        else
            ShowUnknown();
    }

    void ShowCaught()
    {
        if (fishImage != null)
        {
            fishImage.sprite = fishData.fishSprite;
            fishImage.color = Color.white;
        }

        if (fishNameText != null)
            fishNameText.text = fishData.fishName;

        if (rarityText != null)
            rarityText.text = fishData.rarity.ToString();

        Color rarityColor = GetRarityColor(fishData.rarity);

        if (rarityBorder != null)
        {
            rarityBorder.color = rarityColor;
        }

        if (rarityText != null)
        {
            rarityText.color = rarityColor;
        }

        if (unknownOverlay != null)
        {
            unknownOverlay.SetActive(false);
        }

        if (caughtBadge != null)
        {
            caughtBadge.SetActive(true);
        }
    }

    void ShowUnknown()
    {
        if (fishImage != null)
        {
            fishImage.sprite = fishData.fishSprite;
            fishImage.color = Color.black;
        }

        if (fishNameText != null)
        {
            fishNameText.text = "???";
        }

        if (rarityText != null)
        {
            rarityText.text = "???";
            rarityText.color = Color.gray;
        }

        if (rarityBorder != null)
        {
            rarityBorder.color = new Color(0.3f, 0.3f, 0.3f);
        }

        if (unknownOverlay != null)
        {
            unknownOverlay.SetActive(true);
        }

        if (caughtBadge != null)
        {
            caughtBadge.SetActive(false);
        }
    }

    Color GetRarityColor(Rarity rarity)
    {
        return rarity switch
        {
            Rarity.Comum => commonColor,
            Rarity.Incomum => uncommonColor,
            Rarity.Raro => rareColor,
            Rarity.Épico => epicColor,
            Rarity.Lendário => legendaryColor,
            _ => commonColor
        };
    }
}