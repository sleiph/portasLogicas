using UnityEngine;

public class Porta : MonoBehaviour
{
    Plug[] entradas { get; set; }

    Plug saida { get; set; }

    bool valor;

    bool calcularValor() {
        bool temp = false;

        switch (gameObject.tag) {
            case "AND":
                temp = entradas[0];
                foreach (Plug plug in entradas) {
                    temp = temp && plug.getValor();
                }
                break;
            case "OR":
                foreach (Plug plug in entradas) {
                    temp = temp || plug.getValor();
                }
                break;
        }

        return temp;
    }

    void getEntradas() {
        entradas = transform.GetChild(1).GetComponentsInChildren<Plug>();
    }
    void getSaida() {
        saida = transform.GetChild(2).GetChild(1).GetComponent<Plug>();
    }

    public bool getValor() {
        return valor;
    }
    public void setValor() {
        this.valor = calcularValor();
    }

    void Awake() {
        getEntradas();
        getSaida();
    }
    
}
