using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class plugSaida : MonoBehaviour
{
    private bool v_valor = false;
    public bool valor = false;
    public static bool isOverMan = false;

    

    public void TrueCheck (bool bule)
    {
        foreach (Transform child in transform)     
        {  
            if (child.tag == "cor") child.gameObject.SetActive(bule);
            else if (child.tag == "notCor") child.gameObject.SetActive(!bule);
            v_valor = bule;
        } 
    }
    void OnMouseEnter()
    {
        isOverMan = true;
    }
    void OnMouseExit()
    {
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

    }

    // Update is called once per frame
    void Update()
    {
        if (valor != v_valor){TrueCheck(valor);}
    }
}