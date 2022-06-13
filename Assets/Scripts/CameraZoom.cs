using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float lookSpeed = 1;
    private CinemachineFreeLook _cinemachine;
    private Player _playerInput;
    private void Awake()
    {
        _playerInput = new Player();
        _cinemachine = GetComponent<CinemachineFreeLook>();
    }
    private void OnEnable()
    {
        _playerInput.Enable();
    }
    private void OnDisable()
    {
        _playerInput.Disable();
    }
    private void Update()
    {
        Vector2 delta = _playerInput.Game.LookAround.ReadValue<Vector2>();
        _cinemachine.m_XAxis.Value += delta.x * 10f *  lookSpeed * Time.deltaTime;
        _cinemachine.m_YAxis.Value += delta.y * 0.1f * lookSpeed * Time.deltaTime;
    }


}
