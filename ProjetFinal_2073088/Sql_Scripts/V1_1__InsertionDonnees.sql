INSERT INTO Promotions.Promotion (Nom, PourcentageReduction, DateDebut, DateFin)
VALUES
    ('Promo1', 10.00, '2024-03-01', '2024-03-15'),
    ('Promo2', 15.00, '2024-03-05', '2024-03-20'),
    ('Promo3', 20.00, '2024-03-10', '2024-03-25');

INSERT INTO Maillots.Maillot (NomEquipe, Taille, Couleur, Annee, Prix, EstPremium, PromotionID)
VALUES 
    ('Real Madrid', 'S', 'Blanc', 2022, 89.99, 'Non', 1),
    ('Real Madrid', 'S', 'Blanc', 2022, 199.99, 'Oui', 2),
    ('Paris Saint-Germain', 'M', 'Bleu', 2023, 89.99, 'Non', 3),
    ('Paris Saint-Germain', 'M', 'Bleu', 2023, 179.99, 'Oui', 1),
    ('FC Barcelona', 'M', 'Bleu et Grenat', 2023, 79.99, 'Non', 2),
    ('FC Barcelona', 'M', 'Bleu et Grenat', 2023, 249.99, 'Oui', 3),
    ('Manchester United', 'L', 'Rouge', 2022, 94.99, 'Non', 1),
    ('Bayern Munich', 'XL', 'Rouge', 2023, 99.99, 'Non', 2),
    ('Liverpool FC', 'S', 'Rouge', 2022, 84.99, 'Non', NULL),
    ('Juventus', 'L', 'Noir et Blanc', 2022, 92.99, 'Non', 3),
    ('Manchester City', 'XL', 'Ciel et Bleu marine', 2023, 95.99, 'Non', NULL),
    ('Real Madrid', 'S', 'Blanc', 2022, 87.99, 'Non', NULL),
    ('AC Milan', 'M', 'Rouge et Noir', 2023, 81.99, 'Non', 1),
    ('Chelsea FC', 'L', 'Bleu', 2022, 93.99, 'Non', 2),
    ('Borussia Dortmund', 'XL', 'Jaune et Noir', 2023, 96.99, 'Non', 3),
    ('Arsenal FC', 'S', 'Rouge', 2022, 88.99, 'Non', NULL),
    ('Atletico Madrid', 'M', 'Rouge et Blanc', 2023, 83.99, 'Non', 1),
    ('Tottenham Hotspur', 'L', 'Blanc', 2022, 91.99, 'Non', 2),
    ('Inter Milan', 'XL', 'Noir et Bleu', 2023, 97.99, 'Non', 3),
    ('AS Roma', 'S', 'Bordeaux et Or', 2022, 86.99, 'Non', NULL),
    ('Napoli', 'M', 'Bleu et Blanc', 2023, 82.99, 'Non', 1),
    ('Ajax Amsterdam', 'L', 'Rouge et Blanc', 2022, 90.99, 'Non', 2),
    ('Olympique de Marseille', 'XL', 'Bleu et Blanc', 2023, 98.99, 'Non', 3),
    ('Leicester City', 'S', 'Bleu', 2022, 85.99, 'Non', NULL)


    -- Ajoutez d'autres lignes selon vos besoins ...

-- Insertion de données dans la table Maillots.MaillotPremium
INSERT INTO Maillots.MaillotPremium (Nom, Numero, Flocage,MaillotID)
VALUES 
    ('Real Madrid ', 10, 'Zinedine Zidane',3),
    ('FC Barcelona', 10, 'Lionel Messi',6),
    ('Paris Saint-Germain', 10, 'Kylian Mbappe',4),
	('Manchester United' , 7 , 'Cristiano Ronaldo', 7)
    -- Ajoutez d'autres lignes selon vos besoins ...

-- Insertion de données dans la table Fournisseurs.Fournisseur
INSERT INTO Fournisseurs.Fournisseur (Nom, Adresse, Telephone)
VALUES 
    ('Fournisseur1', 'Adresse1', '1234567890'),
    ('Fournisseur2', 'Adresse2', '9876543210')
    -- Ajoutez d'autres lignes selon vos besoins ...

-- Insertion de données dans la table Promotions.Promotion


    -- Ajoutez d'autres lignes selon vos besoins ...

-- Insertion de données dans la table Clients.Client
INSERT INTO Clients.Client (Nom, Adresse, CourrielEncrypt, Telephone)
VALUES 
    ('Client1', 'Adresse1', CONVERT(VARBINARY(MAX), 'client1@email.com'), '1234567890'),
    ('Client2', 'Adresse2', CONVERT(VARBINARY(MAX), 'client2@email.com'), '9876543210');
    -- Ajoutez d'autres lignes selon vos besoins ...

INSERT INTO Clients.Courriel(ClientID,Courriel)
SELECT ClientID,CONVERT(VARCHAR(50),CourrielEncrypt) AS Courriel
FROM Clients.Client
-- Insertion de données dans la table Achats.Achat
INSERT INTO Achats.Achat ( DateAchat, PrixTotal,ClientID,PromotionID)
VALUES 
    ( '2024-03-01', 49.99,1,1),
    ( '2024-03-05', 39.99,2,1), 
	('2024-02-15',360.00,1,2)

INSERT INTO Achats.ArticleCommande(MaillotID , Quantite , AchatID , DateAchat  , Prix)
VALUES 
	(2 , 2 , 1 , '2024-03-15' , 89.99 ),
	(4, 2 , 2 , '2024-02-15' , NULL )
    -- Ajoutez d'autres lignes selon vos besoins ...
	

-- Insertion de données dans la table ConcurentsPrincipaux.Concurents
INSERT INTO Concurrents.Concurrent (Nom, Adresse, Telephone, MaillotID, PrixVendu)
VALUES 
    ('Concurrent1', 'Adresse1', '1111111111', 6, 54.99),
    ('Concurrent2', 'Adresse2', '2222222222', 5, 44.99)
    -- Ajoutez d'autres lignes selon vos besoins ...


