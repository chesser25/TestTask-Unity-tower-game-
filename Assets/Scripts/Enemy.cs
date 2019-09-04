using UnityEngine;

namespace TowerGame
{
	public class Enemy : MonoBehaviour 
	{
		// Basic enemy characteristics
		public int health;
		public int speed;
		public int damage;

		// How much money, the player will have, when the enemy will be destroyed
		public int money;

		// Links to other scene objects
		private GameManager gameManager;
		public EnemySpawner enemySpawner;

		// Waypoints parameters
		private Transform currentWaypoint;
		private Transform[] wayPoints;
		private int wayPointIndex = 1;

		// Enemy health text
		public TextMesh enemyHealth;

		private void Start()
		{
			gameManager = GameObject.FindObjectOfType<GameManager> ();
			wayPoints = gameManager.wayPoints;
			currentWaypoint = wayPoints [wayPointIndex];
			SetHealthText();
		}

		private void Update() 
		{
			Move();
		}

		private void Move() 
		{
			// Find rotation relative to a target and rotate to that direction
			Quaternion targetRotation = Quaternion.LookRotation (currentWaypoint.position - transform.position, transform.up);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, Time.deltaTime * 100);

			// Move logic
			transform.position += transform.forward * Time.deltaTime * speed;
			float distance = Vector3.Distance (transform.position, currentWaypoint.position);
			if ( distance < 3f)
			{
				if ((wayPoints.Length - 1) > wayPointIndex)
				{
					NextWayPoint();
				}
				else
				{
					// if enemy has arrived to the lat waypoint, player loses the game
					Die();
					gameManager.playerHealth = 0;
				}
			}
		}

		// When enemy collides with tower collider, enemy makes damage to the player health
		private void OnTriggerEnter(Collider other)
		{
			Tower tower = other.gameObject.GetComponent<Tower>();
			if (tower != null)
			{
				gameManager.Damage(damage);
			}
		}

		// Moving enemy via waypoints
		private void NextWayPoint()
		{
			wayPointIndex += 1;
			currentWaypoint = wayPoints [wayPointIndex];
		}

		// Destroy enemy
		private void Die()
		{
			enemySpawner.SpawnedEnemies.Remove(this.gameObject);
			Destroy (gameObject);
		}

		// Damage to a current enemy
		private void GetDamage (int damage)
		{
			health -= damage;
			SetHealthText();
			if (health <= 0)
			{
				gameManager.AddMoney(money);
				enemySpawner.destroyedEnemiesCount++;
				Die ();
			}
		}

		private void SetHealthText()
		{
			enemyHealth.text = health.ToString();
		}
	}
}
