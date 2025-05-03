USE smartbuilding_rh;

-- Catálogo de Tipo de Permiso
CREATE TABLE TiposPermiso (
  id_tipo_permiso INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL,
  con_goce BIT NOT NULL,
  descripcion TEXT,
  fecha_creacion DATETIME DEFAULT GETDATE(),
  fecha_actualizacion DATETIME DEFAULT GETDATE()
);
EXEC sp_addextendedproperty 'MS_Description', 'Tipos de permisos laborales', 'SCHEMA', 'dbo', 'TABLE', 'TiposPermiso';
EXEC sp_addextendedproperty 'MS_Description', 'Si el permiso afecta el salario', 'SCHEMA', 'dbo', 'TABLE', 'TiposPermiso', 'COLUMN', 'con_goce';

INSERT INTO TiposPermiso (nombre, con_goce, descripcion) VALUES 
('Enfermedad', 1, 'Permiso con goce salarial'),
('Personal', 0, 'Permiso sin goce salarial');