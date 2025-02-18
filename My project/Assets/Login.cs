using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button registerButton;

    void Start()
    {
        loginButton.onClick.AddListener(() => 
        { 
            StartCoroutine(Main.Instance.web.Login(usernameInput.text, passwordInput.text));
        });

        registerButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.Instance.web.RegisterUser(usernameInput.text, passwordInput.text));
        });
    }
}
