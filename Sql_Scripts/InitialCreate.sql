USE master 
GO



-- Création de la BD
CREATE DATABASE ProjetFinal_Maillots
GO
USE ProjetFinal_Maillots
GO

EXEC sp_configure filestream_access_level, 2 RECONFIGURE 

ALTER DATABASE ProjetFinal_Maillots
ADD FILEGROUP FG_Images_2073088 CONTAINS FILESTREAM;
GO
ALTER DATABASE ProjetFinal_Maillots
ADD FILE(
Name = FG_Images_2073088,
FILENAME  = 'C:\EspaceLabo\FG_Images_2073088'
)
TO FILEGROUP FG_Images_2073088
GO 
