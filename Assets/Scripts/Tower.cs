using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace TowerGame
{
    public class Tower : MonoBehaviour 
    {
        // Basic tower parameters
        public int range;
        public int shootInterval;
        public int damage;

        // Link to main manager
        private GameManager gameManager;

        // GameObject is used to detect enemies (are there in a specified range)
        public Transform detectCircle;

        // Enemies info
        private GameObject currentTarget;
        private List<GameObject> spawnedEnemies;

        public GameObject bulletPrefab;

        // The gun of the tower
        public Transform head;

        private void Awake()
        {
            // Set range, from what tower will detect enemies
            detectCircle.localScale = new Vector3 (range, detectCircle.localScale.y, range);
            spawnedEnemies = new List<GameObject>();
        }

        private void Start()
        {
            gameManager = GameObject.FindObjectOfType<GameManager> ();
        }

        private void Update()
        {
            float detectRange = detectCircle.localScale.x;
            if (currentTarget == null)
                SearchTarget ();
            if (currentTarget != null) 
            {
                // If target moves over the detect circle, the tower should not hit one
                Vector3 projectionOnGround = new Vector3 (currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z);
                if (Vector3.Distance (transform.position, projectionOnGround) > detectRange) 
                {
                        currentTarget = null;
                        return;
                }
                transform.LookAt(currentTarget.transform);
                StartCoroutine(AttackWithBullet());
            }
        }

        private void SearchTarget()
        {
            foreach (var enemy in spawnedEnemies) 
            {
                Vector3 projectionOnGround = new Vector3 (enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
                if (Vector3.Distance (transform.position, projectionOnGround) < detectCircle.localScale.x)
                {
                    currentTarget = enemy;
                }
            }
        }

        // Basic bullet setup and instantiation
        private IEnumerator AttackWithBullet ()
        {
            Bullet bullet;
            for (int i = 0; i < gameManager.bulletsPool.Count; i++)
            {
                if (!gameManager.bulletsPool [i].gameObject.activeSelf) 
                {
                     bullet = gameManager.bulletsPool [i];
                     bullet.target = currentTarget;
                     bullet.damage = damage;
                     bullet.gameObject.SetActive (true);
                     bullet.transform.position = new Vector3(head.transform.position.x, head.transform.position.y, head.transform.position.z);
                     bullet.transform.rotation = head.transform.rotation;
                    break;
                }
                yield return new WaitForSeconds (shootInterval);
            }
        }

        // Update a list of spawned enemies
        public void SetEnemies(List<GameObject> enemies)
        {
            this.spawnedEnemies = enemies;
        }
    }
}