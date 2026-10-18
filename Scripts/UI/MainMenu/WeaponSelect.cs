using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Localization.Settings;

public class WeaponSelect : MonoBehaviour
{
    const string UI_TABLE = "MainMenu";
    string[] weaponKeys = {
        "Assult",   
        "Shotgun",   
        "SMG",
        "Pistol"      
    };

    [SerializeField] WeaponButton[] mainWeapons;
    [SerializeField] WeaponButton[] subWeapons;
    [SerializeField] UISFXPlayer UISFXPlayer;
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI subText;
    [SerializeField] Button startButton;
    public void Init()
    {
        SelectWeapon(0, true);
        SelectWeapon(1, false);
        SetMainText("돌격 소총");
        SetSubText("산탄총");
    }

    public void SelectMain(int i) {
        UISFXPlayer.PlayClick();
        SelectWeapon(i, true);
    }
    public void SelectSub(int i) {
        UISFXPlayer.PlayClick();
        SelectWeapon(i, false);
    }

    public void SelectWeapon(int i, bool isMain)
    {
        WeaponButton[] btns;
        if (isMain) btns = mainWeapons;
        else btns = subWeapons;
        for (int j = 0; j < btns.Length; j++)
        {
            if (i == j) btns[j].Selected();
            else        btns[j].DeSelected();
        }
    }

    public void SetMainText(String text) {
        if (text == "돌격 소총")
            mainText.text = LocalizationSettings.StringDatabase
                .GetLocalizedString(UI_TABLE, weaponKeys[0]);
        else if (text == "산탄총")
            mainText.text = LocalizationSettings.StringDatabase
                .GetLocalizedString(UI_TABLE, weaponKeys[1]);
        else if (text == "기관단총")
            mainText.text = LocalizationSettings.StringDatabase
                .GetLocalizedString(UI_TABLE, weaponKeys[2]);
        else if (text == "권총")
            mainText.text = LocalizationSettings.StringDatabase
                .GetLocalizedString(UI_TABLE, weaponKeys[3]);
    }
    public void SetSubText(String text) {
        if (text == "돌격 소총")
            subText.text = LocalizationSettings.StringDatabase
                .GetLocalizedString(UI_TABLE, weaponKeys[0]);
        else if (text == "산탄총")
            subText.text = LocalizationSettings.StringDatabase
                .GetLocalizedString(UI_TABLE, weaponKeys[1]);
        else if (text == "기관단총")
            subText.text = LocalizationSettings.StringDatabase
                .GetLocalizedString(UI_TABLE, weaponKeys[2]);
        else if (text == "권총")
            subText.text = LocalizationSettings.StringDatabase
                .GetLocalizedString(UI_TABLE, weaponKeys[3]);
    }
    public void DIsableStartButton() => startButton.enabled = false;
}
