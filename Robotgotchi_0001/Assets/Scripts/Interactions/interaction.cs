using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class interaction : MonoBehaviour
{
    //The main player of the game 
    public GameObject character_controller;
    // get the current interaction object 
    public GameObject interaction_current = null;
    //base interaction angle 
    public int interaction_angle_input = 20;
    //interact main thiger 
    public GameObject interact_label;
    //THe character is facing the correct direction 
    //private bool character_in_range = false;


    void Update()
    {
        Direction();
      
    }

    public void Interact_begin()
    {
       
        
        if(interaction_current != null)
        {
                Debug.Log("go go go");
        }
        else
        {
            Debug.Log("No Interaction found"); 
        }
    }

    private void Direction()
    {
        if (interaction_current != null)
        {
            if(Vector3.Angle(character_controller.transform.forward, interaction_current.transform.position - character_controller.transform.position) < interaction_angle_input)
            {
                Debug.Log("Character facing the character");
                Debug.Log(interaction_angle_input);
                //character_in_range = true;
                interact_label.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("you are not facing the character");
                //character_in_range = false;
                interact_label.gameObject.SetActive(false);
                
            }
        }
    }
}


