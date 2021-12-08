using Robotgotchi.Dto.Identity;
using UnityEngine;
using UnityEngine.UI;

public class LoginUiGroup : MonoBehaviour
{
    public Text UidText;
    public GameObject LoginButton;

    public GameObject LogoutButton;
    // Start is called before the first frame update

    void Awake()
    {
        var messageReceiver = FindObjectOfType<MessageReceiver>();
        messageReceiver.UserInfoChanged += OnUserInfoChanged;

        this.UpdateButtonState(null);
    }

    void Start()
    {
    }

    private void OnUserInfoChanged(UserInfo user)
    {
        this.UpdateButtonState(user);
    }

    private void UpdateButtonState(UserInfo user)
    {
        if (user != null)
        {
            UidText.text = user.Uid;
            LoginButton.SetActive(false);
            LogoutButton.SetActive(true);
        }
        else
        {
            UidText.text = "";
            LoginButton.SetActive(true);
            LogoutButton.SetActive(false);
        }
    }
}