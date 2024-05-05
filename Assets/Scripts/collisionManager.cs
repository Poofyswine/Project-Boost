using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionManager : MonoBehaviour
{
    [SerializeField] float SceneChangeDelay = 1;
    void OnCollisionEnter(Collision other) {    
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
        // add sound affect and particle affects
        DisableRocketMovement();
         Invoke("reloadLevel", 1f);

    }
    void reloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

        void LoadNextLevel(){
        DisableRocketMovement();
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
