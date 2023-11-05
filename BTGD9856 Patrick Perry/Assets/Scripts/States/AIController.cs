using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AIProperties
{

}

[System.Serializable]
public class CircleAIProperties : AIProperties
{
    public Transform playerLocation;
}

public class AttackAIProperties : AIProperties
{
    public Transform[] enemyLocations;
}

public class FollowAIProperties : AIProperties
{

}

public class IdleAIProperties : AIProperties
{

}
public class AIController : AdvancedFSM
{

    [SerializeField]
    private CircleAIProperties circleAIProperties;

    private string GetStateString()
    {

        string state = "NONE";
        if (CurrentState.ID == FSMStateID.CIRCLE)
        {
            state = "DEAD";
        }
        else if (CurrentState.ID == FSMStateID.CIRCLE)
        {
            state = "CIRCLE";
        }

        return state;
    }

    protected override void Initialize()
    {
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;
        ConstructFSM();
    }

    protected override void FSMUpdate()
    {

        if (CurrentState != null)
        {
            CurrentState.Reason(playerTransform, transform);
            CurrentState.Act(playerTransform, transform);
        }
        //StateText.text = "MONSTER STATE IS: " + GetStateString();

/*        if (debugDraw)
        {
            Debug.DrawRay(transform.position, transform.forward * 5.0f, Color.red);
        }*/
    }


    private void ConstructFSM()
    {
        //Create States

        //Circle State
        CircleState circleState = new CircleState(this, circleAIProperties, transform);
        //Transitions out of the Circle State
        circleState.AddTransition(Transition.enemyNear, FSMStateID.ATTACK);

        //Attack State
        /*AttackState attackState = new AttackState(this, )*/
    }
}


