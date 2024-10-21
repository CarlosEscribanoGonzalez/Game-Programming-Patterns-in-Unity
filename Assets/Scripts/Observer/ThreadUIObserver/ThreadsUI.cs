using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThreadsUI : MonoBehaviour, IObserver<float>
{
    private TextMeshProUGUI text;
    private InventoryManager inventory;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        inventory = GameObject.FindObjectOfType<InventoryManager>();
        inventory.AddObserver(this);
    }

    public void StartObserver(float data)
    {
        text.text = inventory.GetThreads().ToString();
    }

    public void UpdateObserver(float data)
    {
        text.text = inventory.GetThreads().ToString();
    }

}
