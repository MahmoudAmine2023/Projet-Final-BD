
CREATE NONCLUSTERED INDEX IX_Maillots_NomEquipe
ON Maillots.Maillot(NomEquipe);


CREATE NONCLUSTERED INDEX IX_Maillots_Annee
ON Maillots.Maillot(Annee);

-- Explication : Dans mon projet, les utilisateurs cherchent souvent par nom d'équipe (NomEquipe) et par année (Annee). 
-- Pour rendre ces recherches plus rapides, il serait utile de créer des index sur ces deux critères. 
-- Un index sur NomEquipe aidera à trouver les équipes plus rapidement, tandis qu'un index 
-- sur Annee accélérera la recherche des maillots par année.
