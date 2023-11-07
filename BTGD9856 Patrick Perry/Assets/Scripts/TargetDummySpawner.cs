using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummySpawner : MonoBehaviour
{

    [SerializeField]
    GameObject targetDummy1;
    [SerializeField]
    GameObject targetDummy2;
    [SerializeField]
    GameObject targetDummy3;

    GameObject sp1;
    GameObject sp2;
    GameObject sp3;

    public List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        sp1 = GameObject.Find("TargetDummySpawner1");
        sp2 = GameObject.Find("TargetDummySpawner2");
        sp3 = GameObject.Find("TargetDummySpawner3");
    }

    // Update is called once per frame
    void Update()
    {
        /*if(GameObject.Find("TargetDummy1(Clone)") == false)
        {
            Instantiate(targetDummy1, sp1.transform.position, sp1.transform.rotation);
        }

        if (GameObject.Find("TargetDummy2(Clone)") == false)
        {
            Instantiate(targetDummy2, sp2.transform.position, sp2.transform.rotation);
        }

        if (GameObject.Find("TargetDummy3(Clone)") == false)
        {
            Instantiate(targetDummy3, sp3.transform.position, sp3.transform.rotation);
        }*/
    
    }


}
