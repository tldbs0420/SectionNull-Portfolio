using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZombieAnimatorTest : MonoBehaviour
{
    [SerializeField] NormalZombieAnimator normalZombieAnimator;
    [SerializeField] RunawayZombieAnimator runawayZombieAnimator;
    [SerializeField] SpitZombieAnimator spitZombieAnimator;
    [SerializeField] MinionAnimator minionAnimator;
    [SerializeField] Boss1Animator boss1Animator;
    [SerializeField] Boss2Animator boss2Animator;
    [SerializeField] Boss3Animator boss3Animator;

    void Awake()
    {
        normalZombieAnimator.StartIdle();
        runawayZombieAnimator.StartIdle();
        spitZombieAnimator.StartIdle();
        minionAnimator.StartIdle();
        boss1Animator.StartIdle();
        boss2Animator.StartIdle();
        boss3Animator.StartIdle();
    }

    public void TestAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            normalZombieAnimator.StartAttack();
            runawayZombieAnimator.StartAttack();
            spitZombieAnimator.StartAttack();
            minionAnimator.StartAttack();
            boss1Animator.StartAttack();
            boss2Animator.StartAttack();
            boss3Animator.StartAttack();
        }
    }

    public void TestRunning(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            normalZombieAnimator.StartRunning();
            runawayZombieAnimator.StartRunning();
            minionAnimator.StartRunning();
            boss1Animator.StartRunning();
            boss2Animator.StartRunning();
            boss3Animator.StartRunning();
        }
    }

    public void TestIdle(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            normalZombieAnimator.StartIdle();
            runawayZombieAnimator.StartIdle();
            spitZombieAnimator.StartIdle();
            minionAnimator.StartIdle();
            boss1Animator.StartIdle();
            boss2Animator.StartIdle();
            boss3Animator.StartIdle();
        }
    }

    public void TestWalking(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            normalZombieAnimator.StartWalking();
            runawayZombieAnimator.StartWalking();
            spitZombieAnimator.StartWalking();
            boss1Animator.StartWalking();
            boss2Animator.StartWalking();
            boss3Animator.StartWalking();
        }
    }

    public void TestHowling(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            runawayZombieAnimator.StartHowling();
            minionAnimator.StartHowling();
            boss1Animator.StartHowling();
            boss2Animator.StartHowling();
            boss3Animator.StartHowling();
        }
    }

    public void TestDead(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            normalZombieAnimator.gameObject.GetComponent<RagdollMaster>().SetRagdollState(true);
            runawayZombieAnimator.gameObject.GetComponent<RagdollMaster>().SetRagdollState(true);
            spitZombieAnimator.gameObject.GetComponent<RagdollMaster>().SetRagdollState(true);
            minionAnimator.StartDead();
            boss1Animator.StartDead();
            boss2Animator.StartDead();
            boss3Animator.StartDead();
        }
    }

    public void TestEmmision(InputAction.CallbackContext context)
    {
        normalZombieAnimator.gameObject.GetComponent<DissolveOnDeath>().PlayDissolveAndHide();
        runawayZombieAnimator.gameObject.GetComponent<DissolveOnDeath>().PlayDissolveAndHide();
        spitZombieAnimator.gameObject.GetComponent<DissolveOnDeath>().PlayDissolveAndHide();
        minionAnimator.gameObject.GetComponent<DissolveOnDeath>().PlayDissolveAndHide();
        boss1Animator.gameObject.GetComponent<DissolveOnDeath>().PlayDissolveAndHide();
        boss2Animator.gameObject.GetComponent<DissolveOnDeath>().PlayDissolveAndHide();
        boss3Animator.gameObject.GetComponent<DissolveOnDeath>().PlayDissolveAndHide();
    }

    public void TestSpecial1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            boss1Animator.StartDownAttack();
            boss2Animator.StartBite();
            boss3Animator.StartSpit();
        }
    }

    public void TestSpecial2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            boss1Animator.StartStomping();
            boss3Animator.StartSpike();
        }
    }
}  
