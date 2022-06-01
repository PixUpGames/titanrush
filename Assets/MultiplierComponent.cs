using UnityEngine;

public class MultiplierComponent : MonoBehaviour
{
    [Range(1, 15)]
    [SerializeField] private float multiplier;
    [SerializeField] private MeshRenderer meshRenderer;

    public float Multiplier => multiplier;

    public void ChangeMaterial(Material material)
    {
        meshRenderer.sharedMaterial = material;
    }
}