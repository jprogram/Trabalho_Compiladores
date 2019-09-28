public class Tag
{
    /* Criei variaveis do tipo enum, seguindo um pouco do
    *  programa que o professor disponibilizou em Python
    *  onde ele utiliza o enum para a criacao das tags. 
    */
    public enum Tags
    {
        EOF = -1, 
        KW_IF = 1, 
        KW_ELSE = 2, 
        KW_THEN = 3, 
        KW_PRINT = 4,

        ID = 20,
        NUM = 21

    }

    public enum operadores
    {
        OP_MENOR_IGUAL = 11,
        OP_MAIOR_IGUAL = 12,
        OP_MAIOR = 13,
        OP_IGUAL = 14
    }
}
