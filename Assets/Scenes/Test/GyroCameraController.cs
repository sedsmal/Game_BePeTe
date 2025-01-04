using UnityEngine;
using BizzyBeeGames;

public class GyroCameraController : SingletonComponent<GyroCameraController>
{
    //====Mouse
    // flag to keep track whether we are dragging or not
    public bool isDragging = false;

    // starting point of a camera movement
    float startMouseX;
    float startMouseY;

    // Camera component
    public Camera cam;

    //====Mouse


    //private Gyroscope gyro;
    private bool gyroSupported;
    private Quaternion initialRotation;
    //public float sensitivity = 1.0f;
    //public float smoothSpeed = 5.0f;
    private Quaternion targetRotation;
    private float x;
    private float y;
    


    public bool gyroEnabled = false;
    readonly float sensitivity = 50.0f;

    private Gyroscope gyro;

    void Start()
    {
        InitializeGyro();
    }

    public void InitializeGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroEnabled = EnableGyro();
            cam = this.gameObject.GetComponent<Camera>();
            // Capture the initial rotation from the gyroscope
            initialRotation = new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w);
            // Convert the initial quaternion to Euler angles (X, Y, Z)
            Vector3 initialEuler1 = initialRotation.eulerAngles;
            // Apply a 90-degree rotation around the X-axis and preserve X and Y, but set Z to 0
            Quaternion tempRotation = Quaternion.Euler(90f, 0f, 0f) * Quaternion.Euler(initialEuler1.x, initialEuler1.y, 0f);
            // Convert the resulting quaternion to Euler angles
            Vector3 tempEuler = tempRotation.eulerAngles;
            // Apply the rotation to the camera with Z locked to 0
            transform.rotation = Quaternion.Euler(tempEuler.x, tempEuler.y, 0f);
        }
    }

    private Quaternion ConvertGyroRotation(Quaternion gyroAttitude)
    {
        //return new Quaternion(gyroAttitude.x, gyroAttitude.y, -gyroAttitude.z, -gyroAttitude.w);
        return new Quaternion(gyroAttitude.x, 0, 0, 0);
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }

        return false;
    }

    void Update()
    {
        if (gyroEnabled)
        {
            GyroRotation();
            //Debug.Log("Gyroscope Rotation: " + ConvertGyroRotation(gyro.attitude));
            //Debug.Log("Camera Rotaion: " + transform.rotation);
            //Debug.Log("Initial Rotaion: " + Quaternion.Euler(90f, 0f, 0f) * ConvertGyroRotation(gyro.attitude));
        }
        else
        {
            // if we press the left button and we haven't started dragging
            if (Input.GetMouseButtonDown(0) && !isDragging)
            {
                // set the flag to true
                isDragging = true;

                // save the mouse starting position
                startMouseX = Input.mousePosition.x;
                startMouseY = Input.mousePosition.y;
            }
            // if we are not pressing the left btn, and we were dragging
            else if (Input.GetMouseButtonUp(0) && isDragging)
            {
                // set the flag to false
                isDragging = false;
            }
        }
    }

    void GyroRotation()
    {
        x = Input.gyro.rotationRate.x;
        y = Input.gyro.rotationRate.y;
        

        float xFiltered = FilterGyroValues(x);
        RotateUpDown(xFiltered * sensitivity);

        float yFiltered = FilterGyroValues(y);
        RotateRightLeft(yFiltered * sensitivity);
    }
    void GyroRotationStart()
    {
        x = Input.gyro.attitude.x;
        y = Input.gyro.attitude.y;


        float xFiltered = FilterGyroValues(x);
        RotateUpDown(x);

        float yFiltered = FilterGyroValues(y);
        RotateRightLeft(y);
    }
    float FilterGyroValues(float axis)
    {
        if (axis < -0.1 || axis > 0.1)
        {
            return axis;
        }
        else
        {
            return 0;
        }
    }
    public void RotateUpDown(float axis)
    {
        transform.RotateAround(transform.position, transform.right, -axis * Time.deltaTime);
    }

    //rotate the camera rigt and left (y rotation)
    public void RotateRightLeft(float axis)
    {
        transform.RotateAround(transform.position, Vector3.up, -axis * Time.deltaTime);
    }

//void Start()
//    {


//        gyroSupported = SystemInfo.supportsGyroscope;

//        if (gyroSupported)
//        {
//            gyro = Input.gyro;
//            gyro.enabled = true;

//            // Calibrate the initial rotation
//            initialRotation = new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w);
//        }

//        else
//        {
//            // Get our camera component====Mouse
//            cam = this.gameObject.GetComponent<Camera>();

//            //====Mouse
//        }
//    }

//    void Update()
//    {
//        if (gyroSupported)
//        {
//            // Get the current rotation from the gyroscope
//            Quaternion deviceRotation = new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w);

//            // Apply the initial rotation calibration
//            Quaternion calibratedRotation = initialRotation * deviceRotation;

//            // Log the rotation for debugging
//            Debug.Log("Gyroscope Rotation: " + calibratedRotation.eulerAngles);

//            // Apply the rotation to the camera, limited to X and Y axes
//            transform.localRotation = Quaternion.Euler(calibratedRotation.eulerAngles.x, calibratedRotation.eulerAngles.y, 0f);
//        }


//        else
//        {
//            // if we press the left button and we haven't started dragging
//            if (Input.GetMouseButtonDown(0) && !isDragging)
//            {
//                // set the flag to true
//                isDragging = true;

//                // save the mouse starting position
//                startMouseX = Input.mousePosition.x;
//                startMouseY = Input.mousePosition.y;
//            }
//            // if we are not pressing the left btn, and we were dragging
//            else if (Input.GetMouseButtonUp(0) && isDragging)
//            {
//                // set the flag to false
//                isDragging = false;
//            }
//        }
//    }

    void LateUpdate()
    {
        if (!gyroSupported)
        {
            // Check if we are dragging
            if (isDragging)
            {
                //Calculate current mouse position
                float endMouseX = Input.mousePosition.x;
                float endMouseY = Input.mousePosition.y;

                //Difference (in screen coordinates)
                float diffX = endMouseX - startMouseX;
                float diffY = endMouseY - startMouseY;

                //New center of the screen
                float newCenterX = Screen.width / 2 + diffX;
                float newCenterY = Screen.height / 2 + diffY;

                //Get the world coordinate , this is where we want to look at
                Vector3 LookHerePoint = cam.ScreenToWorldPoint(new Vector3(newCenterX, newCenterY, cam.nearClipPlane));

                //Make our camera look at the "LookHerePoint"
                transform.LookAt(LookHerePoint);

                //starting position for the next call
                startMouseX = endMouseX;
                startMouseY = endMouseY;
            }
        }
    }
}
