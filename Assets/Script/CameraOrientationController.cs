using UnityEngine;

public class CameraOrientationController : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroSupported;
    private Quaternion initialRotation;

    void Start()
    {
        gyroSupported = SystemInfo.supportsGyroscope;

        if (gyroSupported)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            // Calibrate the initial rotation to align with the device's orientation
            initialRotation = gyro.attitude;
        }
    }

    void Update()
    {
        if (gyroSupported)
        {
            // Get the current gyroscope attitude
            Quaternion currentRotation = gyro.attitude;

            // Calculate the rotation relative to the initial orientation
            Quaternion relativeRotation = Quaternion.Inverse(initialRotation) * currentRotation;

            // Extract the yaw (rotation around the Y-axis)
            float yaw = relativeRotation.eulerAngles.y;

            // Adjust the yaw to the desired range (-90 to 90 degrees)
            if (yaw > 180f)
            {
                yaw -= 360f;
            }

            // Apply the yaw rotation to the camera
            transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        }
    }
}