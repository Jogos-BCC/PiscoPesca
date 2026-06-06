using System;
using UnityEngine;

public class SwayComponent : MonoBehaviour
{
    [SerializeField] private BuoyancyComponent buoyancyComponent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float xSwayRate = 1f;
    [SerializeField] private float zSwayRate = 1f;
    [SerializeField] private float xSwayMult = 30f;
    [SerializeField] private float zSwayMult = 30f;

    private void FixedUpdate()
    {
        if (!buoyancyComponent.isUnderwater)
            return;
        
        var angX = (float)Math.Sin(Time.time * xSwayRate);
        var angZ = (float)Math.Sin(Time.time * zSwayRate);
        var torqueX = Vector3.up * angX * xSwayMult * Time.fixedDeltaTime;
        var torqueZ = Vector3.right * angZ * xSwayMult * Time.fixedDeltaTime;
        rb.AddTorque(torqueX);
        rb.AddTorque(torqueZ);
    }
}
