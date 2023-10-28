using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwicher : MonoBehaviour
{
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
