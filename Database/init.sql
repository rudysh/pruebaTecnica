USE master;
GO

IF DB_ID('ArticulosDB') IS NULL
BEGIN
    CREATE DATABASE ArticulosDB;
END;
GO

USE ArticulosDB;
GO

IF OBJECT_ID('dbo.Articulos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Articulos
    (
        IdArticulo INT IDENTITY(1,1) NOT NULL,
        CodigoBarra VARCHAR(15) NOT NULL,
        Referencia VARCHAR(20) NOT NULL,
        CodigoMarca VARCHAR(6) NOT NULL,
        Nombre VARCHAR(255) NOT NULL,
        Talla VARCHAR(12) NULL,
        CodigoColor VARCHAR(6) NULL,
        Fabricante VARCHAR(12) NULL,
        Categoria VARCHAR(6) NULL,
        TipoImpuesto TINYINT NULL,
        PrecioDetal NUMERIC(10,2) NOT NULL,
        PrecioMayor NUMERIC(10,2) NULL,
        PrecioAfiliado NUMERIC(10,2) NULL,
        PrecioPromocion NUMERIC(10,2) NULL,
        Promocion BIT NOT NULL
            CONSTRAINT DF_Articulos_Promocion DEFAULT(0),

        CONSTRAINT PK_Articulos
            PRIMARY KEY (IdArticulo),

        CONSTRAINT UQ_Articulos_CodigoBarra
            UNIQUE (CodigoBarra)
    );
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Articulos_Referencia_CodigoMarca'
)
BEGIN
    CREATE INDEX IX_Articulos_Referencia_CodigoMarca
        ON dbo.Articulos
        (
            Referencia,
            CodigoMarca
        );
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Articulos)
BEGIN

INSERT INTO dbo.Articulos
(
    CodigoBarra,
    Referencia,
    CodigoMarca,
    Nombre,
    Talla,
    CodigoColor,
    Fabricante,
    Categoria,
    TipoImpuesto,
    PrecioDetal,
    PrecioMayor,
    PrecioAfiliado,
    PrecioPromocion,
    Promocion
)
VALUES

('744100000001','REF-001','MAR01','Camiseta Deportiva','S','AZUL','Nike','ROPA',1,10000,9000,8500,NULL,0),

('744100000002','REF-001','MAR01','Camiseta Deportiva','M','AZUL','Nike','ROPA',1,12000,9000,8500,NULL,0),

('744100000003','REF-001','MAR01','Camiseta Deportiva','L','AZUL','Nike','ROPA',1,11000,9000,8500,NULL,0),

('744100000004','REF-002','MAR01','Camisa Casual','M','BLANCO','Adidas','ROPA',1,18000,17000,16500,15000,1),

('744100000005','REF-002','MAR01','Camisa Casual','L','BLANCO','Adidas','ROPA',1,18000,17000,16500,15000,1),

('744100000006','REF-003','MAR02','Pantalon Jeans','32','AZUL','Levis','PANT',1,25000,23000,22000,NULL,0),

('744100000007','REF-003','MAR02','Pantalon Jeans','34','AZUL','Levis','PANT',1,27000,23000,22000,NULL,0),

('744100000008','REF-004','MAR03','Jacket Impermeable','M','NEGRO','Columbia','ABRI',1,45000,42000,40000,38000,1),

('744100000009','REF-005','MAR04','Zapato Deportivo','42','NEGRO','Puma','CALZ',1,38000,35000,34000,NULL,0),

('744100000010','REF-006','MAR05','Gorra Casual','Única','ROJO','New Era','ACCE',1,12000,11000,10000,NULL,0);

END;
GO

CREATE OR ALTER PROCEDURE dbo.SP_ConsultarArticulo
(
    @Referencia VARCHAR(20),
    @CodigoMarca VARCHAR(6)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PrecioMayorEncontrado NUMERIC(10,2);
    DECLARE @CantidadPreciosDiferentes INT;

    SELECT
        @PrecioMayorEncontrado = MAX(PrecioDetal),
        @CantidadPreciosDiferentes = COUNT(DISTINCT PrecioDetal)
    FROM dbo.Articulos
    WHERE Referencia = @Referencia
      AND CodigoMarca = @CodigoMarca;

    IF @CantidadPreciosDiferentes > 1
    BEGIN
        UPDATE dbo.Articulos
        SET PrecioDetal = @PrecioMayorEncontrado
        WHERE Referencia = @Referencia
          AND CodigoMarca = @CodigoMarca;
    END;

    SELECT
        IdArticulo,
        CodigoBarra,
        Referencia,
        CodigoMarca,
        Nombre,
        PrecioDetal
    FROM dbo.Articulos
    WHERE Referencia = @Referencia
      AND CodigoMarca = @CodigoMarca
    ORDER BY IdArticulo;
END;
GO