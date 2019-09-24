using System;
using System.IO;

public class Lexer
{
    private int lookHead, numLinha, numColuna;
    private StreamReader file;
    private Simbol_Table ts = new Simbol_Table();

    public Lexer(string arq)
    {      
        try
        {
            this.file = File.OpenText(arq);

            this.lookHead = 35;
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
    
    // metodo ainda para ser testado
    public void retornaPonteiro()
    {
        if((char)this.lookHead != '')
        {
            this.file.Seek(SeekOrigin.Current-1);
        }
    }

    public void sinalizaErroLexico(string message)
    {
        Console.Write("[Erro Lexico]: "+ message+ "\n");
    }

    public Token proximoToken()
    {
        int estado = 1;
        string lexema = "";
        char c = '\u0000';

        while (this.file.ReadLine()) != null)
        {
            this.lookHead = Convert.ToInt32(this.file.Read());
            c = (char)this.lookHead;

            switch (estado)
            {
                case 1:
                    if(c == ''){ 
                        return Token(Tag.Tags.EOF, "EOF", this.numLinha, this.numColuna);
                    }

                    if(c == ' ' or c == '\t' or c == '\n' or c == '\r'){
                        estado = 1;
                    }

                    if(c == '='){
                        estado = 2;
                    }
                    break;

                case 2:
                    Console.Write("Teste");
                    break;

                default:
                    break;
            }
        }
    }

    /*
    * Codigo que encontrei na net que pode ajudar
    * no desenvolvimento em saber o proximo token
    *
    public void proxPonteiro(){
        int min = 0;
        int max = 128;
        for (int i = min; i < max; i++)
        {
            // Get ASCII character.
            char c = (char)i;

            // Get display string.
            string display = string.Empty;
            if (char.IsWhiteSpace(c))
            {
                display = c.ToString();
                switch (c)
                {
                    case '\t':
                        display = "\\t";
                        Console.WriteLine(display);
                        break;
                    case ' ':
                        display = "space";
                        Console.WriteLine(display);
                        break;
                    case '\n':
                        display = "\\n";
                        Console.WriteLine(display);
                        break;
                    case '\r':
                        display = "\\r";
                        Console.WriteLine(display);
                        break;
                    case '\v':
                        display = "\\v";
                        break;
                    case '\f':
                        display = "\\f";
                        break;
                }
            }
        }    
    }
    */
}
