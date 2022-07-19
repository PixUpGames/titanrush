using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level")]
public class Level : ScriptableObject
{
    public GameObject LevelPrefab;
    //public int index;
    public int AllowedMutations;
    public int MutationBarsToEvolve;
    public FinishState FinishState;
    public PlayerType PlayerType;
    public bool WithFlying;
    public float WallsScale;
}