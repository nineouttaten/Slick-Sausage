using UnityEngine;
using UnityEngine.SceneManagement;

public class CatScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(3);
    }
}
