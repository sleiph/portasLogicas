using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linhaColisao : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "entrada")
        {
            cores.entrada = true;
            Vector3 p = new Vector3 (col.transform.position.x-transform.position.x, col.transform.position.y-transform.position.y, 0);
            cores.linePoints[1] = p;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "entrada")
        {
            cores.entrada = false;
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
