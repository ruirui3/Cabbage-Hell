using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{   
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
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
