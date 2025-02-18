using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Transform[] _images;
    [SerializeField] private float _verticalParallaxCoefficient;
    [SerializeField] private float _horizontalParallaxCoefficient;
    [SerializeField] private float _maxPositionX;
    [SerializeField] private float _minPositionX;
    private float cameraLastXPosition;
    private float cameraLastYPosition;

    private void OnUnable()
    {
        cameraLastXPosition = Camera.main.transform.position.x;
        cameraLastYPosition = Camera.main.transform.position.y;
    }

    private void Update()
    {
        transform.localPosition += new Vector3((cameraLastXPosition - Camera.main.transform.position.x) * Time.deltaTime * _horizontalParallaxCoefficient, (cameraLastYPosition - Camera.main.transform.position.y) * Time.deltaTime * _verticalParallaxCoefficient, 0f);
        
        for (int i = 0; i < _images.Length; i++)
        {   
            if (_images[i].localPosition.x <= _minPositionX)
                _images[i].localPosition = new Vector3(_maxPositionX, _images[i].localPosition.y, _images[i].localPosition.z);
        }
        cameraLastXPosition = Camera.main.transform.position.x;
        cameraLastYPosition = Camera.main.transform.position.y;
    }
}
