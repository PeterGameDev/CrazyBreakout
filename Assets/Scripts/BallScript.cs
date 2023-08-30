using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    private float initialSpeed = 10f;
    private float currentSpeed = 0f;
    private Vector3 direction;
    public bool onPaddle = true;
    public GameObject paddle;
    // Start is called before the first frame update
    void Start()
    {
        direction = transform.up;
    }

    // Update is called once per frame
    void Update()
    {        // launch the ball on player input
        if (Input.GetButtonDown("Jump"))
        {
            Launch();
        }

        // the ball stays with the paddle if it's on the paddle
        if (onPaddle)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += Input.GetAxis("Horizontal") * paddle.GetComponent<PaddleScript>().moveSpeed * Time.deltaTime;
            transform.position = newPosition;
            return;
        }


        // move straight by default
        Vector3 movement = direction * currentSpeed * Time.deltaTime;
        transform.position += movement;
    }

    public void Launch()
    {
        Debug.Log("Launch!");
        currentSpeed = initialSpeed;
        direction = transform.up;
        onPaddle = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.GetContact(0).normal;
        direction -= 2 * Vector3.Dot(direction, normal) * normal;
        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    hit wall, bounce back, calculate with vector reflection calculation
        //   direction -= 2 * Vector3.Dot(direction, Vector3.left) * Vector3.left;

        //}
        //if (collision.gameObject.CompareTag("Ceiling"))
        //{
        //    direction -= 2 * Vector3.Dot(direction, Vector3.down) * Vector3.down;
        //}
        //if (collision.gameObject.CompareTag("Paddle"))
        //{
        //    direction -= 2 * Vector3.Dot(direction, Vector3.up) * Vector3.up;
        //}
        //if (collision.gameObject.CompareTag("Brick"))
        //{
        //    Vector3 normal = collision.GetContact(0).normal;
        //    direction -= 2 * Vector3.Dot(direction, normal) * normal;
        //}
        }

}
