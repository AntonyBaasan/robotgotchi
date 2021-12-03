using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class character_menu : MonoBehaviour
{

    //This is still calling a string and cannot be added to
    //Might be fine for these two menus as they live with the player, not the world 
    //Fine for now, but there might be more player menus in the future 

    public void OnPlayerMenu(InputAction.CallbackContext value)
    {
        if (value.started)
        { 
            if(global_variables.active_menu == "player_menu" || global_variables.active_menu == null)
            {
                UpdateMenus("player_menu");
            }
        }
    }

    public void OnEscape(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (global_variables.active_menu == "in_game_menu" || global_variables.active_menu == null)
            {
                UpdateMenus("in_game_menu");
            }
            else if(global_variables.active_menu == "player_menu")
            {
                UpdateMenus("player_menu");
            }
        }
    }

    private void UpdateMenus(string name)
    {
        menu_actions.OnMenuHit?.Invoke(name);
        menu_actions.PlayerCanMove?.Invoke();
    }

}
