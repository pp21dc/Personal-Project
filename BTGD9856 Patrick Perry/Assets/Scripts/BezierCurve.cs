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
    private float midPointMoveSpeed = 2f;

    [SerializeField]
    [Range(0,1)]
    private float t;
    [SerializeField]
    private List<Transform> points = new List<Transform>();

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private float gizmoSize = 1;

    private GameObject player;
    private GameObject companion;
    // Start is called before the first frame update
    void Start()
    {
        midpoint.position = trueMid;
        player = GameObject.Find("TeleportObjectOrigin");
        companion = GameObject.Find("Companion");

        start = player.transform;
        end = companion.transform;
    }

    // Update is called once per frame
    void Update()
    {
        trueMid = Vector3.Lerp(start.position, end.position, 0.5f);
        gameObject.GetComponent<LineRenderer>().SetPosition(0, player.transform.position);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, GetPoint(start.position, end.position, l1.position, r1.position, .2f));
        gameObject.GetComponent<LineRenderer>().SetPosition(2, GetPoint(start.position, end.position, l1.position, r1.position, .4f));
        gameObject.GetComponent<LineRenderer>().SetPosition(3, GetPoint(start.position, end.position, l1.position, r1.position, .6f));
        gameObject.GetComponent<LineRenderer>().SetPosition(4, GetPoint(start.position, end.position, l1.position, r1.position, .8f));
        gameObject.GetComponent<LineRenderer>().SetPosition(5, companion.transform.position);

        midpoint.position = Vector3.MoveTowards(midpoint.position, trueMid, midPointMoveSpeed * Time.deltaTime);
    }

    Vector3 GetPoint(Vector3 start, Vector3 end, Vector3 l1, Vector3 r1, float t)
    {
        
        return Vector3.Lerp(Vector3.Lerp(start, end, t), Vector3.Lerp(Vector3.Lerp(l1, r1, t), end, t), t); ;
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
