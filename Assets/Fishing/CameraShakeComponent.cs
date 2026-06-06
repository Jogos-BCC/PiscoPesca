using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShakeComponent : MonoBehaviour
{
    [SerializeField] private RodThrowComponent rod;
    [SerializeField] private AudioSource sound;
    [SerializeField] private float soundVolumeRatio;

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
            if (sound.isPlaying)
                sound.Stop();
            return;
        }
            

        var ratio = rod.GetRangeRatio();
        ratio = Mathf.Clamp(ratio - 0.3f, 0f, 1f);

        if (ratio >= 0)
        {
            if (!sound.isPlaying)
                sound.Play();
            sound.volume = ratio * soundVolumeRatio;
        }

        transform.localPosition = originalCam + Random.insideUnitSphere * shakeAmount * ratio;
    }
}
