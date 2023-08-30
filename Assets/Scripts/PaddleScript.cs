using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float distance = Input.GetAxis("Horizontal")* moveSpeed * Time.deltaTime;
        Vector3 currentPosition = transform.position;
        currentPosition.x += distance;
        transform.position = currentPosition;
    }

}
