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

	public GameObject enemyPrefab;
	public int waveDuration = 20; // In seconds
	public int enemiesCountPerWave = 20;
	private void CreateEnemies()
	{
		for(int i = 0; i < enemiesCountPerWave; i++)
		{
			GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, enemyPrefab.transform.rotation);
		}
	}

	private void MoveEnemy()
	{

	}

	private bool isWaveDurationExpired()
	{
		return false;
	}
}
