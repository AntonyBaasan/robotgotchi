using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class character_menu : MonoBehaviour
{
    public void OnPlayerMenu(InputAction.CallbackContext value)
    {
        if (value.started)
        { 
            if(global_variables.active_menu == "player_menu" || global_variables.active_menu == null)
            {
                menu_actions.OnMenuHit?.Invoke("player_menu");
            }
        }
    }

    public void OnEscape(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (global_variables.active_menu == "in_game_menu" || global_variables.active_menu == null)
            {
                menu_actions.OnMenuHit?.Invoke("in_game_menu");
            }
            else if(global_variables.active_menu == "player_menu")
            {
                menu_actions.OnMenuHit?.Invoke("player_menu");
            }
        }
    }
}
