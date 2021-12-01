using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_menu_in_game : MonoBehaviour
{
    public GameObject menu_base;
    public string call_menu;
    
    
    private void OnEnable()
    {
        menu_actions.OnMenuHit += PlayerMenuUpdate;
    }

    private void OnDisable()
    {
        menu_actions.OnMenuHit += PlayerMenuUpdate;
    }

    private void PlayerMenuUpdate(string menu_hit)
    {
        //this can go into the interaction script
        if (menu_hit == call_menu)
        {
            //Set the active menu on
            menu_base.SetActive(!menu_base.activeSelf);
            //set the active menu 
            if(global_variables.active_menu == call_menu)
            {
                global_variables.active_menu = null;
            }
            else 
            {
                global_variables.active_menu = call_menu;
            }
        }
    }
}
