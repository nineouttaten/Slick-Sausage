using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Sausage.timer = 0;
        Sausage.reversed = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
