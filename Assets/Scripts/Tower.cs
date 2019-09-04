using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace TowerGame
{
    public class Tower : MonoBehaviour 
    {
        public int range;
        public int shootInterval;
        public int damage;

        public Transform detectCircle;
        private GameObject currentTarget;
        private List<GameObject> spawnedEnemies;

        public GameObject bulletPrefab;
        private GameManager gameManager;
        public Transform head, body;

        void Awake()
        {
            detectCircle.localScale = new Vector3 (range, detectCircle.localScale.y, range);
            spawnedEnemies = new List<GameObject>();
        }

        void Start()
        {
            gameManager = GameObject.FindObjectOfType<GameManager> ();
        }

        void Update()
        {
            float detectRange = detectCircle.localScale.x;
            if (currentTarget == null)
                SearchTarget ();
            if (currentTarget != null) 
            {
                //Vector3 projectionOnGround = new Vector3 (currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z);
                // if (Vector3.Distance (transform.position, projectionOnGround) > detectRange) 
                // {
                //         currentTarget = null;
                //         return;
                // }
                if(head != null & body != null)
                {
                    transform.LookAt(currentTarget.transform);
                }
                StartCoroutine(AttackWithBullet());
            }
        }

        void SearchTarget()
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

        public void SetEnemies(List<GameObject> enemies)
        {
            this.spawnedEnemies = enemies;
        }

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
    }
}