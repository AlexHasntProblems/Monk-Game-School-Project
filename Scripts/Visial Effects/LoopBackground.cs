using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _horizontalParallaxCoefficient;
    [SerializeField] private float _verticalParallaxCoefficient;
    [SerializeField] private GameObject[] _image;
    [SerializeField] private float _minPositionX;
    [SerializeField] private float _maxPositionX;
    private float _cameraLastXPosition;
    private float _cameraLastYPosition;

    private void Start()
    {
        _cameraLastXPosition = Camera.main.transform.position.x;
        _cameraLastYPosition = Camera.main.transform.position.y;
    }
    private void Update()
    {  
        Vector3 delta = (new Vector3(-1f, 0f, 0f) * _speed + new Vector3((_cameraLastXPosition - Camera.main.transform.position.x) * _horizontalParallaxCoefficient, (_cameraLastYPosition - Camera.main.transform.position.y) * _verticalParallaxCoefficient, 0f)) * Time.deltaTime;

        for (int i = 0; i < _image.Length; i++)
        {
            _image[i].transform.localPosition += delta;
            if (_image[i].transform.localPosition.x <= _minPositionX)
            {
                _image[i].transform.localPosition = new Vector3(_maxPositionX - (_minPositionX - _image[i].transform.localPosition.x), _image[i].transform.localPosition.y, _image[i].transform.localPosition.z); 
            }
        }
        _cameraLastXPosition = Camera.main.transform.position.x;
        _cameraLastYPosition = Camera.main.transform.position.y;
    }
}
