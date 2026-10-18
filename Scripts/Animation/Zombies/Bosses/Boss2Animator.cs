using UnityEngine;

public class Boss2Animator : ZombieAnimator
{
    [SerializeField] Animator animatorLOD1;
    [SerializeField] Animator animatorLOD2;
    private string[] states = { "Walking", "Running" };
    private string[] triggers = { "Attack", "Bite", "Howling", "Dead" };
    public override bool HasRunAnimation => true; // Run 애니메이션 있음

    public override void StartIdle()
    {
        animatorLOD1.SetBool(states[0], false);
        animatorLOD1.SetBool(states[1], false);
        animatorLOD2.SetBool(states[0], false);
        animatorLOD2.SetBool(states[1], false);
    }

    public override void StartWalking()
    {
        animatorLOD1.SetBool(states[0], true);
        animatorLOD1.SetBool(states[1], false);
        animatorLOD2.SetBool(states[0], true);
        animatorLOD2.SetBool(states[1], false);
    }

    public override void StartRunning()
    {
        animatorLOD1.SetBool(states[0], false);
        animatorLOD1.SetBool(states[1], true);
        animatorLOD2.SetBool(states[0], false);
        animatorLOD2.SetBool(states[1], true);
    }

    public override void StartAttack()
    {
        animatorLOD1.SetTrigger(triggers[0]);
        animatorLOD2.SetTrigger(triggers[0]);
    }

    public override void StartBite()
    {
        animatorLOD1.SetTrigger(triggers[1]);
        animatorLOD2.SetTrigger(triggers[1]);
    }

    public override void StartHowling()
    {
        animatorLOD1.SetTrigger(triggers[2]);
        animatorLOD2.SetTrigger(triggers[2]);
    }

    public override void StartDead()
    {
        animatorLOD1.SetTrigger(triggers[3]);
        animatorLOD2.SetTrigger(triggers[3]);
    }

    public override float GetDuration()
    {
        return animatorLOD1.GetCurrentAnimatorStateInfo(0).length;
    }
}
