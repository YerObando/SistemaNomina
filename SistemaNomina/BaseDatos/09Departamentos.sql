USE smartbuilding_rh;

-- Tabla Departamentos 
CREATE TABLE Departamentos (
  id_departamento INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(100) NOT NULL,
  fecha_creacion DATETIME DEFAULT GETDATE(),
  fecha_actualizacion DATETIME DEFAULT GETDATE()
);
EXEC sp_addextendedproperty 'MS_Description', 'Departamentos de la empresa', 'SCHEMA', 'dbo', 'TABLE', 'Departamentos';
