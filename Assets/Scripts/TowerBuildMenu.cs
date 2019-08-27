using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
	public class TowerBuildMenu : MonoBehaviour 
	{
		public GameManager gameManager; 
		private bool isOpened;

		void Start()
		{
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
			string towerType = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.tag;
			gameManager.BuyTower(towerType);
		}
	}
}