using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject menu1;

    [SerializeField]
    private GameObject menu2;

    [SerializeField]
    private TMP_InputField InputField;

    // Start is called before the first frame update
    void Start()
    {
		AkSoundEngine.PostEvent("MENU_START", gameObject);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        menu1.SetActive(false);
        menu2.SetActive(true);
    }

    public void ReturnToMenu1()
    {
        menu2.SetActive(false);
        menu1.SetActive(true);
    }

    Text nameEntered;
    public void NameEntered()
    {
        if(InputField.text != "")
        {
            Debug.Log("Input Correct");
        GameController.PlayerName = InputField.text;
		AkSoundEngine.PostEvent("MENU_STOP", gameObject);
		SceneManager.LoadScene(1);
        }
    }

    public void TwitterButton()
    {
        Application.OpenURL("https://twitter.com/NousBienvenue");
    }

}
