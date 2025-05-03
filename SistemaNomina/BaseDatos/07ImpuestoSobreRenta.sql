USE smartbuilding_rh;

-- Catálogo de ISR
CREATE TABLE ISR (
  id_isr INT IDENTITY(1,1) PRIMARY KEY,
  anio INT NOT NULL,
  limite_inferior DECIMAL(12,2) NOT NULL,
  limite_superior DECIMAL(12,2) NOT NULL,
  porcentaje DECIMAL(5,2) NOT NULL,
  exceso DECIMAL(12,2) NOT NULL,
  credito_hijo DECIMAL(10,2) DEFAULT 1720.00,
  credito_conyuge DECIMAL(10,2) DEFAULT 2600.00,
  descripcion TEXT,
  fecha_creacion DATETIME DEFAULT GETDATE(),
  fecha_actualizacion DATETIME DEFAULT GETDATE()
);
EXEC sp_addextendedproperty 'MS_Description', 'Tabla de tramos del Impuesto sobre la Renta', 'SCHEMA', 'dbo', 'TABLE', 'ISR';
EXEC sp_addextendedproperty 'MS_Description', 'Año fiscal al que aplican estos valores', 'SCHEMA', 'dbo', 'TABLE', 'ISR', 'COLUMN', 'anio';
EXEC sp_addextendedproperty 'MS_Description', 'Salario mínimo del tramo impositivo', 'SCHEMA', 'dbo', 'TABLE', 'ISR', 'COLUMN', 'limite_inferior';
EXEC sp_addextendedproperty 'MS_Description', 'Salario máximo del tramo impositivo', 'SCHEMA', 'dbo', 'TABLE', 'ISR', 'COLUMN', 'limite_superior';
EXEC sp_addextendedproperty 'MS_Description', '% de impuesto a aplicar sobre el excedente', 'SCHEMA', 'dbo', 'TABLE', 'ISR', 'COLUMN', 'porcentaje';
EXEC sp_addextendedproperty 'MS_Description', 'Monto base sobre el que se calcula el impuesto', 'SCHEMA', 'dbo', 'TABLE', 'ISR', 'COLUMN', 'exceso';
EXEC sp_addextendedproperty 'MS_Description', 'Reducción mensual por hijo', 'SCHEMA', 'dbo', 'TABLE', 'ISR', 'COLUMN', 'credito_hijo';
EXEC sp_addextendedproperty 'MS_Description', 'Reducción mensual por cónyuge', 'SCHEMA', 'dbo', 'TABLE', 'ISR', 'COLUMN', 'credito_conyuge';
EXEC sp_addextendedproperty 'MS_Description', 'Detalles legales o cambios fiscales', 'SCHEMA', 'dbo', 'TABLE', 'ISR', 'COLUMN', 'descripcion';
