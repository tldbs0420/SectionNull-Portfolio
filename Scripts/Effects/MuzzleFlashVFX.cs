using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class MuzzleFlashVFX : MonoBehaviour
{
    private VisualEffect muzzleVFX;
    private float fireDelay;
    private GameObject lights;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        muzzleVFX = GetComponent<VisualEffect>();
        fireDelay = muzzleVFX.GetFloat("FireDelay");
        fireDelay -= 0.1f;
        lights = transform.GetChild(0).gameObject;
        muzzleVFX.Play();
    }
}
