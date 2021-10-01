using System;
using UnityEngine;

public class ItemSway : MonoBehaviour
{
    [SerializeField] private bool legacySway;

    [Header("Legacy Settings")]
    [SerializeField] float smooth;
    [SerializeField] float multiplier;

    [Header("New Settings")]
    [SerializeField] float Xsmooth;
    [SerializeField] float Xmultiplier;

    private float mouseX, mouseY;

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (legacySway) LegacySway();
        else Sway();
    }

    private void HandleInput()
    {
        // get mouse input
        mouseX = Input.GetAxisRaw("Mouse X") * multiplier;
        mouseY = Input.GetAxisRaw("Mouse Y") * multiplier;
    }

    private void LegacySway()
    {
        mouseX = Mathf.Clamp(mouseX, -3, 3);
        mouseY = Mathf.Clamp(mouseX, -3, 3);

        // calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        // rotate 
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    private void Sway()
    {
        mouseX = Mathf.Clamp(mouseX, -5, 5);

        // smoothly lerps between the rotations
        float targetX = Mathf.Lerp(transform.localRotation.z, mouseX, Xsmooth * Time.deltaTime);

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetX);

        // rotate the item
        transform.localRotation = targetRotation;
    }
}
