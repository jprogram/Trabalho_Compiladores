using System;

public class Program
{
    public static void Main(string[] args)
    {
    	Lexer lex = new Lexer("HelloWorld.txt");
		Tag tag = new Tag();

    	Console.Write("\n=>Lista de tokens:");

    	Token token = lex.proximoToken();
		
		// Modo de impressao dos tokens
    	while( token != null )
		{
			Console.WriteLine();
    		Console.WriteLine(token.toString()+ " Linha: " + token.getLinha() + " Coluna: "+ token.getColuna() );
			token = lex.proximoToken();
    	}

    	Console.Write("\n=>Tabela de simbolos: ");
    	lex.printTS();
    	lex.closeFile();

    	Console.WriteLine("\n\n=> Fim da Compilação");
    }
}