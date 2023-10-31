using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{
    public Enemy EnemyPrefab;

    private GameManager instance => GameManager.instance;

    public void SpawnEnemies(DayPhase currentPhase)
    {
        if (currentPhase == DayPhase.night)
        {
            foreach(var enemyProperties in instance.enemies)
            {
                float angl = Random.Range(0,360);

                var enemy = Instantiate(EnemyPrefab, new Vector3(50 * Mathf.Cos(angl), 0, 50 * Mathf.Sin(angl)), Quaternion.identity);

                enemy.unitProperties = enemyProperties;
            }
        }
    }

    private void Start()
    {
        instance.dayChange += SpawnEnemies;
    }
}
