using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class character_interaction : MonoBehaviour
{
    //Interaction Inputs
    public LayerMask interaction_layer;
    public List<GameObject> interaction_objects;
    [SerializeField]
    private GameObject active_interaction_object;
    private float interaction_closest;
   
    public void OnInteract(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            InteractionStart();
        }
        if(value.canceled)
        {
            InteractionEnd();
        }
    }

    private void Update()
    {
        ClosestInteractionObject();
    }

    private void OnEnable()
    {
        menu_actions.ActiveInteractiveReset += InteractiveReset;
    }

    private void OnDisable()
    {
        menu_actions.ActiveInteractiveReset -= InteractiveReset;
    }

    // Add and remove the object from the interactive list // 
    private void OnTriggerEnter(Collider collision)
    {
        //Looking at the NPC layer for now 
        if (collision.gameObject.layer == Mathf.Log(interaction_layer.value, 2))
        {
            interaction_objects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        //Looking at the NPC Layer for now 
        if (collision.gameObject.layer == Mathf.Log(interaction_layer.value, 2))
        {
            interaction_objects.Remove(collision.gameObject);
        }
    }

    // This might be cleaner in a different way
    // Fine with this for now 
    //Still calling something from this script - can this be handled on the npc or interaction object? 
    private void ClosestInteractionObject()
    {

        
        
        if (interaction_objects.Count == 0) //set the interactive to null 
        {
            if (active_interaction_object != null)
            {
                active_interaction_object.GetComponent<interaction_start>().interaction_message.SetActive(false);
            }
            active_interaction_object = null;
            interaction_closest = 30.0f;
        }
        else if (interaction_objects.Count == 1) //set the interactive to near by 
        {
            active_interaction_object = interaction_objects[0];
            interaction_closest = 30.0f;
            active_interaction_object.GetComponent<interaction_start>().interaction_message.SetActive(true);
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
                    active_interaction_object.GetComponent<interaction_start>().interaction_message.SetActive(true);
                }
                else
                {
                    active_interaction_object.GetComponent<interaction_start>().interaction_message.SetActive(false);
                }
            }
        }
    }

    //Invoke the Interaction 
    private void InteractionStart()
    {
        if(active_interaction_object == null)
        {
            return;
        }
        //From the interaction start invoke the unity event 
        //I decided to do it like this instead of an action as I didn't want to add ever NPC function and have to check if it was action 
        //But let me know if there is another way to do this

        //Set the interaction type 
        if (active_interaction_object.GetComponent<interaction_start>().is_a_counter == true)
        {
            active_interaction_object.GetComponent<interaction_start>().interaction_counter_input = 1;
        }
        active_interaction_object.GetComponent<interaction_start>().interaction_event.Invoke();

    }

    private void InteractionEnd()
    {
        if (active_interaction_object == null)
        {
            return;
        }

        if (active_interaction_object.GetComponent<interaction_start>().is_a_counter == true)
        {
            active_interaction_object.GetComponent<interaction_start>().interaction_counter_input = 0;
            active_interaction_object.GetComponent<interaction_start>().interaction_event.Invoke();
        }
    }

    private void InteractiveReset()
    {
        active_interaction_object = null;
        interaction_objects.Clear();
    }

}

