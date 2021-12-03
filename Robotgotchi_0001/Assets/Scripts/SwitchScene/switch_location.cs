using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switch_location : MonoBehaviour
{
    //main active scene 
    public string main_active_scene;
    //Scenes To Load 
    public List<string> scenes_to_load;
    //Scene To Unload 
    public List<string> scenes_to_unload;

    public void SceneUpdate()
    {
        menu_actions.PlayerCanMove?.Invoke();
        //Set the active scene 
        if (main_active_scene != "")
        {
            SceneManager.LoadScene(main_active_scene);
        }
        //Load Scenes 
        foreach (string scene_load in scenes_to_load)
        {
            SceneManager.LoadSceneAsync(scene_load, LoadSceneMode.Additive);
        }
        //Unload Scenes 
        foreach (string scene_unload in scenes_to_unload)
        {
            SceneManager.UnloadSceneAsync(scene_unload);
        }
        //update the character interactive objects 
        menu_actions.ActiveInteractiveReset?.Invoke();
    }
}
