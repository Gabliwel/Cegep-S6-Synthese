using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private bool rollAnim = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
