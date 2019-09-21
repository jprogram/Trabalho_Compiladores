using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write((int)Tag.Tags.KW_IF);

        Simbol_Table s = new Simbol_Table();

        s.printTokens();

        Lexer lexer = new Lexer("arq01.txt");
    }
}