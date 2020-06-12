﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverComponente : MonoBehaviour
{
    private bool isAreadetrabalho = false;
    private bool isOverOutro = false;
    private bool isOverConector = false;
    private bool isClonado = false;
    public float gridTamanho = 0.25f;

    Vector3 originalPoint;
    Vector3 screenPoint;
    Vector3 offset;
    Vector3 curPosition;

    private GameObject pai;
    private Transform areaDeTrabalho;
    public GameObject meuPrefab;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (transform.parent.tag == "conector"){
            if (col.gameObject.name == "areaDeConexao"){
                isAreadetrabalho = true;
            }
        }
        else{
            if (col.gameObject.name == "areaDeTrabalho"){
                isAreadetrabalho = true;
            }
        }
        /// if (col.gameObject.tag == "comeco" || col.gameObject.tag == "final"){}
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (transform.parent.tag == "conector"){
            if (col.gameObject.name == "areaDeConexao"){
                isAreadetrabalho = false;
            }
        }
        else{
            if (col.gameObject.name == "areaDeTrabalho"){
                isAreadetrabalho = false;
            }
        }
        isOverOutro = false;
        isOverConector = false;
    }
    void OnMouseDown()
    {
        originalPoint = transform.position;
        Vector3 scanPos = gameObject.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(scanPos);
        offset = scanPos - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isOverConector = false;
    }
    void OnMouseUp()
    {
        if (isAreadetrabalho && !isOverOutro && !isOverConector){
            if (!isClonado){
                GameObject minhaInstancia = Instantiate(meuPrefab, new Vector3(curPosition.x, curPosition.y, 1), Quaternion.identity);
                minhaInstancia.transform.SetParent(areaDeTrabalho);
                if (minhaInstancia.transform.tag != "conector"){
                    minhaInstancia.transform.GetChild(0).GetComponent<moverComponente>().isClonado = true;
                }
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
        Vector3 curScreenPoint = Input.mousePosition;
        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition.x = (float)(Mathf.Round(curPosition.x / gridTamanho) * gridTamanho);
        curPosition.y = (float)(Mathf.Round(curPosition.y / gridTamanho) * gridTamanho);
        pai.transform.position = curPosition;
        AreaCheck();
        if (isAreadetrabalho){
            Collider2D[] hitColliders;
            if (pai.transform.tag == "conector"){
                hitColliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), new Vector2(1f, .1f), 0);
            }
            else{
                hitColliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), new Vector2(2, 1), 0);
            }
            foreach (Collider2D Colover in hitColliders){
                if (Colover.transform.name == "mover" && Colover.transform != this.transform){
                    isOverOutro = true;
                }
                if (Colover.gameObject.tag == "comeco" || Colover.gameObject.tag == "final"){
                    if (Colover.transform.parent != this.transform.parent){
                        isOverConector = true;
                    }
                }
            }
        }
    }

    public void AreaCheck()
    {
        foreach (Transform child in transform.parent)     
        {
            if (child.gameObject.tag == "contorno") {
                if (isAreadetrabalho && !isOverOutro && !isOverConector){
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
        areaDeTrabalho = GameObject.Find("areaDeTrabalho").transform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}