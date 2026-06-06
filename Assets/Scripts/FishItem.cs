using UnityEngine;

[CreateAssetMenu(fileName = "FishItem", menuName = "Fishing Codex/Fish Item")]
public class FishItem : ScriptableObject
{
    public string fishName = "Peixe Desconhecido";
    public string description = "Um peixe misterioso...";
    public Sprite fishSprite;
    public Rarity rarity = Rarity.Comum;
    public float minWeight = 0.5f;
    public float maxWeight = 5f;
    public float catchDifficulty = 0.3f;
}

public enum Rarity
{
    Comum,
    Incomum,
    Raro,
    Épico,
    Lendário
}