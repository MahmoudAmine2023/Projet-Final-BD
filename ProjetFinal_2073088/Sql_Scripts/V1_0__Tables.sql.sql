 USE master

 USE ProjetFinal_Maillots;
 GO


-- Create the Maillot table in the Maillots schema with identity property
CREATE TABLE Maillots.Maillot (
    MaillotID INT PRIMARY KEY IDENTITY,
    NomEquipe VARCHAR(50) NOT NULL,
    Taille VARCHAR(10),
    Couleur VARCHAR(20),
	Annee INT , 
	EstPremium NVARCHAR(10) NULL,
    Prix DECIMAL(10, 2) NOT NULL,
	PromotionID int NULL
);
GO

-- Create the MaillotPremium table in the Maillots schema with identity property
CREATE TABLE Maillots.MaillotPremium (
	MaillotPremiumID INT IDENTITY,
	Nom NVARCHAR(50),
	Numero INT ,
	Flocage NVARCHAR(50),
	MaillotID INT ,
);
GO

-- Create the Fournisseur table in the Fournisseurs schema with identity property
CREATE TABLE Fournisseurs.Fournisseur (
    FournisseurID INT PRIMARY KEY IDENTITY,
    Nom VARCHAR(50) NOT NULL,
    Adresse VARCHAR(100),
    Telephone VARCHAR(15)
);
GO

-- Create the Promotion table in the Promotions schema with identity 
CREATE TABLE Promotions.Promotion (
    PromotionID INT PRIMARY KEY IDENTITY,
    Nom VARCHAR(50) NOT NULL,
    PourcentageReduction DECIMAL(5, 2) NOT NULL,
	DateDebut DATETIME , 
	DateFin DATETIME
);
GO

-- Create the Client table in the Clients schema with identity property
CREATE TABLE Clients.Client (
    ClientID INT PRIMARY KEY IDENTITY,
    Nom VARCHAR(50) NOT NULL,
    Adresse VARCHAR(100),
    Email VARCHAR(50),
    Telephone VARCHAR(15)
);
GO
CREATE TABLE Achats.ArticleCommande (
	ArticleCommandeID INT PRIMARY KEY IDENTITY,
    MaillotID INT,
    DateAchat DATE,
    Quantite INT,
    Prix DECIMAL(10, 2) NULL,
	AchatID INT,
);
GO

-- Create the Achat table in the Achats schema with identity property
CREATE TABLE Achats.Achat (
    AchatID INT PRIMARY KEY IDENTITY,
    DateAchat DATE,
    PrixTotal DECIMAL(10, 2),
	ClientID INT ,
	PromotionID INT NULL
);
GO

CREATE TABLE Concurrents.Concurrent (
    ConcurentID INT PRIMARY KEY IDENTITY,
    Nom VARCHAR(50) NOT NULL,
    Adresse VARCHAR(100),
    Telephone VARCHAR(15), 
	MaillotID INT NULL , 
	PrixVendu DECIMAL(10,2)
);
GO


-- Add foreign key constraints
ALTER TABLE Maillots.MaillotPremium
ADD CONSTRAINT FK_MaillotPremium_MaillotID
FOREIGN KEY (MaillotID) REFERENCES Maillots.Maillot(MaillotID);


ALTER TABLE Achats.Achat
ADD CONSTRAINT FK_Achats_Achat_PromotionID
FOREIGN KEY (PromotionID) REFERENCES Promotions.Promotion(PromotionID)

ALTER TABLE Achats.ArticleCommande
ADD CONSTRAINT FK_Articles_ArticlesCommande_AchatID
FOREIGN KEY (AchatID) REFERENCES Achats.Achat(AchatID)

ALTER TABLE Maillots.MaillotPremium
ADD CONSTRAINT FK_Maillots_MaillotsPremium_MaillotID
FOREIGN KEY (MaillotID) REFERENCES Maillots.Maillot(MaillotID)

ALTER TABLE Achats.Achat
ADD CONSTRAINT FK_Achats_Achat_Client
FOREIGN KEY (ClientID) REFERENCES Clients.Client(ClientID)

ALTER TABLE Maillots.Maillot
ADD CONSTRAINT FK_Maillots_Promotions
FOREIGN KEY (PromotionID) REFERENCES Promotions.Promotion(PromotionID);

ALTER TABLE Concurrents.Concurrent
ADD CONSTRAINT FK_Concurents_Concurrent_MaillotID
FOREIGN KEY(MaillotID) REFERENCES Maillots.Maillot(MaillotID)
