using System.Collections;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;
using System;

public class GooglePlayManager : MonoBehaviour
{
    public Text authStatus;


    private void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        OnConnectionResponse(PlayGamesPlatform.Instance.localUser.authenticated);
    }

    private void OnConnectionResponse(bool authenticated)
    {
        if (authenticated)
        {
            authStatus.text = "Logged";
        }
        else
        {

        }
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInCallback);
    }


    public void SignInCallback(bool success)
    {
        if (success)
        {
            Debug.Log("(Lollygagger) Signed in!");

            authStatus.text = "Signed in as: " + Social.localUser.userName;
        }
        else
        {
            Debug.Log("(Lollygagger) Sign-in failed...");

            authStatus.text = "Sign-in failed";
        }
    }
}
