using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class character_controller_v0001 : MonoBehaviour
{
    public InputMaster controls;
    
    private float speed;
    public CharacterController character_controller;
    public GameObject character_rotation;
    public float walk_speed = 6;
    public float rotation_smooth_time = 0.1f;
    private float rotation_smooth_velocity;

    public Animator character_animator;


    private float gravity = 9.87f;
    private float vertical_speed = 0;

    public float run_speed = 10;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = walk_speed;
    }

    // Update is called once per frame
    //Used for the old input system
    void Update()
    {
       
        float horizontal_move = Input.GetAxisRaw("Horizontal");
        float vertical_move = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal_move, 0f, vertical_move).normalized;

        //Figure out a way  to smoothly go up and down colliders 
        if(character_controller.isGrounded)
        {
            vertical_speed = 0;
        }
        else
        {
            vertical_speed -= gravity * Time.deltaTime;
        }

        Vector3 movement = new Vector3(direction[0], vertical_speed, direction[2]);
        Vector3 gravity_activate = new Vector3(0, vertical_speed, 0);

        if(direction.magnitude >= 0.1f)
        {
            float target_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float turn_angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref rotation_smooth_velocity, rotation_smooth_time);

            transform.rotation = Quaternion.Euler(0f, turn_angle, 0f);
            character_controller.Move(movement * speed * Time.deltaTime);
            character_animator.SetBool("walk_start", true);
        }
        else
        {
            character_animator.SetBool("walk_start", false);
            character_controller.Move(gravity_activate);
        }
   

    }

}
