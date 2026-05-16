using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalMenuController : MonoBehaviour
{
    [SerializeField] private GameObject goalMenuPanel;

    public void ShowGoalMenu()
    {
        ShowGoalMenuPanel();

        LetGameFreeze();

        LetMouseCursorBeVisible();
    }

    private void ShowGoalMenuPanel()
    {
        goalMenuPanel.SetActive(true);
    }

    private void LetGameFreeze()
    {
        Time.timeScale = 0f;
    }

    private void LetMouseCursorBeVisible()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Retry()
    {
        ContinueGame();

        LoadScene(FetchScene());
    }

    private void ContinueGame()
    {
        Time.timeScale = 1f;
    }

    private void LoadScene(Scene activeScene)
    {
        SceneManager.LoadScene(activeScene.name);
    }

    private Scene FetchScene()
    {
        return SceneManager.GetActiveScene();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
