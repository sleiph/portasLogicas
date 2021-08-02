using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fonteValor : MonoBehaviour
{
    public List<plugSaida> saidas;

    void OnMouseDown()
    {
        foreach (plugSaida crianca in saidas) crianca.valor = !crianca.valor;
    }

    void Start()
    {
        foreach (Transform crianca in transform.parent)     
        {
            if (crianca.tag == "saida"){
                saidas.Add(crianca.gameObject.GetComponent<plugSaida>());
            }
        }
    }

    void Update()
    {
        
    }
}
