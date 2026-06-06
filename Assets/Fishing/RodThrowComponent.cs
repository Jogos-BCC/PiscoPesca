using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RodThrowComponent : MonoBehaviour
{
    [SerializeField] private GameObject targetRange;
    [SerializeField] private GameObject floater;
    [SerializeField] private float maxRange = 30f;
    [SerializeField] private float minRange = 4f;
    [SerializeField] private float rangeIncreaseRate = 1f;

    private bool isFishing => FishingManager.Instance.isFishing;
    
    public bool isHolding;
    private float range;

    public float GetRangeRatio() => range / maxRange;

    private void Start()
    {
        range = minRange;
    }

    private void FixedUpdate()
    {
        if (!isHolding)
            return;
        
        IncreaseRange();
        SetTargetRangePosition();
    }

    private void SetTargetRangePosition()
    {
        var floor = transform.position;
        floor.y = 0;
        targetRange.transform.position = floor;
        // TODO: Get the camera direction and do that * range
        targetRange.transform.position += transform.forward * range;
    }

    private void IncreaseRange()
    {
        range += rangeIncreaseRate * Time.fixedDeltaTime;
        if (range > maxRange)
            range = maxRange;
    }

    private void SetTargetRangeVisibility(bool enabled)
    {
        targetRange.GetComponent<MeshRenderer>().enabled = enabled;
    }

    private void SetFloaterVisibility(bool enabled)
    {
        floater.GetComponent<MeshRenderer>().enabled = enabled;
    }

    public void Throw(InputAction.CallbackContext context)
    {
        if (isFishing)
        {
            if (!context.canceled)
                return;

            FishingManager.Instance.StopFishing();
            SetTargetRangeVisibility(false);
            SetFloaterVisibility(false);
            return;
        }
        // Throw
        if (context.performed)
        {
            isHolding = true;
            SetTargetRangePosition();
            SetTargetRangeVisibility(true);
        }
        // Release
        else if (context.canceled)
        {
            isHolding = false;
            range = minRange;
            
            FishingManager.Instance.StartFishing();
            SetTargetRangeVisibility(false);
            SetFloaterVisibility(true);
            floater.transform.position = targetRange.transform.position;
        }
    }
}
