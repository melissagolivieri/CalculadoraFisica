namespace CalculadoraFisica;


public class MenuForm : Form
{
    private readonly Formula[] _formulas =
    {
        new VelocidadeMedia(),  
        new Aceleracao(),       
        new Forca(),            
        new ForcaAtrito(),      
        new Peso(),             
        new LeiDeOhm()          
    };

    public MenuForm()
    {
        Text = "Calculadora de Fórmulas de Física";
        ClientSize = new Size(640, 340);
        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Segoe UI", 11F);

        var titulo = new Label
        {
            Text = "Escolha a fórmula que deseja calcular",
            Font = new Font("Segoe UI", 14F, FontStyle.Bold),
            Dock = DockStyle.Top,
            Height = 60,
            TextAlign = ContentAlignment.MiddleCenter
        };

        var grade = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 2,
            Padding = new Padding(10)
        };
        for (int c = 0; c < 3; c++) grade.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 3));
        for (int r = 0; r < 2; r++) grade.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));

        for (int i = 0; i < _formulas.Length; i++)
        {
            var formula = _formulas[i];
            var botao = new Button
            {
                Text = $"{i + 1}\n{formula.Nome}",
                Dock = DockStyle.Fill,
                Margin = new Padding(8)
            };
            botao.Click += (s, e) => AbrirTela(formula);
            grade.Controls.Add(botao, i % 3, i / 3);
        }

        Controls.Add(grade);
        Controls.Add(titulo);
    }

    private void AbrirTela(Formula formula)
    {
        using var tela = new FormCalculo(formula);
        Hide();
        tela.ShowDialog();
        Show();
    }
}

