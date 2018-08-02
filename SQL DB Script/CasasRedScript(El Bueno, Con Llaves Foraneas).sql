CREATE DATABASE CasasRed;

USE CasasRed;
USE model
DROP DATABASE CasasRed

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
    Gral_Fechaalta DATE DEFAULT GETDATE()
);

/*Departamento de Corretaje*/
--También llevara el nombre del asesor de la venta
CREATE TABLE Corretaje(
	Id INT IDENTITY(1,1) PRIMARY KEY, --id de la casa
	Crt_Status VARCHAR(20),
	Crt_Cliente_Nombre VARCHAR(15),
	Crt_Cliente_ApMat VARCHAR(15),
	Crt_Cliente_ApPat VARCHAR(15),
	Crt_Direccion VARCHAR(100),
	Crt_Precio VARCHAR(20), /*SUPONGO QUE DE LA CASA, NO SE*/ /*<<< Yo tampoco pero me imagino que es el precio de la casa y el gasto para ponerla al corriente y rehabilitacion*/
	Crt_Gasto VARCHAR(10),  /*NI IDEA*/
	Crt_Tipo_Vivienda VARCHAR(15),	/*<<<<<En esta parte pidio poder seleccionar entre Casa o Departamento, si es casa que aparezcan opciones de cuantas habitaciones, 
	planta baja, segundaplanta. Si es Departamento que nivel (nivel 1, nivel 2, nivel 3, nivel 4)*/
	Crt_Nivel INT,
	Crt_Num_Habitaciones INT,
	Crt_Planta INT,
	Crt_Ano_compra DATE,
	Crt_Num_Credito_Infonavit VARCHAR(10),
	Crt_Saldo_infonavit MONEY,
	Crt_Fec_Nac DATE,
	Crt_Tel_Celular INT,
	Crt_Estado_Civil VARCHAR(15),
	Crt_Tel_Casa VARCHAR(15),
	Crt_Tel_Trabajo VARCHAR(15),
	Crt_Tel_Ref1 VARCHAR(15),
	Crt_Tel_Ref2 VARCHAR(15),
	Crt_Tel_Ref VARCHAR(15),
	Crt_Recibo_predial_digital BIT, 
	Crt_Clave_Catastral VARCHAR(15), 
	Crt_Adeudo_predial MONEY,
	Crt_Recibo_luz_digitalizar BIT,
	Crt_Num_servicio_luz VARCHAR(12),
	Crt_Adeudo_luz MONEY,
	Crt_NombreC_Titular_luz VARCHAR(100),
	Crt_Recibo_agua_digital BIT,
	Crt_No_cuenta_agua VARCHAR(7), /*EN EL CASO DE CESPT SON 7 NUMEROS*/
	Crt_Adeudo_agua MONEY,
	Crt_Ine_Titu BIT,
	Crt_Ine_Conyu BIT,
	Crt_Escritura_Simple BIT,
	Crt_Acuerdo BIT, /*CREO QUE SE REFIERE SI TIENE ACUERDO EN LA CESPT*/ /*Asi es*/
	Crt_ActaNacTitu BIT,
	Crt_ActaNacConyu BIT,
	Crt_ActaMatr BIT,
	Crt_EscrCert BIT,
	Crt_CartaDesPre BIT,
	Crt_ReciboLuz BIT, -- Físico
	Crt_ReciboAgua BIT, -- Físico
	Crt_Otros BIT, --Físico
	Crt_Status_Muestra VARCHAR(30),
	Crt_Obervaciones VARCHAR(300),
	Crt_GastosServicios MONEY
);

/*Departamento de Gestion*/
CREATE TABLE Gestion(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Gtn_Escrituras BIT,
	Gtn_Planta_Cartografica BIT,
	Gtn_Predial BIT,
	Gtn_Recibo_Luz BIT, /*ESTOS NO DEBERIAN SER CAMPOS DE FOTO?*/
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
	Gtn_Gastos MONEY,
	Id_Corretaje int, -- Llave Foranea a corretaje
	Id_Cliente int, -- Llave Foranea a Cliente
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
		ON DELETE CASCADE
);
 
/*Departamento de Verificacion*/
CREATE TABLE Verificacion(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Vfn_Persona_fisica BIT, /*CONOCE A LA PERSONA FISICAMENTE?*/
	Vfn_Visto_persona BIT, /*LA HA VISTO PERSONALMENTE?*/
	Vfn_Tiempo_estimado BIT, /*LE DIJO EL TIEMPO ESTIMADO DE ENTREGA?*/
	Vfn_Tiempo VARCHAR(20), /*QUE TIEMPO LE DIJO EL VENDEDOR?*/
	Vfn_Tiene_costo BIT, /*LE COMENTO EL ASESOR QUE TIENE UN COSTO?*/
	Vfn_Costo MONEY, /*QUE COSTO LE DIJO?*/
	Vfn_Trato_asesor VARCHAR(2), /*DEL 1 AL 10 QUE CALIFICACIN LE DA*/
	Vfn_Observaciones VARCHAR(150),
	Id_Gestion int,
	CONSTRAINT FK_GtnVer_Id -- (Cambiar a cliente)
		FOREIGN KEY (Id_Gestion)
		REFERENCES Gestion(Id)
		ON DELETE CASCADE
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
	Id_Corretaje int,
	--Agregar campos para foto y vídeo
	/*LLAVE FORANEA DE LA CASA QUE ES LA DE CORRETAJE*/
	CONSTRAINT FK_CrtHab_Id
		FOREIGN KEY (Id_Corretaje)
		REFERENCES Corretaje(Id)
		ON DELETE CASCADE
);

/*Departamento de Contaduria*/ --No permite crear en cascada
CREATE TABLE Contaduria(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Cnt_Presupuesto_gestion MONEY,
	Cnt_Presupuesto_corretaje MONEY,
	Cnt_Presupuesto_habilitacion MONEY,
	Cnt_Mensualidad MONEY,
	Cnt_Vigilancia MONEY,
	Id_Corretaje int,
	Id_Gestion int,
	Id_Habilitacion int,
	/*LLAVE FORANEA DE CASA QUE SERIA LA DE CORRETAJE (Tambien para saber el gasto)*/
	CONSTRAINT FK_CrtCon_Id
		FOREIGN KEY (Id_Corretaje)
		REFERENCES Corretaje(Id),
	--	ON DELETE CASCADE,

	/*LLAVES FORANEAS DE GESTION PARA SABER CUANTO GASTARON*/
	CONSTRAINT FK_GtnCon_Id
		FOREIGN KEY (Id_Gestion)
		REFERENCES Gestion(Id),
	--	ON DELETE CASCADE,
	/*LLAVES FORANEAS DE HABILITACION PARA SABER CUANTO GASTARON*/
	CONSTRAINT FK_HabCon_Id
		FOREIGN KEY (Id_Habilitacion)
		REFERENCES Habilitacion(Id)
		--ON DELETE CASCADE
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


CREATE TABLE Articulos(
	art_id VARCHAR(15) PRIMARY KEY,
	art_nombre VARCHAR(150),
	art_descripcion VARCHAR(250),
	art_fechaIngreso DATE, --Cambio de DATETIME a DATE
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
	ext_fechaAgregado date, --Cambio de Decimal a Date
	ext_usuarioAgrego INT FOREIGN KEY REFERENCES Usuario(Id),
	ext_ubicacion INT FOREIGN KEY REFERENCES Ubicaciones(ubi_id)
)
