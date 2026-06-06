using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShakeComponent : MonoBehaviour
{
    [SerializeField] private RodThrowComponent rod;

    [SerializeField] private float shakeAmount = 0.2f;

    private Vector3 originalCam;

    private void OnEnable()
    {
        originalCam = transform.localPosition;
    }

    private void Update()
    {
        if (!rod.isHolding)
        {
            if (transform.localPosition != originalCam)
                transform.localPosition = originalCam;
            return;
        }
            

        var ratio = rod.GetRangeRatio();
        ratio = Mathf.Clamp(ratio - 0.3f, 0f, 1f);

        transform.localPosition = originalCam + Random.insideUnitSphere * shakeAmount * ratio;
    }
}
