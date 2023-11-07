/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIState : FSMState
{
    private POIAIProperties POIAIProperties;
    private AIController companion;
    private float timer = 0f;
    public POIState(AIController controller, POIAIProperties POIAIProperties, Transform trans)
    {
        this.POIAIProperties = POIAIProperties;
        companion = controller;
        stateID = FSMStateID.POI;

    }

    public override void Reason(Transform player, Transform npc)
    {
        if (!playerLocation.CompareTag("Dancing"))
        {
            companion.PerformTransition(Transition.notDancing);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        if(timer < 0.2f)
        {
            POIAIProperties.material.SetColor("_EmissiveColor", Color.red);
        }

        if(timer < 0.4f)
        {
            POIAIProperties.material.SetColor("_EmissiveColor", Color.green);
        }
        if(timer < 0.6f)
        {
            POIAIProperties.material.SetColor("_EmissiveColor", Color.blue);
        }

        if(timer < 0.8f)
        {
            POIAIProperties.material.SetColor("_EmissiveColor", Color.cyan);
        }
        
    }
}
*/