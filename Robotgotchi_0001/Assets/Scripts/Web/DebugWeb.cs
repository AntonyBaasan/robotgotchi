using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWeb : MonoBehaviour
{
    private NftManager nftManager;
    private MessageSender messageSender;
    
    // Start is called before the first frame update
    void Start()
    {
        this.messageSender = FindObjectOfType<MessageSender>();
        this.nftManager = FindObjectOfType<NftManager>();

    }

    // Update is called once per frame
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 40, 100, 30), "UpdateGlobalSettings"))
        {
            messageSender.UpdateGlobalSettings();
        }

        if (GUI.Button(new Rect(10, 80, 100, 30), "LoadUserNft"))
        {
            nftManager.LoadUserNft();
        }

    }
}
