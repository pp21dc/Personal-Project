using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector3 mousePosition;
    public Vector3 MousePosition { get { return mousePosition; } }

    private RaycastHit hit;

    private float verticalMag;
    public float VerticalMag { get { return verticalMag; } }


    private float horizontalMag;
    public float HorizontalMag { get { return horizontalMag; } }

    private float rightStickHorizontalMag;
    public float RightStickHorizontalMag { get { return rightStickHorizontalMag; } }

    private bool primaryFire = false;
    public bool PrimaryFire { get { return primaryFire; } }

    private bool altFire = false;
    public bool AltFire {  get { return altFire; } }

    private bool jump = false;

    public bool Jump { get { return jump; } }

    // Update is called once per frame
    void Update()
    {
        checkMagnitude();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            mousePosition = hit.point;
        }

        checkAttack();
    }

    private void checkMagnitude()
    {
        verticalMag = Input.GetAxis("Vertical");
        horizontalMag = Input.GetAxis("Horizontal");
        rightStickHorizontalMag = Input.GetAxis("RightStickHorizontal");
        Debug.Log(rightStickHorizontalMag);
    }

    private void checkAttack()
    {
        primaryFire = Input.GetButtonDown("Fire1");
        altFire = Input.GetButtonDown("Fire3");
        jump = Input.GetButtonDown("Jump");
    }
}
