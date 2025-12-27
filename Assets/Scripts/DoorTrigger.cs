using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public string targetScene;
    public string targetSpawnPoint;

    private bool playerInside;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            SceneLoader.Instance.LoadScene(targetScene, targetSpawnPoint);
        }
    }
}