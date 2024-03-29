using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class is adapted and modified from the FSM implementation class available on UnifyCommunity website
/// The license for the code is Creative Commons Attribution Share Alike.
/// It's originally the port of C++ FSM implementation mentioned in Chapter01 of Game Programming Gems 1
/// You're free to use, modify and distribute the code in any projects including commercial ones.
/// Please read the link to know more about CCA license @http://creativecommons.org/licenses/by-sa/3.0/

/// This class represents the States in the Finite State System.
/// Each state has a Dictionary with pairs (transition-state) showing
/// which state the FSM should be if a transition is fired while this state
/// is the current state.
/// Reason method is used to determine which transition should be fired .
/// Act method has the code to perform the actions the NPC is supposed to do if it�s on this state.
/// </summary>
public abstract class FSMState
{
    protected Dictionary<Transition, FSMStateID> map = new Dictionary<Transition, FSMStateID>();
    protected FSMStateID stateID;
    public FSMStateID ID { get { return stateID; } }
    protected Vector3 destPos;
    protected Transform playerLocation;
    protected float curRotSpeed;
    protected float curSpeed;
    protected List<GameObject> enemies1;
    protected GameObject projectile;
    protected float range;
    protected bool ran = false;
    public void AddTransition(Transition transition, FSMStateID id)
    {
        // Check if anyone of the args is invallid
        if (transition == Transition.None || id == FSMStateID.None)
        {
            Debug.LogWarning("FSMState : Null transition not allowed");
            return;
        }

        //Since this is a Deterministc FSM,
        //Check if the current transition was already inside the map
        if (map.ContainsKey(transition))
        {
            Debug.LogWarning("FSMState ERROR: transition is already inside the map");
            return;
        }

        map.Add(transition, id);
        //Debug.Log("Added : " + transition + " with ID : " + id);
    }

    /// <summary>
    /// This method deletes a pair transition-state from this state�s map.
    /// If the transition was not inside the state�s map, an ERROR message is printed.
    /// </summary>
    public void DeleteTransition(Transition trans)
    {
        // Check for NullTransition
        if (trans == Transition.None)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return;
        }

        // Check if the pair is inside the map before deleting
        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
        Debug.LogError("FSMState ERROR: Transition passed was not on this State�s List");
    }


    /// <summary>
    /// This method returns the new state the FSM should be if
    ///    this state receives a transition  
    /// </summary>
    public FSMStateID GetOutputState(Transition trans)
    {
        // Check for NullTransition
        if (trans == Transition.None)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return FSMStateID.None;
        }

        // Check if the map has this transition
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }

        Debug.LogError("FSMState ERROR: " + trans + " Transition passed to the State was not on the list");
        return FSMStateID.None;
    }

    /// <summary>
    /// Used to initialize variables when re-entering state
    /// </summary>
    public virtual void EnterStateInit(Transform player, Transform npc)
    {
    }

    /// <summary>
    /// Decides if the state should transition to another on its list
    /// NPC is a reference to the npc tha is controlled by this class
    /// </summary>
    public abstract void Reason(Transform player, Transform npc);

    /// <summary>
    /// This method controls the behavior of the NPC in the game World.
    /// Every action, movement or communication the NPC does should be placed here
    /// NPC is a reference to the npc tha is controlled by this class
    /// </summary>
    public abstract void Act(Transform player, Transform npc);

    /// <summary>
    /// Find the next point
    /// </summary>
    public virtual void FindNextPoint()
    {
        /*int randomIndex = Random.Range(0, waypoints.Length);
        destPos = waypoints[randomIndex].position;*/
    }

    public virtual GameObject FindClosestPosition()
    {

        /*GameObject closestEnemy = enemies[0];
        
        float currentLowestDistance = 500f;
        foreach(GameObject enemy in enemies)
        {
            if(enemy == null)
            {
                enemies.Remove(enemy);
            }
            if(Vector3.Distance(playerLocation.position, enemy.transform.position) < currentLowestDistance)
            {
                closestEnemy = enemy;
                currentLowestDistance = Vector3.Distance(playerLocation.position, enemy.transform.position);
            }
        }
        return closestEnemy;*/

        return GameObject.Find("EnemyDetection").GetComponent<EnemyManager>().getClosestEnemy();
    }

    public virtual List<GameObject> UpdateList(List<GameObject> inputList)
    {
        foreach (GameObject enemy in inputList)
        {
            if (enemy == null)
            {
                inputList.Remove(enemy);
                return inputList;
            }
        }
            return inputList;
    }

    public virtual float GetPlayerHealth(GameObject player)
    {
        return player.GetComponent<PlayerBody>().playerHealth;
    }

    public virtual void HealPlayer(GameObject player, float healAmount)
    {
        player.GetComponent<PlayerBody>().playerHealth += healAmount;
    }
}
