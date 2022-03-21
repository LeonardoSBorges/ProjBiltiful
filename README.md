# PROJETO BILTIFUL

Para iniciar o executável a primeira coisa que deve ser feita é definir o projeto como inicialização. Após está definição executar com F5 ou Ctrl + F5.

Ao iniciar a execução o mesmo irá se deparar com um menu inicial com as seguintes opcoes:

* OBS: Para que o programa funcione corretamente você terá que seguir a seguinte ordem, você deve cadastrar um cliente, um fornecedor, um produto e uma materia prima, após isso conseguirá efetuar producao, vendas e compras.

### MENU
| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 1.  |    Cadastro    | Terá as funções de cadastro e navegações de Clientes, Fornecedores, Produtos e Materias-Primas.|
| 2.  |    Producao    | Terá as funções de Produção para Cadastrar uma produção, Localizar um registro e Imprimir um registro.|
| 3.  |    Compras    | Terá as funções de compras para fazer Nova Compra, Consultar Compra e Imprimir registro de compra.|
| 4.  |    Vendas    | Terá as funções de vendas para Nova venda, Consultar venda e Imprimir registros de venda.|
| 0.  |    Sair    | O número zero foi escolhido para ser retorno de menu e tambem finalização do programa caso esteja no menu principal|




# Cadastro 
---------------------------------------------
### Cliente
| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 1.  |    Cadastrar Cliente    | Deverá inserir as seguintes informações: CPF, Nome do cliente a ser cadastrado, Data de nascimento(Terá que ser maior de 18 anos, caso contrário o cadastro nao será efetuado), sexo,Situação [A - Ativo / I - Inativo].|
| 2.  |    Listar Clientes    | Um menu de navegação entre os clientes já cadastrados.|
| 3.  |    Editar Registro de Cliente    | Somente e possível editar as seguintes informações de um cliente: Nome, Data de nascimento e Situação.|
| 4.  |    Bloquear/Desbloquear Cliente   | Para bloquear um cliente devera informar o cpf do mesmo, caso o mesmo esteja bloqueado uma mensagem será exibida e terá que selecionar dentre as opções de desbloquear ou manter bloqueado.|
| 5.  |    Localizar Cliente    | Para localizar um cliente especifico pelo seu cpf.|
| 6.  |    Localizar cliente Bloquado    | Para localizar um cliente que esteja bloqueado pelo seu cpf.|

### Fornecedor
| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 7.  |    Cadastrar Fornecedor    | Deverá inserir as seguintes informações: CNPJ, Razão social, Data de criação da empresa (Tera que ser maior que 6 meses, caso ao contrario nao sera efetuado o cadastro),Situação [A - ativo / I - Inativo].|
| 8.  |    Listar Fornecedor    | Um menu de navegação entre os fornecedores ja cadastrados.|
| 9.  |    Editar Registro de Fornecedor    | Somente e possivél editar as seguintes informações de um fornecedor: Razão social e Situação.|
| 10.  |    Bloquear/Desbloquear Fornecedor   | Para bloquear um fornecedor devera informar o cnpj do mesmo, caso o mesmo já esteja bloqueado uma mensagem sera exibida e tera que selecionar dentre as opcoes de desbloquear ou manter bloqueado.|
| 11.  |    Localizar Fornecedor    | Para localizar um fornecedor especifico pelo seu cnpj.|
| 12.  |    Localizar Fornecedor Bloquado    | Para localizar um fornecedor que esteja bloqueado pelo seu cnpj.|
| 0.  |    Retornar ao menu anterior    | Retorna menu.|

# Produtos

| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 1.  |    Cadastrar Produto    | Para cadastrar um produto deverá ser inserido um código de barras, nome para o mesmo, valor por qual o mesmo será comercializado e a situação se o produto está ativo ou inativo.|
| 2.  |    Localizar Produto | Localiza um produto pelo código de barras.|
| 3.  |    Imprimir Produtos    | Imprime todos os produtos já inserido na lista.|
| 4.  |    Alterar situacao de produto   | A única informação que poderá ser alterada do produto será a situação.|
| 0.  |    Retornar ao menu anterior    | Retorna menu.|


# Materia-Prima

| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 1.  |    Cadastrar Materia-Prima  | Para cadastrar uma materia prima deverá ser inserido o nome e a situação.|
| 2.  |    Localizar Materia-Prima | Para localizar a mataria prima deverá ser inserido o código que foi gerado para a mesma.|
| 3.  |    Imprimir Materia-Prima    | Imprime todas as materias primas já inseridas na lista.|
| 4.  |    Alterar situação de Materia-Prima   | A única informação que poderá ser alterada da materia prima será a situação.|
| 0.  |    Retornar ao menu anterior    | Retorna menu.|



-----------------------
# Compras
-----------------------

# Materia-Prima

| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 1.  |    Nova Compra  | Para efetuar uma nova compra de materia prima deverá informar o cnpj do fornecedor para consultar no sistema se é existente, caso não seja o mesmo terá que se registrar antes de nos fornecer a materia prima.|
| 2.  |    Consultar Compra | Consulta a compra pelo id da mesma  .|
| 3.  |    Imprimir registros de compra    | Imprime todas a materias primas que foram compradas.|
| 0.  |    Retornar ao menu anterior    | Retorna menu.|
 
 -----------------------
# Vendas
-----------------------

# Produto

| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 1.  |    Nova Venda  | Para efetuar uma nova venda de produto deve ser informado o cpf do cliente para consultar no sistema se é existente, caso não seja o mesmo terá que se registrar antes de realizar uma venda para o mesmo.|
| 2.  |    Consultar Venda | Consulta a venda pelo id da mesma.|
| 3.  |    Imprimir registros de venda    | Imprime todas as vendas e os itens que foram vendidos. |
| 0.  |    Retornar ao menu anterior    | Retorna menu.|
 
 
 
 
 
 
 
 
 -----------------------
 # Organizacao
 
  -  Tentamos utilizar o trello para melhor gerenciamento de equipe e projeto 

![image](https://user-images.githubusercontent.com/66486368/157447143-1cad7934-b107-486d-bc5e-b15b75df611f.png)
