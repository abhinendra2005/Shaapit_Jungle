using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 2f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        if (transform.position.x == 1000)
        {
            this.enabled = false;
        }
    }
}
