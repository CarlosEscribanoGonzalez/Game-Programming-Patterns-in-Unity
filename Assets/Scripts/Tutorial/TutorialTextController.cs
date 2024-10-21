using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTextController : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject player;
    private float fadeSpeed = 5f;
    [SerializeField] private float visibleDistance = 10f;
    [SerializeField] private bool startVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();

        if (startVisible)
        {
            Color newColor = textMesh.color;
            newColor.a = 1f;
            textMesh.color = newColor;
        }
        else
        {
            Color newColor = textMesh.color;
            newColor.a = 0f;
            textMesh.color = newColor;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(this.transform.position.x - player.transform.position.x) > visibleDistance)
        {
            Color newColor = textMesh.color;
            newColor.a = Mathf.Clamp(newColor.a - fadeSpeed*Time.deltaTime,0f,1f);
            textMesh.color = newColor;
        }
        else
        {
            Color newColor = textMesh.color;
            newColor.a = Mathf.Clamp(newColor.a + fadeSpeed * Time.deltaTime, 0f, 1f);
            textMesh.color = newColor;
        }
    }
}
