USE ProjetoBiltiful;

SET LANGUAGE 'Brazilian'

CREATE TABLE Cliente 
(

CPF 									NVARCHAR (14) NOT NULL,
Nome 									NVARCHAR (50) NOT NULL,
DataNasc 								DATE NOT NULL,
Sexo									CHAR NOT NULL,
Ultima_Compra 							DATE NULL						DEFAULT CONVERT(DATE, GETDATE()),
Data_Cadastro 							DATE NOT NULL					DEFAULT CONVERT(DATE, GETDATE()),
Situacao 								CHAR NOT NULL					DEFAULT 'A',
Risco									BIT NOT NULL					DEFAULT 0,

CONSTRAINT PK_Cliente					PRIMARY KEY (CPF)

);

CREATE TABLE Fornecedor
(

CNPJ 									NVARCHAR(18) NOT NULL,
Razao_Social 							NVARCHAR(50) NOT NULL,
Data_Abertura							DATE NOT NULL, 
Ultima_Compra 							DATE NULL						DEFAULT CONVERT(DATE, GETDATE()),
Data_Cadastro 							DATE NOT NULL					DEFAULT CONVERT(DATE, GETDATE()),
Situacao 								CHAR NOT NULL					DEFAULT 'A',
Bloqueado								BIT NOT NULL					DEFAULT 0,

CONSTRAINT 	PK_Fornecedor				PRIMARY KEY (CNPJ)

);

CREATE TABLE Materia_Prima
(

ID 										NVARCHAR(6)		NOT NULL,
Nome									NVARCHAR(30)	NOT NULL,
Ultima_Compra							DATE NULL						DEFAULT CONVERT(DATE, GETDATE()),
Data_Cadastro							DATE NOT NULL					DEFAULT	CONVERT(DATE, GETDATE()),
Situacao								CHAR NOT NULL					DEFAULT 'A',	

CONSTRAINT PK_Materia_Prima				PRIMARY KEY(ID),
CONSTRAINT UN_Materia_Prima				UNIQUE (ID, Nome)

);

CREATE TABLE Produto 
(

Codigo_Barras							NVARCHAR(13) NOT NULL,
Nome									NVARCHAR(30) NOT NULL,
Valor_Venda								DECIMAL (10,2) NOT NULL,
Ultima_Venda							DATE NULL						DEFAULT CONVERT(DATE, GETDATE()),
Data_Cadastro							DATE NOT NULL					DEFAULT CONVERT(DATE, GETDATE()),
Situacao								CHAR NOT NULL					DEFAULT 'A',

CONSTRAINT PK_Produto					PRIMARY KEY(Codigo_Barras),
CONSTRAINT UN_Produto					UNIQUE (Codigo_Barras, Nome)

);

CREATE TABLE Venda 
(

	Id 									INT IDENTITY(1, 1) NOT NULL,
	CPF_Cliente							NVARCHAR(14) NOT NULL,
	Data_Venda 							DATE NOT NULL					DEFAULT CONVERT(DATE, GETDATE()),
	Valor_Total							DECIMAL(7,2) NULL				DEFAULT 0.00,
	
	CONSTRAINT PK_Venda					PRIMARY KEY (Id),
	CONSTRAINT FK_Cliente				FOREIGN KEY (CPF_Cliente)		REFERENCES Cliente (CPF)
	

);

CREATE TABLE Item_Venda 
(

	Id 									INT NOT NULL,
	Produto 							NVARCHAR(13) NOT NULL,
	Quantidade 							FLOAT NOT NULL					DEFAULT 0.00,
	Valor_Unitario						DECIMAL(5,2) NOT NULL			DEFAULT 0.00,
	Total_Item 							DECIMAL(7,2) NOT NULL			DEFAULT 0.00,

	CONSTRAINT PK_ItemVenda				PRIMARY KEY (Id, produto),
	CONSTRAINT FK_Produto				FOREIGN KEY (Produto)			REFERENCES Produto(Codigo_Barras),
	CONSTRAINT FK_Venda					FOREIGN KEY (Id)				REFERENCES Venda(Id)

);

CREATE TABLE Producao
(

	ID 									INT IDENTITY NOT NULL,
	Data_Producao 						DATE NOT NULL					DEFAULT CONVERT(DATE, GETDATE()),
	Produto	 							NVARCHAR (13) NOT NULL,
	Quantidade 							DECIMAL (5,2) NOT NULL,
	
	CONSTRAINT PK_Producao				PRIMARY KEY (ID),
	CONSTRAINT FK_Producao_Produto		FOREIGN KEY(Produto)			REFERENCES Produto(Codigo_Barras)	

);

CREATE TABLE Item_Producao
(

	ID 									INT NOT NULL,
	Data_Producao 						DATE NOT NULL					DEFAULT CONVERT(DATE, GETDATE()),
	Id_Materia_Prima 					NVARCHAR (6) NOT NULL,
	Quantidade_Materia_Prima 			DECIMAL (5,2) NOT NULL,

	CONSTRAINT PK_ItemProducao			PRIMARY KEY (ID),
	CONSTRAINT FK_Producao				FOREIGN KEY (ID)				REFERENCES Producao(ID),
	CONSTRAINT FK_Materia_Prima			FOREIGN KEY (Id_Materia_Prima)	REFERENCES  Materia_Prima(ID) 

);

CREATE TABLE Compra 
(

	ID 									INT IDENTITY(1,1)NOT NULL,
	Data_Compra 						DATE NOT NULL					DEFAULT CONVERT(DATE, GETDATE()),
	Fornecedor 							NVARCHAR(18) NOT NULL,
	Valor_Total 						DECIMAL(7,2) NULL				DEFAULT 0.00,
	
	CONSTRAINT PK_Compra				PRIMARY KEY(Id),
	CONSTRAINT FK_Fornecedor			FOREIGN KEY(Fornecedor)			REFERENCES Fornecedor(CNPJ)

);

CREATE TABLE Item_Compra
(

	ID 									INT NOT NULL,
	Data_Compra 	 					DATE NOT NULL					DEFAULT CONVERT(DATE, GETDATE()),
	Codigo_MPrima 						NVARCHAR(6) NOT NULL,
	Quantidade 							INT NULL DEFAULT 0,
	Valor_Unitario 						DECIMAL(5,2) NOT NULL			DEFAULT 0.00,
	Total_Item  						DECIMAL(7,2) NULL				DEFAULT 0.00,
	
	CONSTRAINT PK_ItemCompra			PRIMARY KEY (ID),
	CONSTRAINT FK_Compra				FOREIGN KEY (ID)				REFERENCES Compra(ID),
	CONSTRAINT FK_Compra_Materia_Prima	FOREIGN KEY (Codigo_MPrima)		REFERENCES Materia_Prima(ID)

);


GO
CREATE PROCEDURE AtualizaTotalVenda
@ID INT
AS 
BEGIN

	DECLARE @TOTAL_ITEM DECIMAL (7,2)
	SELECT @TOTAL_ITEM = Total_Item, @ID = ID FROM Item_Venda WHERE Id = @ID

	UPDATE Venda SET Valor_Total = Valor_Total + @TOTAL_ITEM WHERE Id = @ID

END
GO


GO
CREATE TRIGGER AtualizaTotalItemVenda
ON Item_Venda
AFTER INSERT
AS
BEGIN 

	DECLARE @ID INT, @QUANTIDADE INT, @VALOR_UNITARIO DECIMAL(5,2)
	SELECT @ID = ID, @QUANTIDADE = Quantidade, @VALOR_UNITARIO = Valor_Unitario FROM INSERTED

	UPDATE Item_Venda SET Total_Item = @QUANTIDADE * @VALOR_UNITARIO WHERE ID = @ID
	EXECUTE AtualizaTotalVenda @ID

END
GO


GO
CREATE PROCEDURE AtualizaTotalCompra
@ID INT
AS 
BEGIN

	DECLARE @TOTAL_ITEM DECIMAL (7,2)
	SELECT @TOTAL_ITEM = Total_Item, @ID = ID FROM Item_Compra WHERE ID = @ID

	UPDATE Compra SET Valor_Total = Valor_Total + @TOTAL_ITEM WHERE ID = @ID

END

GO


GO
CREATE TRIGGER AtualizaTotalItemCompra
ON Item_Compra
AFTER INSERT
AS
BEGIN 

	DECLARE @ID INT, @QUANTIDADE INT, @VALOR_UNITARIO DECIMAL(5,2)
	SELECT @ID = ID, @QUANTIDADE = Quantidade, @VALOR_UNITARIO = Valor_Unitario FROM INSERTED

	UPDATE Item_Compra SET Total_Item = @QUANTIDADE * @VALOR_UNITARIO WHERE ID = @ID
	EXECUTE AtualizaTotalCompra @ID

END
GO


GO
CREATE TRIGGER UltimaCompraFornecedor
ON Compra
AFTER INSERT
AS
BEGIN

	DECLARE @FORNECEDOR NVARCHAR (18)

	SELECT @FORNECEDOR = Fornecedor FROM INSERTED

	UPDATE Fornecedor SET Ultima_Compra = CONVERT(DATE, GETDATE()) WHERE CNPJ = @FORNECEDOR

END
GO


GO
CREATE TRIGGER UltimaCompraMPrima 
ON Item_Compra
AFTER INSERT
AS
BEGIN

	DECLARE @CODIGO_MPRIMA NVARCHAR (6)

	SELECT @CODIGO_MPRIMA = Codigo_MPrima FROM INSERTED

	UPDATE Materia_Prima SET Ultima_Compra = CONVERT(DATE, GETDATE()) WHERE ID = @CODIGO_MPRIMA

END
GO


GO
CREATE TRIGGER UltimaCompraCliente
ON Venda
AFTER INSERT
AS
BEGIN

	DECLARE @CPF_CLIENTE NVARCHAR (14)

	SELECT @CPF_CLIENTE = CPF_Cliente FROM INSERTED

	UPDATE Cliente SET Ultima_Compra = CONVERT(DATE, GETDATE()) WHERE CPF = @CPF_CLIENTE

END
GO


GO
CREATE TRIGGER UltimaVendaProduto 
ON Item_Venda
AFTER INSERT
AS
BEGIN

	DECLARE @PRODUTO NVARCHAR (13)

	SELECT @PRODUTO = Produto FROM INSERTED

	UPDATE Produto SET Ultima_Venda = CONVERT(DATE, GETDATE()) WHERE Codigo_Barras = @PRODUTO

END
GO



select * FROM Cliente        
select * FROM Fornecedor
select * FROM Materia_Prima
select * FROM Produto
select * FROM Venda
select * FROM Item_Venda
select * FROM Producao
select * FROM Item_Producao
select * FROM Compra
select * FROM Item_Compra


drop table Item_Compra, Compra, Item_Producao, Producao, Item_Venda, Venda, Produto, Materia_Prima, Fornecedor, Cliente


--DELETE FROM Item_Compra
--DELETE FROM Compra
--DELETE FROM Item_Producao
--DELETE FROM Producao
--DELETE FROM Item_Venda
--DELETE FROM Venda
--DELETE FROM Produto
--DELETE FROM Materia_Prima
--DELETE FROM Fornecedor
--DELETE FROM Cliente  



drop table Item_Producao, Producao