using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
	public class UI_Controller : MonoBehaviour 
	{
		private GameManager gameManager;
		public UnityEngine.EventSystems.EventSystem eventSystem;

		void Awake ()
		{
			gameManager = GameObject.FindObjectOfType<GameManager> ();

			if (gameManager == null) 
			{
				Debug.LogError ("GameManager was not found!");
				return;
			}

			SendInstances ();
		}

		void SendInstances ()
		{
			gameManager.UI_Controller = this;
		}
	}
}
