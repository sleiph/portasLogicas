using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linhaColisaoSaida : MonoBehaviour
{
    public bool valorConectorSaida = false;
    private bool isOverAlguem = false;
    private bool isOverConector = false;
    private bool isMousedownConector = false;
    private Vector3[] linhaSaida = new Vector3[2];
    private Vector3[] coliderCoords = new Vector3[2];

    private LineRenderer lr;
    /// private CircleCollider2D colisor;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "entrada" || col.gameObject.tag == "saida"){
            isOverAlguem = true;
            coliderCoords[1] = new Vector3 (col.transform.position.x, col.transform.position.y,0);
        }
        if (col.gameObject.tag == "saida"){
            valorConectorSaida = col.gameObject.GetComponent<plugSaida>().valor;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        isOverAlguem = false;
        valorConectorSaida = false;
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
        /// colisor.radius = .1f;
    }
    void OnMouseUp()
    {
        isMousedownConector = false;
        /// colisor.radius = .33f;
        transform.position = new Vector3(transform.position.x, transform.position.y, -3);
    }
    void Awake()
    {
        lr = gameObject.GetComponentInParent<LineRenderer>();
        /// colisor = GetComponent<CircleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton (0) && isMousedownConector) {
            if (!plugEntrada.isOverEntrada && !plugSaida.isOverMan){
                Camera c = Camera.main;
                Vector3 p = c.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));
                linhaSaida[1] = p;
                lr.SetPosition (1, linhaSaida[1]);
                this.transform.position = new Vector3 (linhaSaida[1][0], linhaSaida[1][1], linhaSaida[1][2]);
            }
            else{
                lr.SetPosition (1, coliderCoords[1]);
                this.transform.position = new Vector3 (coliderCoords[1][0], coliderCoords[1][1], coliderCoords[1][2]);
            }
        }
    }
}