using System.Collections;
using UnityEngine;
using Billy.Weapons;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController warrior;
    [SerializeField] RuntimeAnimatorController archer;
    [SerializeField] RuntimeAnimatorController warlock;
    [SerializeField] RuntimeAnimatorController knight;
    [SerializeField] RuntimeAnimatorController thief;
    [SerializeField] RuntimeAnimatorController wizard;
    private Animator animator;
    private bool rollAnim = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeOnWeaponType(WeaponsType type)
    {
        switch (type)
        {
            case WeaponsType.AXE:
                animator.runtimeAnimatorController = warrior;
                break;
            case WeaponsType.BOW:
                animator.runtimeAnimatorController = archer;
                break;
            case WeaponsType.DAGGER:
                animator.runtimeAnimatorController = thief;
                break;
            case WeaponsType.SWORD:
                animator.runtimeAnimatorController = knight;
                break;
            case WeaponsType.STAFF:
                animator.runtimeAnimatorController = wizard;
                break;
            case WeaponsType.WARLORCK_STAFF:
                animator.runtimeAnimatorController = warlock;
                break;
        }
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void SetRoll(bool isRolling)
    {
        if (isRolling != rollAnim)
        {
            animator.SetBool("Roll", isRolling);
            rollAnim = isRolling;
        }
    }

    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayDie()
    {
        animator.SetTrigger("Die");
    }
}
