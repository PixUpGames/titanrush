using Cinemachine;
using UnityEngine;

public class CamerasComponent : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerMainCamera;
    [SerializeField] private CinemachineVirtualCamera playerMainCameraFirstEvolve;
    [SerializeField] private CinemachineVirtualCamera fightCamera;
    [SerializeField] private CinemachineVirtualCamera defeatedEnemyCamera;
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
    public void SetMainCameraFirstEvolve()
    {
        ResetAllCameras();

        playerMainCameraFirstEvolve.m_Priority = 10;
    }
    public void SetFightCamera()
    {
        ResetAllCameras();

        fightCamera.m_Priority = 10;
    }
    public void SetDefeatedBossCamera()
    {
        ResetAllCameras();

        defeatedEnemyCamera.m_Priority = 10;
    }
}