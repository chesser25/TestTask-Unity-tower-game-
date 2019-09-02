using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
	public class Enemy : MonoBehaviour 
	{
		public int health;
		public int speed;
		public int damage;

		private GameManager gameManager;
		private Transform currentWaypoint;
		private Transform[] wayPoints;
		private int wayPointIndex;

		private EnemySpawner enemySpawner;

		void Awake()
		{
			wayPointIndex = 1;
		}
		void Start()
		{
			gameManager = GameObject.FindObjectOfType<GameManager> ();
			wayPoints = gameManager.wayPoints;
			currentWaypoint = wayPoints [wayPointIndex];
		}

		private void Move() 
		{
			Quaternion targetRotation = Quaternion.LookRotation (currentWaypoint.position - transform.position, transform.up);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, Time.deltaTime * 100);
			transform.position += transform.forward * Time.deltaTime * speed;
			float distance = Vector3.Distance (transform.position, currentWaypoint.position);
			if ( distance < 2f)
			{
				if ((wayPoints.Length - 1) > wayPointIndex)
				{
					NextWayPoint();
				}
				else
				{
					Die();
				}
			}
		}

		private void OnCollisionEnter(Collision other)
		{
			Debug.Log(other.gameObject.name);
			Tower tower = other.gameObject.GetComponent<Tower>();
			if (tower != null)
			{
				gameManager.Damage(damage);
			}
		}

		void Update() 
		{
			Move();
		}

		void NextWayPoint()
		{
			wayPointIndex += 1;
			currentWaypoint = wayPoints [wayPointIndex];
		}

		void Die()
		{
			EnemySpawner.SpawnedEnemies.Remove(this.gameObject);
			Destroy (gameObject);
		}

		public EnemySpawner EnemySpawner
		{
			private get
			{
				return enemySpawner;
			}
			set
			{
				enemySpawner = value;
			}
		}
	}
}
