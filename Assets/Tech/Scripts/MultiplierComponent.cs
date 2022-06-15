using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class MultiplierComponent : MonoBehaviour
{
    [Range(1, 25)]
    [SerializeField] private float multiplier;
    [SerializeField] private MeshRenderer meshRenderer;

    public float Multiplier => multiplier;

    public void EnableMeshRenderer()
    {
        meshRenderer.enabled = true;
    }
    public void SetMultiplier(float value) 
    {
        multiplier = value;
    }

    [Button]
    private void UpdateMultipliers()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        GetComponentInChildren<TextMeshPro>().text = $"X{multiplier}";
    }
}