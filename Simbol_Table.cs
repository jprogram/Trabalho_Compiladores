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
        definirToken("KW_IF", "if", 0 , 0);
        definirToken("KW_ELSE", "else", 0, 0);
        definirToken("KW_THEN", "then", 0 , 0);
        definirToken("KW_PRINT", "print", 0, 0);

    }

    public Token definirToken(string chave, string lexema, int linha, int coluna)
    {
        this.token = new Token(chave, lexema, linha, coluna);
        this.tokens.Add(token);
        return this.token;
    }

    // Metodo que add token criado para a tabela de simbolos
    public void addTokenTS(string nomeTag, string lexema) { this.table.Add(nomeTag, lexema); }

    public Token getToken(string lexema){
        foreach(Token tok in this.tokens){
            if(tok.getLexema().Equals(lexema)){
                return tok;
            }
        }
        return null;
    }

    public void printTokens()
    {
        foreach(Token tok in this.tokens)
        {
            Console.Write("\n"+tok.toString());
        }
    }
}