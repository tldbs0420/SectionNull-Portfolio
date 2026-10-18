using UnityEngine;

public abstract class ZombieAnimator : MonoBehaviour
{
    // Run 애니메이션 보유 여부
    public virtual bool HasRunAnimation => false;

    // 기본 이동 상태 (모든 좀비 공통)
    public virtual void StartIdle() {}          // 대부분 존재
    public virtual void StartWalking() {}          // Normal/Runaway/Spit/Boss etc
    public virtual void StartRunning() {}           // 일부 일반 좀비는 없음, NO-OP 허용

    // 기본 공격 (모든 좀비 공통) 
    public virtual void StartAttack() {}        // 모든 좀비는 Attack 1개 이상 존재
    
    // 추가 공격 종류 (보스/특수 좀비) 
    public virtual void StartBite() {}    // Boss2 등
    public virtual void StartDownAttack() {}    // Boss1
    public virtual void StartSpit() {}    // SpitZombie, Boss3
    public virtual void StartStomping() {}   // Boss1 stomp 공격
    public virtual void StartSpike() {}   // Boss3 spike
        
    // 랜덤 공격이 필요한 경우
    public virtual void StartAttackRandom() {}  // Boss3 등에서 override 가능

    // 특수 행동 (보스/일부 일반)
    public virtual void StartHowling() {}       // Runaway, Boss들
    public virtual void StartRoar() {}          // Boss1/2/3에서 Howling이랑 분리 가능
    public virtual void StartBuff() {}          // Boss 특수 패턴 대비해 확장 가능

    // 상태 변화
    public virtual void StartHit() {}           // 피격 애니메이션 (필요시 확장)
    public virtual void StartDead() {}          // 대부분의 보스/좀비에 존재
    public virtual void StartDown() {}          // Boss1의 Down 상태 등

    // 기타 기능 (대형 NPC 대응)
    public virtual void StartTurnLeft() {}      // 일부 보스 회전 모션
    public virtual void StartTurnRight() {}
    
    // LOD 애니메이션이 필요한 좀비(Boss2)
    public virtual void SyncLOD() {}       // LOD1/LOD2 같이 제어할 때 override

    public virtual float GetDuration() { return 0f; } // 애니매이션 길이 반환
}
