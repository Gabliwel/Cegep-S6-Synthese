using System.Collections;
using UnityEngine;
using Billy.Weapons;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController warrior;
    [SerializeField] RuntimeAnimatorController archer;
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
                // impossible pour le moment
                break;
            case WeaponsType.SWORD:
                // impossible pour le moment
                break;
            case WeaponsType.STAFF:
                break;
            case WeaponsType.WARLORCK_STAFF:
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
