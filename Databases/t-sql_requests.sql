DROP TABLE IF EXISTS RolePermission;
DROP TABLE IF EXISTS Roles;
DROP TABLE IF EXISTS Permissions;
CREATE TABLE Roles
(
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	name TEXT NOT NULL DEFAULT "Untitled role"
);
CREATE TABLE Permissions
(
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	act TEXT NOT NULL UNIQUE,
	name TEXT DEFAULT "Untitled permission"
);
CREATE TABLE RolePermission
(
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	role_id INTEGER NOT NULL,
	permission_id INTEGER NOT NULL,
	CONSTRAINT roles_pk FOREIGN KEY (role_id) REFERENCES Roles(id) ON DELETE CASCADE,
	CONSTRAINT permissions_pk FOREIGN KEY (permission_id) REFERENCES Permissions(id) ON DELETE CASCADE
);
INSERT INTO Permissions (act, name) VALUES ("view","Просмотр");
INSERT INTO Permissions (act, name) VALUES ("add","Добавить");
INSERT INTO Permissions (act, name) VALUES ("remove","Удалить");
INSERT INTO Permissions (act, name) VALUES ("edit","Редактировать");
INSERT INTO Roles (name) VALUES ("Administrator");
INSERT INTO Roles (name) VALUES ("User");
INSERT INTO Roles (name) VALUES ("Guest");
INSERT INTO RolePermission (role_id, permission_id) VALUES (1, 1);
INSERT INTO RolePermission (role_id, permission_id) VALUES (1, 2);
INSERT INTO RolePermission (role_id, permission_id) VALUES (1, 3);
INSERT INTO RolePermission (role_id, permission_id) VALUES (1, 4);
INSERT INTO RolePermission (role_id, permission_id) VALUES (2, 1);
INSERT INTO RolePermission (role_id, permission_id) VALUES (2, 2);
INSERT INTO RolePermission (role_id, permission_id) VALUES (2, 4);
INSERT INTO RolePermission (role_id, permission_id) VALUES (3, 1);

DELETE FROM Roles WHERE id=1;