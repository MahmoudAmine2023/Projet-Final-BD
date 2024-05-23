
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
USE ProjetFinal_Maillots
GO
-- Je modifie la procédure pour rajotuer l'identifiant et la photo car sinon la procédure stocké ne fonctionne plus 
CREATE OR ALTER PROCEDURE Maillots.usp_MaillotsDisponiblePourUneCertaineEquipe
    @NomEquipe NVARCHAR(50)
AS
BEGIN
    SELECT
        M.MaillotID,
        M.NomEquipe,
        M.Taille,
        M.Couleur,
        M.Annee,
        M.EstPremium,
        M.Prix,
        MP.Nom AS NomPremium,
        MP.Numero,
        MP.Flocage,
		M.PromotionID,
		M.Identifiant,
		M.Photo
    FROM
        Maillots.Maillot AS M
    LEFT JOIN
        Maillots.MaillotPremium AS MP ON M.MaillotID = MP.MaillotID
    WHERE
        M.NomEquipe = @NomEquipe;
END;

-- TESTING 
-- EXEC Maillots.usp_MaillotsDisponiblePourUneCertaineEquipe @NomEquipe = 'Real Madrid' ; 
