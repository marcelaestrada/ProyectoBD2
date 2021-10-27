CREATE DATABASE RedCruises

CREATE TABLE [Embarcacion] (
	ID_Embarcacion int identity(1,1) NOT NULL PRIMARY KEY,
	Nombre nvarchar(50) NOT NULL,
	VelocidadMax decimal,
	CombustibleMax decimal NOT NULL,
	CantidadCamarotes int NOT NULL,
	CapacidadPasajeros int NOT NULL,
	KilometrosPorTanque decimal NOT NULL
)

CREATE TABLE [Clase] (
	ID_Clase int identity(1,1) NOT NULL PRIMARY KEY,
	Tipo_de_clase int NOT NULL,
	CapacidadPasajeros int NOT NULL,
	Precio money NOT NULL
)

CREATE TABLE [Camarote] (
	NumeroCamarote int identity(1,1) NOT NULL PRIMARY KEY,
	TipoCamarote int NOT NULL,
	Estatus int NOT NULL,
	CapacidadPersonas int NOT NULL,
	ID_Embarcacion int NOT NULL,
	ID_Clase int NOT NULL,
	CONSTRAINT ID_Embarcacion FOREIGN KEY (ID_Embarcacion) REFERENCES Embarcación (ID_Embarcacion),
	CONSTRAINT ID_Clase FOREIGN KEY (ID_Clase) REFERENCES Clase (ID_Clase)
)

CREATE TABLE [Viaje] (
	ID_Viaje int identity(1,1) NOT NULL PRIMARY KEY,
	PasajerosConfirmados int NOT NULL,
	Origen nvarchar(50) NOT NULL,
	FechaSalida datetime NOT NULL,
	FechaLlegada datetime NOT NULL,
	PrecioViaje money NOT NULL,
	HorasNauticas float NOT NULL,
	MillasNauticas float NOT NULL,
)

CREATE TABLE [Ruta] (
	ID_Ruta int identity(1,1) NOT NULL PRIMARY KEY,
	PuertoOrigen nvarchar(50) NOT NULL,
	PuertoDestino nvarchar(50) NOT NULL,
	FechaSalida datetime NOT NULL,
	FechaLlegada datetime NOT NULL,
)

CREATE TABLE [ViajeRuta] (
	ID_Viaje int NOT NULL,
	ID_Ruta int NOT NULL,
	CONSTRAINT ID_Viaje FOREIGN KEY (ID_Viaje) REFERENCES Viaje (ID_Viaje),
	CONSTRAINT ID_Ruta FOREIGN KEY (ID_Ruta) REFERENCES Ruta (ID_Ruta)
)

CREATE TABLE [Persona] (
	ID_Persona int identity(1,1) NOT NULL PRIMARY KEY,
	DPI nvarchar(13) NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	Apellido nvarchar(50) NOT NULL,
	Edad int NOT NULL,
	Peso float NOT NULL,
)

CREATE TABLE [Cliente] (
	ID_Cliente int identity(1,1) NOT NULL PRIMARY KEY,
	ID_Persona int NOT NULL,
	ID_Viaje int NOT NULL,
	CONSTRAINT FK_Viaje FOREIGN KEY (ID_Viaje) REFERENCES Viaje (ID_Viaje),
	CONSTRAINT ID_Persona FOREIGN KEY (ID_Persona) REFERENCES Persona (ID_Persona)
)

CREATE TABLE [Tripulante] (
	ID_Tripulante int identity(1,1) NOT NULL PRIMARY KEY,
	TipoTripulante int NOT NULL,
	ID_Persona int NOT NULL,
	ID_Viaje int NOT NULL,
	CONSTRAINT fkViaje FOREIGN KEY (ID_Viaje) REFERENCES Viaje (ID_Viaje),
	CONSTRAINT fkPersona FOREIGN KEY (ID_Persona) REFERENCES Persona (ID_Persona)
)

CREATE TABLE [Reserva] (
	ID_Reserva int identity(1,1) NOT NULL PRIMARY KEY,
	TiempoConfirmacion float NOT NULL,
	Estatus int NOT NULL,
	FechaReservacion datetime NOT NULL,
	FechaConfirmacion datetime NOT NULL,
	CanalReserva int NOT NULL,
	CanalConfirmacion int NOT NULL,
	ID_Cliente int NOT NULL,
	NumeroCamarote int NOT NULL,
	ID_Viaje int NOT NULL,
	CONSTRAINT fViaje FOREIGN KEY (ID_Viaje) REFERENCES Viaje (ID_Viaje),
	CONSTRAINT FK_Cliente FOREIGN KEY (ID_Cliente) REFERENCES Cliente (ID_Cliente),
	CONSTRAINT FK_Camarote FOREIGN KEY (NumeroCamarote) REFERENCES Camarote (NumeroCamarote)
)