# btg-coding-challenge
BTG Pactual (IT-PME) - Teste de proficiência em raciocínio lógico e conhecimentos de programação. Realizado por Igor Ponciano em 03/04.

## 📝Sobre o pedra, papel e tesoura em java

Durante a conversa anterior, trabalhamos um exercício de elaboração da base de um script para um jogo de pedra, papel e tesoura. Na ocasião o código ficou super simples, feito em um bloco de notas mesmo.

Após o final da reunião, não pude evitar de ficar pensando em finalizar aquele código e, como estava aprendendo Java, senti que seria uma boa oportunidade para estudar a linguagem.

## ⚙ Como funciona

Partindo da base da lógica definida no bloco de notas, sabia que deveria utilizar algo como um enum para trabalhar as opções de jogada possíveis.

<img src=https://user-images.githubusercontent.com/79609859/229812184-6ba5b389-eac2-4f7f-bbd6-10acf225b9e3.jpeg width="243.5" height="271">

O Java trabalha os [enums](https://www.w3schools.com/java/java_enums.asp) de forma que podemos atribuir valores a cada elemento. Em um enum de APIs, por exemplo, podemos ter valores como a url da API e o extrator utilizado para tratar os dados vindos dela.

```
public enum RPS_OPTION {
    ROCK {
        @Override
        public List<RPS_OPTION> GetWeakness() {
           return new ArrayList<>(List.of(PAPER));
        }
    },
    PAPER {
        @Override
        public List<RPS_OPTION> GetWeakness() {
            return new ArrayList<>(List.of(SCISSOR));
        }
    },
    SCISSOR {
        @Override
        public List<RPS_OPTION> GetWeakness() {
            return new ArrayList<>(List.of(ROCK));
        }
    };
    
    public abstract List<RPS_OPTION> GetWeakness();
}
```
Aqui, podemos ver que é fácil a criação de novos elementos para o enum de opções. Adiciona-se um novo elemento e, atualizamos as listas de fraquezas.

Digamos que queremos expandir nosso pedra, papel e tesoura, e agora passaremos a suportar também as opções "lagarto" e "Spock". O novo código ficaria com essa cara:
```
public enum RPS_OPTION {
    ROCK {
        @Override
        public List<RPS_OPTION> GetWeakness() {
           return new ArrayList<>(List.of(PAPER, SPOCK));
        }
    },
    PAPER {
        @Override
        public List<RPS_OPTION> GetWeakness() {
            return new ArrayList<>(List.of(SCISSOR, LIZARD));
        }
    },
    SCISSOR {
        @Override
        public List<RPS_OPTION> GetWeakness() {
            return new ArrayList<>(List.of(ROCK, SPOCK));
        }
    },
    LIZARD {

        @Override
        public List<RPS_OPTION> GetWeakness() {
            return new ArrayList<>(List.of(ROCK, SCISSOR));
        }

    },
    SPOCK{

        @Override
        public List<RPS_OPTION> GetWeakness() {
            return new ArrayList<>(List.of(PAPER, LIZARD));
        }

    };
    
    public abstract List<RPS_OPTION> GetWeakness();
}
```
No nosso jogo, podemos trabalhar com o enum de opções e comparar a lista de fraquezas para identificar os resultados de uma rodada:
```
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
```
Podemos notar aqui os benefícios de se trabalhar com os enums do java. Um jogo clássico como o pedra, papel e tesoura tem diversas versões por ai e, é comum encontrarmos versões expandidas. Lembro-me de amigos que gostavam de tentar adicionar tudo ao jogo, em uma versão certamente não balanceada do jogo original.

Em nosso projeto, mesmo que adicionemos n novas possíveis opções de jogadas, como "arma", "buraco negro", "Deus" e muitos outros, o código de checar resultados permanecerá o mesmo.
