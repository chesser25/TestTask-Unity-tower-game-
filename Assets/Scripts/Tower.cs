using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour 
{
	public int range;
    public int shootInterval;
    public int damage;

    public Transform detectCircle;
    private GameObject currentTarget;
    private List<GameObject> spawnedEnemies;

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
    }

    void SearchTarget()
    {
        foreach (var enemy in spawnedEnemies) 
        {
            Vector3 projectionOnGround = new Vector3 (enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
            if (Vector3.Distance (transform.position, projectionOnGround) < detectCircle.localScale.x / 2)
            {
                currentTarget = enemy;
                Debug.Log(((GameObject)enemy).name);
            }
        }
    }

    public void SetEnemies(GameObject[] enemies)
    {
        spawnedEnemies = enemies;
    }
}
