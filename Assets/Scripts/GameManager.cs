using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TowerGame
{
	public class GameManager : MonoBehaviour
	{
		[Header ("Camera boundaries")]
			public float X_Min;
			public float X_Max;
			public float Z_Min;
			public float Z_Max;

		private UnityEngine.EventSystems.EventSystem eventSystem;
		[HideInInspector] public UI_Controller UI_Controller;
		public enum TowerTypes {FlameTower, HouseTower, LightGunTower, RocketLauncherTower };
		public int playerMoney = 100;

		private Dictionary<TowerTypes, int> towerPrices;
		public Dictionary<TowerTypes, GameObject> towers;

		[Header ("Prefabs")]
		public GameObject RocketLauncherTowerPrefab;
		public GameObject LightGunTowerPrefab;
		public GameObject FlameTowerPrefab;
		public GameObject HouseTowerPrefab;

		void Awake()
		{
			//Terrain.activeTerrain.terrainData.SetHeights((int) 0, (int) 0, new float [100,100]);
		}

		void Start()
		{
			eventSystem = UI_Controller.eventSystem;
			towerPrices = new Dictionary<TowerTypes, int>
			{
				{ TowerTypes.FlameTower, 10 },
				{ TowerTypes.HouseTower, 20 },
				{ TowerTypes.LightGunTower, 30 },
				{ TowerTypes.RocketLauncherTower, 40 }
			};

			towers = new Dictionary<TowerTypes, GameObject>
			{
				{ TowerTypes.FlameTower, FlameTowerPrefab},
				{ TowerTypes.HouseTower, HouseTowerPrefab },
				{ TowerTypes.LightGunTower, LightGunTowerPrefab },
				{ TowerTypes.RocketLauncherTower, RocketLauncherTowerPrefab }
			};
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

		// void OnRectTransformDimensionsChange()
        //  {
        //     Camera.main.transform.position = new Vector3 (
		// 		Mathf.Clamp (Camera.main.transform.position.x, X_Min, X_Max),
		// 		Camera.main.transform.position.y,
 		// 		Mathf.Clamp (Camera.main.transform.position.z, Z_Min, Z_Max)); 
        //  }


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
			 Vector3 slotPosition = TowerSlot.currentSlot.transform.position;
			 GameObject tower = Instantiate(towers[towerType], new Vector3 (slotPosition.x, slotPosition.y + TowerSlot.currentSlot.GetComponent<TowerSlot>().SlotHeight, slotPosition.z ), towers[towerType].transform.rotation);
			 tower.transform.SetParent(TowerSlot.currentSlot.transform);
		 }
	}
}
