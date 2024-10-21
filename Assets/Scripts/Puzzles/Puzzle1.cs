using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puzzle1 : MonoBehaviour, ISaveableObject
{
    [SerializeField] private float repairingCost;
    [SerializeField] private GameObject bedGround;
    [SerializeField] private Animator tagAnim;
    [SerializeField] private TextMeshProUGUI priceText;
    private Animator anim;
    private bool canPay = false;
    private bool repaired = false;
    private bool dirty = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        priceText.text = repairingCost.ToString();
    }

    void Update()
    {
        if (canPay && !repaired && Input.GetKeyDown(KeyCode.E))
        {
            if (GameObject.FindObjectOfType<InventoryManager>().UseThreads(repairingCost))
            {
                anim.SetBool("Repaired", true);
                tagAnim.SetBool("Shown", false);
                Invoke("Repair", 0.5f); //Hay que esperar a que se repare para trepar
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) bedGround.SetActive(false);
        if (!repaired)
        {
            if (GameObject.FindObjectOfType<InventoryManager>().GetThreads() < repairingCost) priceText.color = Color.red;
            else priceText.color = Color.green;
        }
    }

    private void Repair()
    {
        dirty = true;
        repaired = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(repaired) collision.GetComponent<PlayerController>().SetCanClimb(true);
            canPay = true;
            bedGround.SetActive(false);
            if (!repaired)
            {
                tagAnim.SetBool("Shown", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(repaired) collision.GetComponent<PlayerController>().SetCanClimb(false);
            canPay = false;
            bedGround.SetActive(true);
            if (!repaired) tagAnim.SetBool("Shown", false);
        }
    }

    //Dirty Flag:
    public bool IsDirty()
    {
        return dirty;
    }

    public void SetDirty(bool d)
    {
        dirty = d;
    }

    public object GetData()
    {
        return new PuzzleData
        {
            solved = repaired
        };
    }

    public void RestoreData(object data)
    {
        PuzzleData pData = (PuzzleData)data;
        repaired = pData.solved;
        if(repaired) anim.SetBool("RepairedNoTransition", true);
        dirty = true;
    }
}
