using UnityEngine;

public class Conector : MonoBehaviour
{
    Plug cabeca { get; set; }
    Plug bunda { get; set; }
    bool valor;

    public bool getValor() {
        return valor;
    }
    public void setValor(bool valor) {
        this.valor = valor;
        cabeca.setValor(valor);
        bunda.setValor(valor);
    }

    void Awake() {
        cabeca = transform.GetChild(1).GetComponent<Plug>();
        bunda = transform.GetChild(2).GetComponent<Plug>();
    }
}
