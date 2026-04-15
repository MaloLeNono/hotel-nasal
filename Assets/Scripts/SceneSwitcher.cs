using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private const string CreditsScene = "Credits";
    
    public void SwitchScene()
    {
        SceneManager.LoadScene(CreditsScene);
    }
}
