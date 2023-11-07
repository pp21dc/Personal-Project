using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FSMState
{
    private AttackAIProperties attackAIProperties;
    private AIController companion;
    private float timer = 0f;
    //private List<GameObject> enemies1 = new List<GameObject>();
    public AttackState(AIController controller, AttackAIProperties attackAIProperties, Transform trans)
    {
        this.attackAIProperties = attackAIProperties;
        companion = controller;
        projectile = attackAIProperties.projectile;
        stateID = FSMStateID.Attack;
        playerLocation = attackAIProperties.playerLocation.transform;
        enemies1 = attackAIProperties.enemies;
        range = attackAIProperties.range;
    }

    public override void Reason(Transform player, Transform npc)
    {
        enemies1 = UpdateList(enemies1);

        if (Vector3.Distance(companion.transform.position, playerLocation.transform.position) > 7)
        {
            companion.PerformTransition(Transition.playerTooFar);
            return;
        }

        else if (Vector3.Distance(FindClosestPosition(npc, enemies1).transform.position, npc.position) > attackAIProperties.range)
        {
            companion.PerformTransition(Transition.NoEnemyNear);
            return;
        }

        if (GetPlayerHealth(attackAIProperties.playerLocation) < 30f)
        {
            companion.PerformTransition(Transition.playerLowHealth);
            return;
        }
    }


    public override void Act(Transform player, Transform npc)
    {
        attackAIProperties.closestEnemyText.text = "Nearest Enemy: " + Vector3.Distance(FindClosestPosition(npc, enemies1).transform.position, npc.position).ToString("#.00");
        enemies1 = UpdateList(enemies1);
        if (Vector3.Distance(FindClosestPosition(npc, enemies1).transform.position, npc.position) < attackAIProperties.range) 
        { 
            npc.LookAt(FindClosestPosition(npc, enemies1).transform);
            Debug.DrawRay(companion.transform.position, companion.transform.forward, Color.red);
            if (timer == 0)
        {
            GameObject projectileIns = GameObject.Instantiate(projectile, companion.transform.position, companion.transform.rotation);
        }
        timer += Time.deltaTime;

        if (timer >= .5)
        {
            timer = 0f;
        }
        }
    }
}
