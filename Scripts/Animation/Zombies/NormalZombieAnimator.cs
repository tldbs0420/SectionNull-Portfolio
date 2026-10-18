using UnityEngine;

public class NormalZombieAnimator : ZombieAnimator
{
    [SerializeField] Animator animator;
    private string[] states = { "Walking", "Running" };
    private string triggers = "Attack";
    public override bool HasRunAnimation => true; // Run 애니메이션 있음

    public override void StartIdle()
    {
        animator.SetBool(states[0], false);
        animator.SetBool(states[1], false);
    }

    public override void StartWalking()
    {
        animator.SetBool(states[0], true);
        animator.SetBool(states[1], false);
    }

    public override void StartRunning()
    {
        animator.SetBool(states[0], false);
        animator.SetBool(states[1], true);
    }

    public override void StartAttack()
    {
        int AttackMotion = Random.Range(0, 2);
        animator.SetInteger("AttackMotion", AttackMotion);
        animator.SetTrigger(triggers);
    }

    public override float GetDuration()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }
}
