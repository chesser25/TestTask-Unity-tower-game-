using UnityEngine;

namespace TowerGame
{
	public class TowerBuildMenu : MonoBehaviour 
	{
		private GameManager gameManager; 
		private bool isOpened;

		void Start()
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
			private set
			{
				isOpened = value;
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
			Debug.Log("buying");
			string towerType = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.tag;
			gameManager.BuyTower(towerType);
		}
	}
}