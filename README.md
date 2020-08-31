# Catherine_Movie
- Para gerar o banco de dados basta ir no arquivo "Global.asax" e tirar a linha DAO.StartDB.CreateTables(false); do comentário. Após a primeira compilação ele vai gerar todas as tabelas com dados padrões. OBS: Para a segunda compilação é necessário voltar a comentar a linha.

- o login padrão é "paulomarcelodal@icloud.com", e a senha "123".

- Criei um formulário de login muito simples, apenas para continuar o teste. Como não era requisito, e pedia para não usar nenhum modelo pronto, fiz algo muito simples, aonde o usuário ao logar, se senha e usuário forem válidos, ele guarda o nome em cookie, enquanto este cookie estiver "vivo", o usuário esta logado. Normalmente usaria a classe do próprio MVC que é bem completa.

- Não conhecia o Dapper, e ao utilizar achei muito interessante, rápido e pode-se utilizar syntaxe SQL =), mas o deixei por último para aproveitar melhor meu tempo desenvolvendo, utilizei ele apenas na classe "DAO.Query.GetLocations()".

- A Connection String esta em "DAO.Models.ConnectionString". Gosto de utilizar uma classe para ela, pois lhe da a alternativa de alternar entre conexões.

- Não me preocupei muito com a estética do site, até porque esse não é meu forte. Usei o básico do bootstrap e css.

- Procurei criar formas fáceis de cadastrar os dados, usando ajax e não o costumeiro Get/Post de páginas.

- Nunca utilizei testes unitários de forma profissional, e como meu tempo era muito curto, deixei por último, porém não deu tempo de estudar e fazer algo legal como eu queria.

- Eu sei que 7 dias são necessários para fazer algo bem bacana, mas meu tempo real era apenas no domingo, e já faziam quase 3 anos que não trabalhava mais com Web, hoj em dia trabalho mais com WPF, UWP e Xamarin, espero que tenha feito algo que lhes possa avaliar o meu conhecimneto. Na persistência de filmes alugados em Locação, minh aintenção era criar um objeto em JavaScript da tabela e enviar um json para o C# tipar uma classe, porém ontem quando eu estava fazendo isso começou a dar um erro de parse e para não deixar de entregar hoje, fiz uma gambiarra para poder prosseguir, não seria o ideal a se fazer em projeto em produção.

- Iria criar uma camada apenas para Regras de Negócio, como costumo fazer, porém o tempo era curto e como o requisito era usar o Entity com Code First, aproveite e usei as Data Annotations.

- Temos duas páginas principais ao entrar no sistema "Locações" e "Filmes", filmes tem cadastro, alteração e exclusão de filmes e gêneros. Na página locação temos a inclusão de locação e seus filmes.
