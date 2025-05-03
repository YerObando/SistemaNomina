USE smartbuilding_rh;

-- Catálogo de Feriados
CREATE TABLE Feriados (
  id_feriado INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(100) NOT NULL,
  fecha DATE NOT NULL,
  pago_obligatorio BIT DEFAULT 1,
  recargo DECIMAL(5,2) DEFAULT 100.00,
  descripcion TEXT,
  fecha_creacion DATETIME DEFAULT GETDATE(),
  fecha_actualizacion DATETIME DEFAULT GETDATE()
);
EXEC sp_addextendedproperty 'MS_Description', 'Registro de feriados y sus reglas de pago', 'SCHEMA', 'dbo', 'TABLE', 'Feriados';
EXEC sp_addextendedproperty 'MS_Description', 'Nombre del feriado', 'SCHEMA', 'dbo', 'TABLE', 'Feriados', 'COLUMN', 'nombre';
EXEC sp_addextendedproperty 'MS_Description', 'Fecha exacta del feriado', 'SCHEMA', 'dbo', 'TABLE', 'Feriados', 'COLUMN', 'fecha';
EXEC sp_addextendedproperty 'MS_Description', 'Porcentaje adicional al salario por trabajar (ej: 100% = doble pago)', 'SCHEMA', 'dbo', 'TABLE', 'Feriados', 'COLUMN', 'recargo';
