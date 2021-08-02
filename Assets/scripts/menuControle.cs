using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuControle : MonoBehaviour
{
    Transform areaDeTrabalho;
    Toggle formulasBotao;
    public void sairPrograma()
    {
        Application.Quit();
    }

    public void clickToggle()
    {
        toggleFormula(formulasBotao.isOn);
    }
    void toggleFormula(bool botao)
    {
        foreach (Transform filho in areaDeTrabalho){
            if (filho.gameObject.layer == 8){
                foreach (Transform neto in filho){
                    if (neto.tag == "texto") neto.gameObject.SetActive(botao);
                }
            }
        }
    }
    
    void Start()
    {
        areaDeTrabalho = GameObject.Find("areaDeTrabalho").transform;
        formulasBotao = transform.GetChild(2).gameObject.GetComponent<Toggle>();
    }

    void Update()
    {

    }
}
