CREATE TABLE IF NOT EXISTS `loginlauncher_users` (
  `username` varchar(50) NOT NULL DEFAULT '',
  `password` varchar(50) DEFAULT 'DefaultPass',
  `externalip` varchar(50) DEFAULT NULL,
  `IsAdmin` bit(1) DEFAULT b'0',
  `IsAllowed` bit(1) DEFAULT b'0',
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

INSERT INTO `loginlauncher_users` (`username`, `password`, `externalip`, `IsAdmin`, `IsAllowed`) VALUES
	('admin', 'admin', '127.0.0.1\n', b'1', b'0');