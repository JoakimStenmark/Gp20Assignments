using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public static MenuManager instance;

    public CanvasGroup signUpGroup;
    public CanvasGroup characterCreateGroup;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        signUpGroup.alpha = 0f;
        characterCreateGroup.alpha = 0f;

    }
    public void OpenLoginPanel()
    {
        //signUpPanel.SetActive(false);
        //loginPanel.SetActive(true);
        signUpGroup.interactable = !signUpGroup.interactable;
        signUpGroup.alpha = 0f;
        signUpGroup.blocksRaycasts = false;
    }

    public void OpenSignUpPanel()
    {
        //signUpPanel.SetActive(true);
        //loginPanel.SetActive(false);
        signUpGroup.interactable = !signUpGroup.interactable;
        signUpGroup.alpha = 1f;
        signUpGroup.blocksRaycasts = true;

    }

    public void ToggleCharacterCreateWindow()
    {
        characterCreateGroup.interactable = !characterCreateGroup.interactable;
        characterCreateGroup.alpha = characterCreateGroup.alpha == 0f ? 1f : 0f;
        characterCreateGroup.blocksRaycasts = !characterCreateGroup.blocksRaycasts;



    }

    public void GoToGame()
    {
        GetComponent<SceneHandler>().LoadLevel(1);
    }

}
