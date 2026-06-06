using System;
using UnityEngine;

public class FishingTargetComponent : MonoBehaviour
{
    public bool isInWater;
    private void OnCollisionEnter(Collision other)
    {
        isInWater = other.gameObject.tag == "Water";
        Debug.LogWarning($"{other.gameObject.tag}");
    }

    private void OnCollisionExit(Collision _)
    {
        isInWater = false;
    }
}
