USE smartbuilding_rh;

-- Tabla Empleados
CREATE TABLE Empleados (
  id_empleado INT IDENTITY(1,1) PRIMARY KEY,
  cedula VARCHAR(20) UNIQUE NOT NULL,
  nombre1 VARCHAR(50) NOT NULL,
  nombre2 VARCHAR(50) NULL,
  apellido1 VARCHAR(50) NOT NULL,
  apellido2 VARCHAR(50) NULL,
  fecha_nacimiento DATE NULL, 
  direccion VARCHAR(255) NULL,
  correo VARCHAR(100) NULL,
  telefono VARCHAR(15) NULL,
  id_estado_civil INT NULL,
  cantidad_hijos INT DEFAULT 0,
  id_puesto INT NOT NULL,
  id_horario INT NULL,
  fecha_ingreso DATE NOT NULL,
  estado VARCHAR(10) DEFAULT 'Activo' CHECK (estado IN ('Activo', 'Inactivo')),
  fecha_creacion DATETIME DEFAULT GETDATE(),
  fecha_actualizacion DATETIME DEFAULT GETDATE(),
  CONSTRAINT fk_empleado_puesto FOREIGN KEY (id_puesto) REFERENCES Puestos (id_puesto),
  CONSTRAINT fk_empleado_estadocivil FOREIGN KEY (id_estado_civil) REFERENCES EstadoCivil (id_estado_civil),
  CONSTRAINT fk_empleado_horario FOREIGN KEY (id_horario) REFERENCES Horarios (id_horario)
);
EXEC sp_addextendedproperty 'MS_Description', 'Información básica de los empleados', 'SCHEMA', 'dbo', 'TABLE', 'Empleados';
EXEC sp_addextendedproperty 'MS_Description', 'Almacena dirección completa', 'SCHEMA', 'dbo', 'TABLE', 'Empleados', 'COLUMN', 'direccion';
EXEC sp_addextendedproperty 'MS_Description', 'Relación con catálogo de estado civil', 'SCHEMA', 'dbo', 'TABLE', 'Empleados', 'COLUMN', 'id_estado_civil';
EXEC sp_addextendedproperty 'MS_Description', 'Relación con catálogo de horarios', 'SCHEMA', 'dbo', 'TABLE', 'Empleados', 'COLUMN', 'id_horario';
