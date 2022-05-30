using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level")]
public class Level : ScriptableObject
{
    public GameObject LevelPrefab;
    public int AllowedMutations;
    public int MutationBarsToEvolve;
    public FinishState FinishState;
}