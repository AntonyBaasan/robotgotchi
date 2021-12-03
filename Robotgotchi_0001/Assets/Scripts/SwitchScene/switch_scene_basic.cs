using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switch_scene_basic : MonoBehaviour
{
    public string scene_goto;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Might have to also check to see if the player is facing the door 
    //Having this be an input with a prompt might help with this 
    private void SwitchScene()
    {
        Debug.Log("In The door");
        //SceneManager.LoadScene(scene_goto);
    }
}
