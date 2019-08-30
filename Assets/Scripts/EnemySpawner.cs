using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	// Singleton
	private EnemySpawner() {}
	private static EnemySpawner instance = null;
	public static EnemySpawner Instance
	{
		get
		{
			if(instance == null)
				instance = new EnemySpawner();
			return instance;
		}
	}

	public int waitTimeToSpawn = 0.0f;
	public int waveDuration = 30; // In seconds
	public int enemiesCountPerWave = 1;
	public int wavesCount = 0;
	public int maxWaveCount = 100;
	public GameObject enemyPrefab;
	public Transform [] wayPoints;

	public void CreateEnemies()
	{
		if(wavesCount == maxWaveCount)
			return;
		wavesCount++;
		InvokeRepeating("Create", waitTimeToSpawn, 0.3F);
	}

	private void Create()
	{
		for(int i = 0; i < enemiesCountPerWave; i++)
		{
			GameObject enemy = Instantiate(enemyPrefab, wayPoints[0].position, enemyPrefab.transform.rotation);
			yield return new WaitForSeconds();
		}
	}
}
