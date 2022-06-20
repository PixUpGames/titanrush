using Cinemachine;
using UnityEngine;

public class CamerasComponent : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerMainCamera;
    [SerializeField] private CinemachineVirtualCamera playerMainCameraFirstEvolve;
    [SerializeField] private CinemachineVirtualCamera fightCamera;
    [SerializeField] private CinemachineVirtualCamera defeatedEnemyCamera;
    [SerializeField] private CinemachineVirtualCamera StartCamera;
    [SerializeField] private CinemachineImpulseSource ImpulseSource;
    [SerializeField] private CinemachineVirtualCamera[] allCameras;

    private const int highestPriority = 20;

    private void ResetAllCameras()
    {
        foreach(var camera in allCameras)
        {
            camera.m_Priority = 1;
        }
    }
    public void SetStartCamera()
    {
        ResetAllCameras();

        StartCamera.m_Priority = highestPriority;
    }
    public void SetMainCamera()
    {
        ResetAllCameras();

        playerMainCamera.m_Priority = highestPriority;
    }
    public void SetMainCameraFirstEvolve()
    {
        ResetAllCameras();

        playerMainCameraFirstEvolve.m_Priority = highestPriority;
    }
    public void SetFightCamera(Transform @object)
    {
        ResetAllCameras();

        fightCamera.m_LookAt = @object;
        fightCamera.m_Follow = @object;
        fightCamera.m_Priority = highestPriority;
    }
    public void SetDefeatedBossCamera(Transform @object)
    {
        ResetAllCameras();

        defeatedEnemyCamera.m_LookAt = @object;
        defeatedEnemyCamera.m_Follow = @object;
        defeatedEnemyCamera.m_Priority = highestPriority;
    }
    public void ImpulseCamera()
    {
        ImpulseSource.GenerateImpulse();
    }
}