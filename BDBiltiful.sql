SET LANGUAGE 'BRAZILIAN'

CREATE TABLE Cliente (
	CPF 			NVARCHAR(14) NOT NULL,
	Nome 			NVARCHAR(50) NOT NULL,
	Data_Nasc 		DATE NOT NULL,
	Sexo			CHAR NOT NULL,
	Ultima_Compra 	DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Data_Cadastro 	DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Situacao 		CHAR NOT NULL DEFAULT 'A',

	CONSTRAINT PK_Cliente PRIMARY KEY (CPF)
);

CREATE TABLE Fornecedor (
	CNPJ 			NVARCHAR(18) NOT NULL,
	Razao_Social 	NVARCHAR(50) NOT NULL,
	Data_Abertura	DATE NOT NULL, 
	Ultima_Compra 	DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Data_Cadastro 	DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Situacao 		CHAR NOT NULL DEFAULT 'A',

	CONSTRAINT 	PK_Fornecedor PRIMARY KEY (CNPJ)
);

CREATE TABLE MateriaPrima (
	Codigo 			NVARCHAR(6) NOT NULL,
	Nome			NVARCHAR(20) NOT NULL,
	Ultima_Compra	DATE DEFAULT CONVERT(DATE, GETDATE()),
	Data_Cadastro	DATE DEFAULT CONVERT(DATE, GETDATE()),
	Situacao		CHAR NOT NULL DEFAULT 'A',

	CONSTRAINT PK_MateriaPrima PRIMARY KEY(Codigo)
);

CREATE TABLE Produto (
	Codigo_Barras	NVARCHAR(13) NOT NULL,
	Nome			NVARCHAR(20) NOT NULL,
	Valor_Venda		DECIMAL(10,2) NOT NULL,
	Ultima_Venda	DATE DEFAULT CONVERT(DATE, GETDATE()),
	Data_Cadastro	DATE DEFAULT CONVERT(DATE, GETDATE()),
	Situacao		CHAR NOT NULL DEFAULT 'A',

	CONSTRAINT PK_Produto PRIMARY KEY(Codigo_Barras),
	CONSTRAINT UN_Produto UNIQUE (Nome)
);



CREATE TABLE Compra (
	ID 				INT NOT NULL,
	CNPJ_Fornecedor NVARCHAR(18) NOT NULL,
	Data_Compra		DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Valor_Total		DECIMAL(7,2) NOT NULL,
	
	CONSTRAINT PK_Compra PRIMARY KEY(ID),
	CONSTRAINT FK_Fornecedor_Compra FOREIGN KEY(CNPJ_Fornecedor) REFERENCES Fornecedor(CNPJ)
);

CREATE TABLE ItemCompra (
	ID 					INT NOT NULL,
	Codigo_MateriaPrima NVARCHAR(6) NOT NULL,
	Data_Compra	 		DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Quantidade 			INT NOT NULL,
	Valor_Unitario 		DECIMAL(5,2) NOT NULL,
	Total_Item  		DECIMAL(6,2) NULL,
	
	CONSTRAINT PK_ItemCompra PRIMARY KEY (ID),
	CONSTRAINT FK_Compra_ItemCompra FOREIGN KEY (ID) REFERENCES Compra(ID),
	CONSTRAINT FK_MateriaPrima_ItemCompra FOREIGN KEY (Codigo_MateriaPrima) REFERENCES MateriaPrima(Codigo)
);



CREATE TABLE Producao (
	ID 				INT IDENTITY NOT NULL,
	Codigo_Produto	NVARCHAR(13) NOT NULL,
	Data_Producao 	DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Quantidade 		NUMERIC(5,2) NOT NULL,
	
	CONSTRAINT PK_Producao PRIMARY KEY (ID),
	CONSTRAINT FK_Produto_Producao FOREIGN KEY(Codigo_Produto) REFERENCES Produto(Codigo_Barras)	
);

CREATE TABLE ItemProducao (
	ID 				INT NOT NULL,
	Codigo_MateriaPrima NVARCHAR(6) NOT NULL,
	Data_Producao 	DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Quantidade	 	NUMERIC(5,2) NOT NULL,

	CONSTRAINT PK_ItemProducao PRIMARY KEY (ID, Codigo_MateriaPrima),
	CONSTRAINT FK_Producao_ItemProducao FOREIGN KEY (ID) REFERENCES Producao(ID),
	CONSTRAINT FK_MateriaPrima_ItemProducao FOREIGN KEY (Codigo_MateriaPrima) REFERENCES  MateriaPrima(Codigo) 
);



CREATE TABLE Venda (
	ID 			INT IDENTITY NOT NULL,
	CPF_Cliente NVARCHAR(14) NOT NULL,
	Data_Venda 	DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	Valor_Total DECIMAL(7,2) NULL,
	
	CONSTRAINT PK_Venda PRIMARY KEY (ID),
	CONSTRAINT FK_Cliente_Venda FOREIGN KEY (CPF_Cliente) REFERENCES Cliente(CPF),
);

CREATE TABLE ItemVenda (
	ID 				INT NOT NULL,
	Codigo_Produto 	NVARCHAR(13) NOT NULL,
	Quantidade 		NUMERIC(5,2) NOT NULL,
	Valor_Unitario	DECIMAL(5,2) NOT NULL,
	Total_Item 		DECIMAL(7,2) NULL,

	CONSTRAINT PK_ItemVenda PRIMARY KEY (ID, Codigo_Produto),
	CONSTRAINT FK_Venda_ItemVenda FOREIGN KEY (ID) REFERENCES Venda(ID),
	CONSTRAINT FK_Produto_ItemVenda FOREIGN KEY (Codigo_Produto) REFERENCES Produto(Codigo_Barras)
);



CREATE TABLE FornecedorBloqueado(
	CNPJ_Fornecedor NVARCHAR(18) NOT NULL,
	Data_Bloqueio	DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	
	CONSTRAINT PK_FornecedorBloqueado PRIMARY KEY(CNPJ_Fornecedor),
	CONSTRAINT FK_Fornecedor_FornecedorBloqueado FOREIGN KEY(CNPJ_Fornecedor) REFERENCES Fornecedor(CNPJ)
);



CREATE TABLE ClienteRisco (
	CPF_Cliente		NVARCHAR(14) NOT NULL,
	Data_Bloqueio	DATE NOT NULL DEFAULT CONVERT(DATE, GETDATE()),
	
	CONSTRAINT PK_ClienteRisco PRIMARY KEY(CPF_Cliente),
	CONSTRAINT FK_Cliente_ClienteRisco FOREIGN KEY (CPF_Cliente) REFERENCES Cliente(CPF),
);