using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    [SerializeField] GameObject outline;

    public void Selected()
    {
        outline.SetActive(true);
    }

    public void DeSelected()
    {
        outline.SetActive(false);
    }
}
