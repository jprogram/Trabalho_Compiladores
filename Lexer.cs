/*
    Esta classe esta com APENAS 4 estados,
    
    Estado 1 = Estado inicial, onde verifica tudo e muda de estados
    Estado 2 = Operador de igual
    Estado 3 = Verifica os caracters, para montagem de Id's
    Estado 4 = Tratamento para valores numericos
 */

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
        Token token;

        int estado = 1; // estado do automato

        // Separei em lexema para strings e a var valor para numerico
        // soh para identificar melhor na montagem do lexema e jogar na hashtable
        // mas poderia muito bem deixar apenas o var lexema para os dois.
        string lexema = "", valor = "";

        // Vetor que armazena a string(linha) lida pelo arquivo txt
        char[] c = this.linha.ToCharArray();

        while (linha != null && this.lookHead <= c.Length)
        {
            // Maldito recurso tecnico para ler a maldita
            // linha e criar o maldito token
            // mas este maldito alem de estar horrivelmente horrivel escrito
            // este maldito nao le a maldita variavel a e a outra maldita 
            // variavel que esta carinhosamente chamada de maldito
            if(this.lookHead == c.Length){
                this.lookHead = 0;
                this.linha = this.file.ReadLine();
                if(this.linha != null){
                    this.numLinha++;
                    c = this.linha.ToCharArray();
                }
                else{
                    break;
                }
            }
            
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
                        Console.Write("\nLexema = {0}", lexema);
                        lexema += c[this.lookHead];
                        this.lookHead++;
                        estado = 3;
                    }

                    else if(Char.IsDigit(c[this.lookHead])){
                        valor = "";
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
                        token = ts.getToken(lexema);

                        if(token is null){
                            token = ts.definirToken(Tag.operadores.OP_IGUAL.ToString(), "=", this.numLinha, this.numColuna);
                            ts.addTokenTS(Tag.operadores.OP_IGUAL.ToString(), "=");
                            return token;
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
                        Console.Write("\n{0}", c[this.lookHead]);
                    }

                    // Caso o token não existir ele cria e add na hashtable
                    // caso contrario só retorna o token ja existente e muda a linha e coluna
                    // em que aparece no arquivo txt
                    token = ts.getToken(lexema);

                    if(token is null){ 
                        this.numColuna = this.lookHead;
                        // Cria o token e add na hashtable
                        token = ts.definirToken((Tag.Tags.ID.ToString()+this.contadorId), lexema, this.numLinha, this.numColuna);
                        ts.addTokenTS((Tag.Tags.ID.ToString()+this.contadorId), lexema);

                        //Contador para incrementar a cada id criado
                        this.contadorId++;
                        lexema = "";
                        
                        return token;
                    }

                    estado = 1;
                    lexema = "";

                    token.setLinha(this.numLinha);
                    token.setColuna(this.lookHead);
        
                    return token;    

                case 4: //caso possua algum valor numerico no arq txt
                    while(this.lookHead < c.Length)
                    {
                        valor += c[this.lookHead];
                        this.lookHead++;
                    }

                    token = ts.getToken(valor);

                    if(token is null){
                        this.numColuna = this.lookHead;
                        
                        token = ts.definirToken((Tag.Tags.NUM.ToString()+this.contadorNum), valor, this.numLinha, this.numColuna);
                        ts.addTokenTS((Tag.Tags.NUM.ToString()+this.contadorNum), valor);
                        this.contadorNum++;    
                        
                        return token;
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
