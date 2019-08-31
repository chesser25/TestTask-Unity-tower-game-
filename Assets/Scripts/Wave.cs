using UnityEngine;
using System;

[Serializable]
public struct Wave
{
	public int waveDuration; // In seconds
	public int enemiesCountPerWave;

	public GameObject enemyPrefab;
	public Vector3 spawnPosition;
}
