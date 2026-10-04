using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        if (SceneManager.GetSceneByName(sceneName).IsValid())
        {
            Debug.Log(sceneName + " has successfully loaded!");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
