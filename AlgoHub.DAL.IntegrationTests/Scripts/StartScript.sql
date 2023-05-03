DELETE FROM dbo.tblUser
DELETE FROM dbo.tblRole
DELETE FROM dbo.tblUserRole
DELETE FROM dbo.tblProblem

DBCC CHECKIDENT('dbo.tblUserRole', RESEED, 1);
DBCC CHECKIDENT('dbo.tblRole', RESEED, 1);
DBCC CHECKIDENT('dbo.tblProblem', RESEED, 1);

INSERT INTO dbo.tblUser (UserId, UserName, FullName, Email, PasswordHash, PasswordSalt, IconName, CreateDate, UpdateDate)
VALUES ('11111111-1111-1111-1111-111111111111', 'user1', 'John Doe', 'user1@example.com', 'hash1', 'salt1', 'icon1', '2001-01-01', '2011-11-11'),
       ('22222222-2222-2222-2222-222222222222', 'user2', 'Jane Doe', 'user2@example.com', 'hash2', 'salt2', 'icon2', '2002-02-02', '2012-12-12');

INSERT INTO dbo.tblRole (RoleName)
VALUES ('Admin'),
       ('User');

INSERT INTO dbo.tblUserRole (UserId, RoleId)
VALUES ('11111111-1111-1111-1111-111111111111', 1),
       ('22222222-2222-2222-2222-222222222222', 2);

INSERT INTO dbo.tblProblem (ProblemName, ProblemContentFileName, AuthorId, ImageName, TimeLimitMs, MemoryLimitBytes, CreateDate, UpdateDate)
VALUES ('Problem 1', 'problem1.txt', '11111111-1111-1111-1111-111111111111', 'image1.png', 1000, 1024, '2001-01-01', '2011-11-11'),
       ('Problem 2', 'problem2.txt', NULL, 'image2.png', 2000, 2048, '2002-02-02', '2012-12-12');