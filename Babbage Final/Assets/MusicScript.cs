using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicScript : MonoBehaviour
{   
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() {
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
        if (musicObjects.Length > 1) {
            Destroy(musicObjects[1]);
        } 
        DontDestroyOnLoad(musicObjects[0]);
        audioSource = musicObjects[0].GetComponent<AudioSource>();
    }

    public void playMusic() {
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }

    public void stopMusic() {
        audioSource.Stop(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
