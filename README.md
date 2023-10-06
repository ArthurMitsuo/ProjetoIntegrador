# ProjetoIntegrador
Repositório para o Projeto Integrador a ser realizado para a matéria de Desenvolvimento de Software Visual


##Descrição das Funcionalidades
1. Cadastro e Login de usuário 
    O Cadastro de usuário, sendo ele a inserção, exclusão e alteração do usuário, fica sob responsabilidade do usuário ADMIN. Este será responsável pela administração e gerenciamento certificando o funcionamento da ferramenta, pelo suporte da mesma e, também, responsável pela comunicação com o banco de dados. 
    Login será realizado com a inserção de nome de usuário e senha, definindo acima dos campos de inserção um menu com as opções de usuário. A categoria de usuário, junto com o usuário e a senha, definirão se é o usuário mesmo a entrar no sistema.
2. Página do usuário
    A página de usuário de nível gerencial, terá os seguintes métodos: 
    * Inserção de suas atividades e projetos. 
    * Controle das atividades e projetos de cada colaborador de sua equipe.
    Neste nível de usuário, o mesmo terá habilitado a verificação de atividades de todos os colaboradores de sua equipe, podendo ter uma visão de toda equipe ou de forma individual, assim como, a verificação de atividades de toda a equipe de forma, diária, semanal ou mensal. 
    A página de nível operacional terá habilitado apenas as ferramentas de edição e inserção, não podendo consultar atividades de outros usuários. O mesmo também terá habilitado as funções de edição dos projetos próprios, podendo categorizar o status deles em 3 etapas, projetos em andamento, parados ou finalizados. 
    Já o usuário de Administração, conta com todos os métodos presentes para o usuário gerencial. Além da possibilidade de criação de usuários e edição dos mesmos, como exclusão das atividades e projetos.
3. Operação de atividades;
    Cada nível terá atribuída a inserção, edição e atualização, no caso da exclusão de atividades/projetos, apenas os níveis Gerencial e Admin. Serão a: 
    * Nível operacional: Poderá realizar os métodos de inserção, edição e atualização em cima de suas próprias atividades, não podendo por exemplo consultar as atividades e projetos de outros usuários ou excluir o seu projeto. 
    * Nível gerencial: Poderá realizar os atributos principais de inserção, edição, atualização de suas atividades, assim como a consulta de atividades dos colaboradores de sua equipe.  Pode excluir apenas as atividades de pessoas do nível operacional.
    * Nível Admin: Responsável por todo o funcionamento da plataforma, tendo um acesso master. Pode inserir, editar, atualizar e excluir qualquer atividade dos colaboradores.
4. Operação de projetos;
    Cada nível terá atribuída a inserção, edição e atualização, no caso da exclusão de atividades/projetos, apenas os níveis Gerencial e Admin. Serão a: 
    * Nível operacional: Poderá realizar os métodos de inserção, edição e atualização em cima de suas próprias atividades, não podendo por exemplo consultar as atividades e projetos de outros usuários ou excluir o seu projeto. 
    * Nível gerencial: Poderá realizar os atributos principais de inserção, edição, atualização de seus projetos à nível gerencial, assim como a consulta de projetos dos colaboradores de sua equipe. Pode excluir apenas as atividades de pessoas do nível operacional.
    * Nível Admin: Responsável por todo o funcionamento da plataforma, tendo um acesso master. Pode inserir, editar, atualizar e excluir qualquer atividade dos colaboradores.
5. Atribuição/alteração de status aos projetos;
    Cada projeto deverá ter um status de três status disponíveis: Pausado, Em Andamento e Finalizado. O usuário operacional pode alterar o status de seu projeto, o gerencial pode alterar o status de alguém de sua equipe e seu próprio. O usuário admin pode alterar o status de qualquer atividade disponível e existente.
6. Organização das lideranças;
    Quem é responsável pela organização das lideranças é o usuário Admin. Ele pode definir o grupo de colaboradores operacionais e determinar um colaborador gerencial à lider do grupo;
7. Atribuição de categorias de usuário;
    Quem é responsável pela definição das categorias de usuário, é o usuário Admin. Ele pode passar alguém do gerencial para o operacional, também passar alguém do operacional para o gerencial e excluir qualquer usuário, com exceção dele mesmo, do sistema.
8. Priorização das atividades individuais;
    Cada usuário pode organizar suas atividades em sua própria página, podendo definir alguma que tenha maior prioridade sob alguma que tenha menor prioridade. Pode também o usuário gerencial definir prioridade em projeto de operacional e, ao Admin, mudar a prioridade e organização das atividades de cada usuário.
9. Comentário em uma atividade;
    Pode o usuário gerencial, deixar um comentário em uma atividade de usuário do nível operacional, que esteja visível para ele, assim como apagar o comentário que realizou. O usuário Admin pode comentar em atividades de quaisquer outros usuários.


## Diagramas
###Diagrama de casos de uso

![Diagrama Casos de Uso](https://github.com/ArthurMitsuo/ProjetoIntegrador/assets/77021980/c7f33ba9-b2cd-46b8-8c96-2fa673a0c584)

###Diagrama de Classes

![Diagrama de classe](https://github.com/ArthurMitsuo/ProjetoIntegrador/assets/77021980/c951e1d9-ba8e-43fa-9222-b1541c5e5797)
