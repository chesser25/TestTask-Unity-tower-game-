using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	private int wavesCount = 1;
	private int maxWaveCount;
	private int waitTimeBetweenEnemySpawns;
	public Wave[] waves;

	private IEnumerator coroutine;

	void Awake()
	{
		maxWaveCount = waves.Length;
	}
	void Start()
	{
		for(int i = 0; i < waves.Length; i++)
		{
			Wave currentWave = waves[i];
			waitTimeBetweenEnemySpawns = (int) (currentWave.waveDuration / currentWave.enemiesCountPerWave);
			wavesCount++;
			coroutine = CreateEnemies(currentWave);
			StartCoroutine(coroutine);
		}
	}

	IEnumerator CreateEnemies(Wave currentWave)
	{
		for(int i = 0; i < currentWave.enemiesCountPerWave; i++)
		{
			GameObject enemy = Instantiate(currentWave.enemyPrefab, currentWave.spawnPosition, currentWave.enemyPrefab.transform.rotation);
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
}
