using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerComponent: MonoBehaviour
{
    [SerializeField] private Mutation[] mutationScales;
    [SerializeField] private GameObject windVFX;
    [SerializeField] private ParticleSystem evolveVFX;

    public NavMeshAgent NavMesh;
    public Rigidbody RB;
    public OnTriggerEnterComponent OnTriggerEnterComp;
    public PlayerCanvasComponent PlayerCanvas;

    [HideInInspector]
    public PlayerAnimatorComponent PlayerAnimator;

    public int Mutations => currentMutation;

    private float currentHealth;

    private int currentMutation = 0;

    public void Init()
    {
        currentMutation = 0;

        RotateModel(transform.position + Vector3.back * 2 + Vector3.left);

        SetMutation();
    }
    public void SetHealth(float value)
    {
        currentHealth = value;
    }
    public void ReceiveDamage(float value)
    {
        PlayerAnimator.ReceiveDamage();

        currentHealth -= value;
    }
    public float GetHealth()
    {
        return currentHealth;
    }

    public void Mutate()
    {
        if (currentMutation >= mutationScales.Length)
        {
            return;
        }

        currentMutation++;

        evolveVFX.Play();

        SetMutation();
        StartRunning(true);
    }

    private void SetMutation()
    {
        foreach (var mutation in mutationScales)
        {
            mutation.model.SetActive(false);
        }

        mutationScales[currentMutation].model.SetActive(true);
        PlayerAnimator = mutationScales[currentMutation].playerAnimator;
    }
    public void StartRunning(bool enable)
    {
        mutationScales[currentMutation].playerAnimator.SetRunAnimation(enable);
    }

    public void RotateModel(Vector3 lookAt)
    {
        mutationScales[currentMutation].model.transform.LookAt(lookAt);
    }
    public void DisableWindVFX()
    {
        windVFX.SetActive(false);
    }
}

[Serializable]
public struct Mutation
{
    public GameObject model;
    public PlayerAnimatorComponent playerAnimator;
}