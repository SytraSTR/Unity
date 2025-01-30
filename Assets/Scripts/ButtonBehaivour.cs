using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaivor : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
