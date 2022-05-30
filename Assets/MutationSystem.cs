using Kuhpik;
using Supyrb;
using UnityEngine;

public class MutationSystem : GameSystem
{
    private int mutations;

    public override void OnStateEnter()
    {
        Signals.Get<MutateSignal>().AddListener(Mutate);
    }
    private void Mutate()
    {
        if (mutations < game.LevelConfig.AllowedMutations)
        {
            mutations++;

            switch (mutations)
            {
                case 1:
                    game.Cameras.SetMainCameraFirstEvolve();
                    break;
            }

            game.PlayerComponent.Mutate();
        }

    }
}