using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class dialogue_base : MonoBehaviour
{
    public GameObject dialogue_menu;
    [SerializeField]
    private GameObject dialogue_text;

    public List<string> dialogue;
    public string dialogue_active;
    private int dialogue_counter = 0;
    public bool dialgue_complete = true;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogue_menu = GameObject.FindGameObjectWithTag("dialogue_menu");
        dialogue_text = dialogue_menu.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
    }

    public void RunInteraction()
    {
        //you might not need the counter, just grab the index of the update
        if(dialogue_counter == dialogue.Count)
        {
            dialogue_counter = 0;
            dialogue_menu.transform.GetChild(0).gameObject.SetActive(false);
            dialgue_complete = true;
        }
        else
        {
            dialogue_active = dialogue[dialogue_counter];
            dialogue_text.GetComponent<Text>().text = dialogue_active;
            dialogue_menu.transform.GetChild(0).gameObject.SetActive(true);
            dialogue_counter += 1;
            dialgue_complete = false;
        }
    }

}
