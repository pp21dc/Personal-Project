using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();

    [SerializeField]
    public Collider companionTouchingEnemy;

    private GameObject companion;

    private GameObject closestEnemy = null;
    private void Awake()
    {
        companion = GameObject.Find("Companion");
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }

    public GameObject getClosestEnemy()
    {
        if (enemies.Count == 0)
        {
            return null;
        }
        if(closestEnemy == null)
        {
            closestEnemy = enemies[0];
        }

        foreach(GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                break;
            }
            if(Vector3.Distance(companion.transform.position, enemy.transform.position) < Vector3.Distance(companion.transform.position, closestEnemy.transform.position) && closestEnemy != null && enemy != null)
            {
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    public void removeOnDeath(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}
