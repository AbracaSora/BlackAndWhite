using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private string targetSpawnPoint;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName, string spawnPointID)
    {
        targetSpawnPoint = spawnPointID;
        StartCoroutine(LoadRoutine(sceneName));
    }

    private IEnumerator LoadRoutine(string sceneName)
    {
        // 这里可以接 UI 淡出
        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene(sceneName);

        yield return null; // 等一帧，确保场景加载完成

        PlacePlayer();
    }

    private void PlacePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SpawnPoint[] points = Object.FindObjectsByType<SpawnPoint>(
            FindObjectsSortMode.None
        );

        foreach (var p in points)
        {
            if (p.spawnID == targetSpawnPoint)
            {
                player.transform.position = p.transform.position;
                return;
            }
        }

        Debug.LogWarning("未找到 SpawnPoint: " + targetSpawnPoint);
    }
}