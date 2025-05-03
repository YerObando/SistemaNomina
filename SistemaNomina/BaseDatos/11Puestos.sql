USE smartbuilding_rh;

-- Tabla Puestos
CREATE TABLE Puestos (
  id_puesto INT IDENTITY(1,1) PRIMARY KEY,
  nombre_puesto VARCHAR(50) NOT NULL,
  id_departamento INT NOT NULL,
  salario_base DECIMAL(12,2) NOT NULL,
  descripcion TEXT,
  horas_jornada INT NOT NULL DEFAULT 8,
  es_jefe BIT DEFAULT 0,
  id_horario INT NULL,
  fecha_creacion DATETIME DEFAULT GETDATE(),
  fecha_actualizacion DATETIME DEFAULT GETDATE(),
  CONSTRAINT fk_puesto_departamento FOREIGN KEY (id_departamento) 
    REFERENCES Departamentos (id_departamento),
  CONSTRAINT fk_puesto_horario FOREIGN KEY (id_horario) 
    REFERENCES Horarios (id_horario)
);
EXEC sp_addextendedproperty 'MS_Description', 'Puestos laborales por departamento', 'SCHEMA', 'dbo', 'TABLE', 'Puestos';
EXEC sp_addextendedproperty 'MS_Description', 'Indica si el puesto es de jefatura', 'SCHEMA', 'dbo', 'TABLE', 'Puestos', 'COLUMN', 'es_jefe';
