CREATE DATABASE CasasRed;

USE CasasRed;

CREATE TABLE Cliente(
	--Gral_Idcliente INT IDENTITY(1,1) PRIMARY KEY,
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
	Gral_Regimen_matrimonial VARCHAR(25), /*Sociedad Conyugal  O  Separaci�n de bienes*/
	Gral_Ocupacion VARCHAR(30),
	Gral_Teltrabajo VARCHAR(15),
	Gral_Correo VARCHAR(320),
	Gral_Identificacion VARCHAR(40),
	Gral_No_identificacion INT,
	Gral_Ref_nombre1 VARCHAR(160),
	Gral_Ref_cel_1 VARCHAR(10),
	Gral_Ref_nombre2 VARCHAR(160),
	Gral_Ref_cel_2 VARCHAR(10),
    Gral_Fechaalta DATE DEFAULT GETDATE()
);

/*CONYUGE SI ESTA CASADO CHECAR NUEVA TABLA*/
/*CREAR REFENCIAS DE TABLAS*/
CREATE TABLE Conyugue(
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
	/*ID DE CLIENTE PARA RELACIONARLO COM CONYUGUE*/
	--Gral_IDCLIENTE int FOREIGN KEY REFERENCES GENERAL(Gral_IDCLIENTE)
	CONSTRAINT fk_Gral_Id
		FOREIGN KEY (Id)
		REFERENCES Cliente(Id)
		ON DELETE CASCADE
);

/*Agregar FK*/
CREATE TABLE Gestion(
	--Gtn_ID INT IDENTITY(1,1) PRIMARY KEY,
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Gtn_ESCRITURAS BIT,
	Gtn_PLANTA_CARTOGRAFICA BIT,
	Gtn_PREDIAL BIT,
	Gtn_RECIBO_LUZ BIT, /*ESTOS NO DEBERIAN SER CAMPOS DE FOTO?*/
	Gtn_RECIBO_AGUA BIT,
	Gtn_ACTA_NACIMIENTO BIT,
	Gtn_IFE_COPIA BIT,
	Gtn_SOL_RET_INFO BIT, /*SOLICITUD DE RETENCI�N DE INFORMACI�N*/
	Gtn_CERT_HIP BIT, /*CERTIFICADO DE HIPOTECA*/
	Gtn_CERT_FISCAL BIT, /*CERTIFICADO FISCAL*/
	Gtn_SOL_ESTADO BIT, /*SOLICITUD DE ESTADO*/
	Gtn_JUNTA_URBI BIT,
	Gtn_AGUA_PAGO_INF BIT,
	Gtn_CERT_CARTOGR BIT,
	Gtn_NO_OFICIAL BIT, /*INVESTIGAR*/
	Gtn_AVALUO BIT,
	Gtn_CT_BANCO BIT, /*INVESTIGAR*/
	Gtn_AVISO_SUSPENSION BIT, /*ENTREGA AVISO DE SUSPENSION*/
	Gtn_FORMATO_INFONAVIT BIT,
	Gtn_FOTOS_PROPIEDAD BIT,
	Gtn_EVALUO_CONTACTO BIT,
	Gtn_PLANEACION_FIANZA BIT,
	Gtn_URBANIZACION BIT,
	Gtn_CREDITO_INFONAVIT BIT,
	Gtn_NOTARIA BIT,
	Gtn_FIRMA_ESCRITURAS BIT
/*AGEREGAR FK CLIENTE*/ /*<<<<<<<<<<ESTO ES DEL VENDEDOR?*/
);

/*AGEREGAR FK*/ /*CHECAR QUE CAMPOS SE OBTIENEN DE LOS DATOS GENERALES*/

/* DATOS QUE SE REPITEN DE LA TABLA GENERAL EN CORRETAJE <<<<<<<<<<----------ESQUE CREO, PERO PREGUNTARE A MARIO, SI SON LOS DATOS DEL CLIENTE O DEL VENDEDOR DE LA CASA
Gral_NACIMIENTO
Gral_CELULAR
Gral_ESTADOCIVIL
Gral_TEL_CASA
Gral_TELTRABAJO
Gral_REF_NOMBRE1
Gral_REF_CEL_1
Gral_REF_NOMBRE2
Gral_REF_CEL_
Gral_IDCLIENTE
NOMBRE
{
Gral_NOMBRE
Gral_APELLIDOPA
Gral_APELLIDOMA
}
DIRECCI�N
{
Gral_CALLE
Gral_NUMERO
Gral_CP
Gral_COLONIA
Gral_MUNICIPIO
Gral_ESTADO
} */

CREATE TABLE Corretaje(
	--Crt_Idformulario INT IDENTITY(1,1) PRIMARY KEY,
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Crt_Año_compra DATE,
	Crt_Saldo_infonavit MONEY,
	Crt_Recibo_predial_digital BINARY, /*�NO PIDEN CLAVE CATRASTAL?*/
	Crt_Clave_Catastral VARCHAR(10), /*MEJOR LO PONGO, MAS VALE QUE SOBRE*/ /*<<<<No lo pide, no est� en e*/
	Crt_Adeudo_predial MONEY,
	Crt_Recibo_luz_digitalizar VARBINARY,
	Crt_Num_servicio_elect VARCHAR(12),
	Crt_Adeudo_luz MONEY,
	Crt_Recibo_agua_digital VARBINARY,
	Crt_No_cuenta_agua VARCHAR(7), /*EN EL CASO DE CESPT SON 7 NUMEROS*/
	Crt_Adeudo_agua MONEY,
	Crt_Acuerdo BIT, /*CREO QUE SE REFIERE SI TIENE ACUERDO EN LA CESPT*/ /*As� es*/
	Crt_Status VARCHAR(20),
	Crt_Precio VARCHAR(20), /*SUPONGO QUE DE LA CASA, NO SE*/ /*<<< Yo tampoco pero me imagino que es el precio de la casa y el gasto para ponerla al corriente y rehabilitacion*/
	Crt_Gasto VARCHAR(10),  /*NI IDEA*/
	Crt_Tipo_Vivienda VARCHAR(15),	/*<<<<<En esta parte pidi� poder seleccionar entre Casa o Departamento, si es casa que aparezcan opciones de cuantas habitaciones, 
	planta baja, segundaplanta. Si es Departamento que nivel (nivel 1, nivel 2, nivel 3, nivel 4)*/
				/*Crt_PROTOTIPADO VARCHAR(10),*/
	Crt_Obervaciones VARCHAR(300),
	/*LLAVE PRIMARIA DE LA CASA*/
	--Casa_id int FOREIGN KEY REFERENCES Casa(Casa_id),
	--Gral_Idcliente int FOREIGN KEY REFERENCES GENERAL(Gral_Idcliente) /*<<<< De aqu� necesita campos de NSS y Cr�dito*/
);


CREATE TABLE Verificacion(
	--Vfn_Idformulario INT IDENTITY(1,1) PRIMARY KEY,
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Vfn_Persona_fisica BIT, /*�CONOCE A LA PERSONA FISICAMENTE?*/
	Vfn_Visto_persona BIT, /*�LA HA VISTO PERSONALMENTE?*/
	Vfn_Tiempo_estimado BIT, /*�LE DIJO EL TIEMPO ESTIMADO DE ENTREGA?*/
	Vfn_Tiempo VARCHAR(20), /*�QU� TIEMPO LE DIJO EL VENDEDOR?*/
	Vfn_Tiene_costo BIT, /*�LE COMENT� EL ASESOR QUE TIENE UN COSTO?*/
	Vfn_Costo MONEY, /*�QUE COSTO LE DIJO?*/
	Vfn_Trato_asesor VARCHAR(2), /*DEL 1 AL 10 QUE CALIFICACIN LE DA*/
	Vfn_Observaciones VARCHAR(150),
	/*LLAVE FORANEA DE CASA*/
	--Casa_id int FOREIGN KEY REFERENCES Casa(Casa_id),
	/*LLAVE FORANEA DE CLIENTE*/
	--Gral_Idcliente int FOREIGN KEY REFERENCES GENERAL(Gral_Idcliente)
);
/*LLAVE FORANEA DEL CLIENTE Y CASA*/
/* DATOS QUE SE REPITEN DE LA TABLA GENERAL EN VERIFICACION
NOMBRE
{
Gral_NOMBRE
Gral_APELLIDOPA
Gral_APELLIDOMA
}
Gral_CELULAR
ASESOR // NOS HACE FALTA UN CAMPO ASESOR Y TABLAS DE EMPLEADOS O ASESORES?!?!?! <<<<Los empleados son los usuarios y podemos crear una tabla para los asesores, los cuales no conozco
UBICACION // ME IMAGINO QUE LA DIRECCION
*/

CREATE TABLE Habilitacion(
	--Hbt_Idformulario INT IDENTITY(1,1) PRIMARY KEY,
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
/*LLAVE FORANEA DE LA CASA*/
	--Casa_id int FOREIGN KEY REFERENCES Casa(Casa_id)
);

/*TABLAS DE CONTADUR*/
CREATE TABLE Contaduria(
	--Cnt_Idformulario INT IDENTITY(1,1) PRIMARY KEY,
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Cnt_Presupuesto_gestion MONEY,
	Cnt_Presupuesto_corretaje MONEY,
	Cnt_Presupuesto_habilitacion MONEY,
	Cnt_Mensualidad MONEY,
	Cnt_Vigilancia MONEY,
/*LLAVE FORANEA DE CASA*/
	--Casa_id int FOREIGN KEY REFERENCES Casa(Casa_id)
);

/*Tabla para info de Casa*/
CREATE TABLE Casa(
	--Casa_id INT IDENTITY(1,1) PRIMARY KEY,
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Casa_calle VARCHAR(50),
	Casa_numExt VARCHAR(10), --Porsi las moscas
	Casa_numInt VARCHAR(10),  --<----Esto tal vez
	Casa_colonia VARCHAR(50),
	Casa_propietario VARCHAR(100),
    Casa_Alta DATE DEFAULT GETDATE()
	/*ID VENDEDOR <<< AUN EN DUDA*/
)

CREATE TABLE Usuario (
    --usu_id VARCHAR(20) PRIMARY KEY NOT NULL,
	Id INT IDENTITY(1,1) PRIMARY KEY,
    usu_nombre VARCHAR(80),
    usu_apellidoPa VARCHAR(40),    
    usu_apellidoMa VARCHAR(40),
    usu_alta DATE DEFAULT GETDATE(),
    usu_tipo VARCHAR(15), --Puede ser referenciado a la tabla tipo Usuario, es opcional, podemos definir los tipos de usuarios en las vistas
    usu_activo BIT DEFAULT 1
);

CREATE TABLE TipoUsuario(
	--tipusu_id INT IDENTITY (1,1) PRIMARY KEY,
	Id INT IDENTITY(1,1) PRIMARY KEY,
	tipusu_descricion VARCHAR(50)
)

/*GESTION Y CORRETAJE PIDEN DINERO A CONTADURIA*/
/*IDEA DE CREAR UNA TABLA PARA PAGOS DE CORRETAJE Y CONTADURIA Y QUE TENGA UN TOTAL
DEL DINERO QUE SE PIDI� Y DE CUANTO DINERO SE FUE PIDIENDO POR CADA COSA*/