using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plugEntrada : MonoBehaviour
{
    public static bool isOverEntrada = false;
    SpriteRenderer thisCor;

    void OnMouseEnter()
    {
        thisCor.color = new Color(0.82f, 0.23f, 0.2f, 1f);
        isOverEntrada = true;
    }
    void OnMouseExit()
    {
        thisCor.color = new Color(0f, 0f, 0f, 1f);
        isOverEntrada = false;
    }
    void OnMouseDown()
    {
        
    }
    void OnMouseUp()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        thisCor = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}