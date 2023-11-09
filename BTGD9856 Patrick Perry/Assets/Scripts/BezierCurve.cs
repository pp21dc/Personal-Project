using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform end;

    [SerializeField]
    private Transform midpoint;
    private Vector3 trueMid;

    [SerializeField]
    private Transform l1;

    [SerializeField]
    private Transform r1;

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private float gizmoSize = 1;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject companion;
    // Start is called before the first frame update
    void Start()
    {
        midpoint.position = trueMid;
        player = GameObject.Find("TeleportObjectOrigin");
        companion = GameObject.Find("Companion");

        player.transform.position += new Vector3(0, 0.4f, 0);

        start = player.transform;
        end = companion.transform;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(0, player.transform.position);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, GetPoint(start.position, end.position, l1.position, r1.position, .2f));
        gameObject.GetComponent<LineRenderer>().SetPosition(2, GetPoint(start.position, end.position, l1.position, r1.position, .4f));
        gameObject.GetComponent<LineRenderer>().SetPosition(3, GetPoint(start.position, end.position, l1.position, r1.position, .6f));
        gameObject.GetComponent<LineRenderer>().SetPosition(4, GetPoint(start.position, end.position, l1.position, r1.position, .8f));
        gameObject.GetComponent<LineRenderer>().SetPosition(5, companion.transform.position);

    }

    Vector3 GetPoint(Vector3 start, Vector3 end, Vector3 l1, Vector3 r1, float t)
    {
        
        return Vector3.Lerp(Vector3.Lerp(start, end, t), Vector3.Lerp(Vector3.Lerp(l1, r1, t), end, t), t);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(GetPoint(start.position, end.position, l1.position, r1.position, .2f), gizmoSize);
        Gizmos.DrawSphere(GetPoint(start.position, end.position, l1.position, r1.position, .4f), gizmoSize);
        Gizmos.DrawSphere(GetPoint(start.position, end.position, l1.position, r1.position, .6f), gizmoSize);
        Gizmos.DrawSphere(GetPoint(start.position, end.position, l1.position, r1.position, .8f), gizmoSize);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(l1.position, gizmoSize);
        Gizmos.DrawSphere(r1.position, gizmoSize);
        Gizmos.DrawSphere(start.position, gizmoSize);
        Gizmos.DrawSphere(end.position, gizmoSize);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(trueMid, gizmoSize);
    }
}
