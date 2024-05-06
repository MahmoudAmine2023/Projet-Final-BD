USE ProjetFinal_Maillots
GO


-- Création de la clé
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'PasswordBD2024!'
GO
CREATE CERTIFICATE Final_Certificat WITH SUBJECT = 'ChiffrementAdresseCourrielClient'
GO
CREATE SYMMETRIC KEY ProjetFinal_Cle WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE Final_Certificat
GO

-- AJOUT DU CHAMP CourrielEncrypt 
ALTER TABLE Clients.Client
ADD CourrielEncrypt VARBINARY(MAX);
GO

-- UPDATE ENCRYPTION DE TOUS LES COURRIELS DE CLIENTS
OPEN SYMMETRIC KEY ProjetFinal_Cle DECRYPTION BY CERTIFICATE Final_Certificat;
UPDATE Clients.Client
SET CourrielEncrypt = ENCRYPTBYKEY(KEY_GUID('ProjetFinal_Cle'), Courriel);
CLOSE SYMMETRIC KEY ProjetFinal_Cle;
GO

-- AJOUT DE LA TABLE COURRIEL
CREATE TABLE Clients.Courriel(
Courriel NVARCHAR(100) NULL
);
GO
 -- SUPRÉSSION DE LA COLONNE COURRIEL DANS CLIENT 
ALTER TABLE Clients.Client
DROP COLUMN Courriel;
GO

 --SELECT  * FROM Clients.Client
 --SELECT  * FROM Clients.Courriel

-- SUPPRÉSSION DE LA COLONE COURRIEL NON ENCRYPTÉ


--SELECT  * FROM Clients.Client





-- VERIF DU CERTIFICAT 
-- SELECT *  FROM Sys.certificates

 -- VERIF DE LA CLE 
 -- SELECT * FROM sys.symmetric_keys
 /*
CREATE PROCEDURE Clients.USP_ChiffrementAdresseCourrielClient
    @ClientID INT
AS
BEGIN
    DECLARE @EncryptedEmail VARBINARY(MAX)
    DECLARE @DecryptedEmail NVARCHAR(100)

    -- Sélectionner l'adresse e-mail à chiffrer du client
    SELECT @DecryptedEmail = Courriel
    FROM Clients.Client
    WHERE ClientID = @ClientID;

    -- Chiffrer l'adresse e-mail
    OPEN SYMMETRIC KEY ProjetFinal_Cle
    DECRYPTION BY CERTIFICATE Final_Certificat;

    SELECT @EncryptedEmail = ENCRYPTBYKEY(KEY_GUID('ProjetFinal_Cle'), @DecryptedEmail);

    CLOSE SYMMETRIC KEY ProjetFinal_Cle;

    -- Mettre à jour le champ CourrielEncrypt avec l'adresse e-mail chiffrée
    UPDATE Clients.Client
    SET CourrielEncrypt = @EncryptedEmail
    WHERE ClientID = @ClientID;
END
GO*/

CREATE PROCEDURE Clients.USP_DeChiffrementAdresseCourrielClient
    @ClientID INT
AS
BEGIN
    DECLARE @EncryptedEmail VARBINARY(MAX)
    DECLARE @DecryptedEmail NVARCHAR(100)

    -- Sélectionner l'adresse e-mail chiffrée du client
    SELECT @EncryptedEmail = CourrielEncrypt
    FROM Clients.Client
    WHERE ClientID = @ClientID;
	
    -- Déchiffrer l'adresse e-mail
    OPEN SYMMETRIC KEY ProjetFinal_Cle
    DECRYPTION BY CERTIFICATE Final_Certificat;

    SELECT @DecryptedEmail = CAST(DecryptByKey(CourrielEncrypt) AS NVARCHAR(100))
    FROM Clients.Client
    WHERE ClientID = @ClientID;

    CLOSE SYMMETRIC KEY ProjetFinal_Cle;

    -- Renvoyer l'adresse e-mail déchiffrée
    SELECT @DecryptedEmail AS DecryptedEmail;
END
GO

-- Procédure à utiliser lors de la création d'un client 
CREATE PROCEDURE Clients.USP_CreationClientAvecChiffrementCourriel
    @Nom NVARCHAR(100),
    @Courriel NVARCHAR(100),
	@Adresse VARCHAR(100),
	@Telephone VARCHAR(15)
AS
BEGIN
    DECLARE @EncryptedEmail VARBINARY(MAX)

    -- Chiffrer l'adresse e-mail
    OPEN SYMMETRIC KEY ProjetFinal_Cle
    DECRYPTION BY CERTIFICATE Final_Certificat;

    SET @EncryptedEmail = ENCRYPTBYKEY(KEY_GUID('ProjetFinal_Cle'), @Courriel);

    CLOSE SYMMETRIC KEY ProjetFinal_Cle;

    -- Insérer le nouveau client avec l'adresse e-mail chiffrée
    INSERT INTO Clients.Client (Nom, CourrielEncrypt,Adresse,Telephone)
    VALUES (@Nom, @EncryptedEmail,@Adresse,@Telephone);
	-- Insérer le nouveau client avec l'adresse e-mail dechiffrée aussi dans la table Clients.Courriel
	DECLARE @ClientID INT
	-- Le scope identity va nous retourner l'id value de la derniere valeur identity rentrer 
    SET @ClientID = SCOPE_IDENTITY();
	
	INSERT INTO Clients.Courriel (Courriel)
    VALUES ( @Courriel)
END
GO


------------------------------TEST---------------------------------------
----------------------------
---------------------------
------------------------
------------------
-------------
/*
CREATE SCHEMA Clients 

CREATE TABLE Clients.Client (
    ClientID INT PRIMARY KEY IDENTITY,
    Nom VARCHAR(50) NOT NULL,
    Adresse VARCHAR(100),
    Telephone VARCHAR(15),
	Courriel NVARCHAR(100) 
);
GO

INSERT INTO Clients.Client (Nom, Adresse, Courriel, Telephone)
VALUES 
    ('Client1', 'Adresse1', 'client1@email.com', '1234567890'),
    ('Client2', 'Adresse2',  'client2@email.com', '9876543210');

SELECT  * FROM Clients.Client

EXEC Clients.USP_ChiffrementAdresseCourrielClient @ClientID = 1

SELECT 0 AS 'APRES CHIFFREMENT ', ClientID,Courriel,CourrielEncrypt FROM Clients.Client WHERE ClientID = 1

-- Email déchifrée
EXEC Clients.USP_DeChiffrementAdresseCourrielClient @ClientID = 1


-- Test de l'insertion avec la procédure 

SELECT 0 AS 'AVANT AJOUT CLIENT AVEC EMAIL CHIFFRE', *  FROM Clients.Client 

SELECT 0 AS 'AVANT CLIENT AJOUTE AVEC EMAIL DECHIFFRE', *  FROM Clients.Courriel 

-- AJOUT DU CLIENT 
EXEC Clients.USP_CreationClientAvecChiffrementCourriel 
@Nom = 'Mahmoud' ,
@Adresse = '7777 fffff',
@Courriel = 'chiffrmoi@dechiffre.ca',
@Telephone = '34436'

SELECT 0 AS 'APRES AJOUT CLIENT AVEC EMAIL CHIFFRE', *  FROM Clients.Client WHERE Nom = 'Mahmoud'

SELECT 0 AS 'APRES CLIENT AJOUTE AVEC EMAIL DECHIFFRE', *  FROM Clients.Courriel 

DELETE FROM Clients.Client WHERE ClientID = 3
TRUNCATE TABLE Clients.Courriel
*/ 