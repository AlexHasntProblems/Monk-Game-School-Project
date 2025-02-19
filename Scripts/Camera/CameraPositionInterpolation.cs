using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionInterpolation : MonoBehaviour
{
    [SerializeField] private float _lerpPercent;
    private void LateUpdate()
    {
        if (Player.Instance != null)
        {
            if ((new Vector3(Player.Instance.transform.position.x - transform.position.x, Player.Instance.transform.position.y - transform.position.y, 0f)).magnitude > 0.001f)
                transform.position = Vector3.Lerp(transform.position, new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, transform.position.z), _lerpPercent * Time.deltaTime);
            else
                transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, transform.position.z);
        } 
    }
}
