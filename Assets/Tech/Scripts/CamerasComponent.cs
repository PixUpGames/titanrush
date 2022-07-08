using Cinemachine;
using UnityEngine;

public class CamerasComponent : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerMainCamera;
    [SerializeField] private CinemachineVirtualCamera playerMainCameraFirstEvolve;
    [SerializeField] private CinemachineVirtualCamera playerMainCameraSecondEvolve;
    [SerializeField] private CinemachineVirtualCamera playerMainCameraThirdEvolve;
    [SerializeField] private CinemachineVirtualCamera fightCamera;
    [SerializeField] private CinemachineVirtualCamera defeatedEnemyCamera;
    [SerializeField] private CinemachineVirtualCamera StartCamera;
    [SerializeField] private CinemachineImpulseSource ImpulseSource;
    [SerializeField] private CinemachineVirtualCamera[] allCameras;

    private const int highestPriority = 20;
    private int evolveIndex;
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
    public void SetMainCameraEvolve()
    {
        ResetAllCameras();

        if (evolveIndex == 0)
        {
            playerMainCameraFirstEvolve.m_Priority = highestPriority;
        }
        else if (evolveIndex == 1)
        {
            playerMainCameraFirstEvolve.m_Priority = 1;
            playerMainCameraSecondEvolve.m_Priority = highestPriority * 2;
        }
        else
        {
            playerMainCameraSecondEvolve.m_Priority = 1;
            playerMainCameraThirdEvolve.m_Priority = highestPriority * 2;
        }
        evolveIndex++;
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

    public void SetTargetPlayer(Transform targetPlayer)
    {
        foreach (var camera in allCameras)
        {
            camera.m_LookAt = targetPlayer;
            camera.m_Follow = targetPlayer;
        }
    }
}