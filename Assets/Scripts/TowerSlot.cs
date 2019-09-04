using UnityEngine;

namespace TowerGame
{
	// Class describes a place, where tower could be built
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

		private void Awake()
		{
			var collider = GetComponent<Collider>();
			slotHeight = collider.bounds.size.y;
		}

		private void OnMouseDown()
		{
			if(towerBuildMenu.IsOpened)
				return;
			currentSlot = gameObject;
			towerBuildMenu.ShowMenu();
  		}   
	}
}
