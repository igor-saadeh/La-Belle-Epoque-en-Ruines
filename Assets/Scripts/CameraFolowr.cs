using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolowr : MonoBehaviour
{
    // Referência ao jogador
    public Transform player;

    // Velocidade de suavização
    public float smoothSpeed = 0.125f;

    // Offset horizontal (apenas eixo X)
    public float offsetX = 5f;

    // Update é chamado uma vez por frame
    void FixedUpdate()
    {
        // Apenas movemos a câmera no eixo X (horizontal)
        Vector3 desiredPosition = new Vector3(player.position.x + offsetX, transform.position.y, transform.position.z);

        // Suavizamos a transição usando Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualizamos a posição da câmera
        transform.position = smoothedPosition;
    }
}
