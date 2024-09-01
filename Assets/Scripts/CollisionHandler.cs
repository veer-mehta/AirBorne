using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip crashing;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashingParticles;


    float loadSceneDelay = 1f;
    bool isTransitioning=false;
    bool isCollisionDisabled=false;
    void Start()
    {
        movement = GetComponent<Movement>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleDebugKeys();
    }

    void HandleDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCollisionDisabled = !isCollisionDisabled;
        }
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        HandleCollisions(other);
    }

    void HandleCollisions(Collision other)
    {
        if (isTransitioning || isCollisionDisabled) { return; }
        switch (other.gameObject.tag)
        {
            case "Obstacle":
                InitiateCrashSequence();
                break;
            case "LandingPad":
                InitiateNextLevel();
                break;
            default:
                break;

        }
    }

    void InitiateCrashSequence()
    {
        isTransitioning = true;
        source.PlayOneShot(crashing);
        crashingParticles.Play();
        movement.enabled=false;
        Invoke("ReloadScene", loadSceneDelay);
    }
    void InitiateNextLevel()
    {
        isTransitioning=true;
        source.PlayOneShot(success);
        crashingParticles.Play();
        movement.enabled = false;
        Invoke("LoadNextScene", loadSceneDelay);
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            Application.Quit();
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
