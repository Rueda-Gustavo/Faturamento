using System.Globalization;

class Program
{
    static void Main()
    {        
        string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\dados (2).xml";
        string[] xmlValues = File.ReadAllLines(path);
        List<Valores> valores = new List<Valores>();

        for (int i = 0; i < xmlValues.Length; i++)
        {
            if (xmlValues[i].Contains("dia"))
            {
                Valores novoValor = new Valores();
                string dia = xmlValues[i];
                dia = RetornaValor(dia);
                novoValor.Dia = int.Parse(dia);
                i++;

                string valor = xmlValues[i];
                valor = RetornaValor(valor);
                novoValor.Valor = Convert.ToDecimal(valor, CultureInfo.InvariantCulture);
                valores.Add(novoValor);
            }
        }

        decimal menorFaturamento = 0, maiorFaturamento = 0, totalFaturamento = 0;
        int diasComFaturamento = 0;
        menorFaturamento = valores[0].Valor;
        foreach (var valor in valores)
        {
            if (valor.Valor > 0 && valor.Valor < menorFaturamento)
                menorFaturamento = valor.Valor;
            else if (valor.Valor > 0 && valor.Valor > maiorFaturamento)
                maiorFaturamento = valor.Valor;

            if (valor.Valor > 0)
            {
                totalFaturamento += valor.Valor;
                diasComFaturamento++;
            }
        }


        decimal mediaFaturamento = totalFaturamento / diasComFaturamento;
        int faturamentoMaiorQueMedia = 0;

        valores.ForEach(valor =>
        {
            if (valor.Valor > mediaFaturamento)
                faturamentoMaiorQueMedia++;

        });
        Console.WriteLine("Menor faturamento: " + menorFaturamento);
        Console.WriteLine("Maior faturamento: " + maiorFaturamento);
        Console.WriteLine("Número dias com faturamento diário maior que à média mensal:" + faturamentoMaiorQueMedia);


    }

    private static string RetornaValor(string valor)
    {
        string resultado = string.Empty;
        for (int i = 0; i < valor.Length; i++)
        {
            if (Char.IsDigit(valor[i]) || valor[i].Equals('.'))
                resultado += valor[i];
        }
        return resultado;
    }
}

public class Valores : List<Valores>
{
    public int Dia { get; set; }
    public decimal Valor { get; set; }
}