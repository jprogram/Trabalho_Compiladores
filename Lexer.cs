using System;
using System.IO;

public class Lexer
{
    private int lookHead, numLinha, numColuna;
    private StreamReader file;

    public Lexer(string arq)
    {      
        try
        {
            file = File.OpenText(arq);

            lookHead = 0;
            numLinha = 1;
            numColuna = 1;

            Simbol_Table ts = new Simbol_Table();
        }

        catch (Exception ex)
        {
            Console.WriteLine("Erro de abertura do arquivo.");
        }
    }

    public void closeFile()
    {
        try
        {
            file.Close();
        }
        
        catch (Exception e)
        {
            Console.WriteLine("Erro ao fechar arquivo");
        }
    }  
}