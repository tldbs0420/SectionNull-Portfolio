using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleReloadStrategy", menuName = "Scriptable Objects/IdleReloadStrategy")]
public class IdleReloadStrategy : ReloadStrategy
{
    public override IEnumerator ReloadCoroutine(PlayerWeapon wp)
    {
        yield return null;
    }
}
