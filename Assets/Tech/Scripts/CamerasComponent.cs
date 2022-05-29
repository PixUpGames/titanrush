using Cinemachine;
using UnityEngine;

public class CamerasComponent : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerMainCamera;
    [SerializeField] private CinemachineVirtualCamera fightCamera;
    [SerializeField] private CinemachineVirtualCamera[] allCameras;

    private void ResetAllCameras()
    {
        foreach(var camera in allCameras)
        {
            camera.m_Priority = 1;
        }
    }

    public void SetMainCamera()
    {
        ResetAllCameras();

        playerMainCamera.m_Priority = 10;
    }
    public void SetFightCamera()
    {
        ResetAllCameras();

        fightCamera.m_Priority = 10;

    }
}