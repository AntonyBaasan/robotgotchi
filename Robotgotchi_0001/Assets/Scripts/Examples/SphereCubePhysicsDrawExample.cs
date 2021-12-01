using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCubePhysicsDrawExample : MonoBehaviour
{   
    public bool Grounded = true;
    private bool CubeGrounded = false;

    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.28f;
    public LayerMask GroundLayers;

    public Vector3 CubeSize = new Vector3(1.0f, 1.0f, 1.0f);


    // Update is called once per frame
    void Update()
    {
        //Position holder 
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

        CubeGrounded = Physics.CheckBox(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), CubeSize, Quaternion.Euler(0, 0, 0), GroundLayers, QueryTriggerInteraction.Ignore);

        if (CubeGrounded == true)
        {
            Debug.Log(Grounded);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
        // Cube Draw 


        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), CubeSize);
    }
}
