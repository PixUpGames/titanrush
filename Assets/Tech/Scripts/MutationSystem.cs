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
                    game.Cameras.SetMainCameraEvolve();
                    game.PlayerComponent.PlayerCanvas.SetMutation(mutations);
                    game.playerSpeed *= 1.5f;
                    break;
                case 2:
                    game.Cameras.SetMainCameraEvolve();
                    game.PlayerComponent.PlayerCanvas.SetMutation(mutations);
                    break;
                case 3:
                    game.Cameras.SetMainCameraEvolve();
                    game.PlayerComponent.PlayerCanvas.SetMutation(mutations);
                    break;
            }

            game.PlayerComponent.Mutate();
            game.PlayerComponent.PlayerCanvas.transform.position = game.PlayerComponent.PlayerAnimator.Head.transform.position + Vector3.up * .5f;
        }

    }
}