using System;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class OptionManager : MonoBehaviour
{
    private static OptionManager instance;
    public static OptionManager Instance { get { return instance; } }

    FullScreenMode fullScreenMode;

    [SerializeField] SoundManager soundManager;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] OptionUI optionUI;


    void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
    }

    public void Init()
    {
        if (!PlayerPrefs.HasKey("ScreenMode"))
        {
            string screenMode = "전체 화면";
            fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreenMode = fullScreenMode;
            PlayerPrefs.SetString("ScreenMode", screenMode);
        }
        else
        {
            string screenMode = PlayerPrefs.GetString("ScreenMode");
            if (screenMode == "전체 화면")
                fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            else if (screenMode == "경계없는 창모드")
                fullScreenMode = FullScreenMode.FullScreenWindow;
            else if (screenMode == "창모드")
                fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreenMode = fullScreenMode;
        }

        if (!PlayerPrefs.HasKey("Resolution"))
        {
            int width = 1920;
            int height = 1080;
            Screen.SetResolution(width, height, Screen.fullScreenMode);
            PlayerPrefs.SetString("Resolution", width + " x " + height);
        }
        else
        {
            string resolution = PlayerPrefs.GetString("Resolution");
            int width = int.Parse(resolution.Split(" x ")[0]);
            int height = int.Parse(resolution.Split(" x ")[1]);
            Screen.SetResolution(width, height, fullScreenMode);
        }

        if (!PlayerPrefs.HasKey("Hz"))
        {
            int hz = 144;
            Application.targetFrameRate = 144;
            PlayerPrefs.SetInt("Hz", hz);
        }
        else
        {
            int hz = PlayerPrefs.GetInt("Hz");
            Application.targetFrameRate = hz;
        }

        if (!PlayerPrefs.HasKey("Volume"))
        {
            float volume = 0.75f;
            if (soundManager != null)
                soundManager.SetVolume(volume);
            PlayerPrefs.SetFloat("Volume", volume);
        }
        else
        {
            float volume = PlayerPrefs.GetFloat("Volume");
            if (soundManager != null)
                soundManager.SetVolume(volume);
        }

        if (!PlayerPrefs.HasKey("MouseSensitivity"))
        {
            float sensitivity = 0.7f;
            PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
            if (playerMovement != null)
                playerMovement.MouseSensitivity = sensitivity;
        }

        if (!PlayerPrefs.HasKey("Graphic"))
        {
            string graphic = "매우 낮음";
            PlayerPrefs.SetString("Graphic", graphic);
            ChangeQuality(graphic);
        }
        else
        {
            string graphic = PlayerPrefs.GetString("Graphic");
            ChangeQuality(graphic);
        }

        if (!PlayerPrefs.HasKey("Locale"))
        {
            string locale = "한국어 (ko)";
            PlayerPrefs.SetString("Locale", locale);
            SetLocale(locale);
        }
        else
        {
            string locale = PlayerPrefs.GetString("Locale");
            SetLocale(locale);
        }
    }

    public void ConnectGame(SoundManager soundManager, PlayerMovement playerMovement, OptionUI optionUI)
    {
        this.soundManager = soundManager;
        this.playerMovement = playerMovement;
        this.optionUI = optionUI;
        if (soundManager != null)
            soundManager.SetVolume(PlayerPrefs.GetFloat("Volume"));
        if (playerMovement != null)
            playerMovement.MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        if (optionUI != null)
            optionUI.UISFX.GetAudioSource().volume = PlayerPrefs.GetFloat("Volume");
    }

    public void SetResolution(string resolution)
    {
        int width = int.Parse(resolution.Split(" x ")[0]);
        int height = int.Parse(resolution.Split(" x ")[1]);
        Screen.SetResolution(width, height, fullScreenMode);
        PlayerPrefs.SetString("Resolution", width + " x " + height);
    }

    public void SetScreenMode(string screenMode)
    {
        if (screenMode == "전체 화면")
            fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        else if (screenMode == "경계없는 창모드")
            fullScreenMode = FullScreenMode.FullScreenWindow;
        else if (screenMode == "창모드")
            fullScreenMode = FullScreenMode.Windowed;
        Screen.fullScreenMode = fullScreenMode;
        PlayerPrefs.SetString("ScreenMode", screenMode);
    }

    public void SetHz(int hz)
    {
        Application.targetFrameRate = hz;
        PlayerPrefs.SetInt("Hz", hz);
    }

    public void SetGraphic(string graphic)
    {
        PlayerPrefs.SetString("Graphic", graphic);
        ChangeQuality(graphic);
    }

    public void SetVolume(float volume)
    {
        if (soundManager != null)
            soundManager.SetVolume(volume);
        if (optionUI != null)
            optionUI.UISFX.GetAudioSource().volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetSensitivity(float sensitivity)
    {
        if (playerMovement != null)
            playerMovement.MouseSensitivity = sensitivity;
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
    }

    public void SetLocale(string locale)
    {
        PlayerPrefs.SetString("Locale", locale);
        int index = 0;
        switch (locale)
        {
            case "한국어 (ko)":
                index = 1;
                break;
            case "English (en)":
                index = 0;
                break;
            default:
                index = 1;
                break;
        }
        LocalizationSettings.SelectedLocale = 
		    LocalizationSettings.AvailableLocales.Locales[index];
    }

    private void ChangeQuality(String quality)
    {
        switch(quality)
        {
            case "매우 낮음":
                QualitySettings.SetQualityLevel(0, true);
                break;
            case "낮음":
                QualitySettings.SetQualityLevel(1, true);
                break;
            case "보통":
                QualitySettings.SetQualityLevel(2, true);
                break;
            case "높음":
                QualitySettings.SetQualityLevel(3, true);
                break;
            default:
                QualitySettings.SetQualityLevel(0, true);
                break;
        }
    }
}
