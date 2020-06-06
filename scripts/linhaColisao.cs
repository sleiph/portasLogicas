using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linhaColisao : MonoBehaviour
{
    public bool valorConectorEntrada = false;
    private bool isOverAlguem = false;
    private bool isOverConector = false;
    private bool isMousedownConector = false;
    private Vector3[] linhaEntrada = new Vector3[2];
    private Vector3[] coliderCoords = new Vector3[2];

    private LineRenderer lr;
    /// private CircleCollider2D colisor;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "entrada" || col.gameObject.tag == "saida"){
            isOverAlguem = true;
            coliderCoords[0] = new Vector3 (col.transform.position.x, col.transform.position.y,0);
        }
        if (col.gameObject.tag == "saida"){
            valorConectorEntrada = col.gameObject.GetComponent<plugSaida>().valor;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        isOverAlguem = false;
        valorConectorEntrada = false;
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
                linhaEntrada[0] = p;
                lr.SetPosition (0, linhaEntrada[0]);
                this.transform.position = new Vector3 (linhaEntrada[0][0], linhaEntrada[0][1], linhaEntrada[0][2]);
            }
            else{
                lr.SetPosition (0, coliderCoords[0]);
                this.transform.position = new Vector3 (coliderCoords[0][0], coliderCoords[0][1], coliderCoords[0][2]);
            }
        }
    }
}