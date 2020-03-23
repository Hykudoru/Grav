using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GravitationalField : MonoBehaviour
{
    [SerializeField] [Range(0f, 1000f)] float strength = 100f;
    [SerializeField] LayerMask layers = Physics.AllLayers;
    Rigidbody rb;
    Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
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

    private void OnDrawGizmos()
    {
        Collider[] colliders = null;
        Gizmos.color = Color.black;
        SphereCollider sphere = transform.GetComponent<SphereCollider>();
        if (sphere)
        {
            float radius = (sphere.bounds.extents).magnitude * .6f;
            colliders = Physics.OverlapSphere(transform.position, radius, layers);
            Gizmos.DrawWireSphere(sphere.transform.position, radius);
        }
        else
        {
            BoxCollider box = transform.GetComponent<BoxCollider>();
            if (box)
            {
                Gizmos.DrawWireCube(box.transform.position, box.bounds.size);
                colliders = Physics.OverlapBox(transform.position, box.bounds.size / 2f, box.transform.rotation, layers);
            }
        }

        Gizmos.color = Color.green;

        if (colliders != null && colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Collider col = colliders[i];

                if (col.gameObject == gameObject)
                {
                    return;
                }

                SphereCollider sphCol = col as SphereCollider;
                if (sphCol)
                {
                    float radius = (sphCol.bounds.extents).magnitude * .6f;
                    Gizmos.DrawWireSphere(sphCol.transform.position, radius);
                }
                else
                {
                    Gizmos.DrawWireCube(col.transform.position, col.bounds.size);
                }
            }
        }
    }
}