using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 30f;
    public float range;
    public float maxAngle;
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        Debug.Log("Angle: " + transform.localEulerAngles.z);
        float angle = transform.localEulerAngles.z;
        if (Input.GetMouseButton(0) && (angle < maxAngle || angle > 360 - maxAngle - 5))
        {
            // rotate paddle to left
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        }
        if (Input.GetMouseButton(1) && ( angle > 360 - maxAngle || angle < maxAngle+5))
        {
            // rotate paddle to right
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
        }
    }

    void HandleMovement()
    {
        float distance = Input.GetAxis("Horizontal")* moveSpeed * Time.deltaTime;
        Vector3 currentPosition = transform.position;
        if(Mathf.Abs(currentPosition.x+distance) > range)
        {
            return;
        }
        currentPosition.x += distance;
        transform.position = currentPosition;
    }

}
