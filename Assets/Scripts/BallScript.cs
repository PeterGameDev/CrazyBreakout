using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    private float initialSpeed = 10f;
    private float currentSpeed = 0f;
    private Vector3 direction;
    public bool onPaddle = true;
    public GameObject paddle;
    public float bottomBoundary = -33f;
    // Start is called before the first frame update
    void Start()
    {
        direction = transform.up;
    }

    // Update is called once per frame
    void Update()
    {        
        // launch the ball on player input
        if (Input.GetButtonDown("Jump"))
        {
            Launch();
        }

        // the ball stays with the paddle if it's on the paddle
        if (onPaddle)
        {
            Vector3 newPosition = transform.position;
            Vector3 offset = Vector3.up;
            //newPosition.x += Input.GetAxis("Horizontal") * paddle.GetComponent<PaddleScript>().moveSpeed * Time.deltaTime;
            transform.position = paddle.transform.position + offset;
            return;
        }

        // move straight by default
        Vector3 movement = direction * currentSpeed * Time.deltaTime;
        transform.position += movement;

        CheckBoundary();
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
        if (collision.gameObject.CompareTag("Brick"))
        {
            StartCoroutine(HitBrick(collision.gameObject));
        }
    }

    IEnumerator HitBrick(GameObject brick)
    {
        int oldCount = FindObjectsOfType<BrickScript>().Length;
        Destroy(brick);


        // wait a bit for destroy to finish before check for win
        // can change to wait for seconds for better performance

        // yield return new WaitforSeconds(0.5);
        yield return new WaitUntil( () => oldCount != FindObjectsOfType<BrickScript>().Length);
        Debug.Log("Remaining bricks: "+FindObjectsOfType<BrickScript>().Length);
        GameManager.Instance.CheckWin();
    }


    private void CheckBoundary()
    {
        if(transform.position.y < bottomBoundary)
        {
            Destroy(gameObject);
            GameManager.Instance.GameOver();
        }
    }


}
