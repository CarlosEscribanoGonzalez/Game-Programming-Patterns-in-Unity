using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnsafeArea : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) GameObject.FindObjectOfType<SaveController>().SetCanSave(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) GameObject.FindObjectOfType<SaveController>().SetCanSave(true);
    }
}
