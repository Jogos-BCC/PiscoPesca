using System;
using UnityEngine;

public class FishingLineComponent : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform source;
    [SerializeField] private LineRenderer line;

    private void Update()
    {
        line.enabled = FishingManager.Instance.isFishing;
        if (!line.enabled)
            return;
        
        line.SetPositions(new [] {
            target.position, source.position
        });
    }
}
