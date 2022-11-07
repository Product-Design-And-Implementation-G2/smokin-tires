using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeCarController : MonoBehaviour
{
    private float moveInput;
    private float turnInput;
    private bool isCarGrounded;

    public float airDrag;
    public float groundDrag;

    public float fwdSpeed;
    public float reverseSpeed;
    public float turnSpeed;
    public LayerMask groundLayer;

    public Rigidbody sphereRB;

    void Start()
    {
        //detaches the rigidbody from the car
        sphereRB.transform.parent = null;
    }

    //use update when not dealing with physics objects
    void Update()
    {
        //get user input
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        //adjusts speed for the car
        //when moveInput is >0: true => fwdSpeed; false => reverseSpeed
        moveInput *= moveInput > 0 ? fwdSpeed : reverseSpeed;

        //set cars position to sphere
        transform.position = sphereRB.transform.position;

        //set cars rotation
        float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0, newRotation, 0, Space.World);

        //raycast ground check
        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        //rotate car to be parallel to the ground
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        if(isCarGrounded)
        {
            sphereRB.drag = groundDrag;
        } else
        {
            sphereRB.drag = airDrag;
        }

    }

    private void FixedUpdate()
    {
        if(isCarGrounded)
        {
            //moves the car
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        } else
        {
            //add extra gravity
            sphereRB.AddForce(transform.up * -30f);
        }

       
    }
}
