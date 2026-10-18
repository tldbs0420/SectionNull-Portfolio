using UnityEngine;

[CreateAssetMenu(fileName = "IdleFireStrategy", menuName = "Scriptable Objects/Weapon/FireStrategy/IdleFireStrategy")]
public class IdleFireStrategy : FireStrategy
{
    public override bool Fire(PlayerWeapon context)
    {
        // PlayerWeapon의 발사 후 로직은 다음으로 실행
        // if (ableFire && fireStg.Fire(this))
        // false 리턴으로 후처리 로직 (효과음 애니메이션 재생 방지)
        return false;
    }

    public override void Release(PlayerWeapon context)
    {
        return;
    }
}
