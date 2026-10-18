using UnityEngine;

public class SpitZombieAnimator : ZombieAnimator
{
    [SerializeField] Animator animator;
    private string[] states = { "Walking" };
    private string triggers = "Attack";

    public override void StartIdle()
    {
        animator.SetBool(states[0], false);
    }

    public override void StartWalking()
    {
        animator.SetBool(states[0], true);
    }

    public override void StartAttack()
    {
        animator.SetTrigger(triggers);
    }

    public override float GetDuration()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }
}
