using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    //System
    [SerializeField]
    private bool usingController = true;
    private Rigidbody rb;
    [SerializeField]
    private PlayerController playerController;
    Vector3 mousePositionAdjusted;
    private bool movementLock = false;
    //Movement
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float lookSpeed = 1000f;

    private Vector3 newRotation;

    //Dash
    private float dashCD = 10f;
    [SerializeField]
    private float dashCDTimer;
    private bool dashOnCD = false;
    private bool hasDashed = false;
    [SerializeField]
    GameObject teleportObject;
    GameObject teleportObjectSpawnPoint;
    private float dashSpeed = 30f;
    private float xMagOnDash;
    private float yMagOnDash;

    //Shoot
    [SerializeField]
    GameObject primaryBullet;
    private bool shootOnCD = false;
    [SerializeField]
    private float shootCD = 0.25f;
    private float shootCDTimer = 0f;
    
    //Jump
    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dashCDTimer = dashCD;

        teleportObjectSpawnPoint = GameObject.Find("TeleportObjectOrigin");
    }

    private void Update()
    {
        if (usingController == false) {
            if (movementLock == false)
            {
                mousePositionAdjusted = new Vector3(playerController.MousePosition.x, rb.position.y, playerController.MousePosition.z);
                rb.transform.LookAt(mousePositionAdjusted);
            }
        }

        if (movementLock == false)
        {
            gameObject.transform.Rotate(0f, lookSpeed * playerController.RightStickHorizontalMag * Time.deltaTime, 0f);
        }

        if (playerController.AltFire && dashOnCD == false)
        {
            xMagOnDash = playerController.HorizontalMag;
            yMagOnDash = playerController.VerticalMag;
            dash();

        }

        if (dashOnCD == true) 
        {
            dashCDTimer -= Time.deltaTime;

            if(dashCDTimer <= 0)
            {
                dashOnCD = false;
                dashCDTimer = dashCD;
                hasDashed = false;
            }
        }

        if(playerController.PrimaryFire && shootOnCD == false)
        {
            fire();
        }

        if(shootOnCD == true)
        {
            shootCDTimer += Time.deltaTime;
            if(shootCDTimer >= shootCD)
            {
                shootOnCD = false;
                shootCDTimer = 0f;
            }
        }

        if (playerController.Jump)
        {
            jump();
        }
    }
     
    private void FixedUpdate()
    {
        if (movementLock == false)
        {
            Vector3 newPosition = rb.position + (rb.transform.forward * moveSpeed * playerController.VerticalMag * Time.deltaTime) + (rb.transform.right * moveSpeed * playerController.HorizontalMag * Time.deltaTime);
            Debug.Log(newPosition.magnitude);

            animator.SetFloat("Speed", Mathf.Abs(playerController.VerticalMag));

            rb.MovePosition(newPosition);
        }
    }

    private void dash()
    {
        rb.AddForce((rb.transform.forward * dashSpeed * yMagOnDash * dashSpeed * Time.deltaTime) + (rb.transform.right * dashSpeed * xMagOnDash * dashSpeed * Time.deltaTime));
    }

    private void fire()
    {
        Instantiate(primaryBullet, teleportObjectSpawnPoint.transform.position, gameObject.transform.rotation);
        shootOnCD = true;
    }

    private void jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "TeleportObject")
        {
            hasDashed = true;
            movementLock = false;
            Destroy(GameObject.Find("TeleportObject(Clone)"));
            gameObject.tag = "Player";

        }
    }
}
