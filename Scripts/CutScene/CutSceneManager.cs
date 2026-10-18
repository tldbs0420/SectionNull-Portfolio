using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] PlayableDirector start;
    [SerializeField] BossCutScene boss1;
    [SerializeField] BossCutScene boss2;
    [SerializeField] BossCutScene boss3;
    [SerializeField] PlayableDirector win;
    [SerializeField] PlayableDirector lose;
    [SerializeField] WeaponUpgradeUIController[] laptops;
    [SerializeField] Tutorial tutorial;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;

    public void PlayCutScene(int i)
    {
        PlayableDirector cutScene;
        if (i == 0)         cutScene = start;
        else if (i == 1)    cutScene = win;
        else                cutScene = lose;
        // 컷씬 동안 U로 UI여는거 막는 코드입니다
        if (UIController.Instance != null)
        {
            UIController.Instance.ForceClose();
            UIController.Instance.PushBlock();
        }
        Time.timeScale = 0f;
        cutScene.gameObject.SetActive(true);
        cutScene.Play();
        if (cutScene == start)
            StartCoroutine(TimeFreeze(cutScene, true));
        else
            StartCoroutine(TimeFreeze(cutScene, false));
    }

    public void PlayBossSpawn(int i, GameManager.Areas area)
    {
        BossCutScene bossCutScene;
        if (i == 1)         bossCutScene = boss1;
        else if (i == 2)    bossCutScene = boss2;
        else                bossCutScene = boss3;
        // 컷씬 동안 U로 UI여는거 막는 코드입니다옹
        if (UIController.Instance != null)
        {
            UIController.Instance.ForceClose();
            UIController.Instance.PushBlock();
        }
        foreach (WeaponUpgradeUIController laptop in laptops)
        {
            if (laptop.getOpen())
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                laptop.CloseUI();
            }
        }
        bossCutScene.StartCutscene(area);
    }

    IEnumerator TimeFreeze(PlayableDirector playableDirector, bool b)
    {
        yield return null;
        double margin = 0.1f;
        double stopTime = playableDirector.duration - margin;
        if (stopTime < 0) stopTime = 0;

        while (playableDirector.state == PlayState.Playing)
        {
            if (!b && playableDirector.time > stopTime) break;
            yield return null;
        }
        if (b)
        {
            Time.timeScale = 1f;
            if (playableDirector)
                playableDirector.gameObject.SetActive(false);
            // 컷씬 끝나면 다시 UI작동
            if (UIController.Instance != null)
            {
                UIController.Instance.PopBlock();
            }
            if (playableDirector == start) tutorial.StartTultip();
        }
        else if (!b)
        {
            playableDirector.time = stopTime;
            playableDirector.Evaluate();
            playableDirector.Pause();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (playableDirector == win) WinPanel.SetActive(true);
            else if (playableDirector == lose) LosePanel.SetActive(true);
        }
    }
}
