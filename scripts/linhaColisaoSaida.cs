using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linhaColisaoSaida : MonoBehaviour
{
    private bool entrada = false;
    private bool isOverConector = false;
    private bool isMousedownConector = false;
    private Vector3[] linhaSaida = new Vector3[2];
    private Vector3[] linhaAntiga = new Vector3[2];

    private LineRenderer lr;
    private CircleCollider2D pontaSaida;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "entrada")
        {
            entrada = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "entrada")
        {
            entrada = false;
        }
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
        linhaAntiga[1] = lr.GetPosition(1);
    }
    void OnMouseUp()
    {
        isMousedownConector = false;
        linhaSaida[0] = lr.GetPosition(0);
        lr.SetPosition (0, linhaSaida[0]);
    }
    void Awake()
    {
        lr = gameObject.GetComponentInParent<LineRenderer>();
        pontaSaida = transform.GetComponent<CircleCollider2D>();
        linhaAntiga[1] = lr.GetPosition(1);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton (0) && isMousedownConector) {
            Camera c = Camera.main;
            Vector3 p = c.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));
            /// p[0] -= transform.position.x;
            /// p[1] -= transform.position.y + 1;
            linhaSaida[1] = p;
            lr.SetPosition (1, linhaSaida[1]);
            /// pontaSaida.offset = new Vector2(p[0], p[1]);
            this.transform.position = new Vector3 (p[0], p[1], p[2]-2);
        }
    }
}
