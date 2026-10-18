using Unity.VisualScripting;
using UnityEngine;

public class MinionAnimator : ZombieAnimator
{
    [SerializeField] Animator animator;
    private string[] states = { "Running" };
    private string[] triggers = { "Attack", "Howling", "Dead" };
    public override bool HasRunAnimation => true; // Run 애니메이션 있음

    public override void StartIdle()
    {
        animator.SetBool(states[0], false);
    }

    public override void StartRunning()
    {
        animator.SetBool(states[0], true);
    }

    public override void StartHowling()
    {
        animator.SetTrigger(triggers[1]);
    }

    public override void StartAttack()
    {
        int AttackMotion = Random.Range(0, 2);
        animator.SetInteger("AttackMotion", AttackMotion);
        animator.SetTrigger(triggers[0]);
    }

    public override void StartDead()
    {
        animator.SetTrigger(triggers[2]);
    }

    public override float GetDuration()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }
}
