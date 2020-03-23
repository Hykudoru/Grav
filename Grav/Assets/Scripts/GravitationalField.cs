using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GravitationalField : MonoBehaviour
{
    [SerializeField] [Range(0f, 1000f)] float strength = 100f;
    Rigidbody rb;
    Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true;
        rb = gameObject.GetComponent<Rigidbody>();

        if (rb)
        {
            rb.isKinematic = true;
        }
    }

    public void GravityField(Rigidbody otherBody)
    {
        Vector3 displacment = transform.position - otherBody.position;
        Vector3 relativeDown = displacment.normalized;//body's new relative down direction
        Vector3 gravity = relativeDown * strength;
        // Pull towards center
        otherBody.velocity += gravity * Time.deltaTime;
        // Adjust orientation
        otherBody.rotation = Quaternion.FromToRotation(-otherBody.transform.up, relativeDown) * otherBody.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            other.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody)
        {
            GravityField(other.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.attachedRigidbody)
        {
            collider.attachedRigidbody.useGravity = true;
        }
    }
}