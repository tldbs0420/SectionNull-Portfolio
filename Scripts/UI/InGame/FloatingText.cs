using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private TextMeshProUGUI text;   // 연결할 TMP 텍스트
    [SerializeField] private float duration = 1f;    // 전체 연출 시간
    [SerializeField] private float moveY = 50f;      // 위로 이동할 거리 (UI 좌표 기준)

    private RectTransform rectTransform;
    private Color originalColor;

    private void Awake()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();

        rectTransform = text.GetComponent<RectTransform>();
        originalColor = text.color;
    }

    private void OnEnable()
    {
        // 활성화될 때마다 연출 시작
        StartCoroutine(PlayAnimation());
    }

    public void SetText(string message)
    {
        if (text != null)
            text.text = message;
    }

    private IEnumerator PlayAnimation()
    {
        float elapsed = 0f;
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 endPos = startPos + new Vector2(0f, moveY);

        // 시작 시 알파 1로 리셋
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        while (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // 위치 Lerp
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            // 알파 Lerp (1 → 0)
            float alpha = Mathf.Lerp(1f, 0f, t);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            yield return null;
        }

        // 연출이 끝나면 오브젝트 제거 또는 비활성화
        Destroy(gameObject);
        // 또는 gameObject.SetActive(false);
    }
}