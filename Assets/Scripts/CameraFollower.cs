using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f; // Velocidade normal da câmera
    public float catchUpSpeed = 10f; // Velocidade de aproximação após colisão
    public float cameraOffset = 5f; // Distância normal da câmera ao jogador

    private bool playerHitObstacle = false;

    void Update()
    {
        // Posição alvo da câmera (sempre segue o player)
        Vector3 targetPosition = new Vector3(player.position.x + cameraOffset, transform.position.y, transform.position.z);

        // Verifica se o player bateu no obstáculo
        if (playerHitObstacle)
        {
            // Aproxima mais rápido
            transform.position = Vector3.Lerp(transform.position, targetPosition, catchUpSpeed * Time.deltaTime);
        }
        else
        {
            // Segue normalmente
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    // Este método é chamado quando o player bate em um obstáculo
    public void PlayerHitObstacle()
    {
        playerHitObstacle = true;
    }
}
