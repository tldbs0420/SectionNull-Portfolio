using System.Collections;
using Unity.Collections;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void testload() { animator.SetTrigger(triggers[2]); }
    private string[] triggers = { "Equip", "Unequip", "Fire", 
                                  "Reload", "Insert", "ReloadEnd" };


    // Hash 캐싱
    public static readonly int unequipID = Animator.StringToHash("Unequip");
    public static readonly int equipID = Animator.StringToHash("Equip");
    public static readonly int fireID = Animator.StringToHash("Fire");
    public static readonly int reloadID = Animator.StringToHash("Reload");
    public static readonly int insertID = Animator.StringToHash("Insert");
    public static readonly int reloadEndID = Animator.StringToHash("ReloadEnd");
    public static readonly int reloadSpeedID = Animator.StringToHash("ReloadSpeed");

    public void StartUnequip() { animator.SetTrigger(triggers[1]); }
    public void StartReload() { animator.SetTrigger(triggers[3]); }
    public void TriggerAnimate(int ID) { animator.SetTrigger(ID); }
    public void SetReloadSpeed(float amount) {animator.SetFloat(reloadSpeedID, amount);}
}
