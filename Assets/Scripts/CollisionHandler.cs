using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextLevelDelay = 2f;
    [SerializeField] float currentReloadLevelDelay = 2f; 

    AudioSource audioSource;
    [SerializeField] AudioClip finishAudio;
    [SerializeField] AudioClip crashAudio;

    void Start()  
    {
    audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) 
    {

        switch (other.gameObject.tag)
        {
        case "Friendly":
            break;
        case "Finish":
            StartNextLevel();
            break;
        case "Fuel":
            break;
        default:
            StartCrashSquance();
            break;
        }
    }

    private void StartNextLevel()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(finishAudio);
        Invoke("LoadNextLevel", nextLevelDelay);
    }

    void StartCrashSquance()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashAudio);
        Invoke("CurrentReloadLevel", currentReloadLevelDelay);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene+1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    void CurrentReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    
}
