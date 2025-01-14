using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextLevelDelay = 2f;
    [SerializeField] float currentReloadLevelDelay = 2f; 

    public bool isControllable = true;
    bool isCollidable =true;

    AudioSource audioSource;
    [SerializeField] AudioClip finishAudio;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] ParticleSystem succesParticle;
    [SerializeField] ParticleSystem explosiveParticle;

 
    

    void Start() 

    {
    audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        ResponTheDebugKeys();
    }
    void OnCollisionEnter(Collision other) 
    {
        if(!isControllable || !isCollidable) {return;}

        switch (other.gameObject.tag)
        {
        case "Friendly":
            break;
        case "Finish":
            StartNextLevelSquance();
            break;
        case "Fuel":
            break;
        default:
            StartCrashSquance();
            break;
        }
    }

    private void StartNextLevelSquance()
    {
        isControllable = false;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(finishAudio);
        succesParticle.Play();
        Invoke("LoadNextLevel", nextLevelDelay);
    }

    void StartCrashSquance()
    {
        isControllable = false;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashAudio);
        explosiveParticle.Play();
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
    
    void ResponTheDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            isCollidable =!isCollidable;
            Debug.Log("C key pressed"); 
        }
    }
}
