using UnityEngine;

public class Fonte : MonoBehaviour
{
    bool valor { get; set; }
    [SerializeField] Plug[] saidas;

    void mudarValor() {
        valor = !valor;

        foreach (Plug saida in saidas) {
            saida.setValor(valor);
        }
    }
    
    void OnMouseDown()
    {
        mudarValor();
    }
    
}
