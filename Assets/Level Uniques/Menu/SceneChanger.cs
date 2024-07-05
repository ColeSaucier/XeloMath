using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void Update()
    {
        // Check for mouse input (button down) in the Update loop
        if (Input.GetMouseButtonDown(0))
        {
            // Call the method to change the scene
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}
