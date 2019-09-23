using System;
using System.IO;
using System.Text;

public class Lexer
{
    private int lookHead, numLinha, numColuna;
    private StreamReader file;
    private Simbol_Table ts = new Simbol_Table();
    Encoding ascii = Encoding.ASCII;

    public Lexer(string arq)
    {      
        try
        {
            this.file = File.OpenText(arq);

            this.lookHead = 0;
            this.numLinha = 1;
            this.numColuna = 1;
        }

        catch (Exception ex)
        {
            Console.WriteLine("Erro de abertura do arquivo.");
        }
    }

    public void closeFile()
    {
        try { file.Close(); }
        
        catch (Exception e) { Console.WriteLine("Erro ao fechar arquivo"); }
    }  

    // imprime tabela de simbolos
    public void printTS() { this.ts.printTokens(); }

    /*
        Metodo ainda não esta funcionando, deve olhar como mexe com a
        tabela ascii no C#, pelo exemplo do professor, ele converte os
        num inteiros para caracter.
         
    public void retornaPonteiro(){
        byte[] bytes = {this.lookHead};
        string teste = Encoding.ASCII.GetString(bytes);
        Console.WriteLine(teste);
    }
    */
}