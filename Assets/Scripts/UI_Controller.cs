using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerGame
{
	public class UI_Controller : MonoBehaviour 
	{
		[Header ("UI_elements")]
		public Text waveCountText;
		public Text maxWaveCountText;
		public Text healthText;
		public Text playerCoinsText;
		public Text houseTowerCost;
		public Text flameTowerCost;
		public Text lightGunTowerCost;
		public Text rocketLauncherTowerCost;
		public GameManager gameManager;
		public UnityEngine.EventSystems.EventSystem eventSystem;

		void Awake ()
		{
			gameManager.UI_Controller = this;
		}

		void Start()
		{
			var towerPrices = gameManager.towerPrices;
			// Set UI text values
			waveCountText.text = "Current wave: " + EnemySpawner.Instance.wavesCount;
			maxWaveCountText.text = "Max wave count: " + EnemySpawner.Instance.maxWaveCount;
			healthText.text = "Health: " + gameManager.playerHealth;
			playerCoinsText.text = "Coins: " + gameManager.playerMoney;
			houseTowerCost.text = towerPrices[GameManager.TowerTypes.HouseTower].ToString();
			flameTowerCost.text = towerPrices[GameManager.TowerTypes.FlameTower].ToString();
			lightGunTowerCost.text = towerPrices[GameManager.TowerTypes.LightGunTower].ToString();
			rocketLauncherTowerCost.text = towerPrices[GameManager.TowerTypes.RocketLauncherTower].ToString();
		}
	}
}
