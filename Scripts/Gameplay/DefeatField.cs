using System.Collections.Generic;
using UnityEngine;

public class DefeatField : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies;

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (!enemies.Contains(enemy))
                enemies.Add(enemy);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (enemies.Contains(enemy))
                enemies.Remove(enemy);
        }  
    }

    [SerializeField] int loseCount = 50;

    void Update()
    {
        enemies.RemoveAll(e => e == null || !e.gameObject.activeInHierarchy || e.GetComponent<Enemy>().IsDead());
        
        if (enemies.Count >= loseCount)
        {
            GameManager.Instance.cutSceneManager.PlayCutScene(2);
        }
    }
}
