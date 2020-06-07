using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linhaColisao : MonoBehaviour
{
    public bool valorConector = false;
    private bool isOverAlguem = false;
    private bool isOverConector = false;
    private bool isMousedownConector = false;
    private Vector3[] linhaCoords = new Vector3[2];
    private Vector3[] coliderCoords = new Vector3[2];

    private LineRenderer lr;
    public linhaColisao irmao;
    public GameObject conexao;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "entrada" || col.gameObject.tag == "saida"){
            isOverAlguem = true;
            coliderCoords[0] = new Vector3 (col.transform.position.x, col.transform.position.y,0);
        }
        conexao = col.gameObject;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "saida")
        {
            valorConector = false;
            irmao.valorConector = false;
        }
        isOverAlguem = false;
    }
    void OnMouseEnter()
    {
        isOverConector = true;
    }
    void OnMouseExit()
    {
        isOverConector = false;
    }
    void OnMouseDown()
    {
        isMousedownConector = true;
        if (isOverAlguem){
            if (conexao.tag == "entrada"){
                conexao.GetComponent<plugSaida>().valor = false;
            }
            else if (conexao.tag == "saida"){
                valorConector = false;
                irmao.valorConector = false;
                if (irmao.isOverAlguem && irmao.conexao.tag == "entrada"){
                    irmao.AssignValor(valorConector);
                }
            }
        }
    }
    void OnMouseUp()
    {
        isMousedownConector = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, -3);
        if (isOverAlguem){
            if (conexao.tag == "entrada"){
                AssignValor(valorConector);
                Debug.Log("entradaMouseup " + valorConector);
            }
            else if (conexao.tag == "saida"){
                valorConector = conexao.GetComponent<plugSaida>().valor;
                irmao.valorConector = conexao.GetComponent<plugSaida>().valor;
                if (irmao.isOverAlguem && irmao.conexao.tag == "entrada"){
                    irmao.AssignValor(valorConector);
                }
            }
        }
    }

    public void AssignValor(bool valor)
    {
        conexao.GetComponent<plugSaida>().valor = valorConector;
    }

    void Awake()
    {
        lr = gameObject.GetComponentInParent<LineRenderer>();
        if (transform.gameObject.tag == "comeco"){
            irmao = transform.parent.gameObject.transform.GetChild(1).gameObject.GetComponent<linhaColisao>();
        }
        else{
            irmao = transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<linhaColisao>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton (0) && isMousedownConector) {
            if (!plugSaida.isOverMan){
                Camera c = Camera.main;
                Vector3 p = c.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));
                linhaCoords[0] = p;
                if (transform.gameObject.tag == "comeco"){
                    lr.SetPosition (0, linhaCoords[0]);
                }
                else{
                    lr.SetPosition (1, linhaCoords[0]);
                }
                this.transform.position = new Vector3 (linhaCoords[0][0], linhaCoords[0][1], linhaCoords[0][2]);
            }
            else{
                if (transform.gameObject.tag == "comeco"){
                    lr.SetPosition (0, coliderCoords[0]);
                }
                else{
                    lr.SetPosition (1, coliderCoords[0]);
                }
                this.transform.position = new Vector3 (coliderCoords[0][0], coliderCoords[0][1], coliderCoords[0][2]);
            }
        }
        /// if (valorConector != irmao.valorConector) valorConector = valorConector | irmao.valorConector;
    }
}