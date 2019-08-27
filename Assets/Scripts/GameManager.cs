using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
	public class GameManager : MonoBehaviour
	{
		[Header ("Camera boundaries")]
			public float X_Min;
			public float X_Max;
			public float Z_Min;
			public float Z_Max;

		private UnityEngine.EventSystems.EventSystem _eventSystem;
		[HideInInspector] public UI_Controller _UI_Controller;

		void Start()
		{
			_eventSystem = _UI_Controller._eventSystem;
		}

		void Update()
		{
			DoNavigation ();
		}

		void DoNavigation ()
		{
			if (_eventSystem.IsPointerOverGameObject ())
				return;

			if (Input.GetKey ("mouse 0"))
				Camera.main.transform.position += new Vector3 (-Input.GetAxis ("Mouse X"), 0, -Input.GetAxis ("Mouse Y"));

			Camera.main.transform.position = new Vector3 (
				Mathf.Clamp (Camera.main.transform.position.x, X_Min, X_Max),
				Camera.main.transform.position.y,
				Mathf.Clamp (Camera.main.transform.position.z, Z_Min, Z_Max));
		}
	}
}
