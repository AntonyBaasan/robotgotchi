using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class npc_base_interaction : MonoBehaviour
{
    //Play item that should interact
    public string base_player;
    //Interaction input
    public interaction interact;
    //base angle for interaction 
    public int interaction_angle = 30;

    // Might need a way to check this is a character starts in the interaction 
    private void OnTriggerEnter(Collider other)
    {
        //get if the player is in range 
        interact.interaction_current = this.gameObject;
        interact.interaction_angle_input = interaction_angle;
        //if this does not equal null 
            //figure out which the main character is closets to 
            //then adjust 
            //although this is not a dynamic thing, so might need to figure out the next step
            //something for later on 
            //might need a better way to dothis? Ray distance? Keep interactions apart from each other? 
    }
    private void OnTriggerExit(Collider other)
    {
        //get if the player is in range 
        interact.interaction_current = null;
        interact.interaction_angle_input = 0;
    }

}
