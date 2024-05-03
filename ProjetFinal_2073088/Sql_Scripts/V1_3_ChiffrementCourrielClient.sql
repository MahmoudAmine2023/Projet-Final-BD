USE ProjetFinal_Maillots
GO

CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'PasswordBD2024!'
GO
CREATE CERTIFICATE Final_Certificat WITH SUBJECT = 'ChiffrementAdresseCourrielClient'
GO
CREATE SYMMETRIC KEY ProjetFinal_Cle WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE Final_Certificat
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

    -- S�lectionner l'adresse e-mail chiffr�e du client
    SELECT @EncryptedEmail = CourrielEncrypt
    FROM Clients.Client
    WHERE ClientID = @ClientID;

    -- D�chiffrer l'adresse e-mail
    OPEN SYMMETRIC KEY ProjetFinal_Cle
    DECRYPTION BY CERTIFICATE Final_Certificat;

    SET @DecryptedEmail = CONVERT(NVARCHAR(100), DecryptByKey(@EncryptedEmail));

    CLOSE SYMMETRIC KEY ProjetFinal_Cle;

    -- Renvoyer l'adresse e-mail d�chiffr�e
    SELECT @DecryptedEmail AS DecryptedEmail;
END
