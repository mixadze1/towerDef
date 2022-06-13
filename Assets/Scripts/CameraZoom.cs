using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Transform _model;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    CinemachineComponentBase componentBase;
    float cameraDistance;
    [SerializeField] float sensitivity = 10f;
    private void Update()
    {
        

        if(componentBase == null)
        {
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }
        if(Input.GetMouseButton(1) && (componentBase as CinemachineFramingTransposer).m_CameraDistance != 0 && Input.GetMouseButton(0) != true)
        {
            Debug.Log("Zdec");
            cameraDistance =  sensitivity;
            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance -= cameraDistance;

            }
        }
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
            Debug.Log("Zdec");
            cameraDistance = sensitivity;
            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance += cameraDistance;

            }
        }
    }

}
