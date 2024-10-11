using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class GunPickUp : MonoBehaviour
{
    public Transform rightHandTransform; // Right hand reference
    public Transform leftHandTransform;  // Left hand reference
    private bool isHeld = false;         // Track if the gun is held
    private bool heldInRightHand = false; // Track which hand is holding the gun

    private Rigidbody gunRigidbody;
    private Collider gunCollider;

    private bool rightHandInTrigger = false; // Track if right hand is inside trigger
    private bool leftHandInTrigger = false;  // Track if left hand is inside trigger

    private void Start()
    {
        gunRigidbody = GetComponent<Rigidbody>();
        gunCollider = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        // If gun is being held
        if (isHeld)
        {
            // Check if the button is no longer held for the hand currently holding the gun
            if (heldInRightHand && !OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                ReleaseGun(); // Release if right hand trigger is released
            }
            else if (!heldInRightHand && !OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            {
                ReleaseGun(); // Release if left hand trigger is released
            }
        }
        else
        {
            // Check for holding down right hand trigger to pick up the gun only if right hand is in the trigger zone
            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                PickupGun(rightHandTransform);
                heldInRightHand = true; // Mark the gun as held in the right hand
            }
            // Check for holding down left hand trigger to pick up the gun only if left hand is in the trigger zone
            else if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger ))
            {
                PickupGun(leftHandTransform);
                heldInRightHand = false; // Mark the gun as held in the left hand
            }
        }
    }

    private void PickupGun(Transform handTransform)
    {
        if (Vector3.Distance(this.transform.position, handTransform.position) > 1)
            return;
        // Make the gun a child of the specified hand and disable physics
        transform.SetParent(handTransform);
        Debug.Log("pak wapen");
        if (handTransform.name == "RightHandAnchor")
        {
            // Adjust the local position and rotation to fit the hand properly
            transform.localPosition = new Vector3(0.025f, 0f, 0f);  // Adjust position closer to the hand
            transform.localRotation = Quaternion.Euler(0f, 90f, 0f);  // Adjust rotation
        }
        else if (handTransform.name == "LeftHandAnchor")
        {
            transform.localPosition = new Vector3(-0.025f, 0f, 0f);  // Adjust position closer to the hand
            transform.localRotation = Quaternion.Euler(0f, 90f, 0f);  // Adjust rotation
        }

        gunRigidbody.isKinematic = true;  // Disable physics while holding
        gunCollider.enabled = false;      // Disable collisions while holding

        isHeld = true;

        // Debug Log to confirm gun pickup
        Debug.Log("Picked up gun with: " + handTransform.name);
    }

    private void ReleaseGun()
    {
        // Detach the gun from the hand
        transform.SetParent(null);

        // Re-enable collider
        gunCollider.enabled = true;        // Re-enable collisions

        isHeld = false;

        // Debug Log to confirm gun release
        Debug.Log("Released gun.");
    }

    // When the hand enters the gun's collider, this method will be called
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightHand"))  // Ensure the hand is tagged appropriately
        {
            rightHandInTrigger = true;  // Set flag to true when right hand is inside trigger
            Debug.Log("Right hand entered the gun's trigger.");
        }
        else if (other.CompareTag("LeftHand"))  // Ensure the hand is tagged appropriately
        {
            leftHandInTrigger = true;   // Set flag to true when left hand is inside trigger
            Debug.Log("Left hand entered the gun's trigger.");
        }
    }

    // When the hand exits the gun's collider, this method will be called
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RightHand"))
        {
            rightHandInTrigger = false;  // Reset flag when right hand exits the trigger
            Debug.Log("Right hand exited the gun's trigger.");
        }
        else if (other.CompareTag("LeftHand"))
        {
            leftHandInTrigger = false;  // Reset flag when left hand exits the trigger
            Debug.Log("Left hand exited the gun's trigger.");
        }
    }
}