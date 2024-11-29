

using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    [Tooltip("Nome da cena para carregar. Certifique-se de que o nome esteja correto.")]
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou no trigger � o player
        if (other.CompareTag("Player"))
        {
            // Carrega a pr�xima cena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}