using UnityEngine;

[CreateAssetMenu(fileName = "FishItem", menuName = "Fishing Codex/Fish Item")]
public class FishItem : ScriptableObject
{
    public string fishName = "Peixe Desconhecido";
    public string description = "Um peixe misterioso...";
    public Sprite fishSprite;
    public Rarity rarity = Rarity.Comum;
    
    public GameObject modelPrefab;
}

public enum Rarity
{
    Comum = 50,
    Incomum = 30,
    Raro = 20,
    Épico = 10,
    Lendário = 1
}