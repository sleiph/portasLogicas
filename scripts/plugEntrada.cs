using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plugEntrada : MonoBehaviour
{
    SpriteRenderer thisCor;
    void OnMouseOver()
    {
        thisCor.color = new Color(0.82f, 0.23f, 0.2f, 1f);
    }
    void OnMouseExit()
    {
        thisCor.color = new Color(0f, 0f, 0f, 1f);
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
