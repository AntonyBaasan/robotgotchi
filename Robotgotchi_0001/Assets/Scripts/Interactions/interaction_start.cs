using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class interaction_start : MonoBehaviour
{
    //interaction check 
    public GameObject interaction_message;
    //check to see if there is a UI 
    public bool has_ui = false;
    //Interaction counter 
    public int interaction_counter_input = 0;
    //make a counter 
    public bool is_a_counter = false;
    //Unity Events to call 
    public UnityEvent interaction_event;
}
