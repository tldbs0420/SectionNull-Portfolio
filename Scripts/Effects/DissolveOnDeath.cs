using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveOnDeath : MonoBehaviour
{
    [System.Serializable]
    public class RendererMatPair
    {
        public Renderer renderer;
        // renderer.materials 순서에 맞춰 넣는 배열
        // 디졸브 안 할 슬롯은 null
        public Material[] dissolveMats;
    }

    [SerializeField] private RendererMatPair[] originals;
    [SerializeField] private RendererMatPair[] targets;
    [SerializeField] private float dissolveTime = 1.0f;

    private const string DISSOLVE_AMOUNT = "_DissolveAmount";

    // 이 좀비 안에서 공유할 디졸브 인스턴스들
    private readonly List<Material> runtimeDissolveMats = new List<Material>();
    private readonly Dictionary<Material, Material> templateToInstance = new Dictionary<Material, Material>();

    void OnEnable()
    {
        foreach (RendererMatPair pair in originals)
        {
            if (!pair.renderer) continue;
            pair.renderer.materials = pair.dissolveMats;
        }
    }

    public void PlayDissolveAndHide()
    {
        StartCoroutine(DissolveCo());
    }

    private IEnumerator DissolveCo()
    {
        runtimeDissolveMats.Clear();
        templateToInstance.Clear();

        // 1) 각 렌더러의 materials 배열을 디졸브용으로 교체
        foreach (RendererMatPair pair in targets)
        {
            if (!pair.renderer) continue;

            Material[] original = pair.renderer.materials;
            Material[] newMats  = new Material[original.Length];

            for (int i = 0; i < original.Length; i++)
            {
                Material template = null;
                if (pair.dissolveMats != null &&
                    i < pair.dissolveMats.Length)
                {
                    template = pair.dissolveMats[i];
                }

                if (template != null)
                {
                    // 같은 템플릿을 쓰는 슬롯끼리는 이 좀비 안에서 인스턴스 공유
                    if (!templateToInstance.TryGetValue(template, out var inst))
                    {
                        inst = new Material(template);           // ← 이 좀비용 인스턴스 1개 생성
                        templateToInstance[template] = inst;

                        if (inst.HasProperty(DISSOLVE_AMOUNT))
                            inst.SetFloat(DISSOLVE_AMOUNT, 0f);

                        runtimeDissolveMats.Add(inst);
                    }

                    newMats[i] = inst;
                }
                else
                {
                    // 디졸브 안 할 슬롯은 원래 머티리얼 유지
                    newMats[i] = original[i];
                }
            }

            pair.renderer.materials = newMats;
        }

        // 2) 0 → 1로 디졸브 애니메이션
        float t = 0f;
        while (t < dissolveTime)
        {
            t += Time.deltaTime;
            float v = Mathf.Clamp01(t / dissolveTime);

            foreach (var m in runtimeDissolveMats)
            {
                if (m && m.HasProperty(DISSOLVE_AMOUNT))
                    m.SetFloat(DISSOLVE_AMOUNT, v);
            }

            yield return null;
        }

        // 3) 끝나면 오브젝트 처리
        // 이 부분은 오브젝트 풀에 반납하는거로 바꾸면 됨
        // gameObject.SetActive(false);
        // Destroy(gameObject);
    }
}
