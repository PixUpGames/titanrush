using Kuhpik;
using Supyrb;
using UnityEngine;

public class MutationSystem : GameSystem
{
    public override void OnStateEnter()
    {
        Signals.Get<MutateSignal>().AddListener(Mutate);
    }
    private void Mutate()
    {
        if (game.MutationLevel < game.LevelConfig.AllowedMutations)
        {
            game.MutationLevel++;

            switch (game.MutationLevel)
            {
                case 1:
                    game.Cameras.SetMainCameraEvolve();
                    game.PlayerComponent.PlayerCanvas.SetMutation(game.MutationLevel);
                    game.playerSpeed *= 1.5f;
                    break;
                case 2:
                    game.Cameras.SetMainCameraEvolve();
                    game.PlayerComponent.PlayerCanvas.SetMutation(game.MutationLevel);
                    break;
                case 3:
                    game.Cameras.SetMainCameraEvolve();
                    game.PlayerComponent.PlayerCanvas.SetMutation(game.MutationLevel);
                    break;
            }

            game.PlayerComponent.Mutate();
            game.PlayerComponent.PlayerCanvas.transform.position = game.PlayerComponent.PlayerAnimator.Head.transform.position + Vector3.up * .5f;
            game.PlayerComponent.PlayerAnimator.WearItemOnPlayer(ShopType.GLOVES, player.glovesType);
            game.PlayerComponent.PlayerAnimator.WearItemOnPlayer(ShopType.HAT, player.hatType);

        }

    }
}