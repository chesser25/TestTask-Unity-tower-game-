using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace TowerGame
{
	public class GameManager : MonoBehaviour
	{
		[Header ("Prefabs")]
		public GameObject rocketLauncherTowerPrefab;
		public GameObject lightGunTowerPrefab;
		public GameObject flameTowerPrefab;
		public GameObject houseTowerPrefab;

		// Basic player parameters
		private int playerMoney = 100;
		public int playerHealth = 100;

		// Links to gameObjects from scene
		public UI_Controller viewController;
		public EnemySpawner enemySpawner;
		public Transform[] wayPoints;
		public GameObject menu;

		// Tower setup
		public enum TowerTypes {FlameTower, HouseTower, LightGunTower, RocketLauncherTower };
		private Dictionary<TowerTypes, int> towerPrices;
		private Dictionary<TowerTypes, GameObject> towers;

		private List<GameObject> spawnedTowers;

		// Bullets setup
		[HideInInspector] public Dictionary<int,Bullet> bulletsPool;
		private GameObject bulletsPoolParent;
		public int bulletsCount = 1000;
		public GameObject bulletPrefab;

		// All enemies count (from all waves)
		private int enemiesCount;

		private void Awake()
		{
			// Initialize towers prices
			towerPrices = new Dictionary<TowerTypes, int>
			{
				{ TowerTypes.FlameTower, 10 },
				{ TowerTypes.HouseTower, 20 },
				{ TowerTypes.LightGunTower, 30 },
				{ TowerTypes.RocketLauncherTower, 40 }
			};

			// Towers prefabs
			towers = new Dictionary<TowerTypes, GameObject>
			{
				{ TowerTypes.FlameTower, flameTowerPrefab},
				{ TowerTypes.HouseTower, houseTowerPrefab },
				{ TowerTypes.LightGunTower, lightGunTowerPrefab },
				{ TowerTypes.RocketLauncherTower, rocketLauncherTowerPrefab }
			};
			spawnedTowers = new List<GameObject>();
			CreateBulletsPool();
			CalculateEnemiesCount();
		}

		private void Start()
		{
			SetupInfoPanel();
		}

		// Initialize UI text info
		private void SetupInfoPanel()
		{
			viewController.SetHealthText(playerHealth);
			viewController.SetWaveCountText(enemySpawner.WavesCount);
			viewController.SetMaxWaveCountText(enemySpawner.MaxWavesCount);
			viewController.SetPlayerCoinsText(playerMoney);
			viewController.SetHouseTowerPriceText(towerPrices[TowerTypes.HouseTower]);
			viewController.SetFlameTowerPriceText(towerPrices[TowerTypes.FlameTower]);
			viewController.SetLightGunTowerPriceText(towerPrices[TowerTypes.LightGunTower]);
			viewController.SetRocketLauncherTowerPriceText(towerPrices[TowerTypes.RocketLauncherTower]);
		}

		private void Update()
		{
			SetEnemiesForEachTower(enemySpawner.SpawnedEnemies);
			CheckGameOver();
		}

		public void BuyTower(string tower)
		 {
			 TowerTypes towerType = (TowerTypes) Enum.Parse(typeof(TowerTypes), tower);
			 int towerCost = 0;
			 switch (towerType)
			 {
				 case TowerTypes.FlameTower:
				 	towerCost = towerPrices[TowerTypes.FlameTower];
					break;
				case TowerTypes.HouseTower:
				 	towerCost = towerPrices[TowerTypes.HouseTower];
					break;
				case TowerTypes.LightGunTower:
				 	towerCost = towerPrices[TowerTypes.LightGunTower];
					break;
				case TowerTypes.RocketLauncherTower:
				 	towerCost = towerPrices[TowerTypes.RocketLauncherTower];
					break;
			 }
			 if ( playerMoney < towerCost)
			 	return;
			 DecreaseMoney(towerCost);
			 CreateTower(towerType);
		 }

		 private void CreateTower(TowerTypes towerType)
		 {
			 Transform towerSlotTransform = TowerSlot.currentSlot.transform;
			 Vector3 slotPosition = towerSlotTransform.position;
			 foreach (Transform child in towerSlotTransform)
			 {
				 if(child.name.Contains("Clone"))
				 {
					 spawnedTowers.Remove(child.gameObject);
					 Destroy(child.gameObject);
				 }
			 }
			 GameObject tower = Instantiate(towers[towerType], new Vector3 (slotPosition.x, slotPosition.y + TowerSlot.currentSlot.GetComponent<TowerSlot>().SlotHeight, slotPosition.z ), towers[towerType].transform.rotation);
			 tower.transform.SetParent(TowerSlot.currentSlot.transform);
			 spawnedTowers.Add(tower);
		 }

		// Damage player health
		 public void Damage(int damage)
		 {
			 playerHealth -= damage;
			 viewController.SetHealthText(playerHealth);
		 }

		 public void AddMoney(int amount)
		 {
			 playerMoney += amount;
			 viewController.SetPlayerCoinsText(playerMoney);
		 }

		 public void DecreaseMoney(int amount)
		 {
			 playerMoney -= amount;
			 viewController.SetPlayerCoinsText(playerMoney);
		 }

		// Update enemies list for each tower
		 private void SetEnemiesForEachTower(List<GameObject> enemies)
		 {
			 foreach (GameObject tower in spawnedTowers)
			 {
				 tower.GetComponent<Tower>().SetEnemies(enemies);
			 }
		 }

		// Creates a list of bullets
		private void CreateBulletsPool ()
		{
			bulletsPool = new Dictionary<int,Bullet> ();
			bulletsPoolParent = new GameObject ("BulletsPool");

			for (int i = 0; i < bulletsCount; i++) {
				GameObject newBullet = Instantiate (bulletPrefab);
				bulletsPool [i] = newBullet.GetComponent<Bullet> ();
				bulletsPool [i].gameObject.SetActive (false);	
				bulletsPool [i].transform.parent = bulletsPoolParent.transform;
			}
		}

		public void BackToMenu()
		{
			SceneManager.LoadScene ("Menu");
		}

		// Logic to detect is game over, or user's still playing
		private bool IsWin
		{
			get
			{
				return playerHealth > 0 && enemySpawner.destroyedEnemiesCount == enemiesCount;
			}
		}

		private bool IsLost
		{
			get
			{
				return playerHealth <= 0;
			}
		}
		private void CheckGameOver() 
		{
			if(IsWin || IsLost)	
			{
				menu.SetActive(true);
				if(IsWin)
				{
					viewController.SetGameResultText("You won!!!");
				}
				else
				{
					viewController.SetGameResultText("You lose((");
				}
			}	
		}

		private void CalculateEnemiesCount()
		{
			foreach(Wave wave in enemySpawner.waves)
			{
				enemiesCount += wave.enemiesCountPerWave;
			}
		}
	}
}
