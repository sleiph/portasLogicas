using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linhaColisao : MonoBehaviour
{
    public bool valorConector = false;

    public float gridTamanho = 0.25f;

    public string formulaLinha = "";

    Vector3 screenPoint;
    Vector3 upPoints;
    Vector3 offset;

    private LineRenderer lr;
    public linhaColisao irmao;
    public GameObject conexao;
    private GameObject pai;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "entrada" || col.gameObject.tag == "saida"){
            conexao = col.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "saida")
        {
            valorConector = false;
            irmao.valorConector = false;
            formulaLinha = "";
            irmao.formulaLinha = "";
        }
        if (col.gameObject.tag == "entrada" || col.gameObject.tag == "saida"){
            conexao = pai.transform.GetChild(0).gameObject;
        }
    }

    void OnMouseDown()
    {
        upPoints = transform.position;
        Vector3 scanPos = gameObject.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(scanPos);
        offset = scanPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        if (conexao.tag == "entrada"){
            conexao.GetComponent<plugSaida>().valor = false;
            conexao.GetComponent<plugSaida>().formulaSaida = "";
        }
        else if (conexao.tag == "saida"){
            valorConector = false;
            formulaLinha = "";
            irmao.valorConector = false;
            irmao.formulaLinha = "";
            if (irmao.conexao.tag == "entrada"){
                irmao.conexao.GetComponent<plugSaida>().valor = valorConector;
                irmao.conexao.GetComponent<plugSaida>().formulaSaida = formulaLinha;
            }
        }
    }
    void OnMouseUp()
    {
        upPoints = new Vector3(transform.position.x, transform.position.y, -.5f);
        transform.position = upPoints;
        if (conexao.tag == "entrada"){
            if (conexao.GetComponent<plugSaida>().linha.Count == 2){
                transform.parent = conexao.transform.parent;
                valorConector = irmao.valorConector;
                formulaLinha = irmao.formulaLinha;
                conexao.GetComponent<plugSaida>().valor = valorConector;
                conexao.GetComponent<plugSaida>().formulaSaida = formulaLinha;
            }
            else{
                upPoints = new Vector3(transform.position.x-.25f, transform.position.y-.25f, -.5f);
                this.transform.position = upPoints;
                upPoints = new Vector3(transform.position.x+.25f, transform.position.y+.25f, -.5f);
            }
        }
        else if (conexao.tag == "saida"){
            transform.parent = conexao.transform.parent;
            valorConector = conexao.GetComponent<plugSaida>().valor;
            formulaLinha = conexao.GetComponent<plugSaida>().formulaSaida;
            irmao.valorConector = conexao.GetComponent<plugSaida>().valor;
            irmao.formulaLinha = conexao.GetComponent<plugSaida>().formulaSaida;
            if (irmao.conexao.tag == "entrada"){
                irmao.conexao.GetComponent<plugSaida>().valor = valorConector;
                irmao.conexao.GetComponent<plugSaida>().formulaSaida = formulaLinha;
            }
        }
        else{
            transform.parent = pai.transform;
        }
    }
    void OnMouseDrag() {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.5f);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition.x = (float)(Mathf.Round(curPosition.x / gridTamanho) * gridTamanho);
        curPosition.y = (float)(Mathf.Round(curPosition.y / gridTamanho) * gridTamanho);
        this.transform.position = curPosition;
    }

    void Awake()
    {
        pai = transform.parent.gameObject;
        lr = gameObject.GetComponentInParent<LineRenderer>();
        conexao = transform.parent.GetChild(0).gameObject;
        if (transform.gameObject.tag == "comeco"){
            irmao = transform.parent.gameObject.transform.GetChild(2).gameObject.GetComponent<linhaColisao>();
        }
        else{
            irmao = transform.parent.gameObject.transform.GetChild(1).gameObject.GetComponent<linhaColisao>();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (upPoints != transform.position){
            Vector3 meioPosition = new Vector3(transform.position.x - ((transform.position.x-irmao.transform.position.x)/2), transform.position.y, 0);
            meioPosition.x = (float)(Mathf.Round((meioPosition.x-pai.transform.position.x) / gridTamanho) * gridTamanho);
            meioPosition.y = meioPosition.y-pai.transform.position.y;
            if (transform.gameObject.tag == "comeco"){
                lr.SetPosition (0, new Vector3(transform.position.x-pai.transform.position.x,transform.position.y-pai.transform.position.y, 0));
                lr.SetPosition (1, meioPosition);
                lr.SetPosition (2, new Vector3(meioPosition.x, irmao.transform.position.y-pai.transform.position.y, 0));
            }
            else if (transform.gameObject.tag == "final"){
                lr.SetPosition (1, new Vector3(meioPosition.x, irmao.transform.position.y-pai.transform.position.y, 0));
                lr.SetPosition (2, meioPosition);
                lr.SetPosition (3, new Vector3(transform.position.x-pai.transform.position.x,transform.position.y-pai.transform.position.y, 0));
            }
            upPoints = new Vector3(transform.position.x, transform.position.y, -.5f);
        }
        if (conexao.tag == "entrada"){
            if (conexao.GetComponent<plugSaida>().linha.Count == 2){
                if (conexao.GetComponent<plugSaida>().valor != valorConector){
                    valorConector = irmao.valorConector;
                    conexao.GetComponent<plugSaida>().valor = valorConector;
                }
                if (conexao.GetComponent<plugSaida>().formulaSaida != formulaLinha){
                    formulaLinha = irmao.formulaLinha;
                    conexao.GetComponent<plugSaida>().formulaSaida = formulaLinha;
                }
            }
        }
        else if (conexao.tag == "saida"){
            if (valorConector != conexao.GetComponent<plugSaida>().valor){
                valorConector = conexao.GetComponent<plugSaida>().valor;
                irmao.valorConector = conexao.GetComponent<plugSaida>().valor;
                if (irmao.conexao.tag == "entrada"){
                    irmao.conexao.GetComponent<plugSaida>().valor = valorConector;
                }
            }
            if (formulaLinha != conexao.GetComponent<plugSaida>().formulaSaida){
                formulaLinha = conexao.GetComponent<plugSaida>().formulaSaida;
                irmao.formulaLinha = conexao.GetComponent<plugSaida>().formulaSaida;
                if (irmao.conexao.tag == "entrada"){
                    irmao.conexao.GetComponent<plugSaida>().formulaSaida = formulaLinha;
                }
            }
        }
    }
}