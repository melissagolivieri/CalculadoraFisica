namespace CalculadoraFisica;


public record Campo(string Rotulo, string Padrao = "");


public abstract class Formula
{
    public abstract string Nome { get; }
    public abstract string Equacao { get; }
    public abstract string UnidadeResultado { get; }
    public abstract IReadOnlyList<Campo> Campos { get; }

   
    public abstract double Calcular(double[] valores);
}

public class VelocidadeMedia : Formula
{
    public override string Nome => "Velocidade média";
    public override string Equacao => "Vm = Δs / Δt";
    public override string UnidadeResultado => "m/s";
    public override IReadOnlyList<Campo> Campos { get; } = new[]
    {
        new Campo("Distância percorrida Δs (m):"),
        new Campo("Intervalo de tempo Δt (s):")
    };

    public override double Calcular(double[] v)
    {
        if (v[1] == 0) throw new ArgumentException("O tempo não pode ser zero.");
        return v[0] / v[1];
    }
}

public class Aceleracao : Formula
{
    public override string Nome => "Aceleração";
    public override string Equacao => "a = Δv / Δt";
    public override string UnidadeResultado => "m/s²";
    public override IReadOnlyList<Campo> Campos { get; } = new[]
    {
        new Campo("Variação de velocidade Δv (m/s):"),
        new Campo("Intervalo de tempo Δt (s):")
    };

    public override double Calcular(double[] v)
    {
        if (v[1] == 0) throw new ArgumentException("O tempo não pode ser zero.");
        return v[0] / v[1];
    }
}

public class Forca : Formula
{
    public override string Nome => "Força";
    public override string Equacao => "F = m · a";
    public override string UnidadeResultado => "N";
    public override IReadOnlyList<Campo> Campos { get; } = new[]
    {
        new Campo("Massa m (kg):"),
        new Campo("Aceleração a (m/s²):")
    };

    public override double Calcular(double[] v) => v[0] * v[1];
}

public class ForcaAtrito : Formula
{
    public override string Nome => "Força de atrito";
    public override string Equacao => "Fat = μ · N";
    public override string UnidadeResultado => "N";
    public override IReadOnlyList<Campo> Campos { get; } = new[]
    {
        new Campo("Coeficiente de atrito μ:"),
        new Campo("Força normal N (N):")
    };

    public override double Calcular(double[] v)
    {
        if (v[0] < 0) throw new ArgumentException("O coeficiente de atrito não pode ser negativo.");
        return v[0] * v[1];
    }
}

public class Peso : Formula
{
    public override string Nome => "Peso";
    public override string Equacao => "P = m · g";
    public override string UnidadeResultado => "N";
    public override IReadOnlyList<Campo> Campos { get; } = new[]
    {
        new Campo("Massa m (kg):"),
        new Campo("Gravidade g (m/s²):", "9,8")
    };

    public override double Calcular(double[] v) => v[0] * v[1];
}

public class LeiDeOhm : Formula
{
    public override string Nome => "1ª Lei de Ohm";
    public override string Equacao => "U = R · i";
    public override string UnidadeResultado => "V";
    public override IReadOnlyList<Campo> Campos { get; } = new[]
    {
        new Campo("Resistência R (Ω):"),
        new Campo("Corrente elétrica i (A):")
    };

    public override double Calcular(double[] v) => v[0] * v[1];
}
