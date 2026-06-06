using System;
using UnityEngine;

public class BuoyancyComponent : MonoBehaviour
{
    [SerializeField] private Transform waterLevel;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float floatingStrength = 20f;
    [SerializeField] private float underwaterDamping = 4f;
    [SerializeField] private float underwaterAngularDamping = 2f;

    public bool isUnderwater;
    private void FixedUpdate()
    {
        var difference = transform.position.y - waterLevel.position.y;
        isUnderwater = difference < 0;

        if (isUnderwater)
        {
            rb.AddForceAtPosition(
                Vector3.up * floatingStrength * Math.Abs(difference),
                transform.position,
                ForceMode.Force
            );
        }

        rb.linearDamping = isUnderwater ? underwaterDamping : 0;
        rb.angularDamping = isUnderwater ? underwaterAngularDamping : 0.05f;
    }
}
