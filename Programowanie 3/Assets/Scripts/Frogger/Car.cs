using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float moveSpeed = 5;

    [SerializeField] float raycastDistance;
    [SerializeField] Transform raycastStart;

    void Update()
    {
        DetectCarInFront();
        Move();
    }

    void DetectCarInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastStart.position, transform.right, raycastDistance);
        if (hit.collider)
        {
            Car carInFront = hit.collider.GetComponent<Car>();
            if (carInFront)
            {
                moveSpeed = carInFront.moveSpeed;
            }
        }
        Debug.DrawRay(raycastStart.position, transform.right * raycastDistance, Color.cyan);
    }

    private void Move()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
