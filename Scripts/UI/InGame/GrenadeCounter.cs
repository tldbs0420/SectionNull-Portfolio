using TMPro;
using UnityEngine;

public class GrenadeCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI count;
    [SerializeField] PlayerInventory inventory;
    // Update is called once per frame
    void Update()
    {
        count.text = inventory.GetGrenade().ToString();
    }
}
