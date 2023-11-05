using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleState : FSMState
{
    private CircleAIProperties circleAIProperties;
    private AIController companion;
    bool moving;

    public CircleState(AIController controller, CircleAIProperties circleAIProperties, Transform trans)
    {
        this.circleAIProperties = circleAIProperties;
        companion = controller;
        playerLocation = circleAIProperties.playerLocation;
    }

    public override void Reason(Transform player, Transform npc)
    {
        
    }

    public override void Act(Transform player, Transform npc)
    {
         companion.transform.RotateAround(playerLocation.transform.position, Vector3.right, 20 * Time.deltaTime);
    }
}
