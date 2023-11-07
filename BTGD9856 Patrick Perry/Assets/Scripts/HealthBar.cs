using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Material healthBar;
    [SerializeField]
    private GameObject player;
    // Update is called once per frame
    void Update()
    {
        healthBar.SetFloat("_Health", player.GetComponent<PlayerBody>().playerHealth/100);
    }
}
