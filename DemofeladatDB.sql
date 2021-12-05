-- MySQL dump 10.13  Distrib 8.0.22, for Win64 (x86_64)
--
-- Host: localhost    Database: demofeladat
-- ------------------------------------------------------
-- Server version	8.0.22

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `record`
--

DROP TABLE IF EXISTS `record`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `record` (
  `idRecord` int NOT NULL AUTO_INCREMENT,
  `Date` date NOT NULL,
  `Comment` text,
  `Duration` smallint unsigned NOT NULL,
  `StatusDelete` tinyint DEFAULT '1',
  `User_idUser` int NOT NULL,
  `Task_idTask` int NOT NULL,
  PRIMARY KEY (`idRecord`,`User_idUser`,`Task_idTask`),
  KEY `fk_Record_User1_idx` (`User_idUser`),
  KEY `fk_Record_Task1_idx` (`Task_idTask`),
  CONSTRAINT `fk_Record_Task1` FOREIGN KEY (`Task_idTask`) REFERENCES `task` (`idTask`),
  CONSTRAINT `fk_Record_User1` FOREIGN KEY (`User_idUser`) REFERENCES `user` (`idUser`)
) ENGINE=InnoDB AUTO_INCREMENT=70 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `record`
--

LOCK TABLES `record` WRITE;
/*!40000 ALTER TABLE `record` DISABLE KEYS */;
INSERT INTO `record` VALUES (1,'2000-01-02','elso',10,1,1,1),(2,'2099-01-01','update',10,1,4,4),(4,'2021-01-01','test',69,0,4,3),(5,'2001-01-01','updated',10,1,6,13),(6,'2001-01-01','Delete',1,1,6,13),(7,'0001-01-01','comment',21,1,1,8),(8,'0001-01-01','99',99,1,9,8),(9,'0001-01-01','99',89,1,8,9),(10,'0001-01-01','99',99,0,5,15),(12,'2001-01-01','wat',14,1,5,17),(13,'0001-01-01','nincs',60,1,17,18),(14,'0001-01-01','asd',1,0,4,3),(15,'0001-01-01','jajj',10,0,19,21),(16,'0001-01-01','ssss',45,1,19,21),(17,'2006-03-10','d',0,1,19,21),(18,'2001-01-01','jovot',22,1,19,21),(19,'2001-01-01','ssssss',29,1,5,15),(21,'2021-09-21','test',210,0,1,34),(22,'2021-09-21','csak a doksit nézem át',60,1,22,36),(23,'2021-09-21','tervezes',710,1,22,36),(24,'2021-09-22','co',210,1,17,25),(25,'2021-09-22','ss',220,1,20,28),(26,'2021-09-22','d',170,1,22,36),(27,'2021-09-25','12',12,1,6,31),(28,'2022-01-01','na?',190,0,4,3),(29,'2021-10-03','tippmix',310,0,1,23),(30,'2021-11-02','create',90,1,9,17),(31,'2021-09-22','ss',210,1,4,24),(32,'2021-09-27','cica',210,1,22,39),(33,'2021-09-27','kutya',210,1,22,39),(34,'2021-09-27','now',220,1,9,29),(35,'2010-10-10','gdd',11,1,22,39),(36,'2021-11-04','jo',42,1,1,40),(37,'2021-12-12','jo',12,0,1,40),(38,'2021-09-27','cool',210,1,4,38),(39,'2021-10-10','festek kell',230,1,1,40),(40,'2021-09-28',NULL,210,1,4,24),(41,'2021-09-28',NULL,260,1,16,20),(42,'2021-09-28','d',150,1,5,32),(43,'2021-10-08','kész',250,1,23,42),(44,'2021-09-30','Folyamatban',150,1,23,41),(48,'2021-10-31','new',120,0,1,48),(49,'2021-10-04','interesting',160,0,1,48),(50,'2021-11-04','fuzzy',170,1,1,48),(51,'2021-10-04','maybe??',90,0,1,48),(57,'2021-11-03','penteki teszt',330,1,1,4),(58,'2021-10-30',NULL,210,0,1,4),(59,'2021-10-30','marci??',210,0,1,3),(60,'2021-10-30','marci??',210,0,4,3),(61,'2021-10-31','gyakorlas',260,0,1,4),(62,'2021-11-02','kkk',230,0,1,4),(63,'2021-11-02','ééééé',70,0,6,13),(64,'2021-11-02',NULL,210,0,4,3),(65,'2021-11-02','mukszik???',260,0,1,4),(66,'2021-11-02','pelda??',210,0,1,4),(67,'2021-11-03','asdafgfdg',170,0,1,4),(68,'2021-11-06','f',210,1,44,76),(69,'2021-11-06','ggg',360,1,44,77);
/*!40000 ALTER TABLE `record` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `recordview`
--

DROP TABLE IF EXISTS `recordview`;
/*!50001 DROP VIEW IF EXISTS `recordview`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `recordview` AS SELECT 
 1 AS `idRecord`,
 1 AS `Date`,
 1 AS `Comment`,
 1 AS `Duration`,
 1 AS `User_idUser`,
 1 AS `Username`,
 1 AS `Task_idTask`,
 1 AS `Task_Title`,
 1 AS `Task_Status`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `task`
--

DROP TABLE IF EXISTS `task`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `task` (
  `idTask` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(45) NOT NULL,
  `Description` text,
  `Deadline` date NOT NULL,
  `Status` varchar(45) NOT NULL,
  `StatusDelete` tinyint DEFAULT '1',
  `User_idUser` int NOT NULL,
  `CreationDate` date NOT NULL,
  PRIMARY KEY (`idTask`,`User_idUser`),
  KEY `fk_Task_User1_idx` (`User_idUser`),
  CONSTRAINT `fk_Task_User1` FOREIGN KEY (`User_idUser`) REFERENCES `user` (`idUser`)
) ENGINE=InnoDB AUTO_INCREMENT=84 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `task`
--

LOCK TABLES `task` WRITE;
/*!40000 ALTER TABLE `task` DISABLE KEYS */;
INSERT INTO `task` VALUES (1,'elso','proba','2021-12-01','1',0,1,'2021-01-01'),(2,'masodik','test','2021-12-01','1',0,2,'2021-01-01'),(3,'new','new','2021-12-01','1',1,4,'2021-01-01'),(4,'testt','proba','2021-12-01','1',1,1,'2021-01-01'),(5,'update','update','2021-12-01','1',0,11,'2021-01-01'),(6,'s','dadafa','2021-12-01','1',1,13,'2021-01-01'),(7,'test','test','2021-12-01','1',1,7,'2021-01-01'),(8,'a','a','2021-12-01','1',1,13,'2021-01-01'),(9,'s','s','2021-12-01','1',0,11,'2021-01-01'),(10,'ssss','descgagagag','2021-12-01','1',0,9,'2021-01-01'),(11,'oktatas','leiras','2021-12-01','1',0,15,'2021-01-01'),(13,'updd','update','2021-12-01','1',1,6,'2021-01-01'),(14,'new','new','2021-12-01','1',1,8,'2021-01-01'),(15,'nwq','neqrq','2021-12-01','1',1,5,'2021-01-01'),(17,'create','creaa','2021-11-11','1',1,9,'2021-01-01'),(18,'nagyonuj','nagyonuj','2021-12-01','1',1,17,'2021-01-01'),(19,'del','del','2021-12-01','1',0,17,'2021-01-01'),(20,'a','aaabb','2021-12-01','1',1,16,'2021-01-01'),(21,'jajajj','baaaa','2021-12-01','1',1,19,'2021-01-01'),(22,'program','taskkk','2021-12-01','1',1,16,'2021-01-01'),(23,'cim','leiras','2021-12-01','1',1,1,'2021-01-01'),(24,'please','pls','2021-12-01','1',1,4,'2021-01-01'),(25,'remelem','mukodik','2021-12-01','1',1,17,'2021-01-01'),(26,'game','cool','2021-12-01','1',0,11,'2021-01-01'),(27,'newtask','newdesc','2021-12-01','1',1,20,'2021-01-01'),(28,'newtaskk','desccc','2021-12-01','1',1,20,'2021-01-01'),(29,'most','talan','2021-12-01','1',1,9,'2021-01-01'),(30,'a','a','2021-12-01','1',0,9,'2021-01-01'),(31,'dd','dd','2021-12-01','1',1,6,'2021-01-01'),(32,'das','das','2021-09-19','2',1,5,'2021-01-01'),(34,'plz','plz','2021-12-01','1',0,1,'2021-01-01'),(35,'probatest','probatest','2021-12-01','1',1,19,'2021-01-01'),(36,'siemens project','a siemens zrt részére bérszámfejtö szoftver fejlesztése','2021-09-22','2',1,22,'2021-01-01'),(37,'newww','newww','2021-12-01','1',0,16,'2021-01-01'),(38,'ssss','pspspsps','2021-09-27','2',1,4,'2021-01-01'),(39,'cat','macska','2021-10-01','1',1,22,'2021-01-01'),(40,'festes','munka','2021-11-11','2',1,1,'2021-01-01'),(41,'Jo','nincs kész','2021-10-01','1',1,23,'2021-01-01'),(42,'Nem jó','kész van','2021-10-10','2',1,23,'2021-01-01'),(43,'something','new','2021-10-07','1',1,26,'2021-01-01'),(44,'asddaf','ddddd','2021-10-16','1',1,26,'2021-01-01'),(45,'jljklhugz','jlhukuz','2021-10-29','1',0,28,'2021-01-01'),(46,'fggsdfsd','hgfghfgh','2021-10-06','1',0,26,'2021-01-01'),(47,'newTask','taszk','2021-10-08','1',1,16,'2021-01-01'),(48,'probaTitle','probaDescription','2021-10-30','1',1,1,'2021-01-01'),(55,'Mnetes','dsds','2021-12-01','1',1,9,'2021-01-01'),(56,'asd','ddf','2021-12-01','1',0,9,'2021-01-01'),(57,'Teszt Title','Leiras','2021-12-01','1',1,23,'2021-01-01'),(58,'TesztEnter',NULL,'2021-12-01','1',0,23,'2021-01-01'),(59,'Test1',NULL,'2021-12-01','1',0,4,'2021-01-01'),(60,'Test2','','2021-12-01','1',0,1,'2021-01-01'),(61,'Test1','asdkkk','2021-12-01','1',0,1,'2021-01-01'),(62,'Test2','llll','2021-12-01','1',0,1,'2021-01-01'),(63,'ll','d','2021-12-01','1',0,23,'2021-01-01'),(64,'Create','teszt','2022-11-05','Created',1,6,'2021-11-04'),(65,'CreationDateTest','Na?','2022-04-16','Created',1,23,'2021-11-04'),(66,'asd','d','2021-11-05','Created',0,23,'2021-11-04'),(67,'gfh','fgh','2021-11-05','Created',1,5,'2021-11-04'),(68,'NewTask',':3','2021-11-06','Created',1,4,'2021-11-05'),(69,'TryIt',NULL,'2021-11-28','Created',0,1,'2021-11-05'),(70,'LetsDoIt','dadad','2021-11-06','Created',0,1,'2021-11-05'),(71,'RefreshTest','Static???','2022-02-25','Created',0,1,'2021-11-05'),(72,'skdfj','sdj','2021-11-06','Created',0,1,'2021-11-05'),(73,'kjlh','kjlh','2021-11-06','Created',0,1,'2021-11-05'),(74,'fh','jh','2021-11-21','Created',0,1,'2021-11-05'),(75,'CreatedTest','Proba','2022-03-27','Created',1,1,'2021-11-05'),(76,'porszivozas','szoba takaritas, felmosas','2021-11-25','0',1,44,'2021-11-06'),(77,'festes','furdoszoba festese','2021-12-07','1',1,44,'2021-11-06'),(78,'sfsd',NULL,'2021-11-27','Created',1,44,'2021-11-06'),(79,'TestRefresh','valami','2021-11-28','Created',1,1,'2021-11-06'),(80,'asd','ff','2021-11-19','Created',0,1,'2021-11-08'),(81,'ddd',NULL,'2021-11-09','Created',0,1,'2021-11-08'),(82,'d','','2021-11-09','Created',0,1,'2021-11-08'),(83,'kjasd','sdf','2021-11-09','InProgress',0,1,'2021-11-08');
/*!40000 ALTER TABLE `task` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `taskview`
--

DROP TABLE IF EXISTS `taskview`;
/*!50001 DROP VIEW IF EXISTS `taskview`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `taskview` AS SELECT 
 1 AS `idTask`,
 1 AS `Title`,
 1 AS `Description`,
 1 AS `Deadline`,
 1 AS `Status`,
 1 AS `CreationDate`,
 1 AS `User_idUser`,
 1 AS `Username`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `idUser` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `FirstName` varchar(45) NOT NULL,
  `LastName` varchar(45) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Telephone` varchar(13) NOT NULL,
  `StatusDelete` tinyint DEFAULT '1',
  PRIMARY KEY (`idUser`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'asd','asd','newFm','newLM','new','new',1),(2,'sad','dd','ff','gg','ee','s',0),(3,'me','password','en','te','asd@asd','12145151',0),(4,'marci','jelszo','Cseh','Marcell','asdadas','1415151',1),(5,'s','sajtPass','sajtFirst','sajtLast','sajtEmail','sajtTel',1),(6,'Test','test','test','test','test','test',1),(7,'yxcyxc','xycxycyx','xyc','cycx','yxc','yxc',0),(8,'a','a','a','a','a','a',0),(9,'d','update','update','update','update','update',1),(10,'s','s','s','s','s','s',0),(11,'asd','asdadag','asd','asd','new','061112345678',0),(12,'l','l','l','l','l','l',0),(13,'d','d','d','d','d','d',0),(14,'new','ee','new','ee','ee','ee',0),(15,'laci','pass','cseh','laszlo','laszloooo','23',0),(16,'Username','Password','FirstNamee','LastNameee','email@email.com','066060606060',1),(17,'uj','uj','uj','uj','uj','uj',1),(18,'mars','marsL','marsL','marsL','marsL','marsL',0),(19,'lets','ajajj','ajajj','ajajj','ajajj','ajajj',1),(20,'user','passw','firstN','lmmm','emaill','telll',1),(21,'new','aa','aa','neaaw','aa','aa',0),(22,'laca','11','laszlo','cseh','mail@laci.hu','1111111',1),(23,'Marszu99','Password','Cseh','Marcell','csehmarcell@yagoo.com','06707004990',1),(24,'Username1','Password1','Cseh','Marcell','asd@asd.com','06705678101',1),(25,'Username2','Password2','ASD','DSA','asdd@asd.hu','012345678910',0),(26,'sdjkhaljkdh','asjdhasjdh','asldhjsd','ashldhajd','asdkj@asdjkkk.com','012350123450',1),(27,'aaaaaa','aaaaaaa','aaaaaa','aaaaaaa','aaaaaaaa@aa.hu','01234567810',0),(28,'dsgsdgsdg','sdgsdgs','afsafaas','gfdgfdfdg','asdaa@asaad.hu','0123423525266',0),(29,'NewUser','newPass','newFirst','newLst','asd@asd.com','012345678910',0),(30,'sdakjhdkj','jslhfajs','sjhfsdj','sljfhsd','asdd@asdad.com','012345678910',0),(31,'a','afsafsf','fgddgdfg','fghfhg','gddg@aad.com','01234556771',0),(32,'k','k','k','k','k','k',0),(33,'Username2','Password2','Firstname','Lastname','last@last.hu','06304044551',0),(34,'Username2','Password2','Firstname','Lastname','last@last.hu','06304044551',0),(35,'Username2','Password2','Firstname','Lastname','last@last.hu','06304044551',0),(36,'afgfdgdg','fdgdfgdfg','gfghgfh','fdgdfgfd','ghgfhf@asdkjl.com','0120304404040',0),(37,'Username3','password3','firstname','last','asdk@asdk.com','01234555667',0),(38,'Username3','Password','aslfjaf','askfnak','askdja@askdj.com','01233443555',0),(39,'adshaklsjh','asdkjhajskdh','asdhjkhk','askjdha','jashd@asdj.com','01203013044',0),(40,'asdlaksjdak','sdkfj44','sdkfjjkj','skdfj','ksjd@asdj.com','12476438888',0),(41,'kjasdh','sdkdsk','lsdflj','fdslkj','kdfkj@asd.com','88888888888',0),(42,'asdkadsj','kjhasdkhas','askjdhaksd','kashdk','aksjd@askjdh.com','99999999999',0),(43,'plspls','plspls','plspls','plspls','pls@pls.com','88888888888',0),(44,'csehlaci','csehlaci','cseh','laszlo','lcseh@tradelda.hu','36302988033',1);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `uservaluesview`
--

DROP TABLE IF EXISTS `uservaluesview`;
/*!50001 DROP VIEW IF EXISTS `uservaluesview`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `uservaluesview` AS SELECT 
 1 AS `idUser`,
 1 AS `Username`,
 1 AS `Password`,
 1 AS `FirstName`,
 1 AS `LastName`,
 1 AS `Email`,
 1 AS `Telephone`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `userview`
--

DROP TABLE IF EXISTS `userview`;
/*!50001 DROP VIEW IF EXISTS `userview`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `userview` AS SELECT 
 1 AS `idUser`,
 1 AS `Username`,
 1 AS `Password`,
 1 AS `FirstName`,
 1 AS `LastName`,
 1 AS `Email`,
 1 AS `Telephone`*/;
SET character_set_client = @saved_cs_client;

--
-- Dumping events for database 'demofeladat'
--

--
-- Dumping routines for database 'demofeladat'
--
/*!50003 DROP PROCEDURE IF EXISTS `CreateRecord` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CreateRecord`(IN date DATE,IN comment TEXT, IN duration SMALLINT, IN userid INT,IN taskid INT)
BEGIN
INSERT INTO record (Date,Comment,Duration,User_idUser,Task_idTask) VALUES (date,comment,duration,userid,taskid);
SELECT last_insert_id();
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CreateTaskForUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CreateTaskForUser`(IN title VARCHAR(45),IN description TEXT, IN deadline DATE, IN userid INT )
BEGIN
INSERT INTO task (Title,Description,Deadline,Status,User_idUser,CreationDate) VALUES (title,description,deadline,"Created",userid,CURRENT_DATE());
SELECT last_insert_id();
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CreateUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CreateUser`(IN username VARCHAR(45), IN password VARCHAR(45),
 IN firstname VARCHAR(45),IN lastname VARCHAR(45), IN email VARCHAR(100),IN telephone VARCHAR(13))
BEGIN
INSERT INTO user(Username,Password,FirstName,LastName,Email,Telephone) VALUES(username,password,firstname,
lastname,email,telephone);
SELECT last_insert_id();
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteRecord` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteRecord`(IN id INT)
BEGIN
UPDATE record SET StatusDelete = 0 WHERE record.idRecord = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteTask` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteTask`(IN id INT)
BEGIN
	UPDATE task SET StatusDelete = 0 WHERE task.idTask = id;
    UPDATE record SET StatusDelete = 0 WHERE record.Task_idTask = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteUser`(IN id INT)
BEGIN
	UPDATE user SET StatusDelete = 0 WHERE user.idUser = id;
    UPDATE task SET StatusDelete = 0 WHERE task.User_idUser = id;
    UPDATE record SET StatusDelete = 0 WHERE record.User_idUser = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllActiveTasks` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllActiveTasks`()
BEGIN
SELECT * FROM taskview WHERE taskview.Status != "Done" AND taskview.Status != "2";
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllActiveTasksFromUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllActiveTasksFromUser`(IN userid INT)
BEGIN
SELECT * FROM taskview WHERE taskview.User_idUser = userid AND taskview.Status != "Done" AND taskview.Status != "2";
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllDoneTasksFromUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllDoneTasksFromUser`(IN userid INT)
BEGIN
SELECT * FROM taskview WHERE taskview.User_idUser = userid AND (taskview.Status = "Done" OR taskview.Status = 2);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllRecords` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllRecords`()
BEGIN
SELECT * FROM recordview;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllTasks` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllTasks`()
BEGIN
SELECT * FROM taskview;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAllUsers` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllUsers`()
BEGIN
SELECT * FROM userview;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetUserByID` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetUserByID`(IN id INT)
BEGIN
SELECT * FROM uservaluesview WHERE uservaluesview.idUser=id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetUserByUsername` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetUserByUsername`(IN username VARCHAR(45))
BEGIN
SELECT * FROM userview WHERE userview.Username = username;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetUserRecords` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetUserRecords`(IN userid INT)
BEGIN
SELECT * FROM recordview WHERE recordview.User_idUser = userid;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetUserTasks` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetUserTasks`(IN userid INT)
BEGIN
SELECT * FROM taskview WHERE taskview.User_idUser = userid;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateRecord` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateRecord`(IN id INT, IN date DATE,IN comment TEXT, IN duration SMALLINT, IN userid INT,IN taskid INT)
BEGIN
UPDATE record SET date=Date, comment=Comment, duration=Duration, User_idUser=userid, Task_idTask=taskid WHERE record.idRecord = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateTask` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateTask`(IN id INT, IN title VARCHAR(45),IN description TEXT, IN deadline DATE, IN status VARCHAR(45), IN userid INT)
BEGIN
UPDATE task SET title=Title, description=Description, deadline=Deadline, status=Status, User_idUser=userid WHERE id=task.idTask;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateUser`(IN id INT, IN password VARCHAR(45), IN firstname VARCHAR(45),IN lastname VARCHAR(45),
IN email VARCHAR(100),IN telephone VARCHAR(13))
BEGIN
UPDATE user SET firstname=FirstName, lastname=LastName, password=Password, email=Email, telephone=Telephone WHERE id=user.idUser;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `recordview`
--

/*!50001 DROP VIEW IF EXISTS `recordview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `recordview` AS select `record`.`idRecord` AS `idRecord`,`record`.`Date` AS `Date`,`record`.`Comment` AS `Comment`,`record`.`Duration` AS `Duration`,`userview`.`idUser` AS `User_idUser`,`userview`.`Username` AS `Username`,`taskview`.`idTask` AS `Task_idTask`,`taskview`.`Title` AS `Task_Title`,`taskview`.`Status` AS `Task_Status` from ((`record` join `userview` on((`record`.`User_idUser` = `userview`.`idUser`))) join `taskview` on((`record`.`Task_idTask` = `taskview`.`idTask`))) where (`record`.`StatusDelete` = 1) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `taskview`
--

/*!50001 DROP VIEW IF EXISTS `taskview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `taskview` AS select `task`.`idTask` AS `idTask`,`task`.`Title` AS `Title`,`task`.`Description` AS `Description`,`task`.`Deadline` AS `Deadline`,`task`.`Status` AS `Status`,`task`.`CreationDate` AS `CreationDate`,`userview`.`idUser` AS `User_idUser`,`userview`.`Username` AS `Username` from (`task` join `userview` on((`task`.`User_idUser` = `userview`.`idUser`))) where (`task`.`StatusDelete` = 1) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `uservaluesview`
--

/*!50001 DROP VIEW IF EXISTS `uservaluesview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `uservaluesview` AS select `user`.`idUser` AS `idUser`,`user`.`Username` AS `Username`,`user`.`Password` AS `Password`,`user`.`FirstName` AS `FirstName`,`user`.`LastName` AS `LastName`,`user`.`Email` AS `Email`,`user`.`Telephone` AS `Telephone` from `user` where (`user`.`StatusDelete` = 1) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `userview`
--

/*!50001 DROP VIEW IF EXISTS `userview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `userview` AS select `user`.`idUser` AS `idUser`,`user`.`Username` AS `Username`,`user`.`Password` AS `Password`,`user`.`FirstName` AS `FirstName`,`user`.`LastName` AS `LastName`,`user`.`Email` AS `Email`,`user`.`Telephone` AS `Telephone` from `user` where (`user`.`StatusDelete` = 1) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-11-08 23:06:23
