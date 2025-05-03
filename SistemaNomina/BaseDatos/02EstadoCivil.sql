USE smartbuilding_rh;

-- Catálogo de Estado Civil
CREATE TABLE EstadoCivil (
  id_estado_civil INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL UNIQUE
);
EXEC sp_addextendedproperty 'MS_Description', 'Catálogo de estado civil para empleados', 'SCHEMA', 'dbo', 'TABLE', 'EstadoCivil';

INSERT INTO EstadoCivil (nombre) VALUES 
('Soltero'), ('Casado'), ('Divorciado'), ('Viudo');