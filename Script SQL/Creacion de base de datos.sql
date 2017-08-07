--CREACION DEL MINI MOUSTRITO
USE master
GO
IF EXISTS(Select * FROM SysDataBases WHERE name='BiosRealState')
BEGIN
	DROP DATABASE BiosRealState
END
GO
CREATE DATABASE BiosRealState
GO
USE BiosRealState
GO
--CREACION DE LA TABLA ZONAS
CREATE TABLE ZONAS(
	letra_departamento char,
	codigo varchar(3),
	nombre varchar(30) not null,
	habitantes int not null,
	--Actividad Logica
	activado bit DEFAULT 1 not null,
	--Claves Primarias
	PRIMARY KEY (letra_departamento,codigo),
	--Chequeos de claves unicas
	CONSTRAINT ZONAS_NOMBRE_UNICO UNIQUE(nombre,letra_departamento)
)
GO
--CREACION DE LA TABLA SERVICIOS (Entidad debil de Zonas)
CREATE TABLE SERVICIOS(
	letra_departamento char,
	codigo_zona varchar(3),
	nombre varchar(30) not null,
	--Primarias
	PRIMARY KEY(letra_departamento,codigo_zona,nombre),
	--Claves foraneas
	CONSTRAINT SERVICIOS_DEPENDENCIA_ZONAS FOREIGN KEY (letra_departamento, codigo_zona) REFERENCES Zonas(letra_departamento, codigo),
	--Chequeos de claves unicas
	CONSTRAINT SERVICIOS_ZONAL_UNICO UNIQUE(letra_departamento,codigo_zona,nombre)
)
GO
--CREACION DE TABLA PROPIEDADES
CREATE TABLE PROPIEDADES(
	padron int PRIMARY KEY,
	direccion varchar(100) not null,
	precio money not null,
	accion varchar(8) not null,
	cantidad_banio int not null,
	cantidad_habitaciones int not null,
	metros_cuadrados decimal not null,
	--Actividad Logica
	activado bit DEFAULT 1,
	--chequear clave numerada
	CONSTRAINT PROPIEDADES_TIPO_ACCION CHECK (accion IN('alquiler', 'venta', 'permuta'))
)
GO
--CREACION DE TABLA CONTIENE (Relacion entre zonas y propiedades)
CREATE TABLE CONTIENE(
	padron_propiedad int,
	codigo_zona varchar(3),
	letra_departamento char,
	--Primarias
	PRIMARY KEY(padron_propiedad,letra_departamento,codigo_zona),
	--Claves foraneas
	CONSTRAINT CONTIENE_DEPENDENCIA_ZONAS FOREIGN KEY (letra_departamento, codigo_zona) REFERENCES Zonas(letra_departamento, codigo),
	CONSTRAINT CONTIENE_DEPENDENCIA_PROPIEDAD FOREIGN KEY (padron_propiedad) REFERENCES Propiedades(padron),
)
GO
--CREACION DE LA TABLA CONSULTAS
CREATE TABLE CONSULTAS(
	telefono varchar(9),
	nombre varchar(50) not null,
	fecha date not null,
	hora varchar(5) not null,
	--Actividad Logica
	activado bit DEFAULT 1,
	--claves Primarias
	PRIMARY KEY (telefono,fecha,hora)
	
)
GO
--CREACION DE LA TABLA REALIZA(relacion entre consulta y propiedad)
CREATE TABLE REALIZA(
	telefono_consulta varchar(9),
	padron_propiedad int,
	fecha_consulta date,
	hora_consulta varchar(5) not null,
	--claves Primarias
	PRIMARY KEY(telefono_consulta,padron_propiedad,fecha_consulta,hora_consulta),
	--Claves foraneas
	CONSTRAINT REALIZA_DEPENDENCIA_CONSULTA FOREIGN KEY (telefono_consulta,fecha_consulta,hora_consulta) REFERENCES CONSULTAS(telefono,fecha,hora),
	CONSTRAINT REALIZA_DEPENDENCIA_PROPIEDAD FOREIGN KEY (padron_propiedad) REFERENCES Propiedades(padron)
)
GO
--CREACION DE LA TABLA EMPLEADOS
CREATE TABLE EMPLEADOS(
	nombre varchar(30) PRIMARY KEY,
	contrasenia varchar(10) not null,
	--Actividad Logica
	activado bit DEFAULT 1 not null	
)
GO
--CREACION DE CAMBIA(relacion entre empleados y propiedades)
CREATE TABLE CAMBIA(
	padron_propiedad int,
	nombre_empleado varchar(30),
	--Claves Primarias
	PRIMARY KEY(padron_propiedad,nombre_empleado),
	--Claves Foraneas
	CONSTRAINT CAMBIA_DEPENDENCIA_EMPLEADO FOREIGN KEY (nombre_empleado) REFERENCES EMPLEADOS(nombre),
	CONSTRAINT CAMBIA_DEPENDENCIA_PROPIEDAD FOREIGN KEY (padron_propiedad) REFERENCES Propiedades(padron)
)
GO
--CREACION DE LA TABLA CASAS (Tipo de Propiedad)
CREATE TABLE CASAS(
	padron int PRIMARY KEY,
	tamanio_terreno DECIMAL,
	jardin BIT,
	--Clave Foranea
	CONSTRAINT CASAS_DEPENDENCIA_PROPIEDAD FOREIGN KEY (padron) REFERENCES Propiedades(padron)
)
GO
--CREACION DE LA TABLA APARTAMENTOS (Tipo de Propiedad)
CREATE TABLE APARTAMENTOS(
	padron int PRIMARY KEY,
	piso int not null,
	ascensor bit not null,
	--Clave Foranea
	CONSTRAINT APARTAMENTOS_DEPENDENCIA_PROPIEDAD FOREIGN KEY (padron) REFERENCES Propiedades(padron)
)
GO
--CREACION DE LA TABLA LOCALES (Tipo de Propiedad)
CREATE TABLE LOCALES(
	padron int PRIMARY KEY,
	habilitacion bit not null,
	--Clave Foranea
	CONSTRAINT LOCALES_DEPENDENCIA_PROPIEDAD FOREIGN KEY (padron) REFERENCES Propiedades(padron)
)
GO

--##########################################Procesos de ALTA##########################################

--Alta de ZONAS
CREATE PROCEDURE AltaZona @letra_departamento char,	@codigo varchar(3), @nombre varchar(30), @habitantes int AS
BEGIN

	--No puede haber 2 zonas con los mismos codigos
	IF EXISTS(SELECT * FROM ZONAS WHERE codigo = @codigo AND letra_departamento = @letra_departamento)
	BEGIN
		PRINT '!) Ya existe una zona con el mismo codigo'
		RETURN -1
	END

	--No puede haber 2 zonas con el mismo nombre en el mismo departamento
	IF EXISTS(SELECT * FROM ZONAS WHERE nombre = @nombre AND letra_departamento = @letra_departamento)
	BEGIN
		PRINT '!) Ya existe una zona con el mismo nombre'
		RETURN -2
	END

	INSERT INTO ZONAS(letra_departamento,codigo,nombre,habitantes) VALUES (@letra_departamento,@codigo,@nombre,@habitantes)
	RETURN @@ERROR
END
GO

--Alta de SERVICIOS
CREATE PROCEDURE AltaServicio @codigo_zona varchar(3),@letra_departamento char,  @nombre varchar(30) AS
BEGIN
	--Tiene que existir la zona
	IF NOT EXISTS(SELECT * FROM ZONAS WHERE codigo= @codigo_zona AND letra_departamento = @letra_departamento)
	BEGIN
		PRINT '!) No existe la zona'
		RETURN -1
	END

	--No puede haber 2 servicios con el mismo nombre en la misma zona
	IF EXISTS(SELECT * FROM SERVICIOS WHERE nombre = @nombre and codigo_zona = @codigo_zona AND letra_departamento = @letra_departamento)
	BEGIN
		PRINT '!) Ya existe un servicio con el mismo nombre'
		RETURN -2
	END

	INSERT INTO SERVICIOS(codigo_zona,letra_departamento, nombre) VALUES (@codigo_zona,@letra_departamento,@nombre)
	RETURN @@ERROR
END
GO

--Alta de PROPIEDADES
CREATE PROCEDURE AltaPropiedad @padron int, @direccion varchar(100), @precio money, @accion varchar(8), @cantidad_banio int,
								@cantidad_habitaciones int, @metros_cuadrados decimal, @codigo_zona varchar(3), @letra_departamento char, @nombre_empleado varchar(30) AS
BEGIN
	--No puede haber 2 propiedades con los mismos padron
	IF EXISTS(SELECT * FROM PROPIEDADES WHERE padron = @padron)
	BEGIN
		PRINT '!) Ya existe una propiedad con el mismo padron'
		RETURN -1
	END
		
	--Verifica si existe Esa zona
	IF NOT EXISTS(SELECT * FROM ZONAS WHERE codigo = @codigo_zona AND letra_departamento = @letra_departamento)
	BEGIN
		PRINT '!) No existe la zona asignada'
		RETURN -2
	END

	--No puede haber valores negativos
	IF @precio < 0 OR @padron < 0 OR @cantidad_banio < 0 OR @cantidad_habitaciones < 0 OR @metros_cuadrados < 0
	BEGIN
		PRINT '!) Uno o Varias variables tiene un valor numerico negativo'
		RETURN -3
	END
	--Averigua si existe el empleado
	IF NOT EXISTS(SELECT * FROM EMPLEADOS WHERE nombre = @nombre_empleado)
	BEGIN
		PRINT '!) No existe el empleado'
		RETURN -4
	END
	--Insertar en tabla PROPIEDADES
	INSERT INTO PROPIEDADES(padron, direccion, precio, accion, cantidad_banio,cantidad_habitaciones, metros_cuadrados)
	VALUES(@padron, @direccion, @precio, @accion, @cantidad_banio,@cantidad_habitaciones, @metros_cuadrados)
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo el alta de Propiedades'
		RETURN -5
	END

	
	--Insertar en tabla CONTIENE
	INSERT INTO CONTIENE VALUES(@padron,@codigo_zona, @letra_departamento)
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo el alta de la relacion'
		RETURN -6
	END

	--Insertar en tabla Cambia
	INSERT INTO CAMBIA(nombre_empleado,padron_propiedad) VALUES(@nombre_empleado,@padron)
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo el alta en apartamento'
		RETURN -7
	END

	RETURN 0
END
GO

--Alta de Casa
CREATE PROCEDURE AltaCasa @padron int, @direccion varchar(100), @precio money, @accion varchar(8), @cantidad_banio int, 
                          @cantidad_habitaciones int, @metros_cuadrados decimal, @codigo_zona varchar(3), @letra_departamento char,
						  @tamanio_terreno decimal, @jardin bit, @nombre_empleado varchar(30) AS
BEGIN
	DECLARE @RETORNO INT
	EXEC  @RETORNO = AltaPropiedad @padron, @direccion , @precio , @accion, @cantidad_banio , 
								@cantidad_habitaciones , @metros_cuadrados , @codigo_zona , @letra_departamento, @nombre_empleado
    IF @RETORNO <> 0 
	BEGIN
		PRINT 'X) Fallo la creacion de la propiedad'
		RETURN @RETORNO
	END
	--Insertar en tabla Casas
	INSERT INTO CASAS VALUES(@padron,@tamanio_terreno,@jardin)
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo el alta en casas'
		RETURN -5
	END
	RETURN 0
END
GO
--Alta de Apartamento
CREATE PROCEDURE AltaApartamento @padron int, @direccion varchar(100), @precio money, @accion varchar(8), @cantidad_banio int, 
                          @cantidad_habitaciones int, @metros_cuadrados decimal, @codigo_zona varchar(3), @letra_departamento char,
						  @ascensor bit, @piso int, @nombre_empleado varchar(30) AS
BEGIN
	DECLARE @RETORNO INT
	EXEC @RETORNO = AltaPropiedad @padron, @direccion , @precio , @accion, @cantidad_banio , 
								@cantidad_habitaciones , @metros_cuadrados , @codigo_zona , @letra_departamento, @nombre_empleado
    IF @RETORNO <> 0 
	BEGIN
		PRINT 'X) Fallo la creacion de la propiedad'
		RETURN @RETORNO
	END

	--Insertar en tabla Apartamento
	INSERT INTO APARTAMENTOS VALUES(@padron,@ascensor,@piso)
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo el alta en apartamento'
		RETURN -5
	END

	
	RETURN 0
END
GO
--Alta de Local
CREATE PROCEDURE AltaLocal @padron int, @direccion varchar(100), @precio money, @accion varchar(8), @cantidad_banio int, 
                          @cantidad_habitaciones int, @metros_cuadrados decimal, @codigo_zona varchar(3), @letra_departamento char,
						  @habilitacion bit, @nombre_empleado varchar(30) AS
BEGIN
	DECLARE @RETORNO INT
	EXEC @RETORNO = AltaPropiedad @padron, @direccion , @precio , @accion, @cantidad_banio , 
								@cantidad_habitaciones , @metros_cuadrados , @codigo_zona , @letra_departamento, @nombre_empleado
    IF @RETORNO <> 0 
	BEGIN
		PRINT 'X) Fallo la creacion de la propiedad'
		RETURN @RETORNO
	END
	--Insertar en tabla Locales
	INSERT INTO LOCALES VALUES(@padron,@habilitacion)
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo el alta en Locales'
		RETURN -5
	END
	RETURN 0
END
GO
--Alta de una Consulta
CREATE PROCEDURE AltaConsulta @telefono varchar(9), @nombre varchar(50), @fecha date, @hora varchar(5), @padron_propiedad int AS
BEGIN
	
	--Verifica si existe esa propiedad
	IF NOT EXISTS(SELECT * FROM PROPIEDADES WHERE padron = @padron_propiedad)
	BEGIN
		PRINT '!) No Existe ese padron'
		RETURN -1
	END

	--La fecha es valida?
	IF @fecha < getdate()
	BEGIN
		PRINT '!) Fecha Invalida'
		RETURN -2
	END

	--Verificar que no haya hecho una consulta pendiente a la misma propiedad
	IF EXISTS(SELECT * FROM REALIZA WHERE telefono_consulta = @telefono AND padron_propiedad = @padron_propiedad AND REALIZA.fecha_consulta > GETDATE())
	BEGIN
		PRINT '!) Ya existe una consulta perdiente'
		RETURN -3
	END

	

	--Verifica que no exista mas de 2 consultas en la misma propiedad
	IF (SELECT COUNT(*) FROM REALIZA WHERE padron_propiedad = @padron_propiedad) > 2 AND @fecha > GETDATE()
	BEGIN
		PRINT '!) Supero Limite de consulta esta propiedad'
		RETURN -4
	END
	
	--Verifica que no exista 2 consultas en la mismo horario
	--SET @HoraMas = DATEADD(hour, 1, @Hora)	

	IF EXISTS(SELECT * FROM REALIZA WHERE telefono_consulta = @telefono AND fecha_consulta = @fecha AND hora_consulta  = @hora ) 
		
	BEGIN
		PRINT '!) Ya Tienes una consulta'
	    RETURN -5
	END

	IF EXISTS(SELECT * FROM REALIZA WHERE padron_propiedad = @padron_propiedad AND fecha_consulta = @fecha AND REALIZA.hora_consulta = @hora)
	BEGIN
		PRINT '!) Ya Existe una consulta en esa fecha'
	    RETURN -6
	END

	--Agregar en Consultas
	INSERT INTO CONSULTAS(telefono,	nombre,	fecha ,	hora) VALUES (@telefono, @nombre, @fecha , @hora)
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo el alta en consulas'
		RETURN -7
	END

	--Agregar en Realiza
	INSERT INTO REALIZA(telefono_consulta,padron_propiedad,fecha_consulta,hora_consulta) VALUES (@telefono, @padron_propiedad,@fecha,@hora)
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo el alta en Realiza'
		RETURN -7
	END
	RETURN 0
END
GO

--Alta de Empleado
CREATE PROCEDURE AltaEmpleado @nombre varchar(30), @contrasenia varchar(10) AS
BEGIN
	--Verifica si hay un usuario con el mismo nombre
	IF EXISTS(SELECT * FROM EMPLEADOS WHERE nombre = @nombre)
	BEGIN
		PRINT '!) Ya existe un empleado con el mismo nombre'
		RETURN -1
	END
	--Solo puede haber una contraseña con 10 digitos
	IF LEN(@contrasenia) <> 10
	BEGIN
		PRINT '!) Contraseña invalida, solo puede tener 10 digitos'
		RETURN -2
	END

	--Inserta en empleado
	INSERT INTO EMPLEADOS(nombre,contrasenia) VALUES (@nombre, @contrasenia)
	RETURN @@ERROR
END
GO

--##########################################Procesos de Datos Pruebas##########################################
--Empleados
EXEC AltaEmpleado 'Maxiro','0123456789'
EXEC AltaEmpleado 'Profe','9876543210'
--Zonas
EXEC AltaZona 'a','A25','La Casona',250000
EXEC AltaZona 'a','FGH','BiosPolis',1000000
--Servicios
EXEC AltaServicio 'A25','a','Hospital de la risa'
EXEC AltaServicio 'A25','a','Almacen de lucho'
EXEC AltaServicio 'A25','a','Queseria Queseyo'

EXEC AltaServicio 'FGH','a','Pool Pulpo'
EXEC AltaServicio 'FGH','a','Hotel extranjero'
EXEC AltaServicio 'FGH','a','YouSql'

--Casas
Exec AltaCasa 12345,'Calle Falsa 1234',28000,'alquiler',2,4,50,'A25','a',25,1,'Maxiro'
Exec AltaCasa 52568,'Avenida Colorada 5487',55000000,'venta',1,2,30,'FGH','a',30,0,'Profe'
--Apartamentos
Exec AltaApartamento 54896,'Bulevar Chetio 1235',75000000,'permuta',3,6,50,'FGH','a',0,101,'Maxiro'
Exec AltaApartamento 25801,'Avenida Casimiro 3587',10000,'alquiler',1,3,20,'A25','a',1,505,'Profe'
Exec AltaApartamento 10133,'Avenida Casimiro 3587',60000000,'venta',1,3,20,'A25','a',1,507,'Maxiro'
--Locales
Exec AltaLocal 78563,'Circus 4569',250000,'alquiler',1,4,50,'A25','a',1,'Maxiro'
Exec AltaLocal 78463,'The Avinius 5155',150000000,'venta',2,4,50,'A25','a',1,'Profe'
Exec AltaLocal 71563,'Circus',250000,'alquiler',1,4,50,'A25','a',1,'Maxiro'
--Consulta
Exec AltaConsulta '091689333','Maxi Camilo','11/09/2017','12:00',52568
Exec AltaConsulta '099662233','Otro Maxi Camilo','11/09/2017','12:00',52568

GO

--###################################################Bajas######################################################

--Baja Zonas
CREATE PROCEDURE BajaZona @codigo varchar(3), @letra_departamento char AS
BEGIN
	--Verifica si existe
	IF NOT EXISTS(select * from zonas where letra_departamento = @letra_departamento and codigo = @codigo AND activado = 1)
	BEGIN
		PRINT '!) No existe la zona o ya fue eliminada'
		RETURN -1
	END	
	--Baja Logica o Fisica?
	IF EXISTS(SELECT * FROM CONTIENE WHERE CONTIENE.codigo_zona = @codigo)
	BEGIN
		UPDATE ZONAS SET activado=0 WHERE codigo = @codigo AND letra_departamento = @letra_departamento
		RETURN @@ERROR
	END 
	ELSE
	BEGIN
		DELETE FROM SERVICIOS WHERE codigo_zona = @codigo
		DELETE FROM ZONAS WHERE  codigo = @codigo
		RETURN @@ERROR
	END
END
GO

--Baja Servicios
CREATE PROCEDURE BajaServicio @codigo_zona varchar(3),  @letra_departamento char, @nombre varchar(30) AS
BEGIN
	--Verifica si existe
	IF NOT EXISTS(SELECT * FROM SERVICIOS WHERE nombre = @nombre AND letra_departamento = @letra_departamento AND codigo_zona = @codigo_zona)
	BEGIN
		PRINT '!) No existe ese servicio'
		RETURN -1
	END

	--Procede a Eliminar
	DELETE FROM SERVICIOS WHERE nombre = @nombre AND letra_departamento = @letra_departamento AND codigo_zona = @codigo_zona
	RETURN @@ERROR	
END
GO

--Baja de Propiedad
CREATE PROCEDURE BajaPropiedad @padron int AS
BEGIN
	--Verifica si existe
	IF NOT EXISTS(SELECT * FROM PROPIEDADES WHERE padron = @padron AND activado = 1)
	BEGIN
		PRINT '!) No existe esta propiedad o ya esta eliminada'
		RETURN -1
	END
	IF 	EXISTS(SELECT * FROM REALIZA WHERE padron_propiedad = @padron)
	BEGIN
		UPDATE PROPIEDADES SET activado=0 WHERE padron = @padron
		PRINT @@ERROR
		RETURN @@ERROR
	END
	ELSE
	BEGIN
		--Borra todas las dependencias huecas
		DELETE FROM CONTIENE WHERE padron_propiedad = @padron
		DELETE FROM CAMBIA WHERE padron_propiedad = @padron
		DELETE FROM CASAS WHERE padron = @padron
		DELETE FROM LOCALES WHERE padron = @padron
		DELETE FROM APARTAMENTOS WHERE padron = @padron
		DELETE FROM PROPIEDADES WHERE padron = @padron
		--[X]verifica	que empleado y zonas no tenga dependencias huecas
		--DELETE FROM EMPLEADOS WHERE activado = 0 AND nombre = (SELECT nombre FROM CAMBIA WHERE padron_propiedad = @padron)
		--DELETE FROM ZONAS WHERE activado = 0 AND codigo = (SELECT codigo_zona FROM CONTIENE WHERE padron_propiedad = @padron) AND
		--																 letra_departamento = (SELECT letra_departamento FROM CONTIENE WHERE padron_propiedad = @padron)
		RETURN @@ERROR
	END
END
GO

--Baja de Consulta
CREATE PROCEDURE BajaConsulta @telefono varchar(9), @fecha date , @hora varchar(5)  AS
BEGIN
	--Verifica si existe
	IF NOT EXISTS(SELECT * FROM CONSULTAS WHERE telefono = @telefono AND fecha = @fecha AND hora = @hora)
	BEGIN
		PRINT '!) No existe esa consulta'
		RETURN -1
	END

	--Procede a Eliminar
	DELETE FROM REALIZA WHERE telefono_consulta = @telefono AND fecha_consulta = @fecha  AND hora_consulta = @hora
	DELETE FROM CONSULTAS WHERE telefono = @telefono AND fecha = @fecha  AND hora = @hora
	RETURN @@ERROR	
END
GO
--Eliminar Empleado
CREATE PROCEDURE BajaEmpleado @nombre varchar(30), @contrasenia varchar(10) AS
BEGIN
	--Verifica si existe ese usuario
	IF NOT EXISTS(SELECT * FROM EMPLEADOS WHERE nombre = @nombre AND activado = 1)
	BEGIN
		PRINT '!)No Existe ese usuario o ya fue eliminado'
		RETURN -1
	END
	--Verifica que tengan la misma contrasenia
	IF NOT EXISTS(SELECT * FROM EMPLEADOS WHERE nombre = @nombre AND contrasenia = @contrasenia)
	BEGIN
		PRINT '!)Contrasenia Invalida'
		RETURN -2
	END

	--Baja Logica o Fisica?
	IF EXISTS(SELECT * FROM CAMBIA WHERE nombre_empleado = @nombre)
	BEGIN
		UPDATE EMPLEADOS SET activado = 0 WHERE nombre = @nombre
	END
	ELSE
	BEGIN
		DELETE FROM EMPLEADOS WHERE nombre = @nombre
	END
END
GO

--###################################################Modificar######################################################

--Modificacion de ZONAS
CREATE PROCEDURE ModificarZona @letra_departamento char,	@codigo varchar(3), @nombre varchar(30), @habitantes int AS
BEGIN

	--No puede haber 2 zonas con los mismos codigos
	IF NOT EXISTS(SELECT * FROM ZONAS WHERE codigo = @codigo AND letra_departamento = @letra_departamento)
	BEGIN
		PRINT '!) No existe esa zona'
		RETURN -1
	END

	--No puede haber 2 zonas con el mismo nombre en el mismo departamento (y que no sea la misma)
	IF EXISTS(SELECT * FROM ZONAS WHERE nombre = @nombre AND codigo <> @codigo AND letra_departamento = @letra_departamento)
	BEGIN
		PRINT '!) Ya existe una zona con el mismo nombre'
		RETURN -2
	END

	UPDATE ZONAS SET activado = 1, nombre = @nombre, habitantes = @habitantes WHERE letra_departamento = @letra_departamento AND codigo = @codigo
	RETURN @@ERROR
END
GO

--Modificacion de PROPIEDADES
CREATE PROCEDURE ModificarPropiedad @padron int, @direccion varchar(100), @precio money, @accion varchar(8), @cantidad_banio int,
								@cantidad_habitaciones int, @metros_cuadrados decimal, @codigo_zona varchar(3), @letra_departamento char, @nombre_empleado varchar(30) AS
BEGIN
	--No puede haber 2 propiedades con los mismos padron
	IF NOT EXISTS(SELECT * FROM PROPIEDADES WHERE padron = @padron)
	BEGIN
		PRINT '!) No existe propiedad con ese padron'
		RETURN -1
	END	

	--No puede haber valores negativos
	IF @precio < 0 OR @padron < 0 OR @cantidad_banio < 0 OR @cantidad_habitaciones < 0 OR @metros_cuadrados < 0
	BEGIN
		PRINT '!) Uno o Varias variables tiene un valor numerico negativo'
		RETURN -2
	END
	--Averigua si existe el empleado
	IF NOT EXISTS(SELECT * FROM EMPLEADOS WHERE nombre = @nombre_empleado)
	BEGIN
		PRINT '!) No existe el empleado'
		RETURN -3
	END
	--Insertar en tabla PROPIEDADES
	UPDATE PROPIEDADES SET direccion = @direccion,accion = @accion, activado = 1 ,cantidad_banio = @cantidad_banio, cantidad_habitaciones = @cantidad_habitaciones,
									      metros_cuadrados = @metros_cuadrados, precio = @precio WHERE padron = @padron
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo la modificacion de Propiedades'
		RETURN -4
	END


	--Cambia el registo de cambio en propiedades
	UPDATE CAMBIA SET nombre_empleado = @nombre_empleado WHERE padron_propiedad = @padron
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo el modificado en empleado (Propiedades)'
		RETURN -5
	END
	RETURN 0
END
GO

--Modificacion de Casa
CREATE PROCEDURE ModificarCasa @padron int, @direccion varchar(100), @precio money, @accion varchar(8), @cantidad_banio int, 
                          @cantidad_habitaciones int, @metros_cuadrados decimal, @codigo_zona varchar(3), @letra_departamento char,
						  @tamanio_terreno decimal, @jardin bit, @nombre_empleado varchar(30) AS
BEGIN
	DECLARE @RETORNO INT
	EXEC  @RETORNO = ModificarPropiedad @padron, @direccion , @precio , @accion, @cantidad_banio , 
								@cantidad_habitaciones , @metros_cuadrados , @codigo_zona , @letra_departamento, @nombre_empleado
    IF @RETORNO <> 0 
	BEGIN
		PRINT 'X) Fallo la modificacion de la propiedad'
		RETURN @RETORNO
	END

	IF NOT EXISTS(SELECT * FROM CASAS WHERE padron = @padron)
	BEGIN
		PRINT 'X) No es una casa'
		RETURN -6	
	END


	--Insertar en tabla Casas
	UPDATE CASAS SET tamanio_terreno = @tamanio_terreno, jardin = @jardin WHERE padron = @padron
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo la modificacion en casas'
		RETURN -5
	END
	RETURN 0
END
GO
--Modificar Apartamento
CREATE PROCEDURE ModificarApartamento @padron int, @direccion varchar(100), @precio money, @accion varchar(8), @cantidad_banio int, 
                          @cantidad_habitaciones int, @metros_cuadrados decimal, @codigo_zona varchar(3), @letra_departamento char,
						  @ascensor bit, @piso int, @nombre_empleado varchar(30) AS
BEGIN
	DECLARE @RETORNO INT
	EXEC @RETORNO = ModificarPropiedad @padron, @direccion , @precio , @accion, @cantidad_banio , 
								@cantidad_habitaciones , @metros_cuadrados , @codigo_zona , @letra_departamento, @nombre_empleado
    IF @RETORNO <> 0 
	BEGIN
		PRINT 'X) Fallo la modificacion de la propiedad'
		RETURN @RETORNO
	END	

	IF NOT EXISTS(SELECT * FROM APARTAMENTOS WHERE padron = @padron)
	BEGIN
		PRINT 'X) No es un apartamento'
		RETURN -6	
	END
	


	UPDATE APARTAMENTOS SET ascensor = @ascensor, piso = @piso WHERE padron = @padron
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo la modificacion en apartamento'
		RETURN -5
	END

	
	RETURN 0
END
GO

--Modificacion de Local
CREATE PROCEDURE ModificarLocal @padron int, @direccion varchar(100), @precio money, @accion varchar(8), @cantidad_banio int, 
                          @cantidad_habitaciones int, @metros_cuadrados decimal, @codigo_zona varchar(3), @letra_departamento char,
						  @habilitacion int, @nombre_empleado varchar(30) AS
BEGIN
	DECLARE @RETORNO INT
	EXEC @RETORNO = ModificarPropiedad @padron, @direccion , @precio , @accion, @cantidad_banio , 
								@cantidad_habitaciones , @metros_cuadrados , @codigo_zona , @letra_departamento, @nombre_empleado
    IF @RETORNO <> 0 
	BEGIN
		PRINT 'X) Fallo la modificacion de la propiedad'
		RETURN @RETORNO
	END

	IF NOT EXISTS(SELECT * FROM LOCALES WHERE padron = @padron)
	BEGIN
		PRINT 'X) No es un local'
		RETURN -6	
	END

	UPDATE LOCALES SET habilitacion = @habilitacion WHERE padron = @padron
	IF @@ERROR <> 0 
	BEGIN
		PRINT 'x) Fallo la modificacion en Locales'
		RETURN -5
	END
	RETURN 0
END
GO

--Modificar Contraseña de Usuario
CREATE PROCEDURE ModificarEmpleado @nombre varchar(30), @contraseniaVieja varchar(10),  @contraseniaNueva varchar(10) AS
BEGIN
	--Solo puede haber una contraseña con 10 digitos
	IF LEN(@contraseniaNueva) <> 10
	BEGIN
		PRINT '!) Contraseña invalida, solo puede tener 10 digitos'
		RETURN -1
	END
	
	--Verifica si existe el usuario
	IF NOT EXISTS(SELECT * FROM EMPLEADOS WHERE nombre = @nombre)
	BEGIN
		PRINT '!) No existe el usuario'
		RETURN -2
	END

	--Verifica si la contraseña esta bien
	IF NOT EXISTS(SELECT * FROM EMPLEADOS WHERE nombre = @nombre AND contrasenia = @contraseniaVieja)
	BEGIN
		PRINT '!) Contraseñia invalida'
		RETURN -3
	END

	UPDATE EMPLEADOS SET contrasenia = @contraseniaNueva WHERE nombre = @nombre
	RETURN @@ERROR

END
GO
--###################################################Verificadores######################################################

--Verifica la Zona
CREATE PROCEDURE VerificarZona @letra_departamento char, @codigo varchar(3), @nombre varchar(30) AS
BEGIN
	DECLARE @retorno INT = 0
	IF EXISTS(SELECT * FROM ZONAS WHERE codigo = @codigo AND letra_departamento = @letra_departamento)
	BEGIN
		SET @retorno = 1
	END

	--No puede haber 2 zonas con el mismo nombre en el mismo departamento
	IF EXISTS(SELECT * FROM ZONAS WHERE nombre = @nombre AND letra_departamento = @letra_departamento)
	BEGIN
		SET @retorno = @retorno + 2
	END
	RETURN @retorno
END
GO

--Verifica el servicio
CREATE PROCEDURE VerificServicio @codigo_zona varchar(3),  @nombre varchar(30) AS
BEGIN
	--No puede haber 2 servicios con el mismo nombre en la misma zona
	IF EXISTS(SELECT * FROM SERVICIOS WHERE nombre = @nombre and codigo_zona = @codigo_zona)
	BEGIN
		RETURN 1
	END
	ELSE
	BEGIN
		RETURN 0
	END	
END
GO

--Verificador de Propiedad
CREATE PROCEDURE VerificarPropiedad @padron int, @codigo_zona varchar(3), @letra_departamento char, @nombre_empleado varchar(30) AS
BEGIN
	DECLARE @retorno INT = 0
	--No puede haber 2 propiedades con los mismos padron
	IF EXISTS(SELECT * FROM PROPIEDADES WHERE padron = @padron)
	BEGIN
		SET @retorno = 1
	END	

	--Existen las propiedades
	IF NOT EXISTS(SELECT * FROM ZONAS WHERE letra_departamento = @letra_departamento AND codigo = @codigo_zona)
	BEGIN
		SET @retorno = @retorno + 2
	END

	--Averigua si existe el empleado
	IF NOT EXISTS(SELECT * FROM EMPLEADOS WHERE nombre = @nombre_empleado)
	BEGIN
		SET @retorno = @retorno + 4
	END	
	RETURN @retorno
END
GO

--Verificar Consulta
CREATE PROCEDURE VerificarConsulta @telefono varchar(9), @padron_propiedad int, @fecha date AS
BEGIN
	DECLARE @retorno INT = 0
	--Verifica si existe esa propiedad
	IF NOT EXISTS(SELECT * FROM PROPIEDADES WHERE padron = @padron_propiedad)
	BEGIN
		SET @retorno = 1
	END

	--Verificar que no haya hecho una consulta pendiente a la misma propiedad
	IF EXISTS(SELECT * FROM REALIZA WHERE telefono_consulta = @telefono AND padron_propiedad = @padron_propiedad AND REALIZA.fecha_consulta > GETDATE())
	BEGIN
		SET @retorno = @retorno + 2
	END

	--Verifica que no exista mas de 2 consultas en la misma propiedad
	IF (SELECT COUNT(*) FROM REALIZA WHERE padron_propiedad = @padron_propiedad) > 2
	BEGIN
		SET @retorno = @retorno + 4
	END
	
	RETURN @retorno
END
GO

--Verificador de Empleado al dar alta
CREATE PROCEDURE VerificarEmpleadoAlta @nombre varchar(30), @contrasenia varchar(10) AS
BEGIN
	--Verifica si hay un usuario con el mismo nombre
	IF EXISTS(SELECT * FROM EMPLEADOS WHERE nombre = @nombre)
	BEGIN
		RETURN 1
	END
	ELSE
	BEGIN
		RETURN 0
	END
END
GO

--Verifica si el inicio de sesion es valido
CREATE PROCEDURE InicioSesion @nombre varchar(30), @contrasenia varchar(10) AS
BEGIN
	--Verifica si existe el usuario
	IF NOT EXISTS(SELECT * FROM EMPLEADOS WHERE nombre = @nombre AND activado = 1)
	BEGIN
		RETURN 1
	END

	--Verifica si la contraseña es correcta
	IF NOT EXISTS(SELECT * FROM EMPLEADOS WHERE nombre = @nombre AND contrasenia = @contrasenia AND activado = 1)
	BEGIN
		RETURN 2
	END

	RETURN 0
END
GO

--###################################################Listados (Vistas)######################################################

--select * from LISTADO_APARTAMENTOS_ACTIVAS 

--Listado de todas las zona activas
CREATE VIEW LISTADO_ZONAS_ACTIVAS AS
	SELECT nombre,letra_departamento,codigo,habitantes FROM ZONAS WHERE activado = 1
GO
--Listado de todas las zona activas
CREATE VIEW LISTADO_ZONAS AS
	SELECT nombre,letra_departamento,codigo,habitantes, activado AS 'Esta Activo?' FROM ZONAS
GO
--Listado de Propiedades Activas
CREATE VIEW LISTADO_PROPIEDADES_ACTIVAS AS
	SELECT ZONAS.nombre AS 'nombre_zona', padron, CONTIENE.letra_departamento, CONTIENE.codigo_zona, accion, cantidad_banio, cantidad_habitaciones, direccion, metros_cuadrados, precio, CAMBIA.nombre_empleado FROM PROPIEDADES 
	INNER JOIN CONTIENE ON PROPIEDADES.padron = CONTIENE.padron_propiedad 	
	INNER JOIN  CAMBIA ON PROPIEDADES.padron = CAMBIA.padron_propiedad
	INNER JOIN ZONAS ON ZONAS.letra_departamento = CONTIENE.letra_departamento AND ZONAS.codigo = CONTIENE.codigo_zona
	 WHERE PROPIEDADES.activado = 1
	
	
GO
--Listado de Propiedades
CREATE VIEW LISTADO_PROPIEDADES AS
	SELECT padron, CONTIENE.letra_departamento, CONTIENE.codigo_zona, accion, cantidad_banio, cantidad_habitaciones, direccion, metros_cuadrados, precio,activado AS 'Esta Activo?'  FROM PROPIEDADES 
	INNER JOIN CONTIENE ON PROPIEDADES.padron = CONTIENE.padron_propiedad 
GO

--Listado de Casas activas
CREATE VIEW LISTADO_CASAS_ACTIVAS AS
	SELECT LISTADO_PROPIEDADES_ACTIVAS.*, CASAS.jardin, CASAS.tamanio_terreno FROM LISTADO_PROPIEDADES_ACTIVAS INNER JOIN CASAS ON LISTADO_PROPIEDADES_ACTIVAS.padron = CASAS.padron
GO
--Listado de Casas
CREATE VIEW LISTADO_CASAS AS
	SELECT LISTADO_PROPIEDADES.*, CASAS.jardin, CASAS.tamanio_terreno FROM LISTADO_PROPIEDADES INNER JOIN CASAS ON LISTADO_PROPIEDADES.padron = CASAS.padron
GO

--Listado de Apartamentos activos
CREATE VIEW LISTADO_APARTAMENTOS_ACTIVAS AS
	SELECT LISTADO_PROPIEDADES_ACTIVAS.*, APARTAMENTOS.ascensor, APARTAMENTOS.piso FROM LISTADO_PROPIEDADES_ACTIVAS INNER JOIN APARTAMENTOS ON LISTADO_PROPIEDADES_ACTIVAS.padron = APARTAMENTOS.padron
GO
--Listado de Apartamentos
CREATE VIEW LISTADO_APARTAMENTOS AS
	SELECT LISTADO_PROPIEDADES.*, APARTAMENTOS.ascensor, APARTAMENTOS.piso FROM LISTADO_PROPIEDADES INNER JOIN APARTAMENTOS ON LISTADO_PROPIEDADES.padron = APARTAMENTOS.padron
GO

--Listado de locales activos
CREATE VIEW LISTADO_LOCALES_ACTIVAS AS
	SELECT LISTADO_PROPIEDADES_ACTIVAS.*, LOCALES.habilitacion FROM LISTADO_PROPIEDADES_ACTIVAS INNER JOIN LOCALES ON LISTADO_PROPIEDADES_ACTIVAS.padron = LOCALES.padron
GO
--Listado de locales
CREATE VIEW LISTADO_LOCALES AS
	SELECT LISTADO_PROPIEDADES.*, LOCALES.habilitacion FROM LISTADO_PROPIEDADES INNER JOIN LOCALES ON LISTADO_PROPIEDADES.padron = LOCALES.padron
GO

--Listado de todas las consultas de la base de datos
CREATE VIEW LISTADO_TODAS_CONSULTAS AS
	SELECT REALIZA.padron_propiedad, CONSULTAS.* FROM CONSULTAS INNER JOIN REALIZA ON REALIZA.telefono_consulta = CONSULTAS.telefono
GO

--Listado de todos los empleados activos
CREATE VIEW LISTADO_EMPLEADOS_ACTIVAS AS
	SELECT EMPLEADOS.nombre FROM EMPLEADOS WHERE activado = 1
GO

--Listado de todos los empleados
CREATE VIEW LISTADO_EMPLEADOS AS
	SELECT EMPLEADOS.nombre, activado AS 'Esta Activo?' FROM EMPLEADOS
GO

--###################################################Listados Inteligentes(Funciones)######################################################

--Listar Consultas en una propiedad 
CREATE FUNCTION LISTADO_CONSULTAS_PROPIEDAD (@padron INT)
RETURNS TABLE
AS
RETURN
   SELECT CONSULTAS.*, REALIZA.padron_propiedad FROM CONSULTAS INNER JOIN REALIZA ON CONSULTAS.telefono = REALIZA.telefono_consulta
   WHERE REALIZA.padron_propiedad = @padron and fecha_consulta > GETDATE()
GO

--Detallar Zona de una propiedad
CREATE FUNCTION DETALLAR_ZONA_PADRON (@padron INT)
RETURNS TABLE
AS
RETURN
   SELECT ZONAS.nombre, ZONAS.codigo,ZONAS.letra_departamento, ZONAS.habitantes FROM ZONAS INNER JOIN CONTIENE 
   ON CONTIENE.letra_departamento = ZONAS.letra_departamento AND CONTIENE.codigo_zona = ZONAS.codigo 
   WHERE CONTIENE.padron_propiedad = @padron
GO

--Listar Propiedades en una Zona 
CREATE FUNCTION LISTADO_PROPIEDADES_ZONA (@codigo varchar(3), @letra_departamento char )
RETURNS TABLE
AS
RETURN
   SELECT PROPIEDADES.padron,PROPIEDADES.direccion, PROPIEDADES.metros_cuadrados, PROPIEDADES.accion, PROPIEDADES.cantidad_habitaciones, PROPIEDADES.cantidad_banio FROM PROPIEDADES
   INNER JOIN CONTIENE ON PROPIEDADES.padron = CONTIENE.padron_propiedad WHERE CONTIENE.letra_departamento = @letra_departamento AND codigo_zona = @codigo AND PROPIEDADES.activado = 1
GO
 
 --Listar Agregaciones y Modificaciones en Propiedades hecho por un empleado
 CREATE FUNCTION LISTADO_ACCIONES_EMPLEADO (@nombre varchar(30))
 RETURNS TABLE
AS
RETURN
   SELECT PROPIEDADES.padron,PROPIEDADES.direccion, PROPIEDADES.metros_cuadrados, PROPIEDADES.accion, PROPIEDADES.cantidad_habitaciones, PROPIEDADES.cantidad_banio,PROPIEDADES.precio, CONTIENE.codigo_zona, CONTIENE.letra_departamento FROM PROPIEDADES
   INNER JOIN CAMBIA ON CAMBIA.padron_propiedad = PROPIEDADES.padron  INNER JOIN CONTIENE ON CONTIENE.padron_propiedad = PROPIEDADES.padron WHERE CAMBIA.nombre_empleado = @nombre
GO

--Lista el empleado que modifico la zona
CREATE FUNCTION LISTADO_EMPLEADO_PROPIEDAD (@padron INT)
RETURNS TABLE
AS
RETURN
	SELECT CAMBIA.nombre_empleado AS 'nombre' FROM CAMBIA WHERE CAMBIA.padron_propiedad = @padron
GO

--Listar los servicios de una zona
 CREATE FUNCTION LISTADO_SERVICIOS_ZONA(@codigo varchar(3), @letra_departamento char )
 RETURNS TABLE
AS
RETURN
	SELECT SERVICIOS.nombre FROM SERVICIOS WHERE codigo_zona = @codigo AND letra_departamento = @letra_departamento
GO

--###################################################Constructores######################################################

--Zona
 CREATE FUNCTION SELECCIONAR_ZONA(@codigo varchar(3), @letra_departamento char )
 RETURNS TABLE
AS
RETURN
	SELECT ZONAS.* FROM ZONAS WHERE codigo = @codigo AND letra_departamento = @letra_departamento
GO
--Propiedades
CREATE FUNCTION SELECCIONAR_PROPIEDAD (@padron int )
RETURNS TABLE
AS
RETURN
   SELECT PROPIEDADES.padron,PROPIEDADES.direccion, PROPIEDADES.metros_cuadrados, PROPIEDADES.accion, PROPIEDADES.cantidad_habitaciones, PROPIEDADES.cantidad_banio,PROPIEDADES.precio, CONTIENE.codigo_zona, CONTIENE.letra_departamento, CAMBIA.nombre_empleado FROM PROPIEDADES
   INNER JOIN CONTIENE ON PROPIEDADES.padron = CONTIENE.padron_propiedad 
   INNER JOIN CAMBIA ON CAMBIA.padron_propiedad = PROPIEDADES.padron
   WHERE PROPIEDADES.padron = @padron
GO

--Casa
CREATE FUNCTION SELECCIONAR_CASA (@padron int )
RETURNS TABLE
AS
RETURN
   SELECT PROPIEDADES.padron,PROPIEDADES.direccion, PROPIEDADES.metros_cuadrados, PROPIEDADES.accion, PROPIEDADES.cantidad_habitaciones, PROPIEDADES.cantidad_banio, CASAS.jardin, CASAS.tamanio_terreno, CONTIENE.codigo_zona, CONTIENE.letra_departamento,PROPIEDADES.precio FROM PROPIEDADES
   INNER JOIN CONTIENE ON PROPIEDADES.padron = CONTIENE.padron_propiedad INNER JOIN CASAS ON CASAS.padron = PROPIEDADES.padron WHERE PROPIEDADES.padron = @padron
GO

--APARTAMENTO
CREATE FUNCTION SELECCIONAR_APARTAMENTO (@padron int )
RETURNS TABLE
AS
RETURN
   SELECT PROPIEDADES.padron,PROPIEDADES.direccion, PROPIEDADES.metros_cuadrados, PROPIEDADES.accion, PROPIEDADES.cantidad_habitaciones, PROPIEDADES.cantidad_banio, APARTAMENTOS.ascensor,APARTAMENTOS.piso, CONTIENE.codigo_zona, CONTIENE.letra_departamento,PROPIEDADES.precio FROM PROPIEDADES
   INNER JOIN CONTIENE ON PROPIEDADES.padron = CONTIENE.padron_propiedad INNER JOIN APARTAMENTOS ON APARTAMENTOS.padron = PROPIEDADES.padron WHERE PROPIEDADES.padron = @padron
GO

--LOCAL
CREATE FUNCTION SELECCIONAR_LOCAL (@padron int )
RETURNS TABLE
AS
RETURN
   SELECT PROPIEDADES.padron,PROPIEDADES.direccion, PROPIEDADES.metros_cuadrados, PROPIEDADES.accion, PROPIEDADES.cantidad_habitaciones, PROPIEDADES.cantidad_banio, LOCALES.habilitacion, CONTIENE.codigo_zona, CONTIENE.letra_departamento,PROPIEDADES.precio FROM PROPIEDADES
   INNER JOIN CONTIENE ON PROPIEDADES.padron = CONTIENE.padron_propiedad INNER JOIN LOCALES ON LOCALES.padron = PROPIEDADES.padron WHERE PROPIEDADES.padron = @padron
GO

--Consulta
CREATE FUNCTION SELECCIONAR_CONSULTA (@telefono int, @fecha Date)
RETURNS TABLE
AS
RETURN
   SELECT * FROM CONSULTAS WHERE telefono = @telefono AND fecha = @fecha
GO


--select * from LISTADO_SERVICIOS_ZONA('143', 'K' )
--select * from LISTADO_PROPIEDADES_ACTIVAS
--select * from SELECCIONAR_PROPIEDAD(52568)
--select * from DETALLAR_ZONA_PADRON(12345)
--select * from LISTADO_EMPLEADO_PROPIEDAD(12345)
--select * from LISTADO_CONSULTAS_PROPIEDAD(52568)
select * from EMPLEADOS
--DECLARE @RETORNO INT
--Exec @RETORNO = AltaConsulta '25262475', 'Hola','12/7/2017','14:00',52568
--SELECT @RETORNO


--SELECT * FROM REALIZA WHERE telefono_consulta = '25262475' AND padron_propiedad = 52568 AND REALIZA.fecha_consulta > GETDATE()

--INSERT INTO CONSULTAS VALUES ('25262475', 'Hola','12/12/2016','14:00',52568)
--INSERT INTO REALIZA(padron_propiedad,telefono_consulta,fecha_consulta,hora_consulta) VALUES(52568,'25262475','12/12/2016','14:00')

--DECLARE @RETORNO INT
--Exec @RETORNO = AltaConsulta '25262475', 'Hola','12/20/2017','14:20',52568
--SELECT @RETORNO

--DECLARE @RETORNO INT
--Exec @RETORNO = AltaConsulta '25262475', 'Hola','12/20/2017','13:25',12345
--SELECT @RETORNO

--select * from LISTADO_ACCIONES_EMPLEADO ('Maxi')

--select * from PROPIEDADES
--select * from LOCALES


--DECLARE @RETORNO INT
--Exec @RETORNO = BajaZona 'a', 'A26'
--SELECT @RETORNO
--DECLARE @RETORNO INT
--Exec @RETORNO = AltaCasa 1585, 'Estivao 1558', 12235, 'venta', 5, 15, 50, '26a', 'a', 'Maxi',1,125
--SELECT @RETORNO
--EXEC AltaZona 'a','A25','La Casona',250000
