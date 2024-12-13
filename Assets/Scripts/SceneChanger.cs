using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            // Carrega a pr�xima cena
            AudioManager.instance.Stop("Scene1Theme");
            AudioManager.instance.Play("Scene2Theme");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}