using System;

public class Program
{
    public static void Main(string[] args)
    {
    	Lexer lex = new Lexer("HelloWorld.txt");

    	Console.Write("\n=>Lista de tokens:");

    	Token t = new Token();
    	t = lex.proximoToken();

    	while(t is not null && t.getNome() != Tag.Tags.EOF){
    		Console.WriteLine(t.toString()+ "Linha: " + (str)t.getLinha() + "Coluna: "+ (str)t.getColuna());
    	}

    	Console.Write("\n=>Tabela de simbolos: ");
    	lex.printTS();
    	lex.closeFile();

    	Console.WriteLine("\n=> Fim da Compilação");

        Console.Write((int)Tag.Tags.KW_IF);

        Lexer s = new Lexer("arq1.txt");

        s.printTS();
    }
}