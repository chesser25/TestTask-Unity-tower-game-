using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace TowerGame
{
	public class EnemySpawner : MonoBehaviour
	{
		private int wavesCount;
		private int maxWaveCount;
		private int waitTimeBetweenEnemySpawns;
		public Wave[] waves;
		private IEnumerator coroutine;
		private List<GameObject> spawnedEnemies;
		[HideInInspector]public int destroyedEnemiesCount; 

		void Awake()
		{
			destroyedEnemiesCount = 0;
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
				enemy.GetComponent<Enemy>().EnemySpawner = this;
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

		public List<GameObject> SpawnedEnemies
		{
			get
			{
				return spawnedEnemies;
			}
		}
	}
}
