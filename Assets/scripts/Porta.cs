public class Porta
{

    Porta[] pais { get; set; }

    Porta filho { get; set; }

    bool valor { get; set; }

    string tipo { get; set; }

    Porta (string tipo) {
        this.tipo = tipo;
    }
    
}
