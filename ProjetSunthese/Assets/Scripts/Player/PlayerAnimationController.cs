using System.Collections;
using UnityEngine;

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

    private void Update()
    {
        // debug
        if (Input.GetKeyDown(KeyCode.Alpha1))
            animator.runtimeAnimatorController = warrior;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            animator.runtimeAnimatorController = archer;
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
