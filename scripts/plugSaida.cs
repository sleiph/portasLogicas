using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class plugSaida : MonoBehaviour
{
    private bool v_valor = false;
    public bool valor = false;
    public static bool isOverMan = false;

    SpriteRenderer thisCor;

    public void TrueCheck (bool bule)
    {
        foreach (Transform child in transform.parent)     
        {  
            if (child.tag == "cor") child.gameObject.SetActive(bule);
            else if (child.tag == "notCor") child.gameObject.SetActive(!bule);
            v_valor = bule;
        } 
    }
    void OnMouseEnter()
    {
        thisCor.color = new Color(0.82f, 0.23f, 0.2f, 1f);
        isOverMan = true;
    }
    void OnMouseExit()
    {
        thisCor.color = new Color(0f, 0f, 0f, 1f);
        isOverMan = false;
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
        if (valor != v_valor){TrueCheck(valor);}
    }
}