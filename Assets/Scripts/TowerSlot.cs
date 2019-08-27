using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
	public class TowerSlot : MonoBehaviour 
	{
		public TowerBuildMenu towerBuildMenu;

		void Update()
		{
			RaycastHit  hit;
        	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) 
			{
                if (hit.transform.gameObject.tag == "TowerSlot" )
				{
					ShowTowerBuildMenu();
				}
			}         
		}

		void ShowTowerBuildMenu()
		{
			if(towerBuildMenu.IsOpened)
				return;
			towerBuildMenu.ShowMenu();
		}
	}
}
