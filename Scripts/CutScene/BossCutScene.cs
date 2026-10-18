using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class BossCutScene : MonoBehaviour
{
    [SerializeField] PlayableDirector farm;
    [SerializeField] PlayableDirector town;
    [SerializeField] PlayableDirector factory;

    public void StartCutscene(GameManager.Areas area)
    {
        Time.timeScale = 0f;
        switch (area)
        {
            case GameManager.Areas.Farm:
                farm.gameObject.SetActive(true);
                farm.Play();
                StartCoroutine(TimeFreeze(farm));
                break;
            case GameManager.Areas.Town:
                town.gameObject.SetActive(true);
                town.Play();
                StartCoroutine(TimeFreeze(town));
                break;
            case GameManager.Areas.Factory:
                factory.gameObject.SetActive(true);
                factory.Play();
                StartCoroutine(TimeFreeze(factory));
                break;
        }
    }

    IEnumerator TimeFreeze(PlayableDirector playableDirector)
    {
        while (playableDirector.state == PlayState.Playing)
            yield return null;
        Time.timeScale = 1f;
        if (playableDirector)
            playableDirector.gameObject.SetActive(false);
        // 컷씬 끝나면 다시 UI작동
        if (UIController.Instance != null)
        {
            UIController.Instance.PopBlock();
        }
    }

}
