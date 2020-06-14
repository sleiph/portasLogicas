using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class plugSaida : MonoBehaviour
{
    private bool v_valor = false;
    public bool valor = false;
    private bool[] v_entradas = {false, false, false, false, false};

    public string formulaSaida = "";
    private string[] formulaEntrada = {"", ""};

    SpriteRenderer thisCor;
    TextMesh formulaTexto;

    public List<linhaColisao> linha;
    public List<plugSaida> entradas;
    public List<Transform> cores;
    public List<Transform> nCores;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "comeco" || col.gameObject.tag == "final"){
            thisCor.color = new Color(0.82f, 0.23f, 0.2f, 1f);
            linha.Add(col.gameObject.GetComponent<linhaColisao>());
            linha.Add(linha[0].irmao);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "comeco" || col.gameObject.tag == "final"){
            thisCor.color = new Color(1f, 1f, 1f, 1f);
            linha.RemoveAt(linha.Count - 2);
            linha.RemoveAt(linha.Count - 1);
        }
    }

    public void TrueCheck (bool bule)
    {
        foreach (Transform cor in cores){
            cor.gameObject.SetActive(bule);
        }
        if (transform.parent.gameObject.tag == "NAND" || transform.parent.gameObject.tag == "NOR" || transform.parent.gameObject.tag == "XNOR" || transform.parent.gameObject.tag == "NOT"){
            foreach (Transform nCor in nCores){
                nCor.gameObject.SetActive(!bule);
            }
        }
        v_valor = bule;
    }

    void Awake()
    {
        thisCor = GetComponent<SpriteRenderer>();
        foreach (Transform child in transform.parent){
            if (child.tag == "cor") cores.Add(child);
            if (child.tag == "notCor") nCores.Add(child);
            if (child.tag == "texto"){
                formulaTexto = child.gameObject.GetComponent<TextMesh>();
            }
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
        if (transform.parent.gameObject.tag == "fonte") formulaSaida = formulaTexto.text; 
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
                    linha[0].formulaLinha = formulaSaida;
                    linha[1].valorConector = valor;
                    linha[1].formulaLinha = formulaSaida;
                }
            }
            if (transform.parent.gameObject.tag != "fonte" && transform.parent.gameObject.tag != "NOT"){
                if (transform.parent.gameObject.tag == "AND"){
                    if (entradas[1].valor != v_entradas[1] || entradas[3].valor != v_entradas[3]){
                        valor = entradas[1].valor & entradas[3].valor;
                    }
                    if (entradas[1].linha.Count == 2){
                        formulaEntrada[0] = "(" + entradas[1].linha[0].formulaLinha;
                    }
                    if (entradas[3].linha.Count == 2){
                        formulaEntrada[1] = entradas[3].linha[0].formulaLinha + ")";
                    }
                    if (entradas[1].linha.Count == 2 && entradas[3].linha.Count == 2){
                        formulaSaida = formulaEntrada[0] + "." + formulaEntrada[1];
                    }
                    else{
                        formulaSaida = "";
                    }
                    formulaTexto.text = formulaSaida;
                }
                else if (transform.parent.gameObject.tag == "OR"){
                    if (entradas[1].valor != v_entradas[1] || entradas[3].valor != v_entradas[3]){
                        valor = entradas[1].valor | entradas[3].valor;
                    }
                    if (entradas[1].linha.Count == 2){
                        formulaEntrada[0] = "(" + entradas[1].linha[0].formulaLinha;
                    }
                    if (entradas[3].linha.Count == 2){
                        formulaEntrada[1] = entradas[3].linha[0].formulaLinha + ")";
                    }
                    if (entradas[1].linha.Count == 2 && entradas[3].linha.Count == 2){
                        formulaSaida = formulaEntrada[0] + "+" + formulaEntrada[1];
                    }
                    else{
                        formulaSaida = "";
                    }
                    formulaTexto.text = formulaSaida;
                }
                else if (transform.parent.gameObject.tag == "NAND"){
                    if (entradas[1].valor != v_entradas[1] || entradas[3].valor != v_entradas[3]){
                        valor = !(entradas[1].valor & entradas[3].valor);
                    }
                    if (entradas[1].linha.Count == 2){
                        formulaEntrada[0] = "!(" + entradas[1].linha[0].formulaLinha;
                    }
                    if (entradas[3].linha.Count == 2){
                        formulaEntrada[1] = entradas[3].linha[0].formulaLinha + ")";
                    }
                    if (entradas[1].linha.Count == 2 && entradas[3].linha.Count == 2){
                        formulaSaida = formulaEntrada[0] + "." + formulaEntrada[1];
                    }
                    else{
                        formulaSaida = "";
                    }
                    formulaTexto.text = formulaSaida;
                }
                else if (transform.parent.gameObject.tag == "NOR"){
                    if (entradas[1].valor != v_entradas[1] || entradas[3].valor != v_entradas[3]){
                        valor = !(entradas[1].valor | entradas[3].valor);
                    }
                    if (entradas[1].linha.Count == 2){
                        formulaEntrada[0] = "!(" + entradas[1].linha[0].formulaLinha;
                    }
                    if (entradas[3].linha.Count == 2){
                        formulaEntrada[1] = entradas[3].linha[0].formulaLinha + ")";
                    }
                    if (entradas[1].linha.Count == 2 && entradas[3].linha.Count == 2){
                        formulaSaida = formulaEntrada[0] + "+" + formulaEntrada[1];
                    }
                    else{
                        formulaSaida = "";
                    }
                    formulaTexto.text = formulaSaida;
                }
                else if (transform.parent.gameObject.tag == "XOR"){
                    if (entradas[1].valor != v_entradas[1] || entradas[3].valor != v_entradas[3]){
                        valor = entradas[1].valor != entradas[3].valor;
                    }
                    if (entradas[1].linha.Count == 2){
                        formulaEntrada[0] = "(" + entradas[1].linha[0].formulaLinha;
                    }
                    if (entradas[3].linha.Count == 2){
                        formulaEntrada[1] = entradas[3].linha[0].formulaLinha + ")";
                    }
                    if (entradas[1].linha.Count == 2 && entradas[3].linha.Count == 2){
                        formulaSaida = formulaEntrada[0] + "⊕" + formulaEntrada[1];
                    }
                    else{
                        formulaSaida = "";
                    }
                    formulaTexto.text = formulaSaida;
                }
                else if (transform.parent.gameObject.tag == "XNOR"){
                    if (entradas[1].valor != v_entradas[1] || entradas[3].valor != v_entradas[3]){
                        valor = !(entradas[1].valor != entradas[3].valor);
                    }
                    if (entradas[1].linha.Count == 2){
                        formulaEntrada[0] = "!(" + entradas[1].linha[0].formulaLinha;
                    }
                    if (entradas[3].linha.Count == 2){
                        formulaEntrada[1] = entradas[3].linha[0].formulaLinha + ")";
                    }
                    if (entradas[1].linha.Count == 2 && entradas[3].linha.Count == 2){
                        formulaSaida = formulaEntrada[0] + "⊕" + formulaEntrada[1];
                    }
                    else{
                        formulaSaida = "";
                    }
                    formulaTexto.text = formulaSaida;
                }
                v_entradas[1] = entradas[1].valor;
                v_entradas[3] = entradas[3].valor;

            }
            if (transform.parent.gameObject.tag == "NOT"){
                if (entradas[0].valor != v_entradas[0]){
                    valor = !entradas[0].valor;
                    v_entradas[0] = entradas[0].valor;
                }
                if (entradas[0].linha.Count == 2){
                    formulaSaida = "!" + entradas[0].linha[0].formulaLinha;
                }
                else{
                    formulaSaida = "";
                }
                formulaTexto.text = formulaSaida;
            }
        }
        else if (transform.gameObject.tag == "entrada"){
            if (linha.Count == 2){
                if (valor != linha[0].valorConector){
                        valor = linha[0].valorConector;
                }
            }
            else if (linha.Count == 0) valor = false;
        }
    }
}