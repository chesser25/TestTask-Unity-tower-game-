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
		private EnemySpawner enemySpawner;

		void Awake ()
		{
			gameManager.UI_Controller = this;
			enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
		}

		void Start()
		{
			var towerPrices = gameManager.towerPrices;
			// Set UI text values
			waveCountText.text = "Current wave: " + enemySpawner.WavesCount;
			maxWaveCountText.text = "Max wave count: " + enemySpawner.MaxWavesCount;
			healthText.text = "Health: " + gameManager.playerHealth;
			playerCoinsText.text = "Coins: " + gameManager.playerMoney;
			houseTowerCost.text = towerPrices[GameManager.TowerTypes.HouseTower].ToString();
			flameTowerCost.text = towerPrices[GameManager.TowerTypes.FlameTower].ToString();
			lightGunTowerCost.text = towerPrices[GameManager.TowerTypes.LightGunTower].ToString();
			rocketLauncherTowerCost.text = towerPrices[GameManager.TowerTypes.RocketLauncherTower].ToString();
		}
	}
}
