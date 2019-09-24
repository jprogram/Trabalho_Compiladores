using System;
using System.IO;

public class Lexer
{
    private int lookHead, numLinha, numColuna;
    private FileStream file;
    private Simbol_Table ts = new Simbol_Table();

    public Lexer(string arq)
    {      
        try
        {
            this.file = File.OpenRead(arq);

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
        try { this.file.Close(); }
        
        catch (Exception e) { Console.WriteLine("Erro ao fechar arquivo"); }
    }  

    // imprime tabela de simbolos
    public void printTS() { this.ts.printTokens(); }
    
    // metodo ainda para ser testado
    public void retornaPonteiro()
    {
        if( this.lookHead != 0 )
        {
            this.file.Seek((long)SeekOrigin.Current-1, SeekOrigin.Begin);
        }
    }

    public void sinalizaErroLexico(string message)
    {
        Console.Write("[Erro Lexico]: "+ message+ "\n");
    }

    /*
    // Funcao incompleta falta muito ajustes
    // Fiz retornar tokens criados para testar se ele realmente estava
    // lendo o arquivo txt e entrando nas condicionais
    // Ao retirar do comentario, pode reparar que ele consome um caracter estranho

    public Token proximoToken()
    {
        Tag tag = new Tag();
        int estado = 1;
        string lexema = "";
        char c = '\u0000';

        byte[] bytes = new byte[this.file.Length];
        int total = (int)this.file.Length;

        int ponteiro = 0;

        StreamReader sr = new StreamReader(this.file);

        while (true)
        {
            this.lookHead = this.file.Read(bytes, ponteiro, total);
            c = (char)this.lookHead;

            switch (estado)
            {
                case 1:
                    if(c.Equals('\u0000'))
                    { 
                        return new Token(tag.getEOF(), "EOF", this.numLinha, this.numColuna);
                    }
        
                    else if(c.Equals(' ') || c.Equals('\t') || c.Equals('\n') || c.Equals('\r'))
                    {
                        estado = 1;
                        ponteiro++;
                        return new Token("KW", "tteste", this.numLinha, this.numColuna);
                    }

                    else if(c.Equals('='))
                    {
                        estado = 2;
                        ponteiro++;
                        return new Token("KW_=", "=", this.numLinha, this.numColuna);
                    }
                    
                    return new Token("Ab", c+"", this.numLinha, this.numColuna); 
                    break;
                case 2:
                    Console.Write("Teste");
                    return new Token("a", "teste", this.numLinha, this.numColuna);
                    break;

                default:
                    Console.Write("Vazio");
                    return new Token("aa", "vazio", this.numLinha, this.numColuna);
                    break;
            }
        }
        return new Token("Nulo", "testeparaficartestado", this.numLinha, this.numColuna);
    }
    */

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
