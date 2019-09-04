using UnityEngine;

namespace TowerGame
{
	public class Bullet : MonoBehaviour 
	{
		[HideInInspector] public float damage;
		[HideInInspector] public GameObject target;
		public int bulletSpeed;

		private void Start () 
		{
			Invoke ("Deactivate", 1);
		}
			
		private void Update()
		{
			if (target != null)
			{
				if (Vector3.Distance (transform.position, target.transform.position) < 10) 
					MakeDamage ();
				Fly ();
			}
		}

		private void Fly()
		{
			transform.localPosition += transform.forward * Time.deltaTime * bulletSpeed;
		}

		private void MakeDamage()
		{
			target.transform.SendMessage ("GetDamage", damage);
			Deactivate ();
		}

		private void Deactivate()
		{
			gameObject.SetActive (false);
		}
	}
}