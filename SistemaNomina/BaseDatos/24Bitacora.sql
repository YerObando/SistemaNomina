USE smartbuilding_rh;

-- Tabla Bitacora
CREATE TABLE Bitacora (
  id_log INT IDENTITY(1,1) PRIMARY KEY,
  id_usuario INT NULL,
  accion VARCHAR(100) NOT NULL,
  detalle TEXT NULL,
  fecha_hora DATETIME DEFAULT GETDATE(),
  CONSTRAINT fk_bitacora_usuario FOREIGN KEY (id_usuario) REFERENCES Usuarios (id_usuario)
);
EXEC sp_addextendedproperty 'MS_Description', 'Registro de actividades importantes en el sistema', 'SCHEMA', 'dbo', 'TABLE', 'Bitacora';
EXEC sp_addextendedproperty 'MS_Description', '"Login", "Aprobó vacaciones", "Actualizó empleado"', 'SCHEMA', 'dbo', 'TABLE', 'Bitacora', 'COLUMN', 'accion';
EXEC sp_addextendedproperty 'MS_Description', 'JSON/Texto con datos relevantes de la acción', 'SCHEMA', 'dbo', 'TABLE', 'Bitacora', 'COLUMN', 'detalle';