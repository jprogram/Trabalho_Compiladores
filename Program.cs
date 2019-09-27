using System;

public class Program
{
    public static void Main(string[] args)
    {
    	Lexer lex = new Lexer("HelloWorld.txt");
		Tag tag = new Tag();

    	Console.Write("\n=>Lista de tokens:");

    	Token t = lex.proximoToken();
		
		// Modo de impressao dos tokens que ainda deve ser revisado
    	while( t != null && !t.getNome().Equals( Tag.Tags.EOF.ToString() ) )
		{
			Console.WriteLine();
    		Console.WriteLine(t.toString()+ " Linha: " + t.getLinha() + " Coluna: "+ t.getColuna() );
			t = lex.proximoToken();
			/*  
            // Isso aqui era para testar a logica para ler
            // a proxima linha do arquivo txt e ficar
            // montando os tokens e tals
			if(t is null){
				lex.lerProxLinha();
				t = lex.proximoToken();
			}
			*/
    	}

    	Console.Write("\n=>Tabela de simbolos: ");
    	lex.printTS();
    	lex.closeFile();

    	Console.WriteLine("\n\n=> Fim da Compilação");
    }
}