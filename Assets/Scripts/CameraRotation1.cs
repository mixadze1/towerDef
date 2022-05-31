using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation1 : MonoBehaviour
{
    [SerializeField] private  Transform _camera;

    [SerializeField] private Vector3 _leftPosition;
    [SerializeField] private Vector3 _leftRotation;

    [SerializeField] private Vector3 _rightPosition;
    [SerializeField] private Vector3 _rightRotation;

    [SerializeField] private Vector3 _backPosition;
    [SerializeField] private Vector3 _backRotation;

    [SerializeField] private Vector3 _forwardPosition;
    [SerializeField] private Vector3 _forwardRotation;

    void Update()
    {
    }
    public void MovingCameraLeft()
    {
        Debug.Log("tut");

        if (_camera.position.z == _leftPosition.z)
        {
            _camera.localPosition = _forwardPosition;
            _camera.rotation = Quaternion.Euler(_forwardRotation);
            return;
        }


        if (_camera.position.x == _forwardPosition.x)
        {
            _camera.position = _rightPosition;
            _camera.rotation = Quaternion.Euler(_rightRotation);
            return;
        }
       

        if (_camera.position.z == _rightPosition.z)
        {
            Debug.Log("zde");
            _camera.position = _backPosition;
            _camera.rotation = Quaternion.Euler(_backRotation);
            return;
        }
           

        if (_camera.position.x == _backPosition.x)
        { 
            _camera.position = _leftPosition;
            _camera.rotation = Quaternion.Euler(_leftRotation);
            return;
        }
            
    }

    public void MovingCameraRight()
    {
        if (_camera.position.z == _leftPosition.z)
        {
         
            _camera.position = _backPosition;
            _camera.rotation = Quaternion.Euler(_backRotation);
            return; }
           

        if (_camera.position.x == _backPosition.x)
        { 
            _camera.position = _rightPosition;
            _camera.rotation = Quaternion.Euler(_rightRotation);
            return;
        }
            

        if (_camera.position.z == _rightPosition.z)
        { 
            _camera.position = _forwardPosition;
            _camera.rotation = Quaternion.Euler(_forwardRotation);
            return; 
        }
            

        if (_camera.position.x == _forwardPosition.x)
        { 
            _camera.position = _leftPosition;
            _camera.rotation = Quaternion.Euler(_leftRotation);
            return; 
        }
            
    }
}
