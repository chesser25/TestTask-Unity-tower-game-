using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TowerGame
{
	public class GameManager : MonoBehaviour
	{
		//public enum TowerTypes {FlameTower, HouseTower, LightGunTower, RocketLauncherTower };

		[Header ("Camera boundaries")]
			public float X_Min;
			public float X_Max;
			public float Z_Min;
			public float Z_Max;

		// [Header ("Prefabs")]
		// public GameObject rocketLauncherTowerPrefab;
		// public GameObject lightGunTowerPrefab;
		// public GameObject flameTowerPrefab;
		// public GameObject houseTowerPrefab;

		//public Transform[] wayPoints;

		private EventSystem eventSystem;
		private UI_Controller viewController;
		private EnemySpawner enemySpawner;

		// Variables
		// private Dictionary<TowerTypes, int> towerPrices;
		// private Dictionary<TowerTypes, GameObject> towers;
		private int playerMoney = 100;
		private int playerHealth = 100;

		// void Awake()
		// {
		// 	// Initialize towers prices
		// 	towerPrices = new Dictionary<TowerTypes, int>
		// 	{
		// 		{ TowerTypes.FlameTower, 10 },
		// 		{ TowerTypes.HouseTower, 20 },
		// 		{ TowerTypes.LightGunTower, 30 },
		// 		{ TowerTypes.RocketLauncherTower, 40 }
		// 	};

		// 	// Towers prefabs
		// 	towers = new Dictionary<TowerTypes, GameObject>
		// 	{
		// 		{ TowerTypes.FlameTower, flameTowerPrefab},
		// 		{ TowerTypes.HouseTower, houseTowerPrefab },
		// 		{ TowerTypes.LightGunTower, lightGunTowerPrefab },
		// 		{ TowerTypes.RocketLauncherTower, rocketLauncherTowerPrefab }
		// 	};
		// }

		void Start()
		{
			viewController = GameObject.FindGameObjectWithTag("UI").GetComponent<UI_Controller>();
			eventSystem = viewController.eventSystem;
			enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
			SetupInfoPanel();
		}

		void SetupInfoPanel()
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

		void Update()
		{
			DoNavigation ();
		}

		void DoNavigation ()
		{
			if (eventSystem.IsPointerOverGameObject ())
				return;

			if (Input.GetKey ("mouse 0"))
				Camera.main.transform.position += new Vector3 (-Input.GetAxis ("Mouse X"), 0, -Input.GetAxis ("Mouse Y"));

			Camera.main.transform.position = new Vector3 (
				Mathf.Clamp (Camera.main.transform.position.x, X_Min, X_Max),
				Camera.main.transform.position.y,
 				Mathf.Clamp (Camera.main.transform.position.z, Z_Min, Z_Max)); 
		}

		//  public void BuyTower(string tower)
		//  {
		// 	 TowerTypes towerType = (TowerTypes) Enum.Parse(typeof(TowerTypes), tower);
		// 	 int towerCost = 0;
		// 	 switch (towerType)
		// 	 {
		// 		 case TowerTypes.FlameTower:
		// 		 	towerCost = towerPrices[TowerTypes.FlameTower];
		// 			break;
		// 		case TowerTypes.HouseTower:
		// 		 	towerCost = towerPrices[TowerTypes.HouseTower];
		// 			break;
		// 		case TowerTypes.LightGunTower:
		// 		 	towerCost = towerPrices[TowerTypes.LightGunTower];
		// 			break;
		// 		case TowerTypes.RocketLauncherTower:
		// 		 	towerCost = towerPrices[TowerTypes.RocketLauncherTower];
		// 			break;
		// 	 }
		// 	 if ( playerMoney < towerCost)
		// 	 	return;
		// 	 DecreaseMoney(towerCost);
		// 	 CreateTower(towerType);
		//  }

		 void AddMoney(int amount)
		 {
			 playerMoney += amount;
			 viewController.SetPlayerCoinsText(playerMoney);
		 }

		 void DecreaseMoney(int amount)
		 {
			 playerMoney -= amount;
			 viewController.SetPlayerCoinsText(playerMoney);
		 }

		//  private void CreateTower(TowerTypes towerType)
		//  {
		// 	 Vector3 slotPosition = TowerSlot.currentSlot.transform.position;
		// 	 GameObject tower = Instantiate(towers[towerType], new Vector3 (slotPosition.x, slotPosition.y + TowerSlot.currentSlot.GetComponent<TowerSlot>().SlotHeight, slotPosition.z ), towers[towerType].transform.rotation);
		// 	 tower.transform.SetParent(TowerSlot.currentSlot.transform);
		//  }

		 public void Damage(int damage)
		 {
			 playerHealth -= damage;
			 viewController.SetHealthText(playerHealth);
		 }

		 public bool IsTowerPurchasePossible(int towerPrice)
		 {
			 return playerMoney >= towerPrice;
		 }
	}
}
