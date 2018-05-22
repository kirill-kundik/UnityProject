using UnityEngine;

public class RabbitCameraScript : MonoBehaviour
{
    public Transform Rabbit;

    private Vector2 velocity;
    
    public float SmoothTimeX;
    public float SmoothTimeY;

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, Rabbit.position.x, ref velocity.x, SmoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, Rabbit.position.y, ref velocity.y, SmoothTimeY);
        
        transform.position = new Vector3(posX, posY, transform.position.z);
        
    }
}