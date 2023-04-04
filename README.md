# btg-coding-challenge
BTG Pactual (IT-PME) - Teste de proficiência em raciocínio lógico e conhecimentos de programação. Realizado por Igor Ponciano em 03/04.

## 🔍Compreendendo o problema

Como de costume, gosto de antes de sair codando, parar e analisar o problema até compreender o que realmente o código deverá realizar.

Neste caso, decidi começar resolvendo o enigma no papel, analisando como era minha interação para resolve-lo, para que então pudesse escrever um código para executar isso.

<img src=https://user-images.githubusercontent.com/79609859/229662019-4d713b76-0fe5-4daf-a4bc-8d702ceed174.jpeg width="431.5" heigth="640">

Então, para otimizar o processo de criar novas tabelas para testar o comportamento, decidi seguir para o [Figma](https://www.figma.com/file/CcIyvAJXoljwGL2XDvLEb1/Puzzle_BrickWall?node-id=0-1&t=ie6ZlleOOWWrPCpO-0), onde é possível esboçar aspectos visuais parametrizados, como o caso das paredes compostas de diferentes comprimentos de tijolos.

![FigmaTest](https://user-images.githubusercontent.com/79609859/229662456-bb6f70d4-470c-437a-9d19-eb08eb7471b1.png)

E foi aqui que compreendi um aspecto importante sobre meu comportamento ao resolver o enigma. Era sempre mais fácil encontrar a resposta procurando pelos gaps/arestas entre os tijolos, visto que caminhos com mais gaps possuem menos obstáculos.

Bastava descobrir como identificar o caminho mais otimizado. Foi aqui que desenvolvi uma tabela com os valores da posição de cada gap e o número de vezes que um gap aparece naquela posição.

<img src=https://user-images.githubusercontent.com/79609859/229662035-ba587598-8b75-4cd6-8a7b-91477398efb1.jpeg width="431.5" height="640">

Para identificar o número de gaps em uma coluna, basta realizar: (número tijolos - 1)

E para chegar no número de cortes, basta realizar a operação: (número de fileiras - número de arestas percorridas pela reta)

## 💻 Transformando isso em código

Uma vez compreendido o problema, passamos à parte de realmente por a mão na massa e codar. Ponderei sobre qual linguagem utilizar, e eventualmente escolhi utilizar C# na Unity, pela possibilidade de testar a expansão das ideias deste exercício, em um protótipo mais completo e interativo no futuro. (é possivel, por exemplo, criar um método DrawWall(), que cria elementos de UI com escala baseada no valor do "tijolo" e os posiciona com seus devidos offsets e colunas, algo similar àqueles exercícios de desenhar com * utilizando loops)

Seguindo o que que entendemos do problema, sabemos que um método deverá ser criado. Ele deve receber um array de arrays de int, representando nossa parede de tijolos e, retornar o número mínimo de tijolos cortados por uma linha vertical.
```
public int GetMinCuts(List<List<int>> wall)

// Aqui confesso que utilizei List ao invés de array por uma preferencia na hora de escrever. É algo para se refatorar
```

A nossa parede de teste, é inicializada com os mesmos valores utilizados no papel
```
public List<List<int>> wall = new List<List<int>>{
        new List<int> {2,2,2},
        new List<int>{3,1,1,1},
        new List<int>{2,4},
        new List<int>{3,1,2},
        new List<int>{1,3,1,1},
        new List<int>{3,3}
    };
```
O script em si é bem simples. Nós criamos nossa tabela de posições dos gaps, utilizando um Dict. e depois iteramos pelas colunas da parede, adicionando as arestas dos tijolos a ele.
```
// Dictionary to store gaps between bricks in a row. [position, numberOfGaps]
        Dictionary<int, int> dictOfGaps = new Dictionary<int, int>(wall.Count);
```
```
foreach (List<int> row in wall)
        {
            int pGap = 0;

            // After each tile, count 1 gap at that position. Don't count for the last, because it would be the right border
            for (int tile = 1; tile < row.Count; tile++)
            {
                // Example: 2 | 2 | 2 -> gaps are in p2 and p4
                pGap += row[tile];

                try
                {
                    dictOfGaps[pGap] += 1;
                }
                catch (KeyNotFoundException ex)
                {
                    dictOfGaps.Add(pGap, 1);
                }
                
            }
        }
```
Utilizamos um try catch aqui, para lidar com o erro gerado ao tentar acessar uma key do dicionário que ainda não existe. Neste caso, indicamos para que o código crie a tal key e adicione o valor base 1 (número de gaps naquela posição) a ela.

O for roda da seguinte forma:

![exemploResultadosFor](https://user-images.githubusercontent.com/79609859/229665948-d73a12dc-f1b8-4fa1-9552-8dee824d0a66.png)

Para encontrar nosso resultado, basta realizar o cálculo: (Número de colunas - Número máximo de gaps em uma posição)
```
// min number of bricks cut = number of rows - max gaps used
        return wall.Count - dictOfGaps.OrderByDescending(p => p.Value).First().Value;
```
Aqui, utilizamos a função "OrderByDescending" do Linq, que possibilita de uma forma mais "amigável", realizar a operação de encontrar o valor mais alto dentre os elementos do nosso dicionário, algo que normalmente poderia ser feito com um loop a mais no código, mas utilizando Linq pode ser feito em 1 linha de código.
