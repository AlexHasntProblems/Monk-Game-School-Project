using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;  
    public IEnumerator FocusAt()
    {
        Camera.main.GetComponent<CameraPositionInterpolation>().enabled = false;
        while ((new Vector3(Camera.main.transform.position.x - transform.position.x, Camera.main.transform.position.y - transform.position.y, 0f)).magnitude > 0.3f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z), Time.deltaTime * _cameraSpeed);
            yield return null;
        }
    }
}
