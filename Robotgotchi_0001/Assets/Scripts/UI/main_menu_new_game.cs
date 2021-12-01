using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu_new_game : MonoBehaviour
{
    //main active scene 
    public string main_active_scene;
    //public Scene next_scene;
    public List<string> scenes_to_load;

    public void SceneUpdate()
    {
        Debug.Log("Scenes Are Loading");
        SceneManager.LoadScene(main_active_scene);
        foreach (string scene_load in scenes_to_load)
        {
            SceneManager.LoadSceneAsync(scene_load, LoadSceneMode.Additive);
        }
    }
}
