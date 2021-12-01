using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction_start : MonoBehaviour
{
    //base interaction properties 
    public string interaction_name;
    public bool character_move = true;
    
    //check to see if there is a UI 
    public bool has_ui = false;
    public GameObject ui_input;
    public string ui_name;

}
