using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : FSMState
{
    private FollowAIProperties followAIProperties;
    private AIController companion;
    bool moving;
    float movespeed;
    private Vector3 newPosition;

    public FollowState(AIController controller, FollowAIProperties followAIProperties, Transform trans)
    {
        this.followAIProperties = followAIProperties;
        companion = controller;
        playerLocation = followAIProperties.playerLocation.transform;
        stateID = FSMStateID.Follow;
        movespeed = followAIProperties.movespeed;
    }

    public override void Reason(Transform player, Transform npc)
    {
        if (Vector3.Distance(companion.transform.position, playerLocation.transform.position) < 2)
        {
            companion.PerformTransition(Transition.reachedPlayer);
            return;
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        newPosition = Vector3.MoveTowards(companion.transform.position, new Vector3(playerLocation.transform.position.x, companion.transform.position.y, playerLocation.transform.position.z), movespeed * Time.deltaTime);
        companion.GetComponent<Rigidbody>().MovePosition(newPosition);
    }
}
