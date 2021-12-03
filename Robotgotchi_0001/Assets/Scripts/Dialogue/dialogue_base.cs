using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue_base : MonoBehaviour
{
    public Sprite character_avatar;
    public List<string> dialogue;
    public string dialogue_active;
    private int dialogue_counter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void RunInteraction()
    {
        Debug.Log("This is inside a unity event");
        //you might not need the counter, just grab the index of the update
        if(dialogue_counter == dialogue.Count)
        {
            //reset the counter 
            dialogue_counter = 0;
            global_variables.active_menu = null;
            //Turn off the dialogue menu
            menu_actions.OnDialogueHit?.Invoke(null, null, character_avatar, false);
            //move the player again
            menu_actions.PlayerCanMove?.Invoke();
        }
        else
        {
            //Set the active dialogue
            dialogue_active = dialogue[dialogue_counter];
            //update the dialogue counter 
            dialogue_counter += 1;
            //set the active menu 
            global_variables.active_menu = "dialogue_ui";
            //Run the dialogue
            menu_actions.OnDialogueHit?.Invoke(dialogue_active, "there", character_avatar, true);
            //Turn off the player 
            menu_actions.PlayerCanMove?.Invoke();
            //Active text 
            Debug.Log(dialogue_active);
        }
    }
}
