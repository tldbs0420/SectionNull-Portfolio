using System.Collections;
using UnityEngine;

public class MainMenuBG : MonoBehaviour
{
    [Header("회전 각도 (Y축 기준)")]
    [SerializeField] private float fromY = -7.5f;  // 시작 각도
    [SerializeField] private float toY   = -35f;   // 끝 각도

    [Header("이동 / 멈춤 시간")]
    [SerializeField] private float moveDuration   = 3f; // 한쪽으로 이동하는 데 걸리는 시간
    [SerializeField] private float pauseAtFrom    = 2f; // fromY에서 쉬는 시간
    [SerializeField] private float pauseAtTo      = 2f; // toY에서 쉬는 시간

    private Coroutine loopRoutine;
    [Header("다른 것들")]
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject[] gameObjects;

    void Awake()
    {
        int rand = Random.Range(0, 3);
        gameObjects[rand].SetActive(true);

        Vector3 e = transform.eulerAngles;
        e.y = fromY;
        mainCamera.transform.eulerAngles = e;

        loopRoutine = StartCoroutine(RotateLoop());
    }

    private IEnumerator RotateLoop()
    {
        while (true)
        {
            // fromY → toY
            yield return MoveY(fromY, toY);
            if (pauseAtTo > 0f)
                yield return new WaitForSeconds(pauseAtTo);

            // toY → fromY
            yield return MoveY(toY, fromY);
            if (pauseAtFrom > 0f)
                yield return new WaitForSeconds(pauseAtFrom);
        }
    }

    private IEnumerator MoveY(float startY, float endY)
    {
        if (moveDuration <= 0f)
        {
            // duration 0이면 그냥 바로 세팅
            Vector3 instant = transform.eulerAngles;
            instant.y = endY;
            mainCamera.transform.eulerAngles = instant;
            yield break;
        }

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / moveDuration;
            float clamped = Mathf.Clamp01(t);

            // 양 끝에서 천천히, 중간에서 빠르게 (ease in/out)
            float eased = Mathf.SmoothStep(0f, 1f, clamped);

            float currentY = Mathf.Lerp(startY, endY, eased);

            Vector3 e = transform.eulerAngles;
            e.y = currentY;
            mainCamera.transform.eulerAngles = e;

            yield return null;
        }
    }
}
