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
    public plugSaida entrada2;
    public plugSaida entrada4;

    public void TrueCheck (bool bule)
    {
        foreach (Transform child in transform.parent)     
        {
            if (transform.gameObject.tag == "saida") if (child.tag == "cor") child.gameObject.SetActive(bule);
            if (transform.parent.gameObject.tag == "NAND" || transform.parent.gameObject.tag == "NOR" || transform.parent.gameObject.tag == "XNOR"){
                if (child.tag == "notCor") child.gameObject.SetActive(!bule);
            }
            if (child.name == "plugEntrada2") entrada2 = child.gameObject.GetComponent<plugSaida>();
            else if (child.name == "plugEntrada4") entrada4 = child.gameObject.GetComponent<plugSaida>();
            v_valor = bule;
        }
    }
    void OnMouseEnter()
    {
        thisCor.color = new Color(0.82f, 0.23f, 0.2f, 1f);
        isOverMan = true;
        if (transform.gameObject.tag == "entrada"){
            if (valor != v_valor) TrueCheck(valor);
        }
    }
    void OnMouseExit()
    {
        thisCor.color = new Color(1f, 1f, 1f, 1f);
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
    void Awake()
    {
        thisCor = GetComponent<SpriteRenderer>();
        TrueCheck(valor);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.gameObject.tag == "saida"){
            if (valor != v_valor) TrueCheck(valor);
        }
        if (transform.gameObject.tag == "saida" && transform.parent.gameObject.tag != "fonte"){
            if (transform.parent.gameObject.tag == "AND"){
                valor = entrada2.valor & entrada4.valor;
            }
            else if (transform.parent.gameObject.tag == "OR"){
                valor = entrada2.valor | entrada4.valor;
            }
            else if (transform.parent.gameObject.tag == "NAND"){
                valor = !entrada2.valor & !entrada4.valor;
            }
            else if (transform.parent.gameObject.tag == "NOR"){
                valor = !entrada2.valor | !entrada4.valor;
            }
            else if (transform.parent.gameObject.tag == "XOR"){
                valor = entrada2.valor != entrada4.valor;
            }
            else if (transform.parent.gameObject.tag == "XNOR"){
                valor = !(entrada2.valor != entrada4.valor);
            }
        }
    }
}