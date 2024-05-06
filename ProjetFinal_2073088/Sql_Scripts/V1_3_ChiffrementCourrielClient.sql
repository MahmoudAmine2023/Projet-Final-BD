USE ProjetFinal_Maillots
GO

-- Ajout du Champ
ALTER TABLE Clients.Client
ADD CourrielEncrypt VARBINARY(MAX);
GO

-- Création de la clé
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'PasswordBD2024!'
GO
CREATE CERTIFICATE Final_Certificat WITH SUBJECT = 'ChiffrementAdresseCourrielClient'
GO
CREATE SYMMETRIC KEY ProjetFinal_Cle WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE Final_Certificat
GO

--Encryption du courriel 


CREATE TABLE Clients.Courriel(
ClientID INT,
Courriel VARCHAR(50) NULL
);
GO



-- VERIF DU CERTIFICAT 
-- SELECT *  FROM Sys.certificates

 -- VERIF DE LA CLE 
 -- SELECT * FROM sys.symmetric_keys

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
GO

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
	-- Le scope idendity va nous retourner l'id value de la derniere valeur indentity rentrer 
    SET @ClientID = SCOPE_IDENTITY();
	
	INSERT INTO Clients.Courriel (ClientID, Courriel)
    VALUES (@ClientID, @Courriel)
END
GO


--TEST 
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
EXEC Clients.USP_DeChiffrementAdresseCourrielClient @ClientID = 1 AS
SELECT * FROM Clients.Client
