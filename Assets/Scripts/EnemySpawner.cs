using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace TowerGame
{
	// Class to produce enemies
	public class EnemySpawner : MonoBehaviour
	{
		// Waves parameters
		private int wavesCount;
		private int maxWaveCount;
		private int waitTimeBetweenEnemySpawns;
		public Wave[] waves;

		// Coroutine to create enemies
		private IEnumerator createEnemiesCoroutine;

		// Spawned enemies data
		private List<GameObject> spawnedEnemies;
		[HideInInspector] public int destroyedEnemiesCount; 

		private void Awake()
		{
			destroyedEnemiesCount = 0;
			wavesCount = 1;
			maxWaveCount = waves.Length;
			spawnedEnemies = new List<GameObject>();
		}
		private void Start()
		{
			// Instantiate enemies from each wave (each wave has own duration, so <<waitTimeBetweenEnemySpawns>> calculates spawn interval)
			for(int i = 0; i < waves.Length; i++)
			{
				wavesCount++;
				Wave currentWave = waves[i];
				waitTimeBetweenEnemySpawns = (int) (currentWave.waveDuration / currentWave.enemiesCountPerWave);
				createEnemiesCoroutine = CreateEnemies(currentWave);
				StartCoroutine(createEnemiesCoroutine);
			}
		}

		private IEnumerator CreateEnemies(Wave currentWave)
		{
			for(int i = 0; i < currentWave.enemiesCountPerWave; i++)
			{
				GameObject enemy = Instantiate(currentWave.enemyPrefab, currentWave.spawnPosition, currentWave.enemyPrefab.transform.rotation);
				enemy.GetComponent<Enemy>().enemySpawner = this;
				spawnedEnemies.Add(enemy);
				yield return new WaitForSeconds(waitTimeBetweenEnemySpawns);
			}
		}

		// Public interface
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

		public List<GameObject> SpawnedEnemies
		{
			get
			{
				return spawnedEnemies;
			}
		}
	}
}
