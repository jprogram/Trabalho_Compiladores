using System;

public class Program
{
    public static void Main(string[] args)
    {
    	Lexer lex = new Lexer("HelloWorld.txt");
		Tag tag = new Tag();

		int ponteiro = 0;

    	Console.Write("\n=>Lista de tokens:");

    	Token t = lex.proximoToken(ponteiro);
		
		// Modo de impressao dos tokens que ainda deve ser revisado
    	while( t != null && !t.getNome().Equals( Tag.Tags.EOF.ToString() ) )
		{
			Console.WriteLine();
    		Console.WriteLine(t.toString()+ " Linha: " + t.getLinha() + " Coluna: "+ t.getColuna() );
			
			ponteiro++;
			t = lex.proximoToken(ponteiro);
    	}

    	Console.Write("\n=>Tabela de simbolos: ");
    	lex.printTS();
    	lex.closeFile();

    	Console.WriteLine("\n\n=> Fim da Compilação");
    }
}