using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resource_cut : MonoBehaviour
{
    public int count_down;
    public bool count_down_resource = false;
    private IEnumerator resource_count_down;

    private void Start()
    {
        resource_count_down = ResourceCountdown();
        
    }

    public void CheckTreeInteraction()
    {
        if (this.GetComponent<interaction_start>().interaction_counter_input == 0)
        {
            count_down_resource = false;
            global_variables.active_menu = null;
            menu_actions.PlayerCanMove?.Invoke();
            StopCoroutine(resource_count_down);
        }
        else
        {
            count_down_resource = true;
            global_variables.active_menu = "count_down";
            menu_actions.PlayerCanMove?.Invoke();
            StartCoroutine(resource_count_down);
        }
    }

    private IEnumerator ResourceCountdown()
    {        
        while (count_down > 0)
        {
            count_down --;
            Debug.Log(count_down);
            //there must a better way to do this
            //while true or something like that 
            //wait while 
            yield return null;
            if(count_down <= 0)
            {
                //Reset the active interactive object 
                menu_actions.ActiveInteractiveReset();   
                //disable the game object from the interaction function 
                Destroy(this.gameObject);
                //reset active interactive? 
                //menu_actions.PlayerCanMove?.Invoke();
            }
        }
    }
}
