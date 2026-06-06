using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishSelectorComponent : MonoBehaviour
{
    [SerializeField] private FishItem[] fishItems;

    private int getFishProbability(FishItem fish) => (int) fish.rarity;

    private int maxProb;

    private void Start()
    {
        foreach (var fish in fishItems)
            maxProb += getFishProbability(fish);
    }

    public FishItem GetRandomFish()
    {
        var val = Random.Range(0, maxProb);
        foreach (var fish in fishItems)
        {
            val -= getFishProbability(fish);
            if (val <= 0)
                return fish;
        }

        return fishItems.Last();
    }

}
