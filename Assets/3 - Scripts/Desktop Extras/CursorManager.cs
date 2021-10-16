using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Transform target;
    public LayerMask layer; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out RaycastHit hit,1000,layer))
        {
            print("WE DID IT");
            target.position = new Vector3(hit.point.x, 0, hit.point.z); 
        }
        else
        {
            print("Nothing");
           // print(layer.value);
        }
    }
}
