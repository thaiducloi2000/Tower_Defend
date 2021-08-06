using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameEnd;
    public GameObject gameOverUI;

    private void Start()
    {
        gameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd)
        {
            return;
        }
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }
        if(PlayerStats.lives < 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnd = true;
        gameOverUI.SetActive(true);
    }
}
