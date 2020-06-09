using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverComponente : MonoBehaviour
{
    private bool isMousedownMover = false;
    private bool isAreadetrabalho = false;
    private bool isClonado = false;
    public float gridTamanho = 0.25f;

    Vector3 originalPoint;
    Vector3 screenPoint;
    Vector3 offset;

    private GameObject pai;
    private Transform areaDeTrabalho;
    public GameObject meuPrefab;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "areaDeTrabalho"){
            isAreadetrabalho = true;
            if(!isClonado) areaDeTrabalho = col.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "areaDeTrabalho"){
            isAreadetrabalho = false;
        }
    }
    void OnMouseDown()
    {
        originalPoint = new Vector3(transform.position.x, transform.position.y, -1);
        Vector3 scanPos = gameObject.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(scanPos);
        offset = scanPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        /// transform.position = new Vector3(transform.position.x, transform.position.y, -3);
    }
    void OnMouseUp()
    {
        isMousedownMover = false;
        /// transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        if (isAreadetrabalho){
            if (!isClonado){
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                curPosition.x = (float)(Mathf.Round(curPosition.x / gridTamanho) * gridTamanho);
                curPosition.y = (float)(Mathf.Round(curPosition.y / gridTamanho) * gridTamanho);
                GameObject minhaInstancia = Instantiate(meuPrefab, curPosition, Quaternion.identity);
                minhaInstancia.transform.SetParent(areaDeTrabalho);
                minhaInstancia.transform.GetChild(0).GetComponent<moverComponente>().isClonado = true;
                pai.transform.position = originalPoint;
            }
        }
        else{
            pai.transform.position = originalPoint;
            foreach (Transform child in transform.parent)     
            {
                if (child.gameObject.tag == "contorno") {
                    child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
    }
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition.x = (float)(Mathf.Round(curPosition.x / gridTamanho) * gridTamanho);
        curPosition.y = (float)(Mathf.Round(curPosition.y / gridTamanho) * gridTamanho);
        pai.transform.position = curPosition;
        AreaCheck();
    }

    public void AreaCheck()
    {
        foreach (Transform child in transform.parent)     
        {
            if (child.gameObject.tag == "contorno") {
                if (isAreadetrabalho){
                    child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                }
                else{
                    child.GetComponent<SpriteRenderer>().color = new Color(0.82f, 0.23f, 0.2f, 1f);
                }
            }
        }
    }
    void Awake()
    {
        pai = transform.parent.gameObject;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}