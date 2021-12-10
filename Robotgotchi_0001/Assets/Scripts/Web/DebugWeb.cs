using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWeb : MonoBehaviour
{
    private NftManager nftManager;
    private MessageSender messageSender;
    private CharacterSelector characterSelector;

    // Start is called before the first frame update
    void Start()
    {
        this.messageSender = FindObjectOfType<MessageSender>();
        this.nftManager = FindObjectOfType<NftManager>();
        this.characterSelector = FindObjectOfType<CharacterSelector>();
    }

    // Update is called once per frame
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 40, 150, 30), "UpdateGlobalSettings"))
        {
            messageSender.UpdateGlobalSettings();
        }

        if (GUI.Button(new Rect(10, 80, 150, 30), "LoadUserNft"))
        {
            nftManager.LoadUserNft();
        }

        if (nftManager.UserNfts != null && nftManager.UserNfts.Count > 0)
        {
            var buttonStartPos = 80;
            foreach (var nft in nftManager.UserNfts)
            {
                buttonStartPos += 40;
                if (nft.Metadata == null)
                {
                    GUI.Button(new Rect(10, buttonStartPos, 200, 30), "(Empty Metadata)");
                    continue;
                }

                if (GUI.Button(new Rect(10, buttonStartPos, 200, 30), nft.Metadata.Name))
                {
                    characterSelector.SelectCharacter(nft.Metadata.Name);
                }
            }
        }
    }
}