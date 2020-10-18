using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject prefabEnemy;
    public GameObject prefabPowerUp;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int WaveNumber = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(WaveNumber);
        Instantiate(prefabPowerUp, GenerationSpawnPosition(), prefabPowerUp.transform.rotation);
    }

    void Update()
    {
        //How many enemy left in the scene
        enemyCount = FindObjectsOfType<Enemy>().Length;    
        if (enemyCount == 0)
        {
            //how many enemy will be created
            WaveNumber++;
            SpawnEnemyWave(WaveNumber);

            //Add one PowerUp prefab
            Instantiate(prefabPowerUp, GenerationSpawnPosition(), prefabPowerUp.transform.rotation);
        }
    }

    void SpawnEnemyWave(int spawnToEnemy)
    {
        for (int i = 0; i < spawnToEnemy; i++)
        {
            Instantiate(prefabEnemy, GenerationSpawnPosition(), prefabEnemy.transform.rotation);
        }
    }

    // Update is called once per frame
    private Vector3 GenerationSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3 (spawnPosX, 0, spawnPosZ);
        return spawnPos;

    }
}
