using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resource_cut : MonoBehaviour
{
    public int count_down;
    public bool count_down_resource = false;
    private IEnumerator resource_count_down;
    public GameObject health_input;
    public GameObject health_dial;
    private int total;

    private void Start()
    {
        resource_count_down = ResourceCountdown();
        total = count_down;
        
    }

    public void CheckTreeInteraction()
    {
        if (this.GetComponent<interaction_start>().interaction_counter_input == 0)
        {
            count_down_resource = false;
            global_variables.active_menu = null;
            menu_actions.PlayerCanMove?.Invoke();
            StopCoroutine(resource_count_down);
            health_input.SetActive(false);
        }
        else
        {
            count_down_resource = true;
            global_variables.active_menu = "count_down";
            menu_actions.PlayerCanMove?.Invoke();
            StartCoroutine(resource_count_down);
            health_input.SetActive(true);
        }
    }

    private IEnumerator ResourceCountdown()
    {        
        while (count_down > 0)
        {
            count_down --;
            Debug.Log((float)count_down/total);
            health_dial.transform.localScale = new Vector3((float)count_down/total, 1.0f, 1.0f);
            //Debug.Log(count_down);
            //there must a better way to do this
            //while true or something like that 
            //wait while 
            yield return null;
            if(count_down <= 0)
            {

                global_variables.active_menu = null;
                //Reset the active interactive object 
                menu_actions.ActiveInteractiveReset();
                menu_actions.PlayerCanMove?.Invoke();
                //disable the game object from the interaction function 
                //error happening here with the game object? Need to find a fix. 
                //Maybe the interaction update is taking place again? And the destory itself needs to be put into another coroutine? 
                //And then the layer can be flipped again? 
                Destroy(this.gameObject);
                //reset active interactive? 
                //menu_actions.PlayerCanMove?.Invoke();
            }
        }
    }
}
