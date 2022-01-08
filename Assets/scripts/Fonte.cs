using UnityEngine;

public class Fonte : MonoBehaviour
{
    bool valor;
    [SerializeField] Plug[] saidas;

    void mudarValor() {
        valor = !valor;
    }
    
    void OnMouseDown()
    {
        mudarValor();
    }
    
    public bool getValor() {
        return valor;
    }
    public void setValor(bool valor) {
        this.valor = valor;
    }
}
