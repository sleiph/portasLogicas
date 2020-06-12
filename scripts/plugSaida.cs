using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class plugSaida : MonoBehaviour
{
    private bool v_valor = false;
    public bool valor = false;
    public bool isConectado = false;
    private bool[] v_entradas = {false, false, false, false, false};

    SpriteRenderer thisCor;
    public List<linhaColisao> linha;

    public List<plugSaida> entradas;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "comeco" || col.gameObject.tag == "final"){
            thisCor.color = new Color(0.82f, 0.23f, 0.2f, 1f);
            linha.Add(col.gameObject.GetComponent<linhaColisao>());
            linha.Add(linha[0].irmao);
            /* if (transform.gameObject.tag == "entrada"){
                if (valor != v_valor){
                    TrueCheck(valor);
                    v_entradas[1] = entradas[1].valor;
                    v_entradas[3] = entradas[3].valor;
                }
            } */
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "comeco" || col.gameObject.tag == "final"){
            thisCor.color = new Color(1f, 1f, 1f, 1f);
            linha.Clear();
        }
    }
    void OnMouseEnter()
    {

    }
    void OnMouseExit()
    {

    }
    void OnMouseUp()
    {
        
    }

    public void TrueCheck (bool bule)
    {
        foreach (Transform child in transform.parent)     
        {
            if (child.tag == "cor") child.gameObject.SetActive(bule);
            if (transform.parent.gameObject.tag == "NAND" || transform.parent.gameObject.tag == "NOR" || transform.parent.gameObject.tag == "XNOR"){
                if (child.tag == "notCor") child.gameObject.SetActive(!bule);
            }
        }
        v_valor = bule;
    }
    void Awake()
    {
        thisCor = GetComponent<SpriteRenderer>();
        foreach (Transform child in transform.parent){
            if (transform.parent.gameObject.tag != "fonte"){
                if (child.tag == "entrada"){
                    entradas.Add(child.gameObject.GetComponent<plugSaida>());
                }
            }
            else{
                if (child.tag == "saida"){
                    entradas.Add(child.gameObject.GetComponent<plugSaida>());
                }
            }
        }
        if (transform.gameObject.tag == "saida") TrueCheck(valor);
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (transform.gameObject.tag == "saida"){
            if (valor != v_valor){
                TrueCheck(valor);
                if (linha.Count > 0){
                    linha[0].valorConector = valor;
                    linha[1].valorConector = valor;
                }
            }
            if (transform.parent.gameObject.tag != "fonte"){
                if (entradas[1].valor != v_entradas[1] || entradas[3].valor != v_entradas[3]){
                    if (transform.parent.gameObject.tag == "AND"){
                        valor = entradas[1].valor & entradas[3].valor;
                    }
                    else if (transform.parent.gameObject.tag == "OR"){
                        valor = entradas[1].valor | entradas[3].valor;
                    }
                    else if (transform.parent.gameObject.tag == "NAND"){
                        valor = !(entradas[1].valor & entradas[3].valor);
                    }
                    else if (transform.parent.gameObject.tag == "NOR"){
                        valor = !(entradas[1].valor | entradas[3].valor);
                    }
                    else if (transform.parent.gameObject.tag == "XOR"){
                        valor = entradas[1].valor != entradas[3].valor;
                    }
                    else if (transform.parent.gameObject.tag == "XNOR"){
                        valor = !(entradas[1].valor != entradas[3].valor);
                    }
                    v_entradas[1] = entradas[1].valor;
                    v_entradas[3] = entradas[3].valor;
                }
            }
        }
        else if (transform.gameObject.tag == "entrada"){
            if (linha.Count > 0 && isConectado){
                if (valor != linha[0].valorConector){
                    valor = linha[0].valorConector;
                }
            }
        }
    }
}