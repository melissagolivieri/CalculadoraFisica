using System.Globalization;

namespace CalculadoraFisica;

public class FormCalculo : Form
{
    private readonly Formula _formula;
    private readonly List<TextBox> _caixas = new();
    private readonly Label _lblResultado = new();

    public FormCalculo(Formula formula)
    {
        _formula = formula;

        Text = formula.Nome;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Font = new Font("Segoe UI", 10F);

        MontarInterface();
    }

    private void MontarInterface()
    {
        int y = 15;

        var titulo = new Label
        {
            Text = _formula.Nome,
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(20, y)
        };
        Controls.Add(titulo);
        y += 40;

        var equacao = new Label
        {
            Text = "Fórmula: " + _formula.Equacao,
            ForeColor = Color.DimGray,
            AutoSize = true,
            Location = new Point(20, y)
        };
        Controls.Add(equacao);
        y += 35;

        foreach (var campo in _formula.Campos)
        {
            Controls.Add(new Label
            {
                Text = campo.Rotulo,
                AutoSize = true,
                Location = new Point(20, y)
            });
            y += 24;

            var caixa = new TextBox
            {
                Text = campo.Padrao,
                Width = 340,
                Location = new Point(20, y)
            };
            _caixas.Add(caixa);
            Controls.Add(caixa);
            y += 40;
        }

        var btnCalcular = new Button { Text = "Calcular", Size = new Size(105, 35), Location = new Point(20, y) };
        var btnLimpar = new Button { Text = "Limpar", Size = new Size(105, 35), Location = new Point(140, y) };
        var btnVoltar = new Button { Text = "Voltar", Size = new Size(105, 35), Location = new Point(260, y) };
        btnCalcular.Click += (s, e) => Calcular();
        btnLimpar.Click += (s, e) => Limpar();
        btnVoltar.Click += (s, e) => Close();
        Controls.AddRange(new Control[] { btnCalcular, btnLimpar, btnVoltar });
        y += 55;

        _lblResultado.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        _lblResultado.ForeColor = Color.DarkGreen;
        _lblResultado.AutoSize = false;
        _lblResultado.Size = new Size(340, 35);
        _lblResultado.Location = new Point(20, y);
        Controls.Add(_lblResultado);
        y += 55;

        ClientSize = new Size(380, y);
        AcceptButton = btnCalcular;
    }

    private void Calcular()
    {
        var valores = new double[_caixas.Count];

        for (int i = 0; i < _caixas.Count; i++)
        {
            string texto = _caixas[i].Text.Trim().Replace(',', '.');
            if (!double.TryParse(texto, NumberStyles.Float, CultureInfo.InvariantCulture, out valores[i]))
            {
                MessageBox.Show("Digite um número válido em: " + _formula.Campos[i].Rotulo,
                    "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _caixas[i].Focus();
                return;
            }
        }

        try
        {
            double resultado = _formula.Calcular(valores);
            _lblResultado.Text = $"Resultado: {resultado.ToString("0.####", CultureInfo.CurrentCulture)} {_formula.UnidadeResultado}";
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Erro no cálculo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Limpar()
    {
        for (int i = 0; i < _caixas.Count; i++)
            _caixas[i].Text = _formula.Campos[i].Padrao;

        _lblResultado.Text = "";
        _caixas[0].Focus();
    }
}
