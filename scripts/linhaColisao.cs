using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linhaColisao : MonoBehaviour
{
    public bool valorConector = false;
    private bool isOverAlguem = false;
    private bool isOverConector = false;
    private bool isMousedownConector = false;
    public float gridTamanho = 0.25f;

    Vector3 screenPoint;
    Vector3 offset;

    private LineRenderer lr;
    public linhaColisao irmao;
    public GameObject conexao;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "entrada" || col.gameObject.tag == "saida"){
            isOverAlguem = true;
            conexao = col.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "saida")
        {
            valorConector = false;
            irmao.valorConector = false;
        }
        isOverAlguem = false;
        conexao = transform.parent.GetChild(0).gameObject;
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
        Vector3 scanPos = gameObject.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(scanPos);
        offset = scanPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
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
                valorConector = irmao.valorConector;
                AssignValor(valorConector);
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
    void OnMouseDrag() {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition.x = (float)(Mathf.Round(curPosition.x / gridTamanho) * gridTamanho);
        curPosition.y = (float)(Mathf.Round(curPosition.y / gridTamanho) * gridTamanho);
        Vector3 meioPosition = new Vector3(curPosition.x - ((curPosition.x-irmao.transform.position.x)/2), curPosition.y, curPosition.z);
        meioPosition.x = (float)(Mathf.Round(meioPosition.x / gridTamanho) * gridTamanho);
        if (transform.gameObject.tag == "comeco"){
            lr.SetPosition (0, curPosition);
            lr.SetPosition (1, meioPosition);
            lr.SetPosition (2, new Vector3(meioPosition.x, irmao.transform.position.y, meioPosition.z));
        }
        else if (transform.gameObject.tag == "final"){
            lr.SetPosition (1, new Vector3(meioPosition.x, irmao.transform.position.y, meioPosition.z));
            lr.SetPosition (2, meioPosition);
            lr.SetPosition (3, curPosition);
        }
        this.transform.position = curPosition;
        /// Physics2D.OverlapCircle(new Vector2(curPosition.x, curPosition.y), 0.1f);
    }

    public void AssignValor(bool valor)
    {
        conexao.GetComponent<plugSaida>().valor = valorConector;
    }

    void Awake()
    {
        lr = gameObject.GetComponentInParent<LineRenderer>();
        conexao = transform.parent.GetChild(0).gameObject;
        if (transform.gameObject.tag == "comeco"){
            irmao = transform.parent.gameObject.transform.GetChild(2).gameObject.GetComponent<linhaColisao>();
        }
        else{
            irmao = transform.parent.gameObject.transform.GetChild(1).gameObject.GetComponent<linhaColisao>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}