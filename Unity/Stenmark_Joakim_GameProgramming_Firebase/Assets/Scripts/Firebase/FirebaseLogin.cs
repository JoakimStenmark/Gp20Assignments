﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;

public class FirebaseLogin : MonoBehaviour
{
	public InputField signUpEmailInput;
	public InputField signUpPasswordInput;
    public InputField emailInput;
    public InputField passwordInput;

	//public Button loginButton;

	public TextMeshProUGUI statusText;


	private void Start()
    {
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
			{
				Debug.LogError(task.Exception);
			}

			//Run this the first time, then run the "SignIn" coroutine instead.

		});

	}


    public void SignUp()
    {
		StartCoroutine(RegUser(signUpEmailInput.text, signUpPasswordInput.text));
	}

	public void SignIn()
	{
		StartCoroutine(FirebaseSignIn(emailInput.text, passwordInput.text));
	}

	private IEnumerator RegUser(string email, string password)
	{
		Debug.Log("Starting Registration");
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
		var regTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

		yield return new WaitUntil(() => regTask.IsCompleted);

		if (regTask.Exception != null)
			Debug.LogWarning(regTask.Exception);
		else
        {
			Debug.Log("Registration Complete");
			MenuManager.instance.OpenLoginPanel();
        }
	}

	private IEnumerator FirebaseSignIn(string email, string password)
	{
		Debug.Log("Attempting to log in");
		statusText.text = "Attempting to log in";

		FirebaseAuth auth = FirebaseAuth.DefaultInstance;
		var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
		yield return new WaitUntil(() => loginTask.IsCompleted);

		if (loginTask.Exception != null)
        {
			Debug.LogWarning(loginTask.Exception);
			statusText.text = loginTask.Exception.InnerExceptions[0].InnerException.Message;
        }
		else
        {
			Debug.Log("login completed");
			statusText.text = "";

			ActiveUser.instance.LoadUserData();
			MenuManager.instance.OpenLobbyPanel();
			
        }

		
		//StartCoroutine(DataTest(FirebaseAuth.DefaultInstance.CurrentUser.UserId, "TestWrite"));
	}


}
