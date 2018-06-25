using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHere : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Rabbit rabbit = other.GetComponent<Rabbit>();

        if (rabbit != null)
        {
            if (SceneManager.GetActiveScene().name != "LevelChooser")
                LevelController.Current.OnRabbitDeath(rabbit);
            else 
                rabbit.Revive();
        }
    }
}