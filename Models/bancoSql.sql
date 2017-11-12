create database GerenciadorUsuarios;

CREATE TABLE Usuarios (
idUsuario BIGINT IDENTITY(1,1) NOT NULL,
Nome VARCHAR(80) NOT NULL,
Login VARCHAR(100) NOT NULL,
Senha VARCHAR(60) NOT NULL,
Email VARCHAR(100) NULL,
PRIMARY KEY(idUsuario)
);

CREATE TABLE Direitos (
idDireito BIGINT IDENTITY(1,1) NOT NULL,
Nome VARCHAR(80) NOT NULL,
PRIMARY KEY(idDireito)
);

CREATE TABLE Log (
idLog BIGINT IDENTITY(1,1) NOT NULL,
idUsuario BIGINT NOT NULL,
Acao VARCHAR(255) NOT NULL,
DataAcao DATETIME NOT NULL,
PRIMARY KEY(idLog)
);
ALTER TABLE Log
ADD CONSTRAINT fk_usuario_log
FOREIGN KEY(idUsuario)
REFERENCES Usuarios(idUsuario) ON DELETE CASCADE


CREATE TABLE Usuario_Direito (
idUsuario BIGINT NOT NULL,
idDireito BIGINT NOT NULL
);
ALTER TABLE Usuario_Direito
ADD CONSTRAINT fk_usuario_direito
FOREIGN KEY(idUsuario)
REFERENCES Usuarios(idUsuario) ON DELETE CASCADE
ALTER TABLE Usuario_Direito
ADD CONSTRAINT fk_direito_usuario
FOREIGN KEY(idDireito)
REFERENCES Direitos(idDireito) ON DELETE CASCADE


/* Insert */

INSERT INTO Usuarios (Nome, Login, Senha, Email)
VALUES ('Carlos','carlos','12345','carlos@gmail.com')
INSERT INTO Usuarios (Nome, Login, Senha, Email)
VALUES ('Willian','wschenkel','12345','teste@gmail.com')

INSERT INTO Direitos (Nome)
VALUES ('admin')
INSERT INTO Direitos (Nome)
VALUES ('user')

insert into Usuario_Direito (idUsuario,idDireito)
values (1,1)
insert into Usuario_Direito (idUsuario,idDireito)
values (2,2)