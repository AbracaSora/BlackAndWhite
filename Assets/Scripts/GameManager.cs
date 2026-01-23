using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    void Awake()
    {
        // 如果已经有一个实例存在，则销毁新的 GameManager 实例
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // 保证 GameManager 在场景切换时不销毁
        }
        else
        {
            Destroy(gameObject);  // 如果已经存在，则销毁这个 GameManager 实例
        }
    }

    void Start()
    {
        // 如果玩家之前玩过，加载上次保存的周目数
        int currentPlaythrough = PlayerPrefs.GetInt("Playthrough", 1); // 默认是第一个周目

        // 根据当前周目数决定游戏的初始状态
        if (currentPlaythrough == 1)
        {
            // 设置为第一次周目
            Debug.Log("第一次周目");
        }
        else if (currentPlaythrough == 2)
        {
            // 设置为第二次周目
            Debug.Log("第二次周目");
        }
    }
    
    public void SetPlaythrough(int playthrough)
    {
        PlayerPrefs.SetInt("Playthrough", playthrough);
        PlayerPrefs.Save();
    }
}