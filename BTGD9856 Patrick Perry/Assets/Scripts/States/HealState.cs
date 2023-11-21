using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealState : FSMState
{
    private HealAIProperties healAIProperties;
    private AIController companion;
    bool moving;
    float movespeed;
    private ParticleSystem healFX;

    public HealState(AIController controller, HealAIProperties healAIProperties, Transform trans)
    {
        this.healAIProperties = healAIProperties;
        companion = controller;
        playerLocation = healAIProperties.playerLocation.transform;
        stateID = FSMStateID.Heal;
        healFX = healAIProperties.healFX;
    }

    public override void Reason(Transform player, Transform npc)
    {
        if(GetPlayerHealth(healAIProperties.playerLocation) >= 100)
        {
            companion.GetComponent<LineRenderer>().enabled = false;
            companion.PerformTransition(Transition.playerHealthAcceptable);
            healFX.Stop();
            return;
        }

        if(Vector3.Distance(companion.transform.position, playerLocation.transform.position) > 7)
        {
            companion.GetComponent<LineRenderer>().enabled = false;
            companion.PerformTransition(Transition.playerTooFar);
            healFX.Stop();
            return;
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        if (!companion.GetComponent<LineRenderer>().enabled)
        {
            companion.GetComponent<LineRenderer>().enabled = true;
        }
        companion.transform.LookAt(player.transform, Vector3.up);
        companion.GetComponent<LineRenderer>().SetPosition(0, npc.position);
        companion.GetComponent<LineRenderer>().SetPosition(1, new Vector3(player.position.x, player.position.y + 0.4f, player.position.z));


        HealPlayer(healAIProperties.playerLocation, healAIProperties.healValuePerSeccond * Time.deltaTime);
        Debug.DrawRay(npc.transform.position, npc.transform.forward, Color.red);
    }
}
