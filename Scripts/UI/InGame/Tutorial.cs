using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject[] Panels;
    [SerializeField] float waitTime = 7.5f;

    public void StartTultip()
    {
        StartCoroutine(ShowTultip());
    }

    IEnumerator ShowTultip()
    {
        Panels[0].SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Panels[0].SetActive(false);
        Panels[1].SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Panels[1].SetActive(false);
        Panels[2].SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Panels[2].SetActive(false);
    }
}
