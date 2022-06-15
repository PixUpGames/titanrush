using UnityEngine;

public class FXCaster : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] VFX;

    public void CastFX(int index)
    {
        VFX[index].Play();
    }

    public void StopFX(int index)
    {
        VFX[index].Stop();
    }
}
