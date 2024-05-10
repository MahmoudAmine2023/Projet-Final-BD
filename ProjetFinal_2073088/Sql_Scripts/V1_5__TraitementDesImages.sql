
use ProjetFinal_Maillots

ALTER TABLE Maillots.Maillot
ADD Identifiant uniqueidentifier NOT NULL ROWGUIDCOL DEFAULT newid() 
GO
ALTER TABLE Maillots.Maillot 
ADD CONSTRAINT DF_Image_Identifiant UNIQUE (Identifiant)
GO
ALTER TABLE Maillots.Maillot ADD
Photo varbinary(max) FILESTREAM NULL;
GO
