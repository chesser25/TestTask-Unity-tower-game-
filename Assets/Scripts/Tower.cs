using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Tower : MonoBehaviour 
{
	public int range;
    public int shootInterval;
    public int damage;

    public Transform detectCircle;
    private GameObject currentTarget;
    private List<GameObject> spawnedEnemies;

    public GameObject bulletPrefab;

    void Awake()
    {
        detectCircle.localScale = new Vector3 (range, detectCircle.localScale.y, range);
        spawnedEnemies = new List<GameObject>();
    }

    void OnMouseDown ()
    {
        detectCircle.gameObject.SetActive (true);
    }

    void OnMouseUp()
    {
        detectCircle.gameObject.SetActive (false);
    }

    void Update()
    {
        float detectRange = detectCircle.localScale.x / 2;
        if (currentTarget == null)
			SearchTarget ();
        if (currentTarget != null) 
        {
            Vector3 projectionOnGround = new Vector3 (currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z);
            if (Vector3.Distance (transform.position, projectionOnGround) > detectRange) 
            {
					currentTarget = null;
					return;
			}
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.position += transform.forward * Time.deltaTime;
        }
    }

    void SearchTarget()
    {
        foreach (var enemy in spawnedEnemies) 
        {
            Vector3 projectionOnGround = new Vector3 (enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
            if (Vector3.Distance (transform.position, projectionOnGround) < detectCircle.localScale.x / 2)
            {
                currentTarget = enemy;
            }
        }
    }

    public void SetEnemies(List<GameObject> enemies)
    {
        this.spawnedEnemies = enemies;
    }

    IEnumerator ShootRocketLauncher ()
    {
        GameObject temproc;

        for (int i = 0; i < gameManager.RocketsPool.Count; i++)
            if (!gameManager.RocketsPool [i].gameObject.activeSelf) {
                _temproc = gameManager.RocketsPool [i];
                _temproc.Damage = Damage [Level];
                _temproc.Target = CurrentTarget;
                _temproc.gameObject.SetActive (true);
                _temproc.transform.position = GunPoint1.position;
                _temproc.transform.rotation = GunPoint1.rotation;
                gameManager.RocketLaunchSound.Play ();
                Fire1.Emit ();
                break;
            }

        yield return new WaitForSeconds (0.3f);

        for (int i = 0; i < gameManager.RocketsPool.Count; i++)
            if (!gameManager.RocketsPool [i].gameObject.activeSelf) {
                _temproc = gameManager.RocketsPool [i];
                _temproc.Damage = Damage [Level];
                _temproc.Target = CurrentTarget;
                _temproc.gameObject.SetActive (true);
                _temproc.transform.position = GunPoint2.position;
                _temproc.transform.rotation = GunPoint2.rotation;
                gameManager.RocketLaunchSound.Play ();
                Fire2.Emit ();
                break;
            }

    }
}
