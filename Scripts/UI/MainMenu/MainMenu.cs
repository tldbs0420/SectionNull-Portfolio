using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject MainLayout;
    [SerializeField] GameObject weaponSelect;
    [SerializeField] GameObject Options;
    [SerializeField] GameObject Credit;
    [SerializeField] GameObject ReallyExitGame;
    [SerializeField] GameObject Multiplay;
    [SerializeField] UISFXPlayer UISFXPlayer;

    void Start()
    {
        OptionManager.Instance.Init();
        Options.GetComponent<OptionUI>().Init();
        OptionManager.Instance.ConnectGame(null, null, Options.GetComponent<OptionUI>());
        BackToMain();

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.loop = true;
            audio.Play(); 
        }
    }

    public void BackToMain()
    {
        UISFXPlayer.PlayClick();
        MainLayout.SetActive(true);
        weaponSelect.SetActive(false);
        Options.SetActive(false);
        Credit.SetActive(false);
        ReallyExitGame.SetActive(false);
        Multiplay.SetActive(false);
    }

    public void OpenWeaponSelect()
    {
        UISFXPlayer.PlayClick();
        weaponSelect.SetActive(true);
        weaponSelect.GetComponent<WeaponSelect>().Init();
        Options.SetActive(false);
        MainLayout.SetActive(false);
        Credit.SetActive(false);
        ReallyExitGame.SetActive(false);
        Multiplay.SetActive(false);
    }

    public void OpenOptions()
    {
        UISFXPlayer.PlayClick();
        if (Options.GetComponent<OptionUI>().IsReallyResetOptionOpened()) Options.GetComponent<OptionUI>().CloseReallyResetOption();
        Options.GetComponent<OptionUI>().Init();
        Options.SetActive(true);
        weaponSelect.SetActive(false);
        MainLayout.SetActive(false);
        Credit.SetActive(false);
        ReallyExitGame.SetActive(false);
        Multiplay.SetActive(false);
    }

    public void OpenCredit()
    {
        UISFXPlayer.PlayClick();
        MainLayout.SetActive(false);
        weaponSelect.SetActive(false);
        Options.SetActive(false);
        Credit.SetActive(true);
        ReallyExitGame.SetActive(false);
        Multiplay.SetActive(false);
    }

    public void OpenMultiPlay() {
        UISFXPlayer.PlayClick();
        ReallyExitGame.SetActive(false);
        Multiplay.SetActive(true);
    }

    public void CloseMultiPlay() {
        UISFXPlayer.PlayClick();
        ReallyExitGame.SetActive(false);
        Multiplay.SetActive(false);
    }

    public void OpenReallyExitGame() {
        UISFXPlayer.PlayClick();
        Multiplay.SetActive(false);
        ReallyExitGame.SetActive(true);
    }
    public void CloseReallyExitGame() {
        UISFXPlayer.PlayClick();
        Multiplay.SetActive(false);
        ReallyExitGame.SetActive(false);
    }
}
