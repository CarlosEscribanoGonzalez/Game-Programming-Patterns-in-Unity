using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3 : MonoBehaviour, ISaveableObject
{
    [SerializeField] private GameObject cover;
    private bool dirty = false;
    private bool open = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Finish") && open)
        {
            GameObject.FindObjectOfType<SceneChanger>().EndGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            Destroy(cover);
            dirty = true;
            open = true;
            GetComponent<BoxCollider2D>().isTrigger = true; //Evitar colisiones futuras del cañón
        }
    }

    public object GetData()
    {
        return new PuzzleData
        {
            solved = open
        };
    }

    public bool IsDirty()
    {
        return dirty;
    }

    public void RestoreData(object data)
    {
        PuzzleData pData = (PuzzleData)data;
        open = pData.solved;
        if (open) Destroy(cover);
        dirty = true;
    }

    public void SetDirty(bool d)
    {
        dirty = d;
    }

}
