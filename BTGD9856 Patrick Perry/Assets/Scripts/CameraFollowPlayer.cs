using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    GameObject playerBody;

    private Vector3 oldPos;

    private Vector3 difference;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.Find("PlayerBody");
        oldPos = playerBody.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        difference = playerBody.transform.position - oldPos;

        gameObject.transform.position += difference;

        oldPos = playerBody.transform.position;
    }
}
