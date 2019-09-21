using System;
using System.Collections;
using System.Collections.Generic;

public class Simbol_Table
{
    private Hashtable table = new Hashtable(); 
    private List<Token> parts; //Lista para armazenar o token e a posicao em que aparece
    private Token token;

    public Simbol_Table()
    { 
        parts = new List<Token>();

        // Definindo o tokens
        definirToken("KW_IF", "if");
        definirToken("KW_ELSE", "else");
        definirToken("KW_THEN", "then");
        definirToken("KW_PRINT", "print");

    }

    public void definirToken(string chave, string lexema)
    {
        token = new Token(chave, lexema, 0, 0);
        parts.Add(token);
    }

    // Metodo que add token criado para a tabela de simbolos
    public void addToken(string nomeTag, string lexema) { table.Add(nomeTag, lexema); }


    public void printTokens()
    {
        foreach(Token dinosaur in parts)
        {
            Console.WriteLine(dinosaur.toString();
        }
    }
}