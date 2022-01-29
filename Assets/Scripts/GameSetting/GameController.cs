using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private GameObject gameOverMenu;


    void Start()
    {
        _gameStats.isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    private void GameOver()
    {
        if (_playerStats.Health <= 0)
        {
            _gameStats.isGameOver = true;
            gameOverMenu.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
