use ProjetFinal_Maillots

CREATE Maillots.vw_MaillotsAvecPromotion 
AS
SELECT
    M.MaillotID,
    M.NomEquipe,
    M.Taille,
    M.Couleur,
    M.Annee,
    M.EstPremium,
    M.Prix,
    P.Nom AS NomPromotion,
    P.PourcentageReduction,
    P.DateDebut,
    P.DateFin,
	C.PrixVendu AS 'Prix concurrents'
FROM
    Maillots.Maillot AS M
LEFT JOIN
    Promotions.Promotion AS P
	ON M.PromotionID = P.PromotionID
	LEFT JOIN ConcurentsPrincipaux.Concurents C 
	ON M.MaillotID = C.MaillotID

SELECT  * FROM Maillots.vw_MaillotsAvecPromotion