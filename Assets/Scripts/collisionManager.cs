using UnityEngine;

public class collisionManager : MonoBehaviour
{
    
    void OnCollisionEnter(Collision other) {    switch (other.gameObject.tag){
            case "Friendly":
                Debug.Log("You are on the launch pad");
                break;
            case "Fuel":
                Debug.Log("You touched the fuel powerup");
                break;
            case "Finish":
                Debug.Log("You touched the finish pad");
                break;
            default:
                Debug.Log("You Died");
                break;
        }
    }
}
