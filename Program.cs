using System;

public class Program
{
    public static void Main(string[] args)
    {
    	Lexer lex = new Lexer("HelloWorld.txt");
		Tag tag = new Tag();

    	Console.Write("\n=>Lista de tokens:");

    	Token t = lex.proximoToken();

		/* 
		// Isso aqui são testes, pode ignorar
		Console.Write(tag.getEOF() );
		Console.WriteLine(t.toString() );		

		// Modo de impressao dos tokens que ainda deve ser revisado
    	while( t != null && !t.getNome().Equals( tag.getEOF() ) )
		{
    		Console.WriteLine(t.toString()+ "Linha: " + t.getLinha() + "Coluna: "+ t.getColuna() );
			t = lex.proximoToken();
    	}
		*/
    	Console.Write("\n=>Tabela de simbolos: ");
    	lex.printTS();
    	lex.closeFile();

    	Console.WriteLine("\n=> Fim da Compilação");
    }
}