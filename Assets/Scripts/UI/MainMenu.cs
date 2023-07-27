using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("SFX")]
    public AudioClip buttonSoundEffect;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;
    }

    // This will LinkToWeb easily for windows build
    // For HTML5 Builds you will have to change the way it is intergrated as I have some of the scripts outlined already in a folder.
    // However you gotta add like a bunch of functions and stuff so look that up online if you plan to make a web build :]
    public void LinkToWeb(string url)
    {
        Application.OpenURL(url);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void PlayButtonSound()
    {
        audioManager.PlaySound(buttonSoundEffect);
    }
}
