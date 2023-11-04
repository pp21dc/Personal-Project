using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTeleportObject : MonoBehaviour
{
    private PlayerController playerController;

    private Vector3 targetLocation;

    [SerializeField]
    private float projectileSpeed = 5f;

    private float timer = 0f;

    [SerializeField]
    private float maxTime = 5f;

    private bool hitWall = false;
    Quaternion playerRotation;
   /* private void Awake()
    {
        //playerRotation = GameObject.Find("PlayerBody").transform.rotation;

        //transform.rotation = playerRotation;
    }*/
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Wall"))
        {
            hitWall = true;
        }
    }
}
