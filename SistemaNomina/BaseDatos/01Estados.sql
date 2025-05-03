USE smartbuilding_rh;


-- Catálogo de Estados 
CREATE TABLE Estados (
  id_estado INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL,
  modulo VARCHAR(20) NOT NULL CHECK (modulo IN ('Asistencia', 'Vacaciones', 'HorasExtras', 'Incapacidades', 'Permisos'))
);
EXEC sp_addextendedproperty 'MS_Description', 'Catálogo reutilizable para estados en diferentes módulos', 'SCHEMA', 'dbo', 'TABLE', 'Estados';

INSERT INTO Estados (nombre, modulo) VALUES 
('Pendiente', 'Vacaciones'), ('Aprobado', 'Vacaciones'), ('Rechazado', 'Vacaciones'),
('Presente', 'Asistencia'), ('Ausente', 'Asistencia'), ('Tardanza', 'Asistencia'),
('Pendiente', 'Permisos'), ('Aprobado', 'Permisos'), ('Rechazado', 'Permisos');