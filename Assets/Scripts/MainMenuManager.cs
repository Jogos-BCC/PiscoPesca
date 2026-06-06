using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Debug.Log("Carregando cena: " + sceneName);

        SceneManager.LoadScene(sceneName);
    }
}
