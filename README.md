# PROJETO BILTIFUL

Para iniciar o executavel a primeira coisa que deve ser feita eh definir o projeto como inicializacao. Apos esta definicao executar com F5 ou ctrl F5.

Ao iniciar a execucao do mesmo ira se deparar com um menu inicial com as seguintes opcoes:

* OBS: Para que o programa funcione corretamente voce tera que seguir a seguinte ordem voce deve cadastrar um cliente, um fornecedor, um produto e uma materia prima ai conseguira efetuar producao, vendas e compras.

### MENU
| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 1.  |    Cadastro    | Tera as funcoes de cadastro e navegacoes de Clientes, Fornecedores, Produtos e Materias-Primas.|
| 2.  |    Producao    | Tera as funcoes de Producao para Cadastrar uma producao, Localizar um registro e Imprimir um registro.|
| 3.  |    Compras    | Tera as funcoes de compras para fazer Nova Compra, Consultar Compra e Imprimir registro de compra.|
| 4.  |    Vendas    | Tera as Funcoes de vebdas para Nova venda, Consultar venda e Imprimir registros de venda.|
| 0.  |    Sair    | O numero zero foi escolhido para ser retorno de menu e tambem finalizacao do programa caso esteja no menu principal|




# Cadastro Cliente/Fornecedor
---------------------------------------------
### Cliente
| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 1.  |    Cadastrar Cliente    | Devera inserir as seguintes informacoes: CPF, Nome do cliente a ser cadastrado, Data de nascimento(Tera que ser maior de 18 anos, caso contrario o cadastro nao sera efetuado), Genero,Situacao [A - ativo / I - Inativo].|
| 2.  |    Listar Clientes    | Um menu de navegacao entre os clientes ja cadastrados.|
| 3.  |    Editar Registro de Cliente    | Somente e possivel editar as seguintes informacoes de um cliente: Nome, Data de nascimento e Situacao.|
| 4.  |    Bloquear/Desbloquear Cliente   | Para bloquear um cliente devera informar o cpf do mesmo, caso o mesmo ja esteja bloqueado uma mensagem sera exibida e tera que selecionar dentre as opcoes de desbloquear ou manter bloqueado.|
| 5.  |    Localizar Cliente    | Para localizar um cliente especifico pelo seu cpf.|
| 6.  |    Localizar cliente Bloquado    | Para localizar um cliente que esteja bloqueado pelo seu cpf.|

### Fornecedor
| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 7.  |    Cadastrar Fornecedor    | Devera inserir as seguintes informacoes: CNPJ, Razao social, Data de criacao da empresa (Tera que ser maior que 6 meses, caso ao contrario nao sera efetuado o cadastro),Situacao [A - ativo / I - Inativo].|
| 8.  |    Listar Fornecedor    | Um menu de navegacao entre os fornecedores ja cadastrados.|
| 9.  |    Editar Registro de Fornecedor    | Somente e possivel editar as seguintes informacoes de um fornecedor: Razao social e Situacao.|
| 10.  |    Bloquear/Desbloquear Fornecedor   | Para bloquear um fornecedor devera informar o cnpj do mesmo, caso o mesmo ja esteja bloqueado uma mensagem sera exibida e tera que selecionar dentre as opcoes de desbloquear ou manter bloqueado.|
| 11.  |    Localizar Fornecedor    | Para localizar um fornecedor especifico pelo seu cnpj.|
| 12.  |    Localizar Fornecedor Bloquado    | Para localizar um fornecedor que esteja bloqueado pelo seu cnpj.|
| 0.  |    Retornar ao menu anterior    | Retorna menu.|

# Produtos

| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 1.  |    Cadastrar Produto    | Para cadastrar um produto devera ser inserido um codigo de barras, nome para o mesmo, valor por qual o mesmo sera comercializado e a situacao se o produto esta ativo ou inativo.|
| 2.  |    Localizar Produto | Localiza um produto pelo codigo de barras.|
| 3.  |    Imprimir Produtos    | Imprime todos os produtos ja inserido na lista.|
| 4.  |    Alterar situacao de produto   | A unica informacao que podera ser alterada do produto sera a situacao.|
| 0.  |    Retornar ao menu anterior    | Retorna menu.|


# Materia-Prima

| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 1.  |    Cadastrar Materia-Prima  | Para cadastrar uma materia prima devera ser inserido o nome e a situacao.|
| 2.  |    Localizar Materia-Prima | Para localizar a mataria prima devera ser inserido o codigo que foi gerado para a mesma.|
| 3.  |    Imprimir Materia-Prima    | Imprime todas as materias primas ja inserido na lista.|
| 4.  |    Alterar situacao de Materia-Prima   | A unica informacao que podera ser alterada da materia prima sera a situacao.|
| 0.  |    Retornar ao menu anterior    | Retorna menu.|



-----------------------
# Compras
-----------------------

# Materia-Prima

| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 1.  |    Nova Compra  | Para efetuar uma nova compra de materia prima devera informar o cnpj do fornecedor para consultar no sistema se e existente, caso nao seja o tera que se registrar antes de nos fornecer a materia prima.|
| 2.  |    Consultar Compra | Consulta a compra pelo id da mesma  .|
| 3.  |    Imprimir registros de compra    | Imprime todas a materias primas que foram compradas.|
| 0.  |    Retornar ao menu anterior    | Retorna menu.|
 
 -----------------------
# Vendas
-----------------------

# Materia-Prima

| __Opcoes__ | __Menu Principal__ | __Descricao__ |
|-----|---------------------------|---------------|
| 1.  |    Nova Compra  | Para efetuar uma nova compra de materia prima devera informar o cnpj do fornecedor para consultar no sistema se e existente, caso nao seja o tera que se registrar antes de nos fornecer a materia prima.|
| 2.  |    Consultar Compra | Consulta a compra pelo id da mesma  .|
| 3.  |    Imprimir registros de compra    | Imprime todas a materias primas que foram compradas.|
| 0.  |    Retornar ao menu anterior    | Retorna menu.|
 
