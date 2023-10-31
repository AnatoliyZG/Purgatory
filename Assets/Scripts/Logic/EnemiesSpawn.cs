using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{
    public GameObject Enemy;

    private GameManager instance => GameManager.instance;

    public void SpawnEnemies(DayPhase currentPhase)
    {
        if(currentPhase==DayPhase.night)
            for (int i = 0; i < instance.enemies.Count; i++)
            {
                float angl = (360f / instance.enemies.Count) * i;

                GameObject enemy = Instantiate(Enemy, new Vector3(50 * Mathf.Cos(angl), 0, 50 * Mathf.Sin(angl)), Quaternion.identity);

                enemy.GetComponent<Enemy>().unitProperties = instance.enemies[i].GetComponent<Enemy>().unitProperties;
            }
    }

    private void Start()
    {
        instance.dayChange += SpawnEnemies;
    }
}
