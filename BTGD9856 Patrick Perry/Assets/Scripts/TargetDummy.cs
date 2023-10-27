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

    private void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bullet") || other.tag.Equals("CanHurt"))
        {
            StartCoroutine("deathEffect");
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
