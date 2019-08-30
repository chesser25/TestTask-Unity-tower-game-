using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace TowerGame
{
	public class GameManager : MonoBehaviour
	{
		[Header ("Camera boundaries")]
			public float X_Min;
			public float X_Max;
			public float Z_Min;
			public float Z_Max;

		[Header ("Prefabs")]
		public GameObject rocketLauncherTowerPrefab;
		public GameObject lightGunTowerPrefab;
		public GameObject flameTowerPrefab;
		public GameObject houseTowerPrefab;

		public GameObject enemyPrefab;

		
		public Transform[] wayPoints;
		private UnityEngine.EventSystems.EventSystem eventSystem;
		[HideInInspector] public UI_Controller UI_Controller;
		public enum TowerTypes {FlameTower, HouseTower, LightGunTower, RocketLauncherTower };
		public Dictionary<TowerTypes, int> towerPrices;
		public Dictionary<TowerTypes, GameObject> towers;
		public int playerMoney = 100;
		public int playerHealth = 100;
		void Start()
		{
			eventSystem = UI_Controller.eventSystem;
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
			EnemySpawner.Instance.CreateEnemies(enemyPrefab, wayPoints);
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

		 void AddMoney(int amount)
		 {
			 playerMoney += amount;
		 }

		 void DecreaseMoney(int amount)
		 {
			 playerMoney -= amount;
		 }

		 private void CreateTower(TowerTypes towerType)
		 {
			 Debug.Log(TowerSlot.currentSlot.GetComponent<TowerSlot>().SlotHeight);
			 Vector3 slotPosition = TowerSlot.currentSlot.transform.position;
			 Debug.Log(slotPosition.y);
			 GameObject tower = Instantiate(towers[towerType], new Vector3 (slotPosition.x, slotPosition.y + TowerSlot.currentSlot.GetComponent<TowerSlot>().SlotHeight, slotPosition.z ), towers[towerType].transform.rotation);
			 tower.transform.SetParent(TowerSlot.currentSlot.transform);
		 }
	}
}
