using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    private float initialSpeed = 10f;
    public float currentSpeed = 0f;
    public float speedInterval = 10f;
    [SerializeField]
    private float maxSpeed = 40f;
    [SerializeField]
    private float adjustAngle = 150f;
    private Vector3 direction;
    public GameObject paddle;
    public float bottomBoundary = -33f;
    public ParticleSystem brickBreakParticleSystem;
    public Level1Manager level1Manager;
    // Start is called before the first frame update
    void Start()
    {
        direction = (Vector3.down + Vector3.right) / 2;

        // test horizontal adjustment
        //direction = (Vector3.right + new Vector3(0, -0.1f, 0)).normalized;
        //direction = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 29.5 && direction.y > 0)
        {
            Vector3 normal = new Vector3(0, -1, 0);
            direction = CalculateBounceBack(direction, normal);
            SFXScript.Instance.playBallBounceSound();
            PathCalculation();
        }
        if ((transform.position.x <= -18.5 && direction.x < 0) || (transform.position.x >= 18.5 && direction.x > 0))
        {
            Vector3 normal = new Vector3(0, 0, 0);
            if(direction.x < 0)
            {
                normal = new Vector3(1, 0, 0);
                SFXScript.Instance.playBallBounceSound();
            }
            if(direction.x > 0)
            {
                normal = new Vector3(-1, 0, 0);
                SFXScript.Instance.playBallBounceSound();
            }
            direction = CalculateBounceBack(direction, normal);
            PathCalculation();
        }
        // launch the ball on player input
        if (Input.GetButtonDown("Jump"))
        {
            Launch();
        }

        // the ball stays with the paddle if it's on the paddle
        if (!GameManager.Instance.ballLaunched)
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
        if (!GameManager.Instance.ballLaunched)
        {
            Debug.Log("Launch!");
            currentSpeed = initialSpeed;
            GameManager.Instance.ballLaunched = true;
        }
    }

    // Calculate the bounce back when ball hit objects
    private void OnCollisionEnter(Collision collision)
    {
        SFXScript.Instance.playBallBounceSound();

        Vector3 normal = collision.GetContact(0).normal;

        direction -= 2 * Vector3.Dot(direction, normal) * normal;
        if (collision.gameObject.CompareTag("Brick"))
        {
            StartCoroutine(HitBrick(collision.gameObject));
        }
        // Game over if hit bottom death plane
        if (collision.gameObject.CompareTag("DeathPlane"))
        {
            Destroy(gameObject);
            level1Manager.GameOver();
        }
        if (!collision.gameObject.CompareTag("Paddle"))
        {
            PathCalculation();
        }

    }

    private Vector3 CalculateBounceBack(Vector3 direction, Vector3 normal)
    {
        var angle = Vector3.Angle(direction, normal);
        if (angle > adjustAngle)
        {
            // make ball go upward if totally horizontal
            if (angle == 180)
            {
                direction += new Vector3(0, 0.1f, 0);
            }
            // add adjustAngle angle
            direction = Vector3.RotateTowards(direction, normal, Mathf.Deg2Rad * 5f, Mathf.Infinity);
        }
        direction -= 2 * Vector3.Dot(direction, normal) * normal;
        PathCalculation();
        return direction;
    }

    IEnumerator HitBrick(GameObject brick)
    {

        brick.GetComponent<BrickScript>().Destroy();
        yield return null;
        level1Manager.CheckWin();
    }

    private void CheckBoundary()
    {
        if(transform.position.y < bottomBoundary)
        {
            Destroy(gameObject);
            level1Manager.GameOver();
        }
    }

    // Calculate the path of the ball
    private void PathCalculation()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit)) 
        {
            //Debug.Log("Path gonna hit "+hit.point);
            if(hit.point.y < -10) 
            {
                currentSpeed = initialSpeed;
                //Debug.Log("Slow down");
            }
            else
            {
                if (currentSpeed < maxSpeed)
                {
                    currentSpeed += speedInterval;
                    //Debug.Log("Speed up");
                }
            }
        }
    }


}
