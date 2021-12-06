using Robotgotchi.Dto.Identity;
using UnityEngine;

public class UserProfile : MonoBehaviour
{
    public string uid;
    // Start is called before the first frame update
    void Start()
    {
        var messageReceiver = GameObject.FindObjectOfType<MessageReceiver>();
        messageReceiver.UserInfoChanged += OnUserInfoChanged;
    }

    private void OnUserInfoChanged(UserInfo userInfo)
    {
        uid = userInfo.Uid;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
