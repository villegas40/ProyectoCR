CREATE DATABASE CASASRED;
USE CASASRED

CREATE TABLE GENERAL(
Gral_IDCLIENTE INT IDENTITY(1,1) PRIMARY KEY,
Gral_NOMBRE VARCHAR(80),
Gral_APELLIDOPA VARCHAR(40),
Gral_APELLIDOMA VARCHAR(40),
Gral_NACIMIENTO DATE,
Gral_NSS VARCHAR(11),
Gral_CURP VARCHAR(18),
Gral_RFC VARCHAR(13),
Gral_LUGARNACI VARCHAR(70),
Gral_CALLE VARCHAR(30),
Gral_NUMERO VARCHAR(20),
Gral_CP VARCHAR(8),
Gral_COLONIA VARCHAR(40),
Gral_MUNICIPIO VARCHAR(40),
Gral_ESTADO VARCHAR(40),
Gral_CELULAR VARCHAR(10),
Gral_TEL_CASA VARCHAR(10),
Gral_ESTADOCIVIL VARCHAR(20),
Gral_REGIMEN_MATRIMONIAL VARCHAR(25), /*Sociedad Conyugal  O  Separaci�n de bienes*/
Gral_OCUPACION VARCHAR(30),
Gral_TELTRABAJO VARCHAR(15),
Gral_CORREO VARCHAR(320),
Gral_IDENTIFICACION VARCHAR(40),
Gral_NO_IDENTIFICACION INT,
Gral_REF_NOMBRE1 VARCHAR(160),
Gral_REF_CEL_1 VARCHAR(10),
Gral_REF_NOMBRE2 VARCHAR(160),
Gral_REF_CEL_2 VARCHAR(10),
    Gral_Alta DATE DEFAULT GETDATE()
);

/*CONYUGE SI ESTA CASADO CHECAR NUEVA TABLA*/
/*CREAR REFENCIAS DE TABLAS*/
CREATE TABLE CONYUGE(
Cyg_NOMBRE VARCHAR(80),
Cyg_APELLIDOPA VARCHAR(40),
Cyg_APELLIDOMA VARCHAR(40),
Cyg_NACIMIENTO DATE,
Cyg_NSS VARCHAR(11),
Cyg_CURP VARCHAR(18),
Cyg_RFC VARCHAR(13),
Cyg_LUGARMACI VARCHAR(70),
Cyg_CELULAR VARCHAR(10),
Cyg_TEL_CASA VARCHAR(10),
Cyg_OCUPACION VARCHAR(30),
Cyg_TEL_TRABAJO VARCHAR(15),
Cyg_CORREO VARCHAR(320),
Cyg_IDENTIFICACION VARCHAR(40),
Cyg_NO_IDENTIFICACION INT
);

/*Agregar FK*/
CREATE TABLE GESTION(
Gtn_ID INT IDENTITY(1,1) PRIMARY KEY,
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
/*AGEREGAR FK CLIENTE*/
);

/*AGEREGAR FK*/ /*CHECAR QUE CAMPOS SE OBTIENEN DE LOS DATOS GENERALES*/

/* DATOS QUE SE REPITEN DE LA TABLA GENERAL EN CORRETAJE
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

CREATE TABLE CORRETAJE(
Crt_ID_FORMULARIO INT IDENTITY(1,1) PRIMARY KEY,
Crt_A�O_COMPRA DATE,
Crt_SALDO_INFONAVIT MONEY,
Crt_RECIBO_PREDIAL_DIGITAL VARBINARY, /*�NO PIDEN CLAVE CATRASTAL?*/
Crt_CLAVE_CATRASTAL VARCHAR(10), /*MEJOR LO PONGO, MAS VALE QUE SOBRE*/
Crt_ADEUDO_PREDIAL MONEY,
Crt_RECIBO_LUZ_DIGITAL VARBINARY,
Crt_NO_SERVICIO_ELECT VARCHAR(12),
Crt_ADEUDO_LUZ MONEY,
Crt_RECIBO_AGUA_DIGITAL VARBINARY,
Crt_NO_CUENTA_AGUA VARCHAR(7), /*EN EL CASO DE CESPT SON 7 NUMEROS*/
Crt_ADEUDO_AGUA MONEY,
Crt_ACUERDO BIT, /*CREO QUE SE REFIERE SI TIENE ACUERDO EN LA CESPT*/
Crt_STATUS VARCHAR(20),
Crt_PRECIO VARCHAR(20), /*SUPONGO QUE DE LA CASA, NO SE*/
Crt_GASTO VARCHAR(10),  /*NI IDEA*/
Crt_PROTOTIPADO VARCHAR(10),
Crt_OBSERVACIONES VARCHAR(300)
);


CREATE TABLE VERIFICACION(
Vfn_ID_FORMULARIO INT IDENTITY(1,1) PRIMARY KEY,
Vfn_PERSONA_FISICA BIT, /*�CONOCE A LA PERSONA FISICAMENTE?*/
Vfn_VISTO_PERSONAL BIT, /*�LA HA VISTO PERSONALMENTE?*/
Vfn_TIEMPO_ESTIMADO BIT, /*�LE DIJO EL TIEMPO ESTIMADO DE ENTREGA?*/
Vfn_TIEMPO VARCHAR(20), /*�QU� TIEMPO LE DIJO EL VENDEDOR?*/
Vfn_TIENE_COSTO BIT, /*�LE COMENT� EL ASESOR QUE TIENE UN COSTO?*/
Vfn_COSTO MONEY, /*�QUE COSTO LE DIJO?*/
Vfn_TRATO_ASESOR VARCHAR(2), /*DEL 1 AL 10 QUE CALIFICACIN LE DA*/
Vfn_OBSERVACIONES VARCHAR(150)
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
ASESOR // NOS HACE FALTA UN CAMPO ASESOR Y TABLAS DE EMPLEADOS O ASESORES?!?!?!
UBICACION // ME IMAGINO QUE LA DIRECCION
*/

CREATE TABLE HABILITACION(
Hbt_ID_FORMULARIO INT IDENTITY(1,1) PRIMARY KEY,
Hbt_PUERTAS BIT,
Hbt_CHAPAS BIT,
Hbt_MARCOS_PUERTAS BIT,
Hbt_BISAGRAS BIT,
Hbt_TAZA BIT,
Hbt_LAVAMANOS BIT,
Hbt_BASTAGO BIT,
Hbt_CHAPETON BIT,
Hbt_MANERAL BIT,
Hbt_REGADERA_COMPLETA BIT,
Hbt_KIT_LAVAMANOS BIT,
Hbt_KIT_TAZA BIT,
Hbt_ROSETAS BIT,
Hbt_APAGADOR_SENCILLO BIT,
Hbt_CONECTOR_SENCILLO BIT,
Hbt_APAGOR_DOBLE BIT,
Hbt_CONECTOR_APAGADOR BIT,
Hbt_DOMO BIT,
Hbt_VENTANAS BIT,
Hbt_CABLEADO BIT,
Hbt_CALIBRE_CABLEADO VARCHAR(10),
Hbt_BREAK_INTERIOR BIT,
Hbt_BREAK_MEDIDOR BIT,
Hbt_PINTURAS BIT
/*LLAVE FORANEA DE LA CASA*/
);

/*TABLAS DE CONTADUR*/
CREATE TABLE CONTADURIA(
Cnt_ID_FORMULARIO INT IDENTITY(1,1) PRIMARY KEY,
Cnt_PRESUPUESTO_GESTION MONEY,
Cnt_PRESUPUESTO_CORRETAJE MONEY,
Cnt_PRESUPUESTO_HABILATIACION MONEY,
Cnt_MENSUALIDAD MONEY,
Cnt_VIGILANCIA MONEY
/*LLAVE FORANEA DE CASA*/
);

/*Tabla para info de Casa*/
CREATE TABLE Casa(
Casa_id INT IDENTITY(1,1) PRYMARY KEY,
Casa_calle VARCHAR(50),
Casa_numExt VARCHAR(10) --Porsi las moscas
Casa_numInt VARCHAR(10),  --<----Esto tal vez
Casa_colonia VARCHAR(50),
Casa_propietario VARCHAR(100),
    Casa_Alta DATE DEFAULT GETDATE()
)

CREATE TABLE Usuario (
    usu_id VARCHAR(20) PRIMARY KEY NOT NULL,
    usu_nombre VARCHAR(80),
    usu_apellidoPa VARCHAR(40),    
    usu_apellidoMa VARCHAR(40),
    usu_alta DATE DEFAULT GETDATE(),
    usu_tipo VARCHAR(15),
    usu_activo BIT DEFAULT 1
);