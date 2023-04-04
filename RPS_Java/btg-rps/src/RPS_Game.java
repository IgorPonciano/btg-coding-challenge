public class RPS_Game {

    public static void main(String[] args) throws Exception {

    // Selecionar opções dos jogadores
    RPS_OPTION jogador1 = RPS_OPTION.PAPER;
    RPS_OPTION jogador2 = RPS_OPTION.PAPER;

    ChecaResultado(jogador1, jogador2);

    }

    private static void ChecaResultado(RPS_OPTION jogador1, RPS_OPTION jogador2)
    {
        String resultado = "";

        if(jogador1.equals(jogador2)){
            resultado = "Empate"; 
            // Jogar novamente
        }else if(jogador1.GetWeakness().contains(jogador2)){
            resultado = "Jogador 2 venceu";
            // Jogador 2 +1 ponto
        }else{
            resultado = "Jogador 1 venceu";
            // Jogador 1 +1 ponto
        }

        System.out.println(resultado);
    }
}
