USE smartbuilding_rh;

-- Catálogo de Roles 
CREATE TABLE Roles (
  id_rol INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL UNIQUE,
  descripcion TEXT,
  fecha_creacion DATETIME DEFAULT GETDATE(),
  fecha_actualizacion DATETIME DEFAULT GETDATE()
);
EXEC sp_addextendedproperty 'MS_Description', 'Roles de usuarios en el sistema con sus permisos', 'SCHEMA', 'dbo', 'TABLE', 'Roles';

INSERT INTO Roles (nombre, descripcion) VALUES 
('Admin', 'Acceso total al sistema'),
('RRHH', 'Gestión de empleados y nómina'),
('Supervisor', 'Aprobación de solicitudes y consultas de equipo'),
('Empleado', 'Acceso limitado');