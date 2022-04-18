using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;

    public int enemyCount;
    public int enemyWave = 1;

    public GameObject powerUpPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(enemyWave);
        Instantiate(powerUpPrefab, GnerateSpawnPosition(), powerUpPrefab.transform.rotation);
    }
    
    /// <summary>
    /// Genera una posición aleatoria dentro de la zona de juego
    /// </summary>
    /// <returns>Devuelve una posición</returns>
    private Vector3 GnerateSpawnPosition()
	{
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    /// <summary>
    /// Genera un número determinado de enemigos en pantalla
    /// </summary>
    /// <param name="numberOfEnemies">Número de enemigos a generar</param>

    private void SpawnEnemyWave(int numberOfEnemies)
	{
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, GnerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

	private void Update()
	{
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemyCount<=0)
		{
            enemyWave++;
            SpawnEnemyWave(enemyWave);

            int numberOfPowerUps = Random.Range(0, 3);

            for (int i = 0; i < numberOfPowerUps; i++)
            {
                Instantiate(powerUpPrefab, GnerateSpawnPosition(), powerUpPrefab.transform.rotation);
            }
        }
    }
}
