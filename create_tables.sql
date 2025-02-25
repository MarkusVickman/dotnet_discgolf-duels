

CREATE TABLE User (
Email                       VARCHAR(150) NOT NULL PRIMARY KEY,
DisplayName                 VARCHAR(100) NOT NULL,
PDGA_Nr                     INTEGER,
PictureURL                  VARCHAR(300),
Password                    VARCHAR(20) NOT NULL
);

CREATE TABLE Course (
CourseId                    INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY,
Par                         INTEGER NOT NULL,
CourseName                  VARCHAR(150)
);

CREATE TABLE Play (
PlayId                      INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY,
Par                         INTEGER,
GroupNr                     INTEGER,
CompetitionId               INTEGER,
CourseId                    INTEGER NOT NULL,
UserId                      INTEGER NOT NULL,
Active                      BOOLEAN,
SessionId                   INTEGER,
FOREIGN KEY (CompetitionId) REFERENCES Competition(CompetitionId),
FOREIGN KEY (CourseId)      REFERENCES Course(CourseId),
FOREIGN KEY (UserId)        REFERENCES User(UserId)
);


CREATE TABLE Competition (
CompetitionId               INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY,
RegisterDate                DATE,
CompetitionDate             DATE,
MaxPlayerCount              INTEGER NOT NULL,
UserId                      INTEGER NOT NULL,
FOREIGN KEY (UserId)        REFERENCES User(UserId)
);

CREATE TABLE Friends (
UserId                      INTEGER NOT NULL,
FriendId                    INTEGER NOT NULL,
PRIMARY KEY (UserId, FriendId),
FOREIGN KEY (UserId)        REFERENCES User(UserId),
FOREIGN KEY (FriendId)      REFERENCES User(UserId)
);
