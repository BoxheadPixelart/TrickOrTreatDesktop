using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrigger : MonoBehaviour
{

    public List<GameObject> itemsInCursor = new List<GameObject>();
    public List<Rigidbody> balls = new List<Rigidbody>();
    public float maxThrow;
    bool isGrip; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrip = Input.GetMouseButton(0); 
        if (isGrip)
        {
            foreach (var ball in balls)
            {
                ball.transform.LookAt(gameObject.transform.GetChild(0));
                var heading = transform.GetChild(0).position - ball.transform.position;
                ball.velocity += ((heading * 10)- ball.velocity) * .8f; 
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            foreach (var ball in balls)
            {
                ball.velocity = Vector3.ClampMagnitude(ball.velocity, maxThrow); 
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            if (!balls.Contains(other.attachedRigidbody))
            {
                balls.Add(other.attachedRigidbody);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        { 
            if (!isGrip)
            {
                balls.Remove(other.attachedRigidbody);
            }
            
        }
    }
}
