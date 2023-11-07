using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPrimaryFire : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.tag.Equals("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
