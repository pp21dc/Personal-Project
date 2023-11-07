using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleState : FSMState
{
    private CircleAIProperties circleAIProperties;
    private AIController companion;
    bool moving;
    float movespeed;
    GameObject body;

    public CircleState(AIController controller, CircleAIProperties circleAIProperties, Transform trans)
    {
        this.circleAIProperties = circleAIProperties;
        companion = controller;
        playerLocation = circleAIProperties.playerLocation.transform;
        stateID = FSMStateID.Circle;
        movespeed = circleAIProperties.movespeed;
    }

    public override void Reason(Transform player, Transform npc)
    {
        if (GetPlayerHealth(circleAIProperties.playerLocation) < 100f)
        {
            companion.PerformTransition(Transition.playerLowHealth);
            return;
        }

        //Debug.Log("Distance: " + Vector3.Distance(FindClosestPosition(npc, enemies1).transform.position, companion.transform.position));
        if (GameObject.FindWithTag("Enemy"))
        {
            companion.PerformTransition(Transition.EnemyNear);
            return;
        }

        
    }

    public override void Act(Transform player, Transform npc)
    {
        companion.transform.RotateAround(playerLocation.transform.position, Vector3.up, 90 * Time.deltaTime);

        if(Vector3.Distance(playerLocation.transform.position, companion.transform.position) > 2)
        companion.transform.position = Vector3.MoveTowards(companion.transform.position, new Vector3(playerLocation.transform.position.x, companion.transform.position.y, playerLocation.transform.position.z), movespeed * Time.deltaTime);
    }
}
