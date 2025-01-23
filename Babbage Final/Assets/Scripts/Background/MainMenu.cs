using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadSceneAsync("MainScene");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().stopMusic();
    }

    public void Customize(){
        SceneManager.LoadSceneAsync("Customization");
    }

    public void Homescreen(){
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void PapaF(){
        SceneManager.LoadSceneAsync("choosethisone");
    }
}
