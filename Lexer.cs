using System;
using System.IO;
using System.Collections;

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

    public void lerProxLinha(){
        this.linha = this.file.ReadLine();
        this.ts.table.Remove(Tag.Tags.ID.ToString());
        Console.Write(this.linha);
        this.lookHead = 0;
    }


    public Token proximoToken()
    {
        Tag tag = new Tag();
        Token t;

        int estado = 1;
        string lexema = "", valor = "";

        char[] c = this.linha.ToCharArray();

        while (this.lookHead < c.Length)
        {
            switch (estado)
            {
                case 1:
        
                    if(c[this.lookHead].Equals(' ') || c[this.lookHead].Equals('\t') || c[this.lookHead].Equals('\n') || c[this.lookHead].Equals('\r'))
                    {
                        this.lookHead++;
                        estado = 1;
                    }

                    else if(c[this.lookHead].Equals('='))
                    {
                        lexema = "=";
                        this.lookHead++;
                        estado = 2;
                    }

                    else if(Char.IsLetter(c[this.lookHead])){
                        lexema += c[this.lookHead];
                        this.lookHead++;
                        estado = 3;
                    }

                    else if(Char.IsDigit(c[this.lookHead])){
                        lexema = "";
                        estado = 4;
                    }
                    else{
                        sinalizaErroLexico(" ");
                        return null;
                    }
                    
                    break;

                case 2:

                    if(lexema.Equals("=")){
                        this.lookHead++;
            
                        t = ts.getToken(lexema);

                        if(t is null){
                            t = ts.definirToken(Tag.operadores.OP_IGUAL.ToString(), "=", this.numLinha, this.numColuna);
                            ts.addTokenTS(Tag.operadores.OP_IGUAL.ToString(), "=");
                            return t;
                        }
                        estado = 1;
                    }
                    else{
                        sinalizaErroLexico("Caractere invalido [" + c[this.lookHead] + "] na linha " +
                        numLinha + " e coluna " + numColuna);
                        return null;
                    }
                    break;

                case 3:
                    while(Char.IsLetter(c[this.lookHead])){
                        lexema += c[this.lookHead];
                        this.lookHead++;
                    }

                    t = ts.getToken(lexema);
                    if(t is null){
                        t = ts.definirToken(Tag.Tags.ID.ToString(), lexema, this.numLinha, this.numColuna);
                        ts.addTokenTS(Tag.Tags.ID.ToString(), lexema);
                        return t;
                    }          
                        
                    break;    

                case 4:
                    while(this.lookHead < c.Length)
                    {
                        valor += c[this.lookHead];
                        this.lookHead++;
                    }

                    t = ts.getToken(valor);

                    if(t is null){
                        t = ts.definirToken(Tag.Tags.NUM.ToString(), valor, this.numLinha, this.numColuna);
                        ts.addTokenTS(Tag.Tags.NUM.ToString(), valor);    
                        return t;
                    }            

                    break;

                default:
                    return new Token(Tag.Tags.EOF.ToString(), "EOF", this.numLinha, this.numColuna);
                    break;
                    
            }
        }
        
        return null;
    }
}
