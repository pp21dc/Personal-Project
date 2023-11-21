using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryBullet : MonoBehaviour
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
            gameObject.SetActive(false);
        }

        if (other.tag.Equals("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
