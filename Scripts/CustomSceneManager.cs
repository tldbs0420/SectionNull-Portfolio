using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager Instance;
    string[] weaponNames;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }

        DontDestroyOnLoad(this);
        weaponNames = new string[2];
        weaponNames[0] = "AKM";
        weaponNames[1] = "SPAS";
    }
    
    public void SetMainWeapon(String main) => weaponNames[0] = main;
    public void SetSubWeapon(String sub) => weaponNames[1] = sub;

    public void StartGame()
    {
        StartCoroutine(LoadInGame());
    }

    public void GoToMain()
    {
        Destroy(OptionManager.Instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator LoadInGame()
    {
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync("InGame");
        while (!asyncOper.isDone)
            yield return null;
        GameManager.Instance.InitGame(weaponNames[0], weaponNames[1]);
    }
}
