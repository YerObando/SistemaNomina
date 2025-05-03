USE smartbuilding_rh;

-- Tabla SolicitudesVacaciones
CREATE TABLE SolicitudesVacaciones (
  id_solicitud INT IDENTITY(1,1) PRIMARY KEY,
  id_vacacion INT NOT NULL,
  fecha_inicio DATE NOT NULL,
  fecha_fin DATE NOT NULL,
  fecha_solicitud DATETIME DEFAULT GETDATE(),
  fecha_aprobacion DATETIME NULL,
  aprobado_por INT NULL,
  comentario_solicitud TEXT NULL,
  comentario_respuesta TEXT NULL,
  id_estado INT NOT NULL,
  fecha_creacion DATETIME DEFAULT GETDATE(),
  fecha_actualizacion DATETIME DEFAULT GETDATE(),
  CONSTRAINT fk_solicitud_vacacion FOREIGN KEY (id_vacacion) REFERENCES Vacaciones (id_vacacion),
  CONSTRAINT fk_solicitud_estado FOREIGN KEY (id_estado) REFERENCES Estados (id_estado),
  CONSTRAINT fk_solicitud_aprobador FOREIGN KEY (aprobado_por) REFERENCES Usuarios (id_usuario)
);
EXEC sp_addextendedproperty 'MS_Description', 'Registro detallado de solicitudes de vacaciones', 'SCHEMA', 'dbo', 'TABLE', 'SolicitudesVacaciones';
EXEC sp_addextendedproperty 'MS_Description', 'Justificación del empleado', 'SCHEMA', 'dbo', 'TABLE', 'SolicitudesVacaciones', 'COLUMN', 'comentario_solicitud';
EXEC sp_addextendedproperty 'MS_Description', 'Observaciones del aprobador/rechazador', 'SCHEMA', 'dbo', 'TABLE', 'SolicitudesVacaciones', 'COLUMN', 'comentario_respuesta';
EXEC sp_addextendedproperty 'MS_Description', 'Relación con catálogo Estados', 'SCHEMA', 'dbo', 'TABLE', 'SolicitudesVacaciones', 'COLUMN', 'id_estado';
