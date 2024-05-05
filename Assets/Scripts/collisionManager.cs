using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionManager : MonoBehaviour
{
    [SerializeField] float SceneChangeDelay = 1;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip levelWinAudio;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem levelWinParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool disableCollisions = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeScene();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            disableCollisions = !disableCollisions;
        }

    }

    void OnCollisionEnter(Collision other) {    
        if(isTransitioning || disableCollisions){ return; }
        switch (other.gameObject.tag){
            case "Friendly":
                Debug.Log("You are on the launch pad");
                break;
            case "Fuel":
                Debug.Log("You touched the fuel powerup");
                break;
            case "Finish":
                LoadNextLevel();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    
    void StartCrashSequence(){
        // add particle affects
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        DisableRocketMovement();
        crashParticles.Play();
        Invoke("reloadLevel", 1f);

    }
    void reloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

        void LoadNextLevel(){
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(levelWinAudio);
        DisableRocketMovement();
        levelWinParticles.Play();
        Invoke("ChangeScene", SceneChangeDelay);
    }

    void ChangeScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    void DisableRocketMovement(){
        GetComponent<Movement>().enabled = false;
    }

}
