using Kuhpik;
using Supyrb;
using UnityEngine;

public class MutationSystem : GameSystem
{
    public override void OnStateEnter()
    {
        Signals.Get<MutateSignal>().AddListener(Mutate);

        if (player.isRevive)
        {
            for (int i = 0; i < player.reviveMutation; i++)
            {
                Mutate();
            }

            Debug.Log("MUTATE REVIVE");
            player.isRevive = false;
        }
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
                    game.MutationBars = 0;
                    break;
                case 2:
                    game.Cameras.SetMainCameraEvolve();
                    game.PlayerComponent.PlayerCanvas.SetMutation(game.MutationLevel);
                    game.MutationBars = 0;
                    break;
                case 3:
                    game.Cameras.SetMainCameraEvolve();
                    game.PlayerComponent.PlayerCanvas.SetMutation(game.MutationLevel);
                    game.MutationBars = 0;
                    break;
            }

            game.PlayerComponent.Mutate();
            game.PlayerComponent.PlayerCanvas.transform.position = game.PlayerComponent.PlayerAnimator.Head.transform.position + Vector3.up * .5f;
            game.PlayerComponent.PlayerAnimator.WearItemOnPlayer(ShopType.GLOVES, player.glovesType);
            game.PlayerComponent.PlayerAnimator.WearItemOnPlayer(ShopType.SKINS, player.skinType);

            if (player.TempItem == CustomizableType.Null)
            {
                game.PlayerComponent.PlayerAnimator.WearItemOnPlayer(ShopType.HAT, player.hatType);
            }
            else
            {
                game.PlayerComponent.PlayerAnimator.WearItemOnPlayer(ShopType.HAT, player.TempItem);
            }

        }
    }
}