using Firebase.Auth;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FireBaseAuthManager
{

    private static FireBaseAuthManager instance = null;

    public static FireBaseAuthManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FireBaseAuthManager();
            }
            return instance;
        }
    }

    private FirebaseAuth auth;
    private FirebaseUser user;

    public void Init()
    {
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += OnChanged;
    }

  private void OnChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("로그아웃: " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("로그인: " + user.UserId);
            }
        }
    }

    public void Create(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("에러이유: " + task.Exception);
                return;
            }

            AuthResult result = task.Result;
            FirebaseUser newUser = result.User;

            Debug.Log("회원가입 성공: " + newUser.Email);
        });
    }

    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("에러이유: " + task.Exception);
                return;
            }

            AuthResult result = task.Result;
            FirebaseUser newUser = result.User;
            SceneManager.LoadScene("Lobby");

            Debug.Log("로그인 성공: " + newUser.Email);
        });
    }

    public void Logout()
    {
        auth.SignOut();
    }
}
