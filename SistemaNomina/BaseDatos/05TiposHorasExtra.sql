USE smartbuilding_rh;

-- Catálogo de Tipos de Hora Extra
CREATE TABLE TiposHoraExtra (
  id_tipo INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL,
  recargo DECIMAL(5,2) NOT NULL,
  descripcion TEXT,
  fecha_creacion DATETIME DEFAULT GETDATE(),
  fecha_actualizacion DATETIME DEFAULT GETDATE()
);
EXEC sp_addextendedproperty 'MS_Description', 'Tipos de horas extras y sus recargos', 'SCHEMA', 'dbo', 'TABLE', 'TiposHoraExtra';
EXEC sp_addextendedproperty 'MS_Description', 'Porcentaje base para este tipo de hora extra', 'SCHEMA', 'dbo', 'TABLE', 'TiposHoraExtra', 'COLUMN', 'recargo';
