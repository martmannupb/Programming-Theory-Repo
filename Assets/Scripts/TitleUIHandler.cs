using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleUIHandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SetDifficultyEasy()
    {
        DataManager.Instance.difficulty = DataManager.Difficulty.EASY;
        StartGame();
    }

    public void SetDifficultyMedium()
    {
        DataManager.Instance.difficulty = DataManager.Difficulty.MEDIUM;
        StartGame();
    }

    public void SetDifficultyHard()
    {
        DataManager.Instance.difficulty = DataManager.Difficulty.HARD;
        StartGame();
    }
}
