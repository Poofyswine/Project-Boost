using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionManager : MonoBehaviour
{
    
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
                reloadScene();
                break;
        }
    }

    void reloadScene(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

        void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            SceneManager.LoadScene(0);
        }
        else{
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

}
