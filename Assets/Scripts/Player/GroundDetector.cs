using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private PlayerController controller;
    
    void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickable pickable = collision.GetComponent<IPickable>();
        if (pickable != null) pickable.Pick();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Walkable")) controller.SetIsGrounded(true);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Walkable")) controller.SetIsGrounded(false);
    }
}
