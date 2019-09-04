using UnityEngine;

namespace TowerGame
{
	public class Bullet : MonoBehaviour 
	{
		[HideInInspector] public float damage;

		[HideInInspector] public GameObject target;

		public int bulletSpeed;

		void Start () 
		{
			Invoke ("Deactivate", 1);
		}
			
		void Update()
		{
			if (target != null)
				if (Vector3.Distance (transform.position, target.transform.position) < 10) 
					MakeDamage ();
				Fly ();
		}

		void Fly()
		{
			transform.localPosition += transform.forward * Time.deltaTime * bulletSpeed;
		}

		void MakeDamage()
		{
			target.transform.SendMessage ("GetDamage", damage);
			Deactivate ();
		}

		void Deactivate()
		{
			gameObject.SetActive (false);
		}
	}
}