using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingEnemy : MonoBehaviour
{
    public bool touchingEnemy = false;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            touchingEnemy = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            touchingEnemy = false;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy")
        {
            touchingEnemy = true;
        }
    }

    
}
