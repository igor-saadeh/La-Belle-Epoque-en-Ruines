using UnityEngine;


public class CameraFollower : MonoBehaviour
{
    public Transform player; 
    public float normalFollowSpeed = 5f; 
    public float increasedFollowSpeed = 7f; 
    public float maxDistanceBehind = 2f;
    public float minDistanceBeforeGameOver = 0.5f; 
    private float currentFollowSpeed; 
    private bool isColliding = false; 

    void Start()
    {
        
        currentFollowSpeed = normalFollowSpeed;
    }

    void Update()
    {
        
        Vector3 cameraPosition = transform.position;

        
        float playerX = player.position.x;

        
        float distance = playerX - cameraPosition.x;

      
        cameraPosition.x = Mathf.Lerp(cameraPosition.x, playerX - maxDistanceBehind, currentFollowSpeed * Time.deltaTime);

        transform.position = cameraPosition;

        
        if (distance < minDistanceBeforeGameOver)
        {
            
            Debug.Log("Game Over!");
        }
    }

    
    public void OnPlayerCollision()
    {
        isColliding = true;
        currentFollowSpeed = increasedFollowSpeed; 
    }

    public void RecoverFromCollision()
    {
        isColliding = false;
        currentFollowSpeed = normalFollowSpeed;
    }
}
