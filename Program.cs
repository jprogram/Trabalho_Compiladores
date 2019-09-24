using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write((int)Tag.Tags.KW_IF);

        Lexer s = new Lexer("arq1.txt");

        s.printTS();
    }
}