using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{

    private Vector3 position;

    [SerializeField]
    private Material material;


    [SerializeField]
    private float dissolveSpeed = 1f;

    [SerializeField]
    public float health = 100f;

    [SerializeField]
    private EnemyManager enemyManager;
    private void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bullet"))
        {
            health -= 100f;
            if (health <= 0)
            {
                StartCoroutine("deathEffect");
                enemyManager.removeOnDeath(gameObject);
            }
        }

        if (other.tag.Equals("AIBullet"))
        {
            health -= 7;
            if (health <= 0)
            {
                StartCoroutine("deathEffect");
                enemyManager.removeOnDeath(gameObject);
            }
        }
    }

    IEnumerator deathEffect()
    {

    float dissolveValue = 1;
    float dissolveMin = -7;

        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        while (dissolveValue >= dissolveMin)
        {
            dissolveValue -= Time.deltaTime * dissolveSpeed;

            material.SetFloat("_Cutoff_Height", dissolveValue);
            yield return null;
        }

        if(dissolveValue < dissolveMin)
        {
            Destroy(gameObject);
        }
    }
}
