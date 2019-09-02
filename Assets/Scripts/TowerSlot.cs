using UnityEngine;

namespace TowerGame
{
	public class TowerSlot : MonoBehaviour 
	{
		public TowerBuildMenu towerBuildMenu;

		public static GameObject currentSlot; 

		private float slotHeight;

		public float SlotHeight
		{
			get
			{
				return slotHeight;
			}
		}

		void Awake()
		{
			var collider = GetComponent<Collider>();
			slotHeight = collider.bounds.size.y;
		}

		void OnMouseDown()
		{
			if(towerBuildMenu.IsOpened)
				return;
			currentSlot = gameObject;
			towerBuildMenu.ShowMenu();
  		}   
	}
}
