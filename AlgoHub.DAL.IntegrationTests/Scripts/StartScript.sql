DELETE FROM dbo.tblProblem
DELETE FROM dbo.tblUser
DELETE FROM dbo.tblRole
GO

-- Role

SET IDENTITY_INSERT dbo.tblRole ON

INSERT INTO dbo.tblRole (RoleId, RoleName)
VALUES (1, 'User'),
       (2, 'Admin');

SET IDENTITY_INSERT dbo.tblRole OFF

DBCC CHECKIDENT('dbo.tblRole', RESEED, 2);
GO

-- User

INSERT INTO dbo.tblUser (UserId, UserName, FullName, Email, RoleId, PasswordHash, PasswordSalt, IconName, CreateDate, UpdateDate)
VALUES ('11111111-1111-1111-1111-111111111111', 'user1', 'John Doe', 'user1@example.com', 1, 'hash1', 'salt1', 'icon1', '2001-01-01', '2011-11-11'),
       ('22222222-2222-2222-2222-222222222222', 'user2', 'Jane Doe', 'user2@example.com', 2, 'hash2', 'salt2', NULL, '2002-02-02', '2012-12-12');
GO

-- Problem

SET IDENTITY_INSERT dbo.tblProblem ON

INSERT INTO dbo.tblProblem (ProblemId, ProblemName, ProblemContentFileName, AuthorId, ImageName, TimeLimitMs, MemoryLimitBytes, CreateDate, UpdateDate)
VALUES (1, 'Problem 1', 'problem1.txt', '11111111-1111-1111-1111-111111111111', 'image1.png', 1000, 1024, '2001-01-01', '2011-11-11'),
       (2, 'Problem 2', 'problem2.txt', NULL, 'image2.png', 2000, 2048, '2002-02-02', '2012-12-12');
GO

SET IDENTITY_INSERT dbo.tblProblem OFF

DBCC CHECKIDENT('dbo.tblProblem', RESEED, 2);
GO