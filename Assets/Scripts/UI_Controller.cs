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
		public Text gameResultText;

		public void SetHealthText(int playerHealth)
		{
			healthText.text = "Health: " + playerHealth;
		}

		public void SetWaveCountText(int waveCount)
		{
			waveCountText.text = "Current wave: " + waveCount;
		}

		public void SetMaxWaveCountText(int maxWaveCount)
		{
			waveCountText.text = "Max wave count: " + maxWaveCount;
		}

		public void SetPlayerCoinsText(int playerCoins)
		{
			playerCoinsText.text = "Coins: " + playerCoins + "$";
		}

		public void SetHouseTowerPriceText(int price)
		{
			houseTowerCost.text = price.ToString();
		}

		public void SetFlameTowerPriceText(int price)
		{
			flameTowerCost.text = price.ToString();
		}

		public void SetLightGunTowerPriceText(int price)
		{
			lightGunTowerCost.text = price.ToString();
		}
		
		public void SetRocketLauncherTowerPriceText(int price)
		{
			rocketLauncherTowerCost.text = price.ToString();
		}

		public void SetWinText(int score)
		{
			gameResultText.text = "You won! Your score: " + score;
		}

		public void SetLoseText()
		{
			gameResultText.text = "You lose!";
		}
	}
}
