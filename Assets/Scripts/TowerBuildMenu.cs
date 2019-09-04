using UnityEngine;

namespace TowerGame
{
	// Menu is shown, when user tries to buy a new tower
	public class TowerBuildMenu : MonoBehaviour 
	{
		private GameManager gameManager; 
		private bool isOpened;

		private void Start()
		{
			gameManager = GameObject.FindObjectOfType<GameManager> ();
			isOpened = false;
		}

		public bool IsOpened
		{
			get
			{
				return isOpened;
			}
		}

		public void ShowMenu()
		{
			gameObject.SetActive(true);
		}

		public void HideMenu()
		{
			gameObject.SetActive(false);
		}

		public void BuyTower()
		{
			// Find out, what type of tower, user wants, by button tag
			string towerType = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.tag;
			gameManager.BuyTower(towerType);
		}
	}
}