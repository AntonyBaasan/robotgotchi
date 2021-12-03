using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogue_menu : MonoBehaviour
{
    public GameObject dialogue_menu_base;
    public Text main_text;
    public Image main_sprite;

    private void OnEnable()
    {
        menu_actions.OnDialogueHit += DialogueUpdate;
    }

    private void OnDisable()
    {
        menu_actions.OnDialogueHit -= DialogueUpdate;
    }

    //Update the dialogue
    //name, image, text 
    private void DialogueUpdate(string main_input, string talk_text, Sprite character_image, bool enable)
    {
        //Turn on or off the dialogue menu
        dialogue_menu_base.gameObject.SetActive(enable);
        //Set the main text 
        main_text.text = main_input;
        //Set the character image 
        main_sprite.sprite = character_image;
    }
}
