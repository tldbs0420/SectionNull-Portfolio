using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{

    string[] screenModeKeys = {
        "FullScreen",   
        "Borderless",   
        "Windowed"      
    };

    string[] graphicKeys = {
    "VeryLow",
    "Low",
    "Med",
    "High"
    };

    const string UI_TABLE = "Options";

    string screenMode;
    string resolution;
    int hz;
    float volume;
    float mouseSensitivity;
    string graphic;
    string locale;

    string[] screenModes = { "전체 화면", "경계없는 창모드", "창모드" };
    List<Resolution> resolutions169 = new List<Resolution>();
    int[] hzValues = { 60, 75, 90, 120, 144, 165, 180, 240, 0 };
    string[] graphics = { "매우 낮음", "낮음", "보통", "높음" };
    string[] locales = { "English (en)", "한국어 (ko)" };

    int indexScreenMode;
    int indexResolution;
    int indexHz;
    int indexGraphic;

    [SerializeField] public UISFXPlayer UISFX;
    [SerializeField] TextMeshProUGUI screenModeText;
    [SerializeField] TextMeshProUGUI resolutionText;
    [SerializeField] TextMeshProUGUI hzText;
    [SerializeField] TextMeshProUGUI graphicText;
    [SerializeField] TextMeshProUGUI localeText;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sensitivitySlider;
    [SerializeField] TextMeshProUGUI sensitivityText;

    [SerializeField] Button[] screenModeButtons;
    [SerializeField] Button[] resolutionButtons;
    [SerializeField] Button[] hzButtons;
    [SerializeField] Button[] graphicButtons;
    [SerializeField] Button[] LocaleButtons;
    [SerializeField] GameObject ReallyResetOption;
    [SerializeField] GameObject ReallyBackToMain;
    [SerializeField] GameObject ReallyExitGame;
    [SerializeField] Transform appliedAppearPosition;
    [SerializeField] GameObject applied;

    public void Init()
    {
        screenMode = PlayerPrefs.GetString("ScreenMode");
        resolution = PlayerPrefs.GetString("Resolution");
        hz = PlayerPrefs.GetInt("Hz");
        volume = PlayerPrefs.GetFloat("Volume");
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        graphic = PlayerPrefs.GetString("Graphic");
        locale = PlayerPrefs.GetString("Locale");

        screenModeButtons[0].interactable = true;
        screenModeButtons[1].interactable = true;
        resolutionButtons[0].interactable = true;
        resolutionButtons[1].interactable = true;
        hzButtons[0].interactable = true;
        hzButtons[1].interactable = true;

        indexScreenMode = 0;
        for (int i = 0; i < screenModes.Length; i++)
        {
            if (screenModes[i] == screenMode)
            {
                indexScreenMode = i;
                break;
            }
        }
        screenModeText.text = screenModes[indexScreenMode];
        if (indexScreenMode == 0) { screenModeButtons[0].interactable = false; }
        else if (indexScreenMode == screenModes.Length - 1) { screenModeButtons[1].interactable = false; }

        Resolution[] resolutions = Screen.resolutions;
        resolutions169 = new List<Resolution>();
        foreach (Resolution res in resolutions)
        {
            float aspectRatio = (float)res.width / res.height;
            if (Mathf.Approximately(aspectRatio, 16f / 9f))
            {
                bool isThere = false;
                foreach (Resolution resolution in resolutions169)
                {
                    if (resolution.width == res.width && resolution.height == res.height)
                        isThere = true;
                }
                if (!isThere)
                    resolutions169.Add(res);
            }
        }
        resolutions = null;
        int width = int.Parse(resolution.Split(" x ")[0]);
        int height = int.Parse(resolution.Split(" x ")[1]);
        indexResolution = 0;
        for (int i = 0; i < resolutions169.Count; i++)
        {
            if (resolutions169[i].width == width && resolutions169[i].height == height)
            {
                indexResolution = i;
                break;
            }
        }
        resolutionText.text = width + " x " + height;
        if (indexResolution == 0) { resolutionButtons[0].interactable = false; }
        else if (indexResolution == resolutions169.Count - 1) { resolutionButtons[1].interactable = false; }

        indexHz = 0;
        for (int i = 0; i < hzValues.Length; i++)
        {
            if (hzValues[i] == hz)
            {
                indexHz = i;
                break;
            }
        }

        if (indexHz == 0) { hzButtons[0].interactable = false; }
        else if (indexHz == hzValues.Length - 1) { hzButtons[1].interactable = false; }

        indexGraphic = 0;
        for (int i = 0; i < graphics.Length; i++)
        {
            if (graphics[i] == graphic)
            {
                indexGraphic = i;
                break;
            }
        }
        graphicText.text = graphic;
        if (indexGraphic == 0) { graphicButtons[0].interactable = false; }
        else if (indexGraphic == graphics.Length - 1) { graphicButtons[1].interactable = false; }

        volumeSlider.value = volume;
        sensitivitySlider.value = mouseSensitivity;
        sensitivityText.text = mouseSensitivity.ToString();

        localeText.text = locale;
        OptionManager.Instance.SetLocale(locale);
        UpdateTexts();
    }

    public void ChangeScreenMode(bool right)
    {
        UISFX.PlayClick();
        if (right && indexScreenMode < screenModes.Length - 1)
        {
            if (indexScreenMode == 0)
                screenModeButtons[0].interactable = true;
            else if (indexScreenMode == screenModes.Length - 2)
                screenModeButtons[1].interactable = false;
            indexScreenMode += 1;
        }
        if (!right && indexScreenMode > 0)
        {
            if (indexScreenMode == screenModes.Length - 1)
                screenModeButtons[1].interactable = true;
            else if (indexScreenMode == 1)
                screenModeButtons[0].interactable = false;
            indexScreenMode -= 1;
        }
        screenMode = screenModes[indexScreenMode];
        screenModeText.text = screenMode;
        UpdateScreenModeText();
    }

    public void ChangeResolution(bool right)
    {
        UISFX.PlayClick();
        if (right && indexResolution < resolutions169.Count - 1)
        {
            if (indexResolution == 0)
                resolutionButtons[0].interactable = true;
            else if (indexResolution == resolutions169.Count - 2)
                resolutionButtons[1].interactable = false;
            indexResolution += 1;
        }
        if (!right && indexResolution > 0)
        {
            if (indexResolution == resolutions169.Count - 1)
                resolutionButtons[1].interactable = true;
            else if (indexResolution == 1)
                resolutionButtons[0].interactable = false;
            indexResolution -= 1;
        }
        resolution = resolutions169[indexResolution].width + " x " + resolutions169[indexResolution].height;
        resolutionText.text = resolution;
    }

    public void ChangeHz(bool right)
    {
        UISFX.PlayClick();
        if (right && indexHz < hzValues.Length - 1)
        {
            if (indexHz == 0)
                hzButtons[0].interactable = true;
            else if (indexHz == hzValues.Length - 2)
                hzButtons[1].interactable = false;
            indexHz += 1;
        }
        if (!right && indexHz > 0)
        {
            if (indexHz == hzValues.Length - 1)
                hzButtons[1].interactable = true;
            else if (indexHz == 1)
                hzButtons[0].interactable = false;
            indexHz -= 1;
        }

        hz = hzValues[indexHz];

        UpdateHzText();
    }

    public void ChangeGraphic(bool right)
    {
        UISFX.PlayClick();
        if (right && indexGraphic < graphics.Length - 1)
        {
            if (indexGraphic == 0)
                graphicButtons[0].interactable = true;
            else if (indexGraphic == graphics.Length - 2)
                graphicButtons[1].interactable = false;
            indexGraphic += 1;
        }
        if (!right && indexGraphic > 0)
        {
            if (indexGraphic == graphics.Length - 1)
                graphicButtons[1].interactable = true;
            else if (indexGraphic == 1)
                graphicButtons[0].interactable = false;
            indexGraphic -= 1;
        }
        graphic = graphics[indexGraphic];
        graphicText.text = graphic;
        UpdateGraphicText();
    }

    public void ChangeVolume(float value)
    {
        Debug.Log("Volume Slided");
        volume = value;
    }

    public void ChangeSensitivity(float value)
    {
        Debug.Log("Sensitivity Slided");
        mouseSensitivity = value;
        sensitivityText.text = mouseSensitivity.ToString();
    }

    public void ChangeLocale(bool right)
    {
        if (localeText.text == locales[1])
        {
            localeText.text = locales[0];
            locale = locales[0];
            OptionManager.Instance.SetLocale(locale);
        }
        else
        {
            localeText.text = locales[1];
            locale = locales[1];
            OptionManager.Instance.SetLocale(locale);
        }
        UpdateTexts();
    }

    public void ApplySetting()
    {
        UISFX.PlayClick();
        Instantiate(applied, appliedAppearPosition);
        OptionManager.Instance.SetScreenMode(screenMode);
        OptionManager.Instance.SetResolution(resolution);
        OptionManager.Instance.SetHz(hz);
        OptionManager.Instance.SetVolume(volume);
        OptionManager.Instance.SetGraphic(graphic);
        OptionManager.Instance.SetSensitivity(mouseSensitivity);
        UpdateTexts();
    }

    public void ResetOption()
    {
        UISFX.PlayClick();
        OptionManager.Instance.SetScreenMode("전체 화면");
        OptionManager.Instance.SetResolution("1920 x 1080");
        OptionManager.Instance.SetHz(144);
        OptionManager.Instance.SetVolume(0.75f);
        OptionManager.Instance.SetGraphic("매우 낮음");
        OptionManager.Instance.SetSensitivity(0.7f);

        screenModeButtons[0].interactable = false;
        screenModeButtons[1].interactable = true;
        resolutionButtons[0].interactable = true;
        resolutionButtons[1].interactable = true;
        hzButtons[0].interactable = true;
        hzButtons[1].interactable = true;
        graphicButtons[0].interactable = false;
        graphicButtons[1].interactable = true;

        screenModeText.text = screenModes[0];
        indexScreenMode = 0;
        screenMode = screenModes[indexScreenMode];
        int width = 1920;
        int height = 1080;
        resolution = "1920 x 1080";
        indexResolution = 0;
        for (int i = 0; i < resolutions169.Count; i++)
        {
            if (resolutions169[i].width == width && resolutions169[i].height == height)
            {
                indexResolution = i;
                break;
            }
        }
        resolutionText.text = width + " x " + height;
        hzText.text = "144 Hz";
        hz = 144;
        indexHz = 4;
        graphic = "매우 낮음";
        graphicText.text = graphic;
        indexGraphic = 0;

        volume = 0.75f;
        volumeSlider.value = volume;

        mouseSensitivity = 0.7f;
        sensitivitySlider.value = mouseSensitivity;
        sensitivityText.text = mouseSensitivity.ToString();

        CloseReallyResetOption();
    }

    public void OpenReallyResetOption()
    {
        UISFX.PlayClick();
        if (IsReallyExitGameOpened()) CloseReallyExitGame();
        if (IsReallyBackToMainOpened()) CloseReallyBackToMain();
        ReallyResetOption.SetActive(true);
    }

    public void CloseReallyResetOption()
    {
        ReallyResetOption.SetActive(false);
    }

    public void OpenReallyBackToMain()
    {
        UISFX.PlayClick();
        if (IsReallyResetOptionOpened()) CloseReallyResetOption();
        if (IsReallyExitGameOpened()) CloseReallyExitGame();
        ReallyBackToMain.SetActive(true);
    }

    public void CloseReallyBackToMain()
    {
        ReallyBackToMain.SetActive(false);
    }

     public void OpenReallyExitGame()
    {
        UISFX.PlayClick();
        if (IsReallyResetOptionOpened()) CloseReallyResetOption();
        if (IsReallyBackToMainOpened()) CloseReallyBackToMain();
        ReallyExitGame.SetActive(true);
    }

    public void CloseReallyExitGame()
    {
        ReallyExitGame.SetActive(false);
    }

    public void ExitGame()
    {
   #if UNITY_EDITOR
    // 에디터에서는 플레이 모드 종료
    UnityEditor.EditorApplication.isPlaying = false;
    #else
        // 빌드에서는 게임 종료
        Application.Quit();
    #endif
    }

    public void BackToMain()
    {
        CustomSceneManager.Instance.GoToMain();
    }

    public bool IsReallyResetOptionOpened()
    {
        if (ReallyResetOption == null) return false;
        return ReallyResetOption.activeSelf;
    }

    public bool IsReallyExitGameOpened()
    {
        if (ReallyExitGame == null) return false;
        return ReallyExitGame.activeSelf;
    }

    public bool IsReallyBackToMainOpened()
    {
        if (ReallyBackToMain == null) return false;
        return ReallyBackToMain.activeSelf;
    }

    private void UpdateTexts()
    {
        UpdateScreenModeText();
        UpdateHzText();
        UpdateGraphicText();
    }

    private void UpdateScreenModeText()
    {
        string key = screenModeKeys[indexScreenMode];

        screenModeText.text = LocalizationSettings.StringDatabase
        .GetLocalizedString(UI_TABLE, key);
    }

    private void UpdateHzText()
    {
        if (hzValues[indexHz] == 0)
        {
            hzText.text = LocalizationSettings.StringDatabase
                .GetLocalizedString("Options", "NoRestric");
        }
        else
        {
            hzText.text = hzValues[indexHz] + " Hz";
        }
    }

    private void UpdateGraphicText()
    {
        string key = graphicKeys[indexGraphic];

        graphicText.text = LocalizationSettings.StringDatabase
            .GetLocalizedString(UI_TABLE, key);
    }
}
