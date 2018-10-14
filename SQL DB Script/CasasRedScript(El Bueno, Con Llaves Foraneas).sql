DROP DATABASE CasasRed
GO
CREATE DATABASE CasasRed;
GO
USE CasasRed;
GO


--También llevara el nombre del asesor de la venta
CREATE TABLE Corretaje(
	Id INT IDENTITY(1,1) PRIMARY KEY, --id de la casa
	Crt_Status VARCHAR(20),
	Crt_FechaAlta DATE,
	Crt_Cliente_Nombre VARCHAR(15),
	Crt_Cliente_ApMat VARCHAR(15),
	Crt_Cliente_ApPat VARCHAR(15),
	Crt_Direccion VARCHAR(100),
	Crt_Nss VARCHAR(11),
	Crt_Precio VARCHAR(20),
	Crt_Gasto VARCHAR(10),  /*NI IDEA*/
	Crt_Tipo_Vivienda VARCHAR(15),	/*<<<<<En esta parte pidio poder seleccionar entre Casa o Departamento, si es casa que aparezcan opciones de cuantas habitaciones, 
	planta baja, segundaplanta. Si es Departamento que nivel (nivel 1, nivel 2, nivel 3, nivel 4)*/
	Crt_Nivel INT,
	Crt_Num_Habitaciones INT,
	Crt_Planta INT,
	Crt_Ano_compra VARCHAR(4),
	Crt_Num_Credito_Infonavit VARCHAR(10),
	Crt_Saldo_infonavit DECIMAL(18,4),
	Crt_Fec_Nac DATE,
	Crt_Tel_Celular VARCHAR(15),
	Crt_Estado_Civil VARCHAR(15),
	Crt_Tel_Casa VARCHAR(15),
	Crt_Tel_Trabajo VARCHAR(15),
	Crt_Tel_Ref1 VARCHAR(15),
	Crt_Tel_Ref2 VARCHAR(15),
	Crt_Tel_Ref VARCHAR(15),
	Crt_Recibo_predial_digital VARCHAR(MAX), 
	Crt_Clave_Catastral VARCHAR(15), 
	Crt_Adeudo_predial DECIMAL(18,4),
	--Crt_Recibo_luz_digitalizar BIT,
	Crt_Num_servicio_luz VARCHAR(12),
	Crt_Adeudo_luz DECIMAL(18,4),
	Crt_NombreC_Titular_luz VARCHAR(100),
	--Crt_Recibo_agua_digital BIT,
	Crt_No_cuenta_agua VARCHAR(7), /*EN EL CASO DE CESPT SON 7 NUMEROS*/
	Crt_Adeudo_agua DECIMAL(18,4),
	Crt_Ine_Titu BIT,
	Crt_Ine_Conyu BIT,
	Crt_Escritura_Simple BIT,
	Crt_Acuerdo BIT, /*CREO QUE SE REFIERE SI TIENE ACUERDO EN LA CESPT*/ /*Asi es*/
	Crt_ActaNacTitu BIT,
	Crt_ActaNacConyu BIT,
	Crt_ActaMatr BIT,
	Crt_EscrCert BIT,
	Crt_CartaDesPre BIT,
	Crt_ReciboLuz VARCHAR(MAX), -- Físico FOTO
	Crt_ReciboAgua VARCHAR(MAX), -- Físico FOTO
	Crt_Otros VARCHAR(MAX), --Físico FOTO
	Crt_Status_Muestra VARCHAR(30),
	Crt_Obervaciones VARCHAR(300),
	--Crt_GastosServicios MONEY
	Crt_ProgresoForm INT,
	Id_Vendedor INT,
	Id_Usuario INT,
	CONSTRAINT FK_CrtVendedor_Id
		FOREIGN KEY (Id_Vendedor)
		REFERENCES Vendedor(Id)
		ON DELETE SET NULL,
	CONSTRAINT FK_CrtUsuario_Id
		FOREIGN KEY (Id_Usuario)
		REFERENCES Usuario(Id)
		ON DELETE SET NULL
);

/*Datos Generales del Cliente (Llenado por Gestion)*/
CREATE TABLE Cliente(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Gral_Nombre VARCHAR(80),
	Gral_Apellidopa VARCHAR(40),
	Gral_Apellidoma VARCHAR(40),
	Gral_Fechanac DATE,
	Gral_Nss VARCHAR(11),
	Gral_Curp VARCHAR(18),
	Gral_Rfc VARCHAR(13),
	Gral_Lugarnac VARCHAR(70),
	Gral_Calle VARCHAR(30),
	Gral_Numero VARCHAR(20),
	Gral_Cp VARCHAR(8),
	Gral_Colonia VARCHAR(40),
	Gral_Municipio VARCHAR(40),
	Gral_Estado VARCHAR(40),
	Gral_Celular VARCHAR(10),
	Gral_Tel_casa VARCHAR(10),
	Gral_Estado_civil VARCHAR(20),
	Gral_Regimen_matrimonial VARCHAR(25), /*Sociedad Conyugal  O  Separacion de bienes*/
	Gral_Ocupacion VARCHAR(30),
	Gral_Teltrabajo VARCHAR(15),
	Gral_Correo VARCHAR(320),
	Gral_Identificacion VARCHAR(40),
	Gral_No_identificacion INT,
	Gral_Ref_nombre1 VARCHAR(160),
	Gral_Ref_cel_1 VARCHAR(10),
	Gral_Ref_nombre2 VARCHAR(160),
	Gral_Ref_cel_2 VARCHAR(10),
	--Conyugue
	Cyg_Nombre VARCHAR(80),
	Cyg_Apellidopa VARCHAR(40),
	Cyg_Apellidoma VARCHAR(40),
	Gyg_Fechanac DATE,
	Cyg_Nss VARCHAR(11),
	Cyg_Curp VARCHAR(18),
	Cyg_Rfc VARCHAR(13),
	Cyg_Lugarnac VARCHAR(70),
	Cyg_Celular VARCHAR(10),
	Cyg_Tel_casa VARCHAR(10),
	Cyg_Ocupacion VARCHAR(30),
	Cyg_Tel_trabajo VARCHAR(15),
	Cyg_Correo VARCHAR(320),
	Cyg_Identificacion VARCHAR(40),
	Cyg_No_identificacoion INT,
    Gral_Fechaalta DATE DEFAULT GETDATE(),
	Gral_ProgresoForm INT,
	Grlal_Folio varchar(10),
	--Vndr_Nombre VARCHAR(80), -- Vendedor quien trajo al cliente
	--Vndr_Apellidopa VARCHAR(40), -- Vendedor
	--Vndr_Apellidoma VARCHAR(40), -- Vendedor
	Id_Vendedor INT,
	Id_Corretaje INT,
	Id_Usuario INT,
	CONSTRAINT FK_CrtCasa_Id
		FOREIGN KEY (Id_Corretaje)
		REFERENCES Corretaje(Id)
		ON DELETE SET NULL,
	CONSTRAINT FK_GralVendedor_Id
		FOREIGN KEY (Id_Vendedor)
		REFERENCES Vendedor(Id)
		ON DELETE SET NULL,
	CONSTRAINT FK_GralUsuario_Id
		FOREIGN KEY (Id_Usuario)
		REFERENCES Usuario(Id)
		ON DELETE SET NULL
);


/*Departamento de Gestion*/
--CASCADE si borra la casa
CREATE TABLE Gestion(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Gtn_Escrituras BIT,
	Gtn_Planta_Cartografica BIT,
	Gtn_Predial BIT,
	Gtn_Recibo_Luz BIT, 
	Gtn_Recibo_Agua BIT,
	Gtn_Acta_Nacimiento BIT,
	Gtn_IFE_Copia BIT,
	Gtn_Sol_Ret_Ifo BIT, /*SOLICITUD DE RETENCION DE INFORMACION*/
	Gtn_Cert_Hip BIT, /*CERTIFICADO DE HIPOTECA*/
	Gtn_Cert_Fiscal BIT, /*CERTIFICADO FISCAL*/
	Gtn_Sol_Estado BIT, /*SOLICITUD DE ESTADO*/
	Gtn_Junta_URBI BIT,
	Gtn_Agua_Pago_Inf BIT,
	Gtn_Cert_Cartogr BIT,
	Gtn_No_Oficial BIT, /*INVESTIGAR*/
	Gtn_Avaluo BIT,
	Gtn_CT_Banco BIT, /*INVESTIGAR*/
	Gtn_Aviso_Suspension BIT, /*ENTREGA AVISO DE SUSPENSION*/
	Gtn_Formato_Infonavit BIT,
	Gtn_Fotos_Propiedad BIT,
	Gtn_Evaluo_Contacto BIT,
	Gtn_Planeacion_Fianza BIT,
	Gtn_Urbanizacion BIT,
	Gtn_Credito_INFONAVIT BIT,
	Gtn_Notaria BIT,
	Gtn_Firma_Escrituras BIT,
	Gtn_CuentaBancaria BIT, --Nuevo
	Gtn_Taller BIT, --Nuevo
	--Gtn_Gastos MONEY,
	Gtm_Aviso_Susp BIT,
	Gtn_ProgresoForm INT,
	Id_Corretaje int, -- Llave Foranea a corretaje
	Id_Cliente int, -- Llave Foranea a Cliente1
	Id_Usuario INT,
	/*AGEREGAR FK CASA QUE ES LA DE CORRETAJE*/ /*<<<<<<<<<<ESTO ES DEL VENDEDOR?*/
	--La casa asignada
	CONSTRAINT FK_Crt_Id
		FOREIGN KEY (Id_Corretaje)
		REFERENCES Corretaje(Id)
		ON DELETE CASCADE,
	--El comprador de la casa 
	CONSTRAINT FK_Cliente_Id --Quitar (Pensar mas)
		FOREIGN KEY (Id_Cliente)
		REFERENCES Cliente(Id)
		ON DELETE SET NULL,
	CONSTRAINT FK_Usuario_Id
		FOREIGN KEY (Id_Usuario)
		REFERENCES Usuario(Id)
		ON DELETE SET NULL,
);

 
/*Departamento de Verificacion*/
CREATE TABLE Verificacion(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Vfn_Persona_fisica BIT, /*CONOCE A LA PERSONA FISICAMENTE?*/
	Vfn_Visto_persona BIT, /*LA HA VISTO PERSONALMENTE?*/
	Vfn_Tiempo_estimado BIT, /*LE DIJO EL TIEMPO ESTIMADO DE ENTREGA?*/
	Vfn_Tiempo VARCHAR(20), /*QUE TIEMPO LE DIJO EL VENDEDOR?*/
	Vfn_Tiene_costo BIT, /*LE COMENTO EL ASESOR QUE TIENE UN COSTO?*/
	Vfn_Costo DECIMAL(18,4), /*QUE COSTO LE DIJO?*/
	Vfn_Trato_asesor int, /*DEL 1 AL 10 QUE CALIFICACIN LE DA*/
	Vfn_Observaciones VARCHAR(150),
	Vfn_ProgresoForm INT,
	Id_Cliente int,
	Id_Usuario INT,
	CONSTRAINT FK_GtnCli_Id
		FOREIGN KEY (Id_Cliente)
		REFERENCES Cliente(Id)
		ON DELETE CASCADE,
	CONSTRAINT FK_VrfUsu_Id 
		FOREIGN KEY (Id_Usuario)
		REFERENCES Usuario(Id)
		ON DELETE CASCADE,
);


/*Departamento Habilitacion*/
CREATE TABLE Habilitacion(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Hbt_Puertas BIT,
	Hbt_Chapas BIT,
	Hbt_Marcos_puertas BIT,
	Hbt_Bisagras BIT,
	Hbt_Taza BIT,
	Hbt_Lavamanos BIT,
	Hbt_Bastago BIT,
	Hbt_Chapeton BIT,
	Hbt_Maneral BIT,
	Hbt_Regadera_completa BIT,
	Hbt_Kit_lavamanos BIT,
	Hbt_Kit_taza BIT,
	Hbt_Rosetas BIT,
	Hbt_Apagador_sencillo BIT,
	Hbt_Conector_sencillo BIT,
	Hbt_Apagador_doble BIT,
	Hbt_Conector_apagador BIT,
	Hbt_Domo BIT,
	Hbt_Ventanas BIT,
	Hbt_Cableado BIT,
	Hbt_Calibre_cableado VARCHAR(10),
	Hbt_Break_interior BIT,
	Hbt_Break_medidor BIT,
	Hbt_Pinturas BIT,
	Hbt_AvisoSusp BIT,
	Hbt_StatusCasa VARCHAR(25),
	Hbt_FchEntrega DATE,
	Hbt_ProgresoForm INT,
	Id_Corretaje int,
	Id_Usuario INT,
	/*LLAVE FORANEA DE LA CASA QUE ES LA DE CORRETAJE*/
	CONSTRAINT FK_CrtHab_Id
		FOREIGN KEY (Id_Corretaje)
		REFERENCES Corretaje(Id)
		ON DELETE CASCADE,
	CONSTRAINT FK_HabUsuario_Id
		FOREIGN KEY (Id_Usuario)
		REFERENCES Usuario(Id)
		ON DELETE CASCADE
);


CREATE TABLE TipoUsuario(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	tipusu_descricion VARCHAR(50)
)


/*Para manejo de usuarios*/
CREATE TABLE Usuario (
	Id INT IDENTITY(1,1) PRIMARY KEY,
    usu_username VARCHAR(20),
    usu_correo VARCHAR(150),
    usu_nombre VARCHAR(80),
    usu_password VARCHAR(18),
    usu_apellidoPa VARCHAR(40),    
    usu_apellidoMa VARCHAR(40),
    usu_alta DATE DEFAULT GETDATE(),
    usu_tipo VARCHAR(15), --Puede ser referenciado a la tabla tipo Usuario, es opcional, podemos definir los tipos de usuarios en las vistas
    usu_activo BIT DEFAULT 1,
	Id_TipoUsiario int,
	CONSTRAINT FK_TipoUsuario_Id
		FOREIGN KEY (Id_TipoUsiario)
		REFERENCES TipoUsuario(Id)
		ON DELETE CASCADE
);

DROP TABLE Contaduria
/*Nueva tabla de contaduria*/
CREATE TABLE Contaduria(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Cnt_M_Preguntar DECIMAL(18,4),
	Cnt_Material DECIMAL(18,4),
	Cnt_Vigilancia DECIMAL(18,4),
	Cnt_Tramites DECIMAL(18,4), -- Tramites Posiblemente Borrar
	Cnt_CESPT DECIMAL(18,4), -- Posiblemente Borrar
	Cnt_CFE DECIMAL(18,4), --Corretaje posiblemente borrar
	Cnt_DevMensualidad DECIMAL(18,4), --No
	--Cnt_Otros DECIMAL(18,4), --No
	Id_Corretaje int,
	Id_Usuario INT,
	CONSTRAINT FK_CasaCorretaje_Id
		FOREIGN KEY (Id_Corretaje)
		REFERENCES Corretaje(Id)
		ON DELETE SET NULL,
	CONSTRAINT FK_CntUsuario_Id
		FOREIGN KEY (Id_Usuario)
		REFERENCES Usuario(Id)
		ON DELETE SET NULL,
);

CREATE TABLE GastosContaduria(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	GstCon_Concepto VARCHAR(50),
	GstCon_Monto DECIMAL(18,4),
	GstCon_Descripcion VARCHAR(150),
	GstCon_Fecha DATE,
	Id_Corretaje int,
	Id_Usuario int,
	CONSTRAINT FK_GastosCasa_Id
		FOREIGN KEY (Id_Corretaje)
		REFERENCES Corretaje(Id)
		ON DELETE SET NULL,
	CONSTRAINT FK_GastosCasaUsu_Id
		FOREIGN KEY (Id_Usuario)
		REFERENCES Usuario(Id)
		ON DELETE SET NULL
)


CREATE TABLE Articulos(
	art_id VARCHAR(15) PRIMARY KEY,
	art_nombre VARCHAR(150),
	art_descripcion VARCHAR(250),
	art_fechaIngreso DATE DEFAULT GETDATE(), --Cambio de DATETIME a DATE
	art_cantidadMinima DECIMAL(18,6)
)

CREATE TABLE Ubicaciones(
    ubi_id INT IDENTITY(1,1) PRIMARY KEY,
    ubi_codigo VARCHAR(10),
    ubi_descripcion varchar(50) 
)

CREATE TABLE Existencias(
	Id INT identity(1,1) PRIMARY KEY,
	ext_art_id VARCHAR(15) FOREIGN KEY REFERENCES Articulos(art_id),
	ext_cantidad decimal(18,6),
	ext_cantidadActual decimal(18,6),
	ext_precioUnitario decimal(18,6),
	ext_fechaAgregado date default getdate(), --Cambio de Decimal a Date
	ext_usuarioAgrego INT FOREIGN KEY REFERENCES Usuario(Id),
	ext_ubicacion INT FOREIGN KEY REFERENCES Ubicaciones(ubi_id)
)

CREATE TABLE [CasaInventario](
	[ci_Id] [int] IDENTITY(1,1) NOT NULL,
	[ci_corretaje_id] [int] NOT NULL,
	[ci_cantidadAsignada] [decimal](18, 6) NOT NULL,
	[ci_fecha] [datetime] NOT NULL,
	[ci_usuario_id] [int] NOT NULL,
	[ci_articulo_id] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ci_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [adminDani].[CasaInventario] ADD  DEFAULT (getdate()) FOR [ci_fecha]
GO

ALTER TABLE [adminDani].[CasaInventario]  WITH CHECK ADD FOREIGN KEY([ci_articulo_id])
REFERENCES [adminDani].[Articulos] ([art_id])
GO

ALTER TABLE [adminDani].[CasaInventario]  WITH CHECK ADD FOREIGN KEY([ci_corretaje_id])
REFERENCES [adminDani].[Corretaje] ([Id])
GO

ALTER TABLE [adminDani].[CasaInventario]  WITH CHECK ADD FOREIGN KEY([ci_usuario_id])
REFERENCES [adminDani].[Usuario] ([Id])
GO


CREATE TABLE [adminDani].[HistorialAsignacion](
	[ha_id] [int] IDENTITY(1,1) NOT NULL,
	[ha_casaInventario] [int] NULL,
	[ha_existencia_id] [int] NOT NULL,
	[ha_usuarioEntrego] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ha_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [adminDani].[HistorialAsignacion]  WITH CHECK ADD FOREIGN KEY([ha_casaInventario])
REFERENCES [adminDani].[CasaInventario] ([ci_Id])
GO

ALTER TABLE [adminDani].[HistorialAsignacion]  WITH CHECK ADD FOREIGN KEY([ha_existencia_id])
REFERENCES [adminDani].[Existencias] ([Id])
GO

ALTER TABLE [adminDani].[HistorialAsignacion]  WITH CHECK ADD FOREIGN KEY([ha_usuarioEntrego])
REFERENCES [adminDani].[Usuario] ([Id])
GO


CREATE TABLE FotosHabilitacion(
	fh_id INT IDENTITY(1,1) PRIMARY KEY,
	fh_archivo VARCHAR(MAX) NOT NULL,
	fh_nombre VARCHAR(200) NOT NULL,
	fh_habilitacion INT, 
	CONSTRAINT FK_FotosHab_Id
		FOREIGN KEY (fh_habilitacion)
		REFERENCES Habilitacion(Id) 
		ON DELETE CASCADE
)

--Nuevas Tablas
/*Nueva tabla de Vendedor*/
CREATE TABLE Vendedor(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Vndr_Nombre VARCHAR(80), -- Vendedor quien trajo al cliente
	Vndr_Apellidopa VARCHAR(40), -- Vendedor
	Vndr_Apellidoma VARCHAR(40), -- Vendedor
)

CREATE TABLE CalificacionVendedor(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DVndr_Puntaje DECIMAL(18,4),
	Id_Corretaje INT,
	Id_Vendedor INT,
	CONSTRAINT FK_DVndrCorretaje_Id
		FOREIGN KEY (Id_Corretaje)
		REFERENCES Corretaje(Id)
		ON DELETE CASCADE,
	CONSTRAINT FK_DVndrVendedor_Id
		FOREIGN KEY (Id_Corretaje)
		REFERENCES Vendedor(Id)
		ON DELETE CASCADE
)

/*Nueva tabla de Comisiones*/
CREATE TABLE Comision(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Id_Corretaje INT,
	Id_Usuario INT,
	CONSTRAINT FK_CmsCorretaje_Id
		FOREIGN KEY (Id_Corretaje)
		REFERENCES Corretaje(Id)
		ON DELETE CASCADE,
	CONSTRAINT FK_CmsUsuario_Id
		FOREIGN KEY (Id_Usuario)
		REFERENCES Usuario(Id)
		ON DELETE SET NULL
)

CREATE TABLE DetallesComision(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Id_Vendedor INT,
	Id_Comision INT,
	DCms_Monto DECIMAL(18, 4),
	DCms_TipoCom VARCHAR(25),
	CONSTRAINT FK_DCmsComision_Id
		FOREIGN KEY (Id_Comision)
		REFERENCES Comision(Id)
		ON DELETE CASCADE,
	CONSTRAINT FK_DCmsVendedor_Id
		FOREIGN KEY (Id_Vendedor)
		REFERENCES Vendedor(Id)
		ON DELETE CASCADE,
)


CREATE TABLE Comentarios(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Cmt_Titulo VARCHAR(50),
	Cmt_Nota VARCHAR(MAX),
	Cmt_Fecha DATE,
	Id_Cliente int,
	CONSTRAINT FK_ComentClie_Id
		FOREIGN KEY (Id_Cliente)
		REFERENCES Cliente(Id)
		ON DELETE CASCADE
)

CREATE TABLE Notificaciones(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Not_usuario_id INT,
    Not_titutlo VARCHAR(25),
    Not_descripcion VARCHAR(250),
    Not_idVinculado VARCHAR(50),
    Not_leida BIT default 0,
    Not_mostrada BIT default 0,
    
    CONSTRAINT FK_NUsuario_Id
        FOREIGN KEY (Not_usuario_id)
        REFERENCES Usuario(ID)
        ON DELETE CASCADE
)

--CASCADE
CREATE TABLE VendedorAsig(
Id INT IDENTITY(1,1) PRIMARY KEY,
Id_Vendedor INT,
Id_Corretaje INT,
VndAsig_Departamento VARCHAR(50),
CONSTRAINT FK_VndAsigVendedor_Id
		FOREIGN KEY (Id_Vendedor)
		REFERENCES Vendedor(Id)
		ON DELETE SET NULL,
CONSTRAINT FK_VndAsigCasa_Id
		FOREIGN KEY (Id_Corretaje)
		REFERENCES Corretaje(Id)
		ON DELETE CASCADE,		 
)

/*Campos agregados a gestion*/
--ALTER TABLE Gestion
--ADD Gtn_CuentaBancaria BIT, Gtn_Taller BIT;

TRUNCATE TABLE Corretaje
TRUNCATE TABLE Cliente
TRUNCATE TABLE Gestion
TRUNCATE TABLE Verificacion
TRUNCATE TABLE Habilitacion
TRUNCATE TABLE Contaduria
TRUNCATE TABLE GastosContaduria
TRUNCATE TABLE FotosHabilitacion
TRUNCATE TABLE Comentarios
TRUNCATE TABLE DetallesComision
TRUNCATE TABLE Comision
TRUNCATE TABLE VendedorAsig


DROP TABLE Corretaje
DROP TABLE Cliente
DROP TABLE Gestion
DROP TABLE Verificacion
DROP TABLE Habilitacion
DROP TABLE Contaduria
DROP TABLE GastosContaduria
DROP TABLE FotosHabilitacion
DROP TABLE GastosContaduria
DROP TABLE Comentarios
DROP TABLE DetallesComision
DROP TABLE Comision
DROP TABLE VendedorAsig

--Sin constraint
DROP TABLE HistorialAsignacion
DROP TABLE CasaInventario
DROP TABLE Articulos
DROP TABLE Ubicaciones
DROP TABLE Existencias

TRUNCATE TABLE HistorialAsignacion
TRUNCATE TABLE CasaInventario
TRUNCATE TABLE Articulos
TRUNCATE TABLE Ubicaciones
TRUNCATE TABLE Existencias

--Eliminar Constraints
ALTER TABLE Corretaje
DROP CONSTRAINT FK_CrtVendedor_Id, FK_CrtUsuario_Id; 

ALTER TABLE Cliente
DROP CONSTRAINT FK_CrtCasa_Id , FK_GralVendedor_Id, FK_GralUsuario_Id;

ALTER TABLE Gestion
DROP CONSTRAINT FK_Crt_Id, FK_Cliente_Id, FK_Usuario_Id;

ALTER TABLE Verificacion
DROP CONSTRAINT FK_GtnCli_Id, FK_VrfUsu_Id;

ALTER TABLE Habilitacion
DROP CONSTRAINT FK_CrtHab_Id  , FK_HabUsuario_Id;

ALTER TABLE Usuario
DROP CONSTRAINT FK_TipoUsuario_Id;

ALTER TABLE Contaduria
DROP CONSTRAINT FK_CasaCorretaje_Id, FK_CntUsuario_Id;

ALTER TABLE GastosContaduria 
DROP CONSTRAINT FK_GastosCasa_Id, FK_GastosCasaUsu_Id;

ALTER TABLE CalificacionVendedor 
DROP CONSTRAINT FK_DVndrCorretaje_Id, FK_DVndrVendedor_Id;

ALTER TABLE Comision 
DROP CONSTRAINT FK_CmsCorretaje_Id;

ALTER TABLE DetallesComision 
DROP CONSTRAINT FK_DCmsComision_Id, FK_DCmsVendedor_Id;

ALTER TABLE Comentarios 
DROP CONSTRAINT FK_ComentClie_Id;
Truncate table Comentarios

ALTER TABLE VendedorAsig 
DROP CONSTRAINT FK_VndAsigVendedor_Id,FK_VndAsigCasa_Id;

ALTER TABLE FotosHabilitacion 
DROP CONSTRAINT FK_FotosHab_Id;

--Cosas que no sé para que funcionan
ALTER TABLE Usuario 
ADD CONSTRAINT FK_TipoUsuario_Id 
	FOREIGN KEY (Id_TipoUsiario)
		REFERENCES TipoUsuario(Id)
		ON DELETE SET NULL 

ALTER TABLE Comentarios 
ADD CONSTRAINT FK_ComentClie_Id 
	FOREIGN KEY (Id_Cliente)
		REFERENCES Cliente(Id)
		ON DELETE CASCADE 
