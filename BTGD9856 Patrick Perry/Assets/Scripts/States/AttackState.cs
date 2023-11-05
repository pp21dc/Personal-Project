using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FSMState
{
    private AttackAIProperties attackAIProperties;
    private AIController companion;
    bool moving;

    public AttackState(AIController controller, AttackAIProperties attackAIProperties, Transform trans)
    {
        this.attackAIProperties = attackAIProperties;
        companion = controller;
        enemyLocations = attackAIProperties.enemyLocations;
    }

    public override void Reason(Transform player, Transform npc)
    {
        throw new System.NotImplementedException();
    }

    public override void Act(Transform player, Transform npc)
    {
        throw new System.NotImplementedException();
    }
}
