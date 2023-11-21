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
    private float backwardsMoveSpeed = 2.5f;

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
    //private float dashSpeed = 30f;
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
/*    [SerializeField]
    private float jumpForce = 10f;*/

    [SerializeField]
    private Animator animator;

    public float playerHealth = 100f;

    Vector3 mousePosition;
    Vector3 oldMousePosition = new Vector3(0, 0, 0);

    [SerializeField]
    GameObject camera;

    public float lookSens = 0.5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dashCDTimer = dashCD;

        teleportObjectSpawnPoint = GameObject.Find("TeleportObjectOrigin");
    }

    private void Update()
    {
        if (playerController.AltFire && dashOnCD == false)
        {
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

        //Shoot fireball if not on cooldown - if on CD run timer
        if(playerController.PrimaryFire && shootOnCD == false)
        {
            animator.SetTrigger("CastFireBall");
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

        //if dance is pressed dance
        if (playerController.Dance)
        {
            dance();
        }
        
        //Rotate Camera Around player when right click is pressed
        if (playerController.CameraRotate)
        {
            camera.transform.RotateAround(gameObject.transform.position, Vector3.up, lookSens *getDifference(mousePosition, oldMousePosition)*360*Time.deltaTime);
            gameObject.transform.Rotate(0f, lookSens * getDifference(mousePosition, oldMousePosition) * 360 * Time.deltaTime, 0f);
        }
        oldMousePosition = mousePosition;
    }
     
    private void FixedUpdate()
    {
        if (movementLock == false)
        {

            Vector2 direction = new Vector2(playerController.HorizontalMag, playerController.VerticalMag);

            direction = direction.normalized;

            if (playerController.VerticalMag < 0)
            {
                Vector3 newPosition = rb.position + (camera.transform.forward * backwardsMoveSpeed * direction.y * Time.deltaTime) + (camera.transform.right * backwardsMoveSpeed * direction.x * Time.deltaTime);
                rb.MovePosition(newPosition);
            }

            else
            {
                Vector3 newPosition = rb.position + (camera.transform.forward * moveSpeed * direction.y * Time.deltaTime) + (camera.transform.right * moveSpeed * direction.x * Time.deltaTime);
                rb.MovePosition(newPosition);
            }

            animator.SetFloat("SpeedForward", playerController.VerticalMag);
            animator.SetFloat("SpeedSideways", playerController.HorizontalMag);
        }
    }

    private void dash()
    {
        animator.SetTrigger("CastTeleport");
    }

    public void instanceTeleportObject()
    {
        Instantiate(teleportObject, teleportObjectSpawnPoint.transform.position, gameObject.transform.rotation);
    }

    public void teleportToObject()
    {
        gameObject.transform.position = GameObject.Find("TeleportObject(Clone)").transform.position;
        Destroy(GameObject.Find("TeleportObject(Clone)"));
        unlockMovement();
    }

    public void fire()
    {
        playerHealth -= 20;
        GameObject playerFireball = ObjectPool.Instance.GetPooledObject();
        playerFireball.transform.position = teleportObjectSpawnPoint.transform.position;
        playerFireball.transform.rotation = teleportObjectSpawnPoint.transform.rotation;
        playerFireball.SetActive(true);
        shootOnCD = true;
    }

    private void dance()
    {
        animator.SetTrigger("Dance");
    }

    public void lockMovement()
    {
        movementLock = true;
        Debug.Log("Locked Movment");
    }

    public void unlockMovement()
    {
        movementLock = false;
        Debug.Log("Unlocked Movment");
    }
    private float getDifference(Vector3 a, Vector3 b)
    {
        return a.x - b.x;
    }
}
