USE smartbuilding_rh;

-- Tabla Vacaciones 
CREATE TABLE Vacaciones (
  id_vacacion INT IDENTITY(1,1) PRIMARY KEY,
  id_empleado INT NOT NULL,
  periodo VARCHAR(20) NOT NULL,
  dias_disponibles INT NOT NULL,
  dias_disfrutados INT DEFAULT 0,
  fecha_creacion DATETIME DEFAULT GETDATE(),
  fecha_actualizacion DATETIME DEFAULT GETDATE(),
  CONSTRAINT fk_vacaciones_empleado FOREIGN KEY (id_empleado) REFERENCES Empleados (id_empleado)
);
EXEC sp_addextendedproperty 'MS_Description', 'Días de vacaciones disponibles y disfrutados por empleado', 'SCHEMA', 'dbo', 'TABLE', 'Vacaciones';
EXEC sp_addextendedproperty 'MS_Description', 'Periodo al que corresponden las vacaciones (ej: 2023-2024)', 'SCHEMA', 'dbo', 'TABLE', 'Vacaciones', 'COLUMN', 'periodo';
