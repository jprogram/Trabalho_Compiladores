using System;

public class Token
{
    private string nome, lexema;
    private int linha, coluna;

    public Token(string nome, string lexema, int linha, int coluna)
    {
        this.nome = nome;
        this.lexema = lexema;
        this.linha = linha;
        this.coluna = coluna;
    }

    // Metodos gets
    public string getNome(){ return this.nome; }

    public string getLexema(){ return this.lexema; }

    public int getLinha(){ return this.linha; }

    public int getColuna(){ return this.coluna; }

    // Metodos sets
    public void setLinha(int l){ this.linha = l; }

    public void setColuna(int c){ this.coluna = c; }

    // Metodo que retorna a chave para impressao
    public string toString(){ return "<" + this.nome + ", \""+ this.lexema + "\">"; }
}