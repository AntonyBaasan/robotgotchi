using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class menu_actions
{
    //Main menu hit actions 
    public static Action<string> OnMenuHit;
    //Dialogue menu hit action  - text, text, udpate the image 
    public static Action<string, string, Sprite, bool> OnDialogueHit;
    //Can the player move?
    public static Action PlayerCanMove;
    public static Action<Vector3> PlayerLocationUpdate;
    // Flip the player interactive objects to be off 
    public static Action ActiveInteractiveReset;
  

}
