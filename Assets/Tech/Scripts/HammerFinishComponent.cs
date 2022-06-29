using UnityEngine;

public class HammerFinishComponent : FinishComponent
{
    [SerializeField] private GameObject smallTitan;
    [SerializeField] private GameObject bigTitan;

    [SerializeField] private Transform[] points;

    public Transform GetPoint(int index) => points[index];
    public int GetPointsLength() => points.Length;
    public GameObject BigTitan => bigTitan;
}