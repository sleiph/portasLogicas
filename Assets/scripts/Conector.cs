public class Conector
{

    Porta cabeca { get; set; }
    Porta bunda { get; set; }
    bool valor { get; set; }
    string tipo { get; set; }

    Conector(string tipo) {
        this.tipo = tipo;
    }

}
