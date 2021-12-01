using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class character_interaction : MonoBehaviour
{
    //Interaction Inputs
    public LayerMask npc_layer;
    public LayerMask door_layer;
    public List<GameObject> interaction_objects;
    [SerializeField]
    private GameObject active_interaction_object;
    private float interaction_closest;

    public void OnInteract(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Debug.Log("Hit");
        }
    }

    private void Update()
    {
        ClosestInteractionObject();
    }

    // Add and remove the object from the interactive list // 
    private void OnTriggerEnter(Collider collision)
    {
        //Looking at the NPC layer for now 
        if (collision.gameObject.layer == Mathf.Log(npc_layer.value, 2))
        {
            interaction_objects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        //Looking at the NPC Layer for now 
        if (collision.gameObject.layer == Mathf.Log(npc_layer.value, 2))
        {
            interaction_objects.Remove(collision.gameObject);
        }
    }


    // This might be cleaner in a different way
    // Main issue right now is calling the child object, this should be something cleaner
    // Fine with this for now 

    private void ClosestInteractionObject()
    {
        if (interaction_objects.Count == 0) //set the interactive to null 
        {
            if (active_interaction_object != null)
            {
                active_interaction_object.transform.GetChild(1).gameObject.SetActive(false);
            }
            active_interaction_object = null;
            interaction_closest = 30.0f;
        }
        else if (interaction_objects.Count == 1) //set the interactive to near by 
        {
            active_interaction_object = interaction_objects[0];
            interaction_closest = 30.0f;
            active_interaction_object.transform.GetChild(1).gameObject.SetActive(true);
        }
        else //set the interactive to the closest 
        {
            interaction_closest = 20f;

            for (int i = 0; i < interaction_objects.Count; i++) // Look at all the interactions objects and determine the closest. 
            {
                if (Vector3.Distance(this.transform.position, interaction_objects[i].transform.position) < interaction_closest)
                {
                    interaction_closest = Vector3.Distance(this.transform.position, interaction_objects[i].transform.position);
                    active_interaction_object = interaction_objects[i];
                    //get fixed 
                    active_interaction_object.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    interaction_objects[i].transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }


    private void InteractionStart()
    {
        if(active_interaction_object == null)
        {
            return;
        }
        
        // Run the code from the object 

        //If the object is complete it's thing flip everything back on

        //If the object is not done it's thing do nothing 
        //global_variables.active_menu = null
    }
}
