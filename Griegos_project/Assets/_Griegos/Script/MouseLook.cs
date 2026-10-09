
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The camera that will rotate vertically.")]
    [SerializeField] private Transform cameraTransform;

    [Header("Mouse Settings")]
    [Tooltip("Mouse sensitivity.")]
    [SerializeField] private float sensitivity = 200f;

    [Tooltip("Maximum angle the camera can look up/down from the horizon.")]
    [SerializeField] private float maxVerticalAngle = 80f;

    [Header("Smoothing")]
    [Tooltip("How quickly the camera/body responds to mouse movement.")]
    [SerializeField] private float rotationSpeed = 20f;

    private float verticalRotation = 0f;

    private void Start()
    {
        // Get the camera's current vertical rotation.
        verticalRotation = cameraTransform.localEulerAngles.x;

        // Convert Unity's 0-360 angle into -180 to 180.
        if (verticalRotation > 180f)
        {
            verticalRotation -= 360f;
        }

        // Make sure the starting angle is within our limits.
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);
    }

    private void Update()
    {
        // Only rotate while right click is being held.
        if (!Input.GetMouseButton(1))
            return;

        // Read mouse movement.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // HORIZONTAL ROTATION
        float horizontalRotation = mouseX * sensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up, horizontalRotation);

        // VERTICAL ROTATION
        verticalRotation -= mouseY * sensitivity * Time.deltaTime;

        // Prevent the player from looking too far up/down.
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        // Apply vertical rotation only to the camera.
        cameraTransform.localRotation =
            Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
