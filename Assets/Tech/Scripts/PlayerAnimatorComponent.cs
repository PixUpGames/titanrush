using Kuhpik;
using Supyrb;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAnimatorComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject finalPunchVFX;
    [SerializeField] private GameObject electricityVFX;
    [SerializeField] private Transform head;
    [SerializeField] private Transform FightCameraHolder;

    public Transform Head => head;
    public Transform CameraHolder => FightCameraHolder;
    private const string FIGHT_IDLE = "StartBattle";
    private const string KICK = "Kick";
    private const string PUNCH = "Punches";
    private const string RUN = "Run";
    private const string FINAL_KICK = "PunchFatality";
    private const string RECEIVE_DAMAGE = "TakeDamage";
    private const string DIE = "Death";
    private const string ATTACK_WHIRL_STRAIGHT = "Attack_Whirl_Straight";
    private const string ATTACK_WHIRL_SKEW = "Attack_Whirl_Skew";
    private const string JUMP = "Jump";

    private int fightIdleHash;
    private int kickHash;
    private int runHash;
    private int finalKickHash;
    private int punchHash;
    private int receiveDamageHash;
    private int deathHash;
    private int straightWhirlHash;
    private int skewWhirlHash;
    private int jumpHash;

    private PlayerHitSignal hitSignal;
    private DapFootKickSignal footKickSignal;

    [SerializeField] private List<CustomizableItemComponent> hatsList = new List<CustomizableItemComponent>();
    [SerializeField] private List<CustomizableItemComponent> glovesList = new List<CustomizableItemComponent>();

    private void Awake()
    {
        fightIdleHash = Animator.StringToHash(FIGHT_IDLE);
        kickHash = Animator.StringToHash(KICK);
        runHash = Animator.StringToHash(RUN);
        finalKickHash = Animator.StringToHash(FINAL_KICK);
        punchHash = Animator.StringToHash(PUNCH);
        receiveDamageHash = Animator.StringToHash(RECEIVE_DAMAGE);
        deathHash = Animator.StringToHash(DIE);
        straightWhirlHash = Animator.StringToHash(ATTACK_WHIRL_STRAIGHT);
        skewWhirlHash = Animator.StringToHash(ATTACK_WHIRL_SKEW);
        jumpHash = Animator.StringToHash(JUMP);

        hitSignal = Signals.Get<PlayerHitSignal>();
        footKickSignal = Signals.Get<DapFootKickSignal>();

        var allCustomizables = GetComponentsInChildren<CustomizableItemComponent>(true);

        foreach (var customizable in allCustomizables)
        {
            if (customizable.ItemType == ShopType.HAT)
            {
                hatsList.Add(customizable);
            }
            else if(customizable.ItemType == ShopType.GLOVES)
            {
                glovesList.Add(customizable);
            }
        }
    }

    #region Animations
    public void SetFightIdle(bool value)
    {
        animator.SetBool(fightIdleHash, value);
        animator.SetBool(runHash, false);
    }
    public void SetKickAnimation()
    {
        animator.SetTrigger(kickHash);
    }
    public void SetStraightWhirlAnimation()
    {
        animator.SetTrigger(straightWhirlHash);
    }
    public void SetSkewWhirlAnimation()
    {
        animator.SetTrigger(skewWhirlHash);
    }
    public void SetRunAnimation(bool enable)
    {
        animator.SetBool(runHash, enable);
    }
    public void SetFinalKick()
    {
        animator.SetTrigger(finalKickHash);

        ActivateFinalPunchVFX();
    }
    public void ClearAllAnimations()
    {
        animator.ResetTrigger(kickHash);
    }
    public void Punch(bool enable)
    {
        animator.SetBool(punchHash, enable);
    }
    public void ReceiveDamage()
    {
        animator.SetTrigger(receiveDamageHash);
    }
    public void Die()
    {
        animator.SetTrigger(deathHash);
    }

    public void Jump()
    {
        animator.SetBool(jumpHash, true);
    }

    #endregion
    public void FinishFatalityPunch()
    {
        Time.timeScale = 1f;

        if (Bootstrap.Instance.GetCurrentGamestateID() != GameStateID.EnemyDefeated)
            Bootstrap.Instance.ChangeGameState(GameStateID.EnemyDefeated);

        finalPunchVFX?.SetActive(true);
    }
    public void ActivateFinalPunchVFX()
    {
        electricityVFX?.SetActive(true);
    }
    public void HitSignal()
    {
        hitSignal.Dispatch();
    }

    public void DapFootKickSignal()
    {
        footKickSignal.Dispatch();
        Bootstrap.Instance.GetSystem<DAPEnemyBehaviourSystem>().OnFootKickFeedback();
    }

    public void WearItemOnPlayer(ShopType itemType,CustomizableType itemName)
    {
        if (itemType == ShopType.HAT)
        {
            foreach (var hat in hatsList)
            {
                if (hat.ItemName == itemName)
                {
                    hat.gameObject.SetActive(true);
                }
                else
                {
                    hat.gameObject.SetActive(false);
                }
            }
        }
        else if (itemType == ShopType.GLOVES)
        {
            foreach (var glove in glovesList)
            {
                if (glove.ItemName == itemName)
                {
                    glove.gameObject.SetActive(true);
                }
                else
                {
                    glove.gameObject.SetActive(false);
                }
            }
        }
    }
}