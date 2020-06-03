using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class cores : MonoBehaviour
{
    private bool v_valor = false;
    public bool valor = false;

    public static bool entrada = false;

    private CircleCollider2D collider_s;
    private GameObject conector;
    private LineRenderer lr;
    public static Vector3[] linePoints = new Vector3[2];
    private bool isOverMan = false;

    public void TrueCheck (bool bule)
    {
        foreach (Transform child in transform)     
        {  
            if (child.tag == "cor")
            {
                child.gameObject.SetActive(bule);
            }
            else if (child.tag == "notCor")
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
        collider_s = conector.GetComponent<CircleCollider2D>();
        linePoints[0] = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (valor != v_valor){TrueCheck(valor);}

        if (Input.GetMouseButton (0)) {
            if(isOverMan == true)
            {
                Camera c = Camera.main;
                Vector3 p = c.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));
                p[0] -= conector.transform.position.x;
                p[1] -= conector.transform.position.y;
                if(entrada == false)
                {
                    linePoints[1] = p;
                }
                lr.SetPositions (linePoints);
                collider_s.offset = new Vector2(p[0], p[1]);
            }
        }
        if (Input.GetMouseButtonUp (0)) 
        {
            if(entrada == true && isOverMan == true)
            {
                Debug.Log(linePoints[1]);
                lr.SetPositions (linePoints);
            }
        }

    }
}