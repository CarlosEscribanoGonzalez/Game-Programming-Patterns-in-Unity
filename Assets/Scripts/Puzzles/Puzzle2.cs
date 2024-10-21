using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Puzzle2 : MonoBehaviour, ISaveableObject
{
    private Transform parent;
    private bool hit = false;
    private bool dirty = false;
    void Awake()
    {
        parent = transform.parent;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ThreadBall") && !hit)
        {
            StartCoroutine(RotateObject());
            dirty = true;
            hit = true;
        }
    }

    private IEnumerator RotateObject()
    {
        Debug.Log("Estoy rotando");
        int duration = 3;
        float startRotation = parent.eulerAngles.z;
        float endRotation = startRotation - 90f; //Se gira el objeto en 90 grados a la derecha
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360;
            parent.eulerAngles = new Vector3(parent.eulerAngles.x, parent.eulerAngles.x, zRotation);
            yield return null;
        }

        parent.eulerAngles = new Vector3(parent.eulerAngles.x,  parent.eulerAngles.y, endRotation);


    }
    //Dirty flag:
    public object GetData()
    {
        return new PuzzleData
        {
            solved = hit
        };
    }

    public bool IsDirty()
    {
        return dirty;
    }

    public void RestoreData(object data)
    {
        PuzzleData pData = (PuzzleData)data;
        hit = pData.solved;
        if (hit) parent.eulerAngles = new Vector3(parent.eulerAngles.x, parent.eulerAngles.y, -90);
        dirty = true;
    }

    public void SetDirty(bool d)
    {
        dirty = d;
    }
}
