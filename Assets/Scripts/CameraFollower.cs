using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f; // Velocidade normal da c�mera
    public float catchUpSpeed = 10f; // Velocidade de aproxima��o ap�s colis�o
    public float cameraOffset = 5f; // Dist�ncia normal da c�mera ao jogador

    private bool playerHitObstacle = false;

    void Update()
    {
        // Posi��o alvo da c�mera (sempre segue o player)
        Vector3 targetPosition = new Vector3(player.position.x + cameraOffset, transform.position.y, transform.position.z);

        // Verifica se o player bateu no obst�culo
        if (playerHitObstacle)
        {
            // Aproxima mais r�pido
            transform.position = Vector3.Lerp(transform.position, targetPosition, catchUpSpeed * Time.deltaTime);
        }
        else
        {
            // Segue normalmente
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    // Este m�todo � chamado quando o player bate em um obst�culo
    public void PlayerHitObstacle()
    {
        playerHitObstacle = true;
    }
}
