using UnityEngine;

namespace TowerGame
{
	public class Enemy : MonoBehaviour 
	{
		public int health;
		public int speed;
		public int damage;
		public int money;

		private GameManager gameManager;
		private Transform currentWaypoint;
		private Transform[] wayPoints;
		private int wayPointIndex;

		private EnemySpawner enemySpawner;
		public TextMesh enemyHealth;

		void Start()
		{
			gameManager = GameObject.FindObjectOfType<GameManager> ();
			wayPoints = gameManager.wayPoints;
			currentWaypoint = wayPoints [wayPointIndex];
			SetHealthText();
		}

		void SetHealthText()
		{
			enemyHealth.text = health.ToString();
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
					gameManager.playerHealth = 0;
				}
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			Tower tower = other.gameObject.GetComponent<Tower>();
			if (tower != null)
			{
				gameManager.Damage(damage);
			}
		}

		void Update() 
		{
			Debug.Log("here");
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

		void GetDamage (int damage)
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
	}
}
