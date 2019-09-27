using System;
using System.IO;

public class Lexer
{
    private string linha;
    private int lookHead, numLinha, numColuna;
    private StreamReader file;
    private Simbol_Table ts = new Simbol_Table();

    public Lexer(string arq)
    {      
        try
        {
            this.file = new StreamReader(arq);
            this.linha = this.file.ReadLine();
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

    public void sinalizaErroLexico(string message)
    {
        Console.Write("[Erro Lexico]: "+ message+ "\n");
    }

    // Funcao incompleta falta muito ajustes
    // Fiz retornar tokens criados para testar se ele realmente estava
    // lendo o arquivo txt e entrando nas condicionais
    // Ao retirar do comentario, pode reparar que ele consome um caracter estranho

    public Token proximoToken(int ponteiro)
    {
        Tag tag = new Tag();
        Token t;

        int estado = 1;
        string lexema = "";
        //char c = '\u0000';

        char[] c = this.linha.ToCharArray();

        while (ponteiro <= 20)
        {
            Console.Write("\nEstado: {0}", estado);
            switch (estado)
            {
                case 1:
        
                    if(c[ponteiro].Equals(' ') || c[ponteiro].Equals('\t') || c[ponteiro].Equals('\n') || c[ponteiro].Equals('\r'))
                    {
                        ponteiro++;
                        estado = 1;
                    }

                    else if(c[ponteiro].Equals('='))
                    {
                        lexema = "=";
                        ponteiro++;
                        estado = 2;
                    }

                    else if(Char.IsLetter(c[ponteiro])){
                        lexema += c[ponteiro];
                        ponteiro++;
                        estado = 3;
                    }

                    else if(Char.IsDigit(c[ponteiro])){
                        Console.Write("token: {0}", c[ponteiro]);
                        lexema = "";
                        //ponteiro++;
                        estado = 4;
                    }
                    else{
                        ponteiro++;
                        estado = 1;
                    }
                    
                    break;

                case 2:

                    if(lexema.Equals("=")){
                        ponteiro++;
            
                        t = ts.getToken(lexema);

                        if(t is null){
                            t = ts.definirToken(Tag.operadores.OP_IGUAL.ToString(), "=", this.numLinha, this.numColuna);
                            ts.addTokenTS(Tag.operadores.OP_IGUAL.ToString(), "=");
                            return t;
                        }
                        estado = 1;
                    }
                    else{
                        sinalizaErroLexico("Caractere invalido [" + c[ponteiro] + "] na linha " +
                        numLinha + " e coluna " + numColuna);
                        return null;
                    }
                    break;

                case 3:
                    if(Char.IsLetter(c[ponteiro])){
                        Console.Write("\nEstado: {0} q", estado);
                        //estado = 1;
                        //ponteiro++;
                    }    
                    else{
                        t = ts.getToken(lexema);
                        if(t is null){
                            t = ts.definirToken(Tag.Tags.ID.ToString(), lexema, this.numLinha, this.numColuna);
                            ts.addTokenTS(Tag.Tags.ID.ToString(), lexema);
                        }          
                        return t;
                    }

                    break;    
                /*     
                
                case 4:
                    for(int i = ponteiro; i < c.Length; i++){
                        Console.Write("Entrou e");
                        lexema += c[ponteiro];
                        ponteiro++;
                    }
            
                    Console.Write(lexema);
                    t = ts.getToken(lexema);
                    if(t is null){
                        Console.Write(Tag.Tags.VAL.ToString());
                        t = ts.definirToken(Tag.Tags.VAL.ToString(), lexema, this.numLinha, this.numColuna);
                        ts.addTokenTS(Tag.Tags.VAL.ToString(), lexema);
                    }
                    return t;     
                     
                    //ponteiro++;    
                    break;

                */
                default:
                    return new Token(Tag.Tags.EOF.ToString(), "EOF", this.numLinha, this.numColuna);
                    break;
                    
            }
        }
        return null;
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
