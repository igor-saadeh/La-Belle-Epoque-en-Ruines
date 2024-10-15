using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolowr : MonoBehaviour
{
    // Refer�ncia ao jogador
    public Transform player;

    // Velocidade de suaviza��o
    public float smoothSpeed = 0.125f;

    // Offset horizontal (apenas eixo X)
    public float offsetX = 5f;

    // Update � chamado uma vez por frame
    void FixedUpdate()
    {
        // Apenas movemos a c�mera no eixo X (horizontal)
        Vector3 desiredPosition = new Vector3(player.position.x + offsetX, transform.position.y, transform.position.z);

        // Suavizamos a transi��o usando Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualizamos a posi��o da c�mera
        transform.position = smoothedPosition;
    }
}
