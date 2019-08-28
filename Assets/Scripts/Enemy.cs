using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerGame
{
	public class Enemy : MonoBehaviour 
	{
		public int health;
		public int speed;
		public int damage;

		public GameManager gameManager;
		private int wayPointIndex;

		void Awake()
		{
			wayPointIndex = 0;
		}

		private void Move() 
		{
			transform.position += transform.up * Time.deltaTime * speed;

			if ((gameManager.wayPoints.Length - 1) > wayPointIndex)
			{
				wayPointIndex += 1;
			}
			// if (Vector3.Distance (transform.position, gameManager.wayPoints[wayPointIndex].position) < 2f) 
			// {
			// 	if ((gameManager.wayPoints.Length - 1) > wayPointIndex)
			// 	{
			// 		wayPointIndex += 1;
			// 	}
			// 	else 
			// 	{
			// 		//gameManager.DamageBase ();
			// 		//Deactivating ();
			// 	}
			// }
		}

		void Update() 
		{
			Move();
		}
	}
}
