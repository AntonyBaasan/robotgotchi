using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class character_movement : MonoBehaviour
{
    //Can the character move 
    public bool character_can_move = true;

    //Main animator 
    public Animator character_animator;
    public CharacterController character_controller;

    //direction
    private Vector3 direction;
    //speed 
    public float speed;
    private float base_speed;
    private float walk_speed;
    //gravity = base 9.87
    public float gravity = 9.87f;
    private float vertical_speed = 0;
    //rotation
    public float rotation_smooth_time = 0.1f;
    private float rotation_smooth_velocity;
    //animation changes 
    public float animation_walk_speed;
    //jump action 
    private bool jump = false;
    public float jump_height;
    private string default_animation = "sloth_idle";
    public float jump_time = 1.0f;
    public float jump_time_cut_off = .9f;
    private float jump_time_holder;
    //ground details 
    public float dist_to_ground = .1f;
    private bool on_ground = false;
    public float ground_radius = 0.5f;
    public float ground_offset = 0.0f;
    public Vector3 ground_cube = new Vector3(1, 1, 1);
    public LayerMask ground_layers;

    private void Awake()
    {
        //Speed update 
        base_speed = speed;
        walk_speed = speed / 2;
        //jump holder 
        jump_time_holder = jump_time;
    }

    private void OnEnable()
    {
        menu_actions.OnMenuHit += CanPlayerMove;
    }

    private void OnDisable()
    {
        menu_actions.OnMenuHit += CanPlayerMove;
    }

    //Toggle if the player can move 
    private void CanPlayerMove(string move)
    {
        character_can_move = !character_can_move;
    }

    // Main Directional Movement Input // 
    public void OnMovement(InputAction.CallbackContext value)
    {
        if (character_can_move == true)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            direction = new Vector3(inputMovement.x, 0f, inputMovement.y);
        }
    }

    // Main Run Input // 
    // add if a character can move add in 
    public void OnRun(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            base_speed = walk_speed;
            character_animator.speed = animation_walk_speed;
        }
        if (value.canceled)
        {
            base_speed = speed;
            character_animator.speed = 1f;
        }
    }

    // Main Jump Input // 
    public void OnJump(InputAction.CallbackContext value)
    {
        if (character_can_move == true)
        {
            if (value.started)
            {
                jump = true;
            }
        }
    }

    void Update()
    {
        MovePlayer();
    }

    // Full Move Function // 
    private void MovePlayer()
    {
        Vector3 character_ground_position = new Vector3(transform.position.x, transform.position.y - ground_offset, transform.position.z);
        //On Ground Sphere 
        on_ground = Physics.CheckSphere(character_ground_position, ground_radius, ground_layers, QueryTriggerInteraction.Ignore);
        //Check if the character is on the ground, then start a timer for the jump
        if (on_ground == false)
        {
            jump_time -= Time.deltaTime;
        }
        else
        {
            jump_time = jump_time_holder;
        }
        //Make the character jump 
        if (jump == true)
        {
            if (jump_time > .9f)
            {
                vertical_speed = jump_height;
                jump = false;
            }
            else
            {
                vertical_speed -= gravity * Time.deltaTime;
                jump = false;
            }
        }
        else
        {
            if (on_ground == true)
            {
                vertical_speed = 0;
            }
            else
            {
                vertical_speed -= gravity * Time.deltaTime;
            }
        }

        //Update the animation (this whole thing might need to be a better equation)
        if (on_ground == true)
        {
            character_animator.Play(default_animation);

        }
        else
        {
            character_animator.Play("sloth_jump_mid");
        }

        //Main movement 
        Vector3 movement = new Vector3(direction[0] * base_speed, vertical_speed * speed, direction[2] * base_speed);
        Vector3 gravity_activate = new Vector3(0, vertical_speed, 0);
        //Check if the direction has movement 
        if (direction.magnitude >= 0.1f)
        {
            //Adjust the angle of the character 
            float target_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float turn_angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target_angle, ref rotation_smooth_velocity, rotation_smooth_time);
            //Move the character 
            transform.rotation = Quaternion.Euler(0f, turn_angle, 0f);
            character_controller.Move(movement * Time.deltaTime);
            default_animation = "sloth_walk_main";
        }
        else
        {
            character_controller.Move(gravity_activate * base_speed * Time.deltaTime);
            default_animation = "sloth_idle";
        }
    }

    //Debugger Gizmo For checking
    private void OnDrawGizmosSelected()
    {
        //Set the colors 
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);
        //Adjust the color 
        if (on_ground) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;
        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - ground_offset, transform.position.z), ground_radius);
        //Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y - ground_layers, transform.position.z), ground_cube);
    }
}