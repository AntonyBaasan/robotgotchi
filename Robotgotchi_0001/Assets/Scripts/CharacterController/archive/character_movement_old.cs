using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class character_movement_old : MonoBehaviour
{
    public bool can_character_move = true;

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
    //interaction inputs 
    public LayerMask npc_layer;
    public LayerMask door_layer;
    public List<GameObject> interaction_objects;
    [SerializeField]
    private GameObject active_interaction_object;
    private float interaction_closest;

    //menus 
    public GameObject in_game_menu_object;
    public GameObject player_menu_object;
    public GameObject dialogue_menu_object;
    private GameObject active_menu = null;

    //Make the character awake
    private void Awake()
    {
        //Speed update 
        base_speed = speed;
        walk_speed = speed / 2;
        //jump holder 
        jump_time_holder = jump_time;
        //Set the menus

    }

    private void Start()
    {
        //Tried to add this to awake, but this did not work
        in_game_menu_object = GameObject.FindGameObjectWithTag("in_game_menu");
        player_menu_object = GameObject.FindGameObjectWithTag("player_menu");
        dialogue_menu_object = GameObject.FindGameObjectWithTag("dialogue_menu");
    }


    // Player Input // 
    public void OnMovement(InputAction.CallbackContext value)
    {
        if (can_character_move == true)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            direction = new Vector3(inputMovement.x, 0f, inputMovement.y);
        }
    }

    public void OnInteract(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (active_interaction_object == null)
            {
                Debug.Log("No Active Object");
                return;
            }
            else
            {
                InteractionStart(active_interaction_object);
            }
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (can_character_move == true)
        {
            if (value.started)
            {
                jump = true;
            }
        }
    }

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

    public void OnCancel(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            //The only problem with this system is that you have to carry around all the menus
            //Tag them and define them 
            //If might be better to load and unload scenes, then turn on the menu from there
            //Something to thing about and try 
            //check if nothing is active then run the in game menu
            if (active_menu == null)
            {
                MenuActivation(1, in_game_menu_object);
            }
            //If this is a diaolgue menu plase run 
            else if (active_menu == dialogue_menu_object.transform.GetChild(0).gameObject)
            {
                InteractionStart(active_interaction_object);
            }
            //will close any menu that is currently open
            else
            {
                MenuActivation(2, null);
            }
        }
    }

    public void OnPlayerMenu(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            //check if the active menu is nothing 
            if (active_menu == null)
            {
                MenuActivation(1, player_menu_object);
            }
            //Only flip off if it is this menu - I may return to this one day
            //Grave yard keeper seems to have tab also exit these things, but e to enter 
            else if (active_menu == player_menu_object.transform.GetChild(0).gameObject)
            {
                MenuActivation(2, null);
            }
        }
    }

    private void MenuActivation(int keypressed_driver, GameObject menu_to_activate)
    {
        //Switch state machine for menu options only two right now may grow 
        //might need another look - but far cleaner than before 
        switch (keypressed_driver)
        {
            case 3:
                break;
            case 2:
                active_menu.gameObject.SetActive(false);
                active_menu = null;
                can_character_move = !can_character_move;
                break;
            case 1:
                menu_to_activate.transform.GetChild(0).gameObject.SetActive(!menu_to_activate.transform.GetChild(0).gameObject.activeSelf);
                active_menu = menu_to_activate.transform.GetChild(0).gameObject;
                can_character_move = !can_character_move;
                break;
        }
    }

    void Update()
    {
        MovePlayer();
        ClosestInteractionObject();
    }

    // Player Movement // 
    private void MovePlayer()
    {
        Vector3 character_ground_position = new Vector3(transform.position.x, transform.position.y - ground_offset, transform.position.z);

        //On Ground Sphere 
        on_ground = Physics.CheckSphere(character_ground_position, ground_radius, ground_layers, QueryTriggerInteraction.Ignore);

        // Problems with Ramp - but maybe this can be fixed with a level design thing 
        //RaycastHit hit;
        //Debug.Log(Physics.BoxCast(character_ground_position, ground_cube, Vector3.down, Quaternion.Euler(0, 0, 0), 1.0f, ground_layers, QueryTriggerInteraction.Ignore));
        //Debug.Log(Physics.SphereCast(character_ground_position, ground_radius, Vector3.down, out hit, ground_radius, ground_layers, QueryTriggerInteraction.Ignore));
        //On Ground Square 
        //on_ground = Physics.CheckBox(character_ground_position, ground_cube, Quaternion.Euler(0, 0, 0), ground_layers, QueryTriggerInteraction.Ignore);

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

    private void OnTriggerEnter(Collider collision)
    {
        //Looking at the NPC layer for now 
        if (collision.gameObject.layer == Mathf.Log(npc_layer.value, 2))
        {
            interaction_objects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        //Looking at the NPC Layer for now 
        if (collision.gameObject.layer == Mathf.Log(npc_layer.value, 2))
        {
            interaction_objects.Remove(collision.gameObject);
        }
    }

    //flip to on trigger stay instead of the update function
    //Get the closest interactive object 
    private void ClosestInteractionObject()
    {
        if (interaction_objects.Count == 0) //set the interactive to null 
        {
            if (active_interaction_object != null)
            {
                active_interaction_object.transform.GetChild(1).gameObject.SetActive(false);
            }
            active_interaction_object = null;
            interaction_closest = 30.0f;
        }
        else if (interaction_objects.Count == 1) //set the interactive to near by 
        {
            active_interaction_object = interaction_objects[0];
            interaction_closest = 30.0f;
            active_interaction_object.transform.GetChild(1).gameObject.SetActive(true);
        }
        else //set the interactive to the closest 
        {
            interaction_closest = 20f;

            for (int i = 0; i < interaction_objects.Count; i++)
            {
                if (Vector3.Distance(this.transform.position, interaction_objects[i].transform.position) < interaction_closest)
                {
                    interaction_closest = Vector3.Distance(this.transform.position, interaction_objects[i].transform.position);
                    active_interaction_object = interaction_objects[i];
                    active_interaction_object.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    interaction_objects[i].transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }

    private void InteractionStart(GameObject active_interaction)
    {
        string local_interaction = (active_interaction.GetComponent<interaction_start>().interaction_name);
        //Debug.Log(active_interaction.GetComponent(local_interaction));
        //Debug.Log(active_interaction.GetComponent("dialogue_base"));


        active_interaction.gameObject.GetComponent<dialogue_base>().RunInteraction();
        active_menu = active_interaction.gameObject.GetComponent<dialogue_base>().dialogue_menu.transform.GetChild(0).gameObject;

        //decide if the character can turn back on 
        can_character_move = active_interaction.gameObject.GetComponent<dialogue_base>().dialgue_complete;
        if (can_character_move == true)
        {
            active_menu = null;
        }
        //Debug.Log(active_menu);

        //set the active menu the inact the exit items 

    }

}