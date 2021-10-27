# ProyectoBD2
Base de datos para la empresa RED-CRUISES

## Diseño de la base de datos
### Tabla EMBARCACION
#### ID_Embarcacion 
- Identificador interno del crucero / embarcación, con este identificador se puede desplegar la información completa de la embarcación. 
#### Nombre
- Nombre con el que se le reconoce al crucero o embarcación, este es el nombre comercial. 
#### VelocidadMax 
- Velocidad máxima alcanzada por la embarcación al momento de estar en un viaje. 
#### CombustibleMax
- Cantidad máxima de combustible que puede contener la embarcación en los tanques. 
#### CantidadCamarotes
- Cantidad específica de camarotes que contiene la embarcación. 
#### CapacidadPasajeros
- Cantidad de pasajeros (incluyendo al personal) que soporta la embarcación.
#### KilometrosPorTanque
- Cantidad de kilómetros que puede recorrer la embarcación cuando está al máximo de combustible.


### Tabla CLASE
#### ID_Clase
- Identificador interno de la clase.
#### Tipo_de_clase
- Tipos de clase dentro de un viaje, es la que el cliente pague o contrate. 
#### CapacidadPasajeros
- Cantidad de pasajeros que se pueden atender en cierta clase. 
#### Precio
- Precio adicional, al precio del viaje, según la clase. Las clases más exclusivas tienen un mayor precio. 

### Tabla VIAJE
#### ID_Viaje
- Identificador interno del viaje
#### PasajerosConfirmados
- Cantidad de pasajeros que han confirmado su reserva, este número está en actualización constante, cada vez que un cliente confirma este número aumenta. 
#### Origen
- País de origen del viaje (este será también el destino ya que las embarcaciones salen y vuelven al mismo punto). 
#### FechaSalida
- Fecha en la que sale la embarcación del puerto.
#### FechaLlegada
- Fecha en la que regresa la embarcación al puerto. 
#### PrecioViaje
- Precio base del viaje (a este se le agrega el precio de la clase adquirida por el cliente).
#### HorasNauticas
- Horas que la embarcación se tardó o se estima que se tardará en total en todo el viaje (contando únicamente las horas en las que está avanzando, no en puertos).
#### MillasNauticas
- Cantidad recorrida por la embarcación durante el viaje. 

### Tabla RUTA
#### ID_Ruta
- Identificador interno de la ruta.
#### PuertoOrigen
- Puerto del que saldrá la embarcación en esa ruta.
#### PuertoDestino
- Puerto al que llegará la embarcación en esa ruta.
#### FechaSalida
- Fecha en la que sale del puerto origen con destino al puerto destino.
#### FechaLlegada
- Fecha en la que llega al puerto destino.

### Tabla VIAJERUTA
Tabla intermedia entre viaje y ruta, funciona como enlace, más de un viaje puede tener la misma ruta pero no en la misma fecha. 

### Tabla PERSONA
#### ID_Persona
- Identificador interno de la persona.
#### DPI
- Documento personal de identificación de la persona, si no es una persona proveniente de Guatemala, en este espacio va la identificación o pasaporte de la persona.
#### Nombre
- Nombre de la persona.
#### Apellido
- Apellido de la persona.
#### Edad
- Edad de la persona.
#### Peso
- Peso de la persona. 

### Tabla CLIENTE
Tabla intermedia entre la persona y el viaje, esta tiene un identificador interno tambien llamado ID_Cliente. 

### Tabla TRIPULANTE
#### ID_Tripulante
- Identificador interno del tripulante. 
#### TipoTripulante
- Con tripulante nos referimos a las personas que irán en la embarcación en un viaje específico pero que son empleados o cualquier otro tipo diferente a cliente. 
- Tipos: 
1. Capitán 
2. Seguridad
3. Atención al cliente
4. Atención en restaurante
5. Encargado de tienda
#### ID_Persona
Enlace con la información personal de la persona.
#### ID_Viaje
Enlace con el viaje en el que está asignado. 

### Tabla CAMAROTE
#### NumeroCamarote
- Numero e identificador del camarote.
#### TipoCamarote
- Depende de la cantidad de personas que pueden ocuparlo y de la clase a la que fue asignado. 
- Tipos: 
1. Individual
2. Doble
3. Triple
4. Suite
#### Estatus
- Estado del camarote, puede estar reservado, en espera de confirmación o desocupado.
- Estados: 
0. En espera de confirmación
1. Reservado
2. Libre
#### CapacidadPersonas
- Cantidad de personas que caben en el camarote, en el individual es una, en el doble son dos, en el triple son tres y en la suite son cuatro o cinco máximo.
#### ID_Embarcacion
- Enlace con la embarcación a la que pertenece el camarote.
#### ID_Clase
- Clase en la que se usa el camarote. 

### Tabla RESERVA
#### ID_Reserva
- Identificador y numero de la reserva.
#### TiempoConfirmacion
- Tiempo que fue indicado por el cliente para confirmar su reservación.
#### Estatus
- Estado de la reservación, de igual forma puede estar confirmado, en espera, en cola (en el caso de no haber cupo) o cancelada (si el cliente la cancela).
- Estados: 
0. En espera de confirmación 
1. Confirmada 
2. En cola
3. Cancelada
#### FechaReservacion
- Fecha en la que se realizó la reservación.
#### FechaConfirmacion
- Fecha en la que se confirmó la reservación.
#### CanalReserva
- Canal por el que se realizó la reservación. 
- Canales: 
1. Por teléfono
2. En la página web
3. En las oficinas
#### CanalConfirmacion
- Canal por el que se realizó la confirmación de la reservación.
- Canales: 
1. Por teléfono
2. En la página web
3. En las oficinas
#### ID_Cliente
- Enlace con el cliente que realizó la reservación y su información.
#### NumeroCamarote
- Número de camarote que se le asignó al cliente con su reservación.
#### ID_Viaje
- Identificador del viaje para el que se realizó la reservación.
