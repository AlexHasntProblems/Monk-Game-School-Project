using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private const float targetRatio = 0.5f;
    private float currentRatio = 0.5f;
    private const float pixelsPerUnit = 100f;
    void Awake()
    {
        ResizeCamera();
    }

    private void ResizeCamera()
    {
        currentRatio = Screen.height / Screen.width;

        if (currentRatio > targetRatio)
            SetCameraSize(Screen.width * targetRatio / pixelsPerUnit / 2);
        else
            SetCameraSize(Screen.height / targetRatio / pixelsPerUnit / 2);
    }

    private void SetCameraSize(float size)
    {
        Camera.main.orthographicSize = size;
    }
}
