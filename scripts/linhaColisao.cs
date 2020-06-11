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
    Vector3 upPoints;
    Vector3 offset;

    private LineRenderer lr;
    public linhaColisao irmao;
    public GameObject conexao;
    private GameObject pai;
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
        upPoints = transform.position;
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
        upPoints = new Vector3(transform.position.x, transform.position.y, -.5f);
        transform.position = upPoints;
        if (isOverAlguem){
            transform.parent = conexao.transform.parent;
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
        /// Physics2D.OverlapCircle(new Vector2(curPosition.x, curPosition.y), 0.1f);
    }

    public void AssignValor(bool valor)
    {
        conexao.GetComponent<plugSaida>().valor = valorConector;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
    }
}