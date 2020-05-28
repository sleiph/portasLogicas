using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class cores : MonoBehaviour
{
    private bool v_valor = false;
    public bool valor = false;

    private GameObject conector;
    private LineRenderer lr;
    private Vector3[] linePoints = new Vector3[2];
    private bool isOverMan = false;

    public void TrueCheck (bool bule)
    {
        foreach (Transform child in transform)     
        {  
            if (child.name == "cor")
            {
                child.gameObject.SetActive(bule);
            }
            else if (child.name == "notCor")
            {
                child.gameObject.SetActive(!bule);
            }
            v_valor = bule;  
        } 
    }

    void OnMouseDown()
    {
        isOverMan = true;
    }
    void OnMouseUp()
    {
        isOverMan = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        conector = this.gameObject.transform.GetChild(0).gameObject;
        lr = conector.GetComponent<LineRenderer>();
        linePoints[0] = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (valor != v_valor){TrueCheck(valor);}

        if (Input.GetMouseButton (0) && isOverMan == true) {
            Camera c = Camera.main;
            Vector3 p = c.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));
            p[0] -= conector.transform.position.x;
            p[1] -= conector.transform.position.y;
            linePoints[1] = p;
            lr.SetPositions (linePoints);
        }    
    }
}