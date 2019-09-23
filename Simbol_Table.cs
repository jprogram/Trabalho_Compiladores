using System;
using System.Collections;
using System.Collections.Generic;

public class Simbol_Table
{
    private Hashtable table = new Hashtable(); 
    private List<Token> tokens; //Lista para armazenar o token e a posicao em que aparece
    private Token token;

    public Simbol_Table()
    { 
        this.tokens = new List<Token>();

        // Definindo o tokens
        definirToken("KW_IF", "if");
        definirToken("KW_ELSE", "else");
        definirToken("KW_THEN", "then");
        definirToken("KW_PRINT", "print");

    }

    public void definirToken(string chave, string lexema)
    {
        this.token = new Token(chave, lexema, 0, 0);
        this.tokens.Add(token);
    }

    // Metodo que add token criado para a tabela de simbolos
    public void addToken(string nomeTag, string lexema) { this.table.Add(nomeTag, lexema); }


    public void printTokens()
    {
        foreach(Token tok in this.tokens)
        {
            Console.WriteLine(tok.toString());
        }
    }
}