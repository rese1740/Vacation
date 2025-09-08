using TMPro;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;

    void Start()
    {
        FireBaseAuthManager.Instance.Init();
    }

    public void Create()
    {
        string e = email.text;
        string p = password.text;
        FireBaseAuthManager.Instance.Create(e, p);
    }

    public void Login()
    {
        string e = email.text;
        string p = password.text;
        FireBaseAuthManager.Instance.Login(e, p);

    }

    public void Logout()
    {
        FireBaseAuthManager.Instance.Logout();
    }
}
