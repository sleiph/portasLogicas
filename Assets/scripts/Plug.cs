using UnityEngine;

public class Plug : MonoBehaviour
{
    Plug conexao;

    bool valor;

    private void OnTriggerEnter2D(Collider2D col)
    {
        // se eu fizer parte de um conector
        if (col.gameObject.tag == "entrada" || col.gameObject.tag == "saida") {
            conexao = col.gameObject.GetComponent<Plug>();
            if (col.gameObject.tag == "entrada") {
                conexao.setValor(valor);
                conexao.transform.parent.parent.GetComponent<Porta>()
                    .setValor();
            }
            else if (col.gameObject.tag == "saida") {
                transform.parent.GetComponent<Conector>()
                    .setValor(conexao.getPaiValor());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        // se eu fizer parte de um conector
        if (col.gameObject.tag == "entrada" || col.gameObject.tag == "saida") {
            if (col.gameObject.tag == "entrada") {
                conexao.setValor(false);
                conexao.transform.parent.parent.GetComponent<Porta>()
                    .setValor();
            }
            else if (col.gameObject.tag == "saida") {
                transform.parent.GetComponent<Conector>()
                    .setValor(false);
            }
            conexao = null;
        }
    }

    // só usado em saidas?
    public bool getPaiValor() {
        if (transform.parent.parent.tag == "fonte") {
            return transform.parent.parent.GetComponent<Fonte>().getValor();
        }
        // se for uma porta
        else {
            return transform.parent.parent.GetComponent<Porta>().getValor();
        }
    }

    public bool getValor() {
        return valor;
    }
    public void setValor(bool valor) {
        this.valor = valor;
    }

}
