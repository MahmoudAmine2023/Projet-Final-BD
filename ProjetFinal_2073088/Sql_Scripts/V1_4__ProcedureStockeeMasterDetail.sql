USE ProjetFinal_Maillots
GO

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
		M.PromotionID
    FROM
        Maillots.Maillot AS M
    LEFT JOIN
        Maillots.MaillotPremium AS MP ON M.MaillotID = MP.MaillotID
    WHERE
        M.NomEquipe = @NomEquipe;
END;

-- TESTING 
-- EXEC Maillots.usp_MaillotsDisponiblePourUneCertaineEquipe @NomEquipe = 'Real Madrid' ; 