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

	public int waveDuration = 20; // In seconds
	public int enemiesCountPerWave = 1;
	public int wavesCount = 0;
	public int maxWaveCount = 100;
	public void CreateEnemies(GameObject enemyPrefab, Transform [] wayPoints)
	{
		wavesCount++;
		for(int i = 0; i < enemiesCountPerWave; i++)
		{
			GameObject enemy = Instantiate(enemyPrefab, wayPoints[0].position, enemyPrefab.transform.rotation);
		}
	}
	private bool isWaveDurationExpired()
	{
		return false;
	}
}
