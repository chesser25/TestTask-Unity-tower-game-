using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
	public class TowerSpawner : MonoBehaviour 
	{
		public enum TowerTypes {FlameTower, HouseTower, LightGunTower, RocketLauncherTower };

		[Header ("Prefabs")]
		public GameObject rocketLauncherTowerPrefab;
		public GameObject lightGunTowerPrefab;
		public GameObject flameTowerPrefab;
		public GameObject houseTowerPrefab;

		public Transform[] wayPoints;

		private Dictionary<TowerTypes, int> towerPrices;
		private Dictionary<TowerTypes, GameObject> towers;

		void Awake()
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
			// if (playerMoney < towerCost)
			// 	return;
			if(gameObject.SendMessage("IsTowerPurchasePossible", towerCost))
			{
				gameObject.SendMessage("DecreaseMoney", towerCost);
				//DecreaseMoney(towerCost);
				CreateTower(towerType);
			}
		}

		private void CreateTower(TowerTypes towerType)
		{
			Vector3 slotPosition = TowerSlot.currentSlot.transform.position;
			GameObject tower = Instantiate(towers[towerType], new Vector3 (slotPosition.x, slotPosition.y + TowerSlot.currentSlot.GetComponent<TowerSlot>().SlotHeight, slotPosition.z ), towers[towerType].transform.rotation);
			tower.transform.SetParent(TowerSlot.currentSlot.transform);
		}
	}
}
