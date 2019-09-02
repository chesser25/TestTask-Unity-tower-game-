using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	private int wavesCount;
	private int maxWaveCount;
	private int waitTimeBetweenEnemySpawns;
	public Wave[] waves;
	private IEnumerator coroutine;
	private List<GameObject> spawnedEnemies;

	void Awake()
	{
		wavesCount = 1;
		maxWaveCount = waves.Length;
		spawnedEnemies = new List<GameObject>();
	}
	void Start()
	{
		for(int i = 0; i < waves.Length; i++)
		{
			wavesCount++;
			Wave currentWave = waves[i];
			waitTimeBetweenEnemySpawns = (int) (currentWave.waveDuration / currentWave.enemiesCountPerWave);
			coroutine = CreateEnemies(currentWave);
			StartCoroutine(coroutine);
		}
	}

	IEnumerator CreateEnemies(Wave currentWave)
	{
		for(int i = 0; i < currentWave.enemiesCountPerWave; i++)
		{
			GameObject enemy = Instantiate(currentWave.enemyPrefab, currentWave.spawnPosition, currentWave.enemyPrefab.transform.rotation);
			spawnedEnemies.Add(enemy);
			yield return new WaitForSeconds(waitTimeBetweenEnemySpawns);
		}
	}

	public int WavesCount
	{
		get
		{
			return wavesCount;
		}
	}

	public int MaxWavesCount
	{
		get
		{
			return maxWaveCount;
		}
	}

	public GameObject[] SpawnedEnemies
	{
		get
		{
			return spawnedEnemies;
		}
	}
}
