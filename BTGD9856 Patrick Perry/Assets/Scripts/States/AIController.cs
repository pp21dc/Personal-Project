using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class AIProperties
{
    public GameObject playerLocation;
}

[System.Serializable]
public class CircleAIProperties : AIProperties
{
    public float movespeed = 5f;
}
[System.Serializable]
public class AttackAIProperties : AIProperties
{
    public GameObject projectile;
    public List<GameObject> enemies = new List<GameObject>();
    public float range = 15f;
    public  Text closestEnemyText;
}
[System.Serializable]
public class FollowAIProperties : AIProperties
{
    public float movespeed = 5f;
}
[System.Serializable]
public class HealAIProperties : AIProperties
{
    public float healValuePerSeccond = 5f;
}
public class AIController : AdvancedFSM
{

    [SerializeField]
    private CircleAIProperties circleAIProperties;

    [SerializeField]
    private AttackAIProperties attackAIProperties;
    [SerializeField]
    private FollowAIProperties followAIProperties;
    [SerializeField]
    private HealAIProperties healAIProperties;

    [SerializeField]
    private Text StateText;

    private string GetStateString()
    {

        string state = "NONE";
        if (CurrentState.ID == FSMStateID.Circle)
        {
            state = "CIRCLE";
        }
        else if (CurrentState.ID == FSMStateID.Attack)
        {
            state = "ATTACK";
        }
        else if (CurrentState.ID == FSMStateID.Follow)
        {
            state = "FOLLOW";
        }
        else if(CurrentState.ID == FSMStateID.Heal)
        {
            state = "HEAL";
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
        StateText.text = "STATE IS: " + GetStateString();
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
        circleState.AddTransition(Transition.EnemyNear, FSMStateID.Attack);
        circleState.AddTransition(Transition.playerLowHealth, FSMStateID.Heal);
        
        //Attack State
        AttackState attackState = new AttackState(this, attackAIProperties, transform);
        //Transitions out of the attack state
        attackState.AddTransition(Transition.NoEnemyNear, FSMStateID.Circle);
        attackState.AddTransition(Transition.playerTooFar, FSMStateID.Follow);
        attackState.AddTransition(Transition.playerLowHealth, FSMStateID.Heal);
        //Follow State
        FollowState followState = new FollowState(this, followAIProperties, transform);
        //Transitions out of the follow state
        followState.AddTransition(Transition.reachedPlayer, FSMStateID.Circle);
        followState.AddTransition(Transition.playerLowHealth, FSMStateID.Heal);
        //POI(Dance) State
        HealState healState = new HealState(this, healAIProperties, transform);
        //Transitions
        healState.AddTransition(Transition.playerHealthAcceptable, FSMStateID.Circle);
        healState.AddTransition(Transition.playerTooFar, FSMStateID.Follow);



        AddFSMState(followState);
        AddFSMState(attackState);
        AddFSMState(circleState);
        AddFSMState(healState);
    }
}


