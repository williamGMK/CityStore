# CityStore
# CityMakerspace Lending System (Console, C#)

Run instructions:
1. Create database by running db_init.sql (in SQL Server).
2. Open solution in Visual Studio 2022, update connection string in Data/Db.cs if needed.
3. Build & run.

Seed test accounts (for testing only):
- admin / admin123 (Admin)
- maria / maria123 (Member)
- keegan / keegan123 (Member)
CREATE TABLE Members (
  MemberId     INT IDENTITY PRIMARY KEY,
  Username     VARCHAR(50) NOT NULL UNIQUE,
  PasswordHash VARCHAR(200) NOT NULL,
  Role         VARCHAR(10) NOT NULL CHECK (Role IN ('Admin','Member'))
);

CREATE TABLE Tools (
  ToolId     INT IDENTITY PRIMARY KEY,
  Category   VARCHAR(30) NOT NULL,
  Condition  VARCHAR(15) NOT NULL,
  IsBorrowed BIT NOT NULL DEFAULT 0
);

CREATE TABLE Loans (
  LoanId     INT IDENTITY PRIMARY KEY,
  MemberId   INT NOT NULL FOREIGN KEY REFERENCES Members(MemberId),
  ToolId     INT NOT NULL FOREIGN KEY REFERENCES Tools(ToolId),
  BorrowDate DATE NOT NULL,
  ReturnDate DATE NULL
);

-- Optional: don't insert password hashes here; run seed from app to ensure hashed values.
INSERT INTO Members (Username, PasswordHash, Role) VALUES
('admin',  'temp', 'Admin'),
('maria',  'temp', 'Member'),
('keegan', 'temp', 'Member');

INSERT INTO Tools (Category, Condition, IsBorrowed) VALUES
('Camera',     'Good',   0),
('Electronics','New',    0),
('Woodwork',   'Fair',   0),
('Textbook',   'Good',   0);

