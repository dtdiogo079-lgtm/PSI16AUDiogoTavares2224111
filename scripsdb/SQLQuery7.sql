-- ============================================
-- SCRIPT COMPLETO - TASKQUESTDB (VERSÃO CORRIGIDA)
-- ============================================

-- USAR O BANCO DE DADOS
USE TaskQuestDB;
GO

-- ============================================
-- ELIMINAR TABELAS EXISTENTES (EM ORDEM CORRETA PARA EVITAR ERROS DE FK)
-- ============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'ProgressoDiario')
    DROP TABLE ProgressoDiario;
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'EstatisticasJogos')
    DROP TABLE EstatisticasJogos;
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'Tarefas')
    DROP TABLE Tarefas;
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'Minijogos')
    DROP TABLE Minijogos;
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'Utilizadores')
    DROP TABLE Utilizadores;
GO

-- ============================================
-- CRIAÇÃO DAS TABELAS
-- ============================================

-- 1. TABELA UTILIZADORES
-- ============================================
CREATE TABLE Utilizadores (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Nivel INT DEFAULT 1,
    XP_Total INT DEFAULT 0,
    Moeda_Total INT DEFAULT 0,
    DataRegisto DATETIME DEFAULT GETDATE()
);
GO

-- 2. TABELA MINIJOGOS
-- ============================================
CREATE TABLE Minijogos (
    JogoID INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(500),
    CustoJogar INT DEFAULT 0,
    PremioMin INT DEFAULT 0,
    PremioMax INT DEFAULT 100
);
GO

-- 3. TABELA ESTATISTICASJOGOS
-- ============================================
CREATE TABLE EstatisticasJogos (
    EstatID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    JogoID INT NOT NULL,
    VezesJogado INT DEFAULT 0,
    MelhorPontuacao INT DEFAULT 0,
    TotalXP_Ganho INT DEFAULT 0,
    FOREIGN KEY (UserID) REFERENCES Utilizadores(UserID),
    FOREIGN KEY (JogoID) REFERENCES Minijogos(JogoID)
);
GO

-- 4. TABELA TAREFAS (CORRIGIDA)
-- ============================================
CREATE TABLE Tarefas (
    TarefaID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    Titulo NVARCHAR(200) NOT NULL,
    Descricao NVARCHAR(500),
    Dificuldade NVARCHAR(20) DEFAULT 'Médio',
    [Status] NVARCHAR(20) DEFAULT 'Pendente',
    DataCriacao DATETIME DEFAULT GETDATE(),
    DataLimite DATETIME,
    DataConclusao DATETIME,
    XP_Ganho INT DEFAULT 0,
    Moedas_Ganhas INT DEFAULT 0,
    FOREIGN KEY (UserID) REFERENCES Utilizadores(UserID)
);
GO

-- ADICIONAR CHECK CONSTRAINTS (FORA DA CRIAÇÃO DA TABELA)
ALTER TABLE Tarefas ADD CONSTRAINT CK_Tarefas_Dificuldade 
    CHECK (Dificuldade IN ('Fácil', 'Médio', 'Difícil'));
GO

ALTER TABLE Tarefas ADD CONSTRAINT CK_Tarefas_Status 
    CHECK ([Status] IN ('Pendente', 'Em Andamento', 'Concluída'));
GO

-- 5. TABELA PROGRESSODIARIO
-- ============================================
CREATE TABLE ProgressoDiario (
    ProgressoID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    Data DATE DEFAULT CAST(GETDATE() AS DATE),
    TarefasConcluidas INT DEFAULT 0,
    XPGanhado INT DEFAULT 0,
    FOREIGN KEY (UserID) REFERENCES Utilizadores(UserID)
);
GO

-- ============================================
-- INSERÇÃO DE DADOS DE EXEMPLO
-- ============================================

-- INSERIR UTILIZADORES
-- ============================================
INSERT INTO Utilizadores (Nome, Email, PasswordHash, Nivel, XP_Total, Moeda_Total, DataRegisto)
VALUES 
(N'João Silva', N'joao.silva@email.com', N'hash123456', 5, 1250, 500, '2025-01-15 10:30:00'),
(N'Maria Santos', N'maria.santos@email.com', N'hash789012', 3, 750, 300, '2025-02-20 14:15:00'),
(N'Pedro Costa', N'pedro.costa@email.com', N'hash345678', 7, 2100, 800, '2024-12-01 09:00:00'),
(N'Ana Ferreira', N'ana.ferreira@email.com', N'hash901234', 2, 320, 150, '2025-03-10 16:45:00'),
(N'Carlos Rodrigues', N'carlos.rodrigues@email.com', N'hash567890', 4, 980, 450, '2025-01-05 11:20:00');
GO

-- INSERIR MINIJOGOS
-- ============================================
INSERT INTO Minijogos (Nome, Descricao, CustoJogar, PremioMin, PremioMax)
VALUES 
(N'Quiz Rápido', N'Responda perguntas de cultura geral em 30 segundos', 10, 5, 30),
(N'Memória', N'Encontre os pares de cartas no menor tempo possível', 15, 10, 40),
(N'Caça-Palavras', N'Encontre as palavras escondidas na grade de letras', 20, 15, 50),
(N'Matemática Mental', N'Resolva operações matemáticas rapidamente', 5, 5, 25),
(N'Labirinto', N'Encontre a saída do labirinto no menor tempo', 25, 20, 60);
GO

-- INSERIR ESTATISTICASJOGOS
-- ============================================
INSERT INTO EstatisticasJogos (UserID, JogoID, VezesJogado, MelhorPontuacao, TotalXP_Ganho)
VALUES 
(1, 1, 15, 28, 225),
(1, 2, 8, 35, 120),
(1, 3, 5, 42, 75),
(2, 1, 10, 25, 150),
(2, 4, 20, 22, 200),
(3, 1, 25, 30, 375),
(3, 2, 12, 38, 180),
(3, 5, 6, 55, 90),
(4, 3, 3, 30, 45),
(5, 1, 18, 27, 270),
(5, 4, 15, 20, 150);
GO

-- INSERIR TAREFAS
-- ============================================
INSERT INTO Tarefas (UserID, Titulo, Descricao, Dificuldade, [Status], DataCriacao, DataLimite, DataConclusao, XP_Ganho, Moedas_Ganhas)
VALUES 
(1, N'Estudar SQL', N'Estudar os fundamentos de SQL por 2 horas', N'Médio', N'Concluída', '2025-03-01 08:00:00', '2025-03-05 23:59:00', '2025-03-02 18:30:00', 100, 20),
(1, N'Completar Quiz', N'Completar o quiz de programação', N'Fácil', N'Concluída', '2025-03-10 10:00:00', '2025-03-12 23:59:00', '2025-03-11 15:45:00', 50, 10),
(2, N'Ler Capítulo 5', N'Ler o capítulo 5 do livro de matemática', N'Fácil', N'Em Andamento', '2025-03-15 09:30:00', '2025-03-20 23:59:00', NULL, 30, 5),
(2, N'Projeto JavaScript', N'Criar um projeto simples com JavaScript', N'Difícil', N'Pendente', '2025-03-12 14:00:00', '2025-03-30 23:59:00', NULL, 200, 50),
(3, N'Revisão de HTML', N'Revisar todos os conceitos de HTML', N'Médio', N'Concluída', '2025-02-20 11:00:00', '2025-02-25 23:59:00', '2025-02-24 09:15:00', 80, 15),
(3, N'Exercícios de CSS', N'Resolver 10 exercícios de CSS', N'Médio', N'Em Andamento', '2025-03-01 08:30:00', '2025-03-10 23:59:00', NULL, 120, 30),
(4, N'Leitura de Artigo', N'Ler artigo sobre inteligência artificial', N'Fácil', N'Pendente', '2025-03-18 16:00:00', '2025-03-22 23:59:00', NULL, 40, 8),
(5, N'Projeto de Banco de Dados', N'Modelar um banco de dados para uma loja', N'Difícil', N'Pendente', '2025-03-01 10:00:00', '2025-03-15 23:59:00', NULL, 250, 60);
GO

-- INSERIR PROGRESSODIARIO
-- ============================================
INSERT INTO ProgressoDiario (UserID, Data, TarefasConcluidas, XPGanhado)
VALUES 
(1, '2025-03-02', 2, 150),
(1, '2025-03-11', 1, 50),
(2, '2025-03-15', 0, 0),
(3, '2025-02-24', 2, 80),
(3, '2025-03-02', 1, 30),
(5, '2025-03-05', 1, 40);
GO

-- ============================================
-- CONSULTAS DE EXEMPLO
-- ============================================

-- 1. Top 1000 Utilizadores
SELECT TOP (1000) [UserID], [Nome], [Email], [PasswordHash], 
       [Nivel], [XP_Total], [Moeda_Total], [DataRegisto]
FROM [TaskQuestDB].[dbo].[Utilizadores];
GO

-- 2. Top 1000 Minijogos
SELECT TOP (1000) [JogoID], [Nome], [Descricao], 
       [CustoJogar], [PremioMin], [PremioMax]
FROM [TaskQuestDB].[dbo].[Minijogos];
GO

-- 3. Top 1000 EstatisticasJogos
SELECT TOP (1000) [EstatID], [UserID], [JogoID], 
       [VezesJogado], [MelhorPontuacao], [TotalXP_Ganho]
FROM [TaskQuestDB].[dbo].[EstatisticasJogos];
GO

-- 4. Top 1000 Tarefas
SELECT TOP (1000) [TarefaID], [UserID], [Titulo], [Descricao],
       [Dificuldade], [Status], [DataCriacao], [DataLimite],
       [DataConclusao], [XP_Ganho], [Moedas_Ganhas]
FROM [TaskQuestDB].[dbo].[Tarefas];
GO

-- 5. Top 1000 ProgressoDiario
SELECT TOP (1000) [ProgressoID], [UserID], [Data],
       [TarefasConcluidas], [XPGanhado]
FROM [TaskQuestDB].[dbo].[ProgressoDiario];
GO

-- ============================================
-- CONSULTAS ADICIONAIS ÚTEIS
-- ============================================

-- 1. Resumo do usuário com estatísticas completas
SELECT 
    u.UserID,
    u.Nome,
    u.Nivel,
    u.XP_Total,
    u.Moeda_Total,
    COUNT(DISTINCT t.TarefaID) AS TotalTarefas,
    COUNT(DISTINCT ej.EstatID) AS TotalJogosJogados,
    ISNULL(SUM(ej.VezesJogado), 0) AS TotalPartidasJogos
FROM Utilizadores u
LEFT JOIN Tarefas t ON u.UserID = t.UserID
LEFT JOIN EstatisticasJogos ej ON u.UserID = ej.UserID
GROUP BY u.UserID, u.Nome, u.Nivel, u.XP_Total, u.Moeda_Total
ORDER BY u.XP_Total DESC;
GO

-- 2. Tarefas pendentes por usuário
SELECT 
    u.Nome,
    t.Titulo,
    t.Descricao,
    t.Dificuldade,
    t.DataLimite,
    DATEDIFF(day, GETDATE(), t.DataLimite) AS DiasRestantes
FROM Utilizadores u
INNER JOIN Tarefas t ON u.UserID = t.UserID
WHERE t.[Status] != N'Concluída'
ORDER BY t.DataLimite ASC;
GO

-- 3. Ranking dos jogos mais jogados
SELECT 
    m.Nome AS Jogo,
    COUNT(ej.EstatID) AS TotalJogadores,
    ISNULL(SUM(ej.VezesJogado), 0) AS TotalPartidas,
    ISNULL(AVG(ej.MelhorPontuacao), 0) AS MediaMelhorPontuacao
FROM Minijogos m
LEFT JOIN EstatisticasJogos ej ON m.JogoID = ej.JogoID
GROUP BY m.Nome
ORDER BY TotalPartidas DESC;
GO

-- 4. Progresso diário dos usuários nos últimos 7 dias
SELECT 
    u.Nome,
    pd.Data,
    pd.TarefasConcluidas,
    pd.XPGanhado
FROM Utilizadores u
INNER JOIN ProgressoDiario pd ON u.UserID = pd.UserID
WHERE pd.Data >= DATEADD(day, -7, GETDATE())
ORDER BY pd.Data DESC, u.Nome;
GO

-- 5. Usuários com maior XP acumulado
SELECT TOP 10
    UserID,
    Nome,
    Nivel,
    XP_Total,
    Moeda_Total,
    DATEDIFF(day, DataRegisto, GETDATE()) AS DiasAtivo,
    CAST(XP_Total / NULLIF(DATEDIFF(day, DataRegisto, GETDATE()), 0) AS DECIMAL(10,2)) AS XPDia
FROM Utilizadores
ORDER BY XP_Total DESC;
GO