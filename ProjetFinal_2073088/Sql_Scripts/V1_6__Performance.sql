
CREATE NONCLUSTERED INDEX IX_Maillots_NomEquipe
ON Maillots.Maillot(NomEquipe);


CREATE NONCLUSTERED INDEX IX_Maillots_Annee
ON Maillots.Maillot(Annee);

-- Explication : Dans mon projet, les utilisateurs cherchent souvent par nom d'�quipe (NomEquipe) et par ann�e (Annee). 
-- Pour rendre ces recherches plus rapides, il serait utile de cr�er des index sur ces deux crit�res. 
-- Un index sur NomEquipe aidera � trouver les �quipes plus rapidement, tandis qu'un index 
-- sur Annee acc�l�rera la recherche des maillots par ann�e.
