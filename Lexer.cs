using System;
using System.IO;

public class Lexer
{
    private string linha;
    private int lookHead, numLinha, numColuna, contadorId, contadorNum;
    private StreamReader file;
    private Simbol_Table ts = new Simbol_Table();

    public Lexer(string arq)
    {      
        try
        {
            this.file = new StreamReader(arq);
            this.linha = this.file.ReadLine();

            this.lookHead = 0; // considerei como se fosse um ponteiro para a leitura dos caracters

            // Estes são os contadores para serem melhor identificados
            // na hora de montar e salvar o token na hashtable
            this.contadorId = 1;
            this.contadorNum = 1;

            // variaveis para descobrirmos a localização dos tokens
            // formados no arquivo txt
            this.numLinha = 1;
            this.numColuna = 1;
        }

        catch (Exception ex)
        {
            Console.WriteLine("Erro de abertura do arquivo.");
        }
    }

    public void closeFile()
    {
        try { this.file.Close(); }
        
        catch (Exception e) { Console.WriteLine("Erro ao fechar arquivo"); }
    }  

    // imprime tabela de simbolos
    public void printTS() { this.ts.printTokens(); }

    public void sinalizaErroLexico(string message)
    {
        Console.Write("[Erro Lexico]: "+ message+ "\n");
    }

    // Metodo que simula o automato
    // irá ler os caracters e verifica-los para a montagens dos tokens e da hashtable
    public Token proximoToken()
    {
        // instancias dos objetos
        Tag tag = new Tag();
        Token t;

        int estado = 1; // estado do automato

        // Separei em lexema para strings e a var valor para numerico
        // soh para identificar melhor na montagem do lexema e jogar na hashtable
        // mas poderia muito bem deixar apenas o var lexema para os dois.
        string lexema = "", valor = "";

        char[] c = this.linha.ToCharArray();

        while (this.lookHead < c.Length)
        {
            switch (estado)
            {
                case 1:
        
                    if(c[this.lookHead].Equals(' ') || c[this.lookHead].Equals('\t') || c[this.lookHead].Equals('\n') || c[this.lookHead].Equals('\r'))
                    {
                        this.lookHead++;
                        estado = 1;
                    }

                    else if(c[this.lookHead].Equals('='))
                    {
                        lexema = "=";
                        this.lookHead++;
                        estado = 2;
                    }

                    else if(Char.IsLetter(c[this.lookHead])){
                        lexema += c[this.lookHead];
                        this.lookHead++;
                        estado = 3;
                    }

                    else if(Char.IsDigit(c[this.lookHead])){
                        lexema = "";
                        estado = 4;
                    }
                    else{
                        sinalizaErroLexico(" ");
                        return null;
                    }
                    
                    break;

                case 2: // simbolo de igual

                    if(lexema.Equals("=")){
                        this.numColuna = this.lookHead;

                        this.lookHead++;

                        // verifica se ja existe algum token ja criado
                        t = ts.getToken(lexema);

                        if(t is null){
                            t = ts.definirToken(Tag.operadores.OP_IGUAL.ToString(), "=", this.numLinha, this.numColuna);
                            ts.addTokenTS(Tag.operadores.OP_IGUAL.ToString(), "=");
                            return t;
                        }
                        estado = 1;
                    }
                    else{
                        sinalizaErroLexico("Caractere invalido [" + c[this.lookHead] + "] na linha " +
                        this.numLinha + " e coluna " + this.numColuna);
                        return null;
                    }
                    break;

                case 3: // caso possuir algum caracter

                    while(Char.IsLetter(c[this.lookHead])){
                        lexema += c[this.lookHead];
                        this.lookHead++;
                    }

                    t = ts.getToken(lexema);

                    if(t is null){
                        this.numColuna = this.lookHead;

                        t = ts.definirToken((Tag.Tags.ID.ToString()+this.contadorId), lexema, this.numLinha, this.numColuna);
                        ts.addTokenTS((Tag.Tags.ID.ToString()+this.contadorId), lexema);
                        this.contadorId++;
                        return t;
                    }          
                        
                    break;    

                case 4: //caso possua algum valor numerico no arq txt
                    while(this.lookHead < c.Length)
                    {
                        valor += c[this.lookHead];
                        this.lookHead++;
                    }

                    t = ts.getToken(valor);

                    if(t is null){
                        this.numColuna = this.lookHead;
                        
                        t = ts.definirToken((Tag.Tags.NUM.ToString()+this.contadorNum), valor, this.numLinha, this.numColuna);
                        ts.addTokenTS((Tag.Tags.NUM.ToString()+this.contadorNum), valor);
                        this.contadorNum++;    
                        
                        return t;
                    }            

                    break;

                default:
                    return new Token(Tag.Tags.EOF.ToString(), "EOF", this.numLinha, this.numColuna);
                    break;
                    
            }
        }
        
        return null;
    }
}
