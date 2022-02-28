-- MySQL dump 10.13  Distrib 8.0.22, for Win64 (x86_64)
--
-- Host: localhost    Database: szakdoga
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
-- Table structure for table `company`
--

DROP TABLE IF EXISTS `company`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `company` (
  `idCompany` int NOT NULL AUTO_INCREMENT,
  `CompanyName` varchar(60) NOT NULL,
  `StatusDelete` tinyint DEFAULT '1',
  PRIMARY KEY (`idCompany`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `company`
--

LOCK TABLES `company` WRITE;
/*!40000 ALTER TABLE `company` DISABLE KEYS */;
INSERT INTO `company` VALUES (1,'dd',0),(2,'Mar',0),(3,'BLB-Soft',0),(4,'Tradelda Kft.',0),(5,'trad dddddd',0),(6,'Tradelda Kft.',1);
/*!40000 ALTER TABLE `company` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `companyview`
--

DROP TABLE IF EXISTS `companyview`;
/*!50001 DROP VIEW IF EXISTS `companyview`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `companyview` AS SELECT 
 1 AS `idCompany`,
 1 AS `CompanyName`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `notification`
--

DROP TABLE IF EXISTS `notification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `notification` (
  `idNotification` int NOT NULL AUTO_INCREMENT,
  `Message` text NOT NULL,
  `NotificationFor` tinyint NOT NULL,
  `ReadFlag` tinyint DEFAULT '1',
  `StatusDelete` tinyint DEFAULT '1',
  `Task_idTask` int NOT NULL,
  PRIMARY KEY (`idNotification`,`Task_idTask`),
  KEY `fk_Notification_Task1_idx` (`Task_idTask`),
  CONSTRAINT `fk_Notification_Task1` FOREIGN KEY (`Task_idTask`) REFERENCES `task` (`idTask`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notification`
--

LOCK TABLES `notification` WRITE;
/*!40000 ALTER TABLE `notification` DISABLE KEYS */;
INSERT INTO `notification` VALUES (1,'asd',0,0,1,1),(2,'asd',0,0,0,2),(3,'Updated task!',0,0,1,6),(4,'Updated task!',0,0,1,3),(5,'Updated task!',0,0,1,4),(6,'Updated task!',0,0,1,3),(7,'Updated task!',0,0,1,4),(8,'Updated task!',0,0,1,6),(9,'Updated task!',0,0,1,6),(10,'Updated task!',0,0,1,2),(11,'Updated task!',0,0,1,5),(12,'Updated task!',0,0,1,5),(13,'Updated task!',0,0,1,5),(14,'Updated task!',0,0,1,6),(15,'Updated task!',0,0,1,3),(16,'Updated task!',0,0,1,2),(17,'Updated task!',0,0,1,4),(18,'Updated task!',0,0,1,3),(19,'Updated task!',0,0,1,3),(20,'Updated task!',0,0,1,3),(21,'Task has been updated! Description has changed!',0,0,1,6),(22,'Task has been updated! Description has changed!',0,0,1,4),(24,'Task has been updated! Title and Description and Deadline has changed!',0,0,1,6),(25,'Task has been updated! Title and Description and Deadline has changed!',0,0,1,6),(26,'Task has been updated! Description has changed!',0,0,1,6),(29,'asd',0,0,0,10),(30,'asd',0,0,0,10),(31,'NotificationNewTask',0,1,1,14),(32,'Task has been updated! Deadline has changed!',0,0,1,5),(33,'Task has been updated! Deadline has changed!',0,0,1,2),(34,'New task!',0,0,1,15),(35,'Task has been updated! Description has changed!',0,0,1,15),(36,'Task has been updated! Description and Deadline has changed!',0,0,1,15),(37,'NotificationNewTask',0,0,1,16),(38,'Task has been updated! Description has changed!',0,0,1,16),(39,'NotificationTaskInProgress',1,1,1,6),(40,'NotificationTaskDescriptionChanged',0,1,1,6),(41,'NotificationTaskDeadlineChanged',0,1,1,6);
/*!40000 ALTER TABLE `notification` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `notificationview`
--

DROP TABLE IF EXISTS `notificationview`;
/*!50001 DROP VIEW IF EXISTS `notificationview`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `notificationview` AS SELECT 
 1 AS `idNotification`,
 1 AS `Message`,
 1 AS `NotificationFor`,
 1 AS `ReadFlag`,
 1 AS `Task_idTask`*/;
SET character_set_client = @saved_cs_client;

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
  KEY `fk_Record_Task1_idx` (`Task_idTask`),
  KEY `fk_Record_User1_idx` (`User_idUser`) /*!80000 INVISIBLE */,
  CONSTRAINT `fk_Record_Task1` FOREIGN KEY (`Task_idTask`) REFERENCES `task` (`idTask`),
  CONSTRAINT `fk_Record_User1` FOREIGN KEY (`User_idUser`) REFERENCES `user` (`idUser`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `record`
--

LOCK TABLES `record` WRITE;
/*!40000 ALTER TABLE `record` DISABLE KEYS */;
INSERT INTO `record` VALUES (4,'2021-12-15','Regisztacio',250,1,9,2),(5,'2021-12-11','Teszt',230,1,10,3),(6,'2021-12-18','Proba',220,1,9,5),(7,'2021-12-11',NULL,210,0,10,3),(8,'2021-12-12','Szép lett!',250,1,10,4),(9,'2021-12-18','Komment',120,1,9,2),(10,'2021-12-21',NULL,210,0,10,6),(11,'2021-12-21',NULL,210,0,10,6),(12,'2021-12-22','Kész',210,1,9,9),(13,'2021-12-23',NULL,210,0,10,6),(14,'2021-12-23',NULL,210,0,10,6),(15,'2022-02-08','',220,0,10,14),(16,'2022-02-17',NULL,210,0,10,14),(17,'2022-02-21',NULL,210,0,9,38),(18,'2022-02-22','dd',210,0,10,14),(19,'2022-02-22','ads',210,0,10,14),(20,'2022-02-22','d',210,0,9,45),(21,'2022-02-22','ss',210,0,9,5),(22,'2022-02-22','asd',210,0,9,18),(23,'2022-02-22','dd',240,0,9,18),(24,'2022-02-22','a',230,0,9,18),(25,'2022-02-22','d',210,0,9,18),(26,'2022-02-22','aaaaa',210,0,9,18),(27,'2022-02-23','a',230,0,9,18),(28,'2022-02-23','as',180,0,9,18),(29,'2022-02-23','a',210,0,9,18),(30,'2022-02-23','d',210,0,9,18),(31,'2022-02-23','asd',210,0,9,18),(32,'2022-02-23','dd',210,0,9,18);
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
  `Status` varchar(45) NOT NULL DEFAULT '0',
  `StatusDelete` tinyint DEFAULT '1',
  `CreationDate` date NOT NULL,
  `User_idUser` int NOT NULL,
  PRIMARY KEY (`idTask`,`User_idUser`),
  KEY `fk_Task_User1_idx` (`User_idUser`),
  CONSTRAINT `fk_Task_User1` FOREIGN KEY (`User_idUser`) REFERENCES `user` (`idUser`)
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `task`
--

LOCK TABLES `task` WRITE;
/*!40000 ALTER TABLE `task` DISABLE KEYS */;
INSERT INTO `task` VALUES (1,'ddd',NULL,'2021-12-10','Created',0,'2021-12-09',1),(2,'Programozas','Mukodjon!','2022-07-22','InProgress',1,'2021-12-11',9),(3,'Teszteles','Unit tesztek','2022-03-18','Done',1,'2021-12-11',10),(4,'Design','Szep legyem!','2022-06-24','InProgress',1,'2021-12-11',10),(5,'DB','','2023-05-26','InProgress',1,'2021-12-11',9),(6,'Git','Nagyooooon hosssszuuuuu leiraaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaas','2022-07-31','InProgress',1,'2021-12-11',10),(7,'ASD',NULL,'2021-12-23','Created',0,'2021-12-22',10),(8,'ad','a','2020-01-01','Created',0,'2021-12-22',10),(9,'Wiki','','2021-12-23','Done',1,'2021-12-22',9),(10,'asd',NULL,'2021-12-24','Created',0,'2021-12-23',10),(11,'d',NULL,'2021-12-24','Created',0,'2021-12-23',9),(12,'a',NULL,'2021-12-24','Created',0,'2021-12-23',9),(13,'b',NULL,'2021-12-24','Created',0,'2021-12-23',9),(14,'AutomatákPDF','','2022-05-13','Created',1,'2021-12-23',10),(15,'d','dddd','2022-07-07','Created',0,'2022-01-27',10),(16,'ss','as','2022-01-28','Created',0,'2022-01-27',10),(17,'TestRefresh',NULL,'2022-08-26','Created',0,'2022-02-08',9),(18,'TestRefresh','','2022-08-27','Created',1,'2022-02-08',9),(19,'d',NULL,'2022-02-09','Created',0,'2022-02-08',9),(20,'a',NULL,'2022-02-18','Created',0,'2022-02-17',9),(21,'l',NULL,'2022-02-18','Created',0,'2022-02-17',9),(22,'asd',NULL,'2022-03-04','Created',0,'2022-02-17',9),(23,'kk',NULL,'2022-02-18','Created',0,'2022-02-17',9),(24,'ll',NULL,'2022-02-25','Created',0,'2022-02-17',9),(25,'a',NULL,'2022-02-20','Created',0,'2022-02-19',9),(26,'d','','2022-02-20','Created',0,'2022-02-19',9),(27,'d',NULL,'2022-02-26','Created',0,'2022-02-19',9),(28,'a',NULL,'2022-03-04','Created',0,'2022-02-19',9),(29,'k',NULL,'2022-03-02','Created',0,'2022-02-19',9),(30,'l',NULL,'2022-03-02','Created',0,'2022-02-19',9),(31,'ll',NULL,'2022-03-03','Created',0,'2022-02-19',9),(32,'d',NULL,'2022-02-20','Created',0,'2022-02-19',9),(33,'d',NULL,'2022-03-05','Created',0,'2022-02-21',9),(34,'asd',NULL,'2022-03-11','Created',0,'2022-02-21',9),(35,'ddd',NULL,'2022-02-25','Created',0,'2022-02-21',9),(36,'s',NULL,'2022-02-22','Created',0,'2022-02-21',9),(37,'d',NULL,'2022-02-22','Created',0,'2022-02-21',9),(38,'a',NULL,'2022-02-22','Created',0,'2022-02-21',9),(39,'d',NULL,'2022-02-23','Created',0,'2022-02-22',9),(40,'dd',NULL,'2022-02-23','Created',0,'2022-02-22',9),(41,'dddd',NULL,'2022-02-23','Created',0,'2022-02-22',9),(42,'k',NULL,'2022-02-23','Created',0,'2022-02-22',9),(44,'l','k','2022-02-23','Created',0,'2022-02-22',9),(45,'asd',NULL,'2022-02-23','Created',0,'2022-02-22',9),(46,'ss',NULL,'2022-02-24','Created',0,'2022-02-23',9),(47,'asd','dd','2022-02-24','Created',0,'2022-02-23',9);
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
  `FirstName` varchar(45) DEFAULT NULL,
  `LastName` varchar(45) DEFAULT NULL,
  `Email` varchar(100) NOT NULL,
  `Telephone` varchar(13) DEFAULT NULL,
  `Status` tinyint NOT NULL,
  `StatusDelete` tinyint DEFAULT '1',
  `Company_idCompany` int NOT NULL,
  PRIMARY KEY (`idUser`,`Company_idCompany`),
  KEY `fk_User_Company1_idx` (`StatusDelete`),
  KEY `fk_User_Company1_idx1` (`Company_idCompany`),
  CONSTRAINT `fk_User_Company1` FOREIGN KEY (`Company_idCompany`) REFERENCES `company` (`idCompany`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'asd','asd','asd','asd','asd','76876',1,0,1),(2,'Marszi99','Password','Cseh','Marci','csehmarcell@yahoo.com','06898776778',1,0,2),(3,'Marszi','Password','Marci','Cseh','csehmarcell@yahoo.com','06876876378',1,0,3),(4,'Marszu99','Password','Marcell','Cseh','csehmarcell@yahoo.com','06987394827',1,0,4),(5,'CsehHedvig','nfq3XGTxXP',NULL,NULL,'csehmarcell@yahoo.com',NULL,0,0,4),(6,'CsehMarci','Ht7TpfQDkr',NULL,NULL,'csehmarcell@yahoo.com',NULL,0,0,4),(7,'csehlaszlo','lacko12','aaaa','aaaa','csehmarcell@yahoo.com','36302988033',1,0,5),(8,'KovacsGeza','ObPZ0BmJoK',NULL,NULL,'csehmarcell@yahoo.com',NULL,0,0,5),(9,'Marszu99','Password','Marcell','Cseh','csehmarcell@yahoo.com','06703070041',1,1,6),(10,'Username','Password','Laszlo','Cseh','csehmarcell@yahoo.com','06704009188',0,1,6),(11,'CsehMarcell','Password','Cseh','Marcell','csehmarcell@yahoo.com','+367088788666',0,0,6),(12,'asdddd','od5575qs1D',NULL,NULL,'csehmarcell@yahoo.com',NULL,0,0,6),(13,'DeleteTeszt','DeleteTeszt',NULL,NULL,'DeleteTeszt',NULL,0,0,6),(14,'Username1','IdlUkA5G6W',NULL,NULL,'csehmarcell@yahoo.com',NULL,0,0,6),(15,'Username1','1JiWgX9o5Z',NULL,NULL,'csehmarcell@yahoo.com',NULL,0,0,6),(16,'Username1','AFaYlJnION','asda','asdd','csehmarcell@yahoo.com','06777878788',0,0,6),(17,'Username1','l5MAGzOgsr','asdasd','asd','csehmarcell@yahoo.com','06789877799',0,0,6),(18,'Username1','bo3JzWWbr2','asdd','asdd','csehmarcell@yahoo.com','0678877997',0,0,6),(19,'Username1','PqcE56V3x4','asdd','asdasd','csehmarcell@yahoo.com','0678778999',0,0,6),(20,'Username1','EhhmAJZAKi','asd','aasd','csehmarcell@yahoo.com','0678799777',0,0,6),(21,'Username1','yOsc4bSvx0','asdd','asdd','csehmarcell@yahoo.com','0670987987',0,0,6),(22,'Username1','rqwWWX0GDg','asdd','asdd','csehmarcell@yahoo.com','06709878979',0,0,6),(23,'Username1','n1Po55bbXH','asd','asd','csehmarcell@yahoo.com','0670876878',0,0,6),(24,'Username1','jDnFJbLRfI',NULL,NULL,'csehmarcell@yahoo.com',NULL,0,0,6),(25,'asddddd','h6XACndJwz',NULL,NULL,'csehmarcell@yahoo.com',NULL,0,0,6);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

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
 1 AS `Telephone`,
 1 AS `Status`,
 1 AS `Company_idCompany`,
 1 AS `CompanyName`*/;
SET character_set_client = @saved_cs_client;

--
-- Dumping events for database 'szakdoga'
--

--
-- Dumping routines for database 'szakdoga'
--
/*!50003 DROP PROCEDURE IF EXISTS `CreateNotificationForTask` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CreateNotificationForTask`(IN message TEXT, IN notificationFor TINYINT, IN taskid INT)
BEGIN
INSERT INTO notification (Message,Task_idTask,NotificationFor) VALUES (message,taskid,notificationFor);
SELECT last_insert_id();
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
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
IN email VARCHAR(100), IN companyID INT)
BEGIN
INSERT INTO user(Username,Password,Email,Status,Company_idCompany) VALUES(username,password,email,'0',companyID);
SELECT last_insert_id();
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteNotification` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteNotification`(IN id INT)
BEGIN
	UPDATE notification SET StatusDelete = 0 WHERE notification.idNotification = id;
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
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteUser`(IN id INT, IN status TINYINT)
BEGIN
    IF status = 1 THEN 
		UPDATE company SET company.StatusDelete = 0;
        UPDATE user SET user.StatusDelete = 0;
        UPDATE task SET task.StatusDelete = 0;
        UPDATE record SET record.StatusDelete = 0;
	ELSE
		UPDATE user SET StatusDelete = 0 WHERE idUser = id;
		UPDATE task SET StatusDelete = 0 WHERE task.User_idUser = id;
		UPDATE record SET StatusDelete = 0 WHERE record.User_idUser = id;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAdmin` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAdmin`()
BEGIN
SELECT * FROM userview WHERE userview.Status=1;
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
/*!50003 DROP PROCEDURE IF EXISTS `GetCompany` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetCompany`()
BEGIN
SELECT * FROM companyview;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTaskByID` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTaskByID`(IN taskid INT)
BEGIN
SELECT * FROM taskview WHERE taskview.idTask=taskid;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTaskNotificationsForAdmin` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTaskNotificationsForAdmin`(IN taskid INT)
BEGIN
SELECT * FROM notificationview WHERE notificationview.Task_idTask = taskid && notificationview.ReadFlag = "1" && notificationview.NotificationFor = "1";
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTaskNotificationsForEmployee` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTaskNotificationsForEmployee`(IN taskid INT)
BEGIN
SELECT * FROM notificationview WHERE notificationview.Task_idTask = taskid && notificationview.ReadFlag = "1" && notificationview.NotificationFor = "0";
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTaskRecords` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTaskRecords`(IN taskid INT)
BEGIN
SELECT * FROM recordview WHERE recordview.Task_idTask = taskid;
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
SELECT * FROM userview WHERE userview.idUser=id;
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
/*!50003 DROP PROCEDURE IF EXISTS `HasReadNotification` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `HasReadNotification`(IN id INT, IN notificationFor TINYINT)
BEGIN
	UPDATE notification SET ReadFlag = 0 WHERE notification.Task_idTask = id && notification.NotificationFor = notificationFor;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `RegisterAdmin` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `RegisterAdmin`(IN username VARCHAR(45), IN password VARCHAR(45), IN firstname VARCHAR(45),
IN lastname VARCHAR(45), IN email VARCHAR(100),IN telephone VARCHAR(13), IN companyName VARCHAR(60))
BEGIN
DECLARE companyID INT;
INSERT INTO company(CompanyName) VALUES(companyName);
SET companyID = LAST_INSERT_ID();
INSERT INTO user(Username,Password,FirstName,LastName,Email,Telephone,Status,Company_idCompany) VALUES(username,password,firstname,
lastname,email,telephone,'1',companyID);
SELECT last_insert_id();
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
UPDATE user SET password=Password, firstname=FirstName, lastname=LastName, email=Email, telephone=Telephone WHERE id=user.idUser;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `companyview`
--

/*!50001 DROP VIEW IF EXISTS `companyview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `companyview` AS select `company`.`idCompany` AS `idCompany`,`company`.`CompanyName` AS `CompanyName` from `company` where (`company`.`StatusDelete` = 1) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `notificationview`
--

/*!50001 DROP VIEW IF EXISTS `notificationview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `notificationview` AS select `notification`.`idNotification` AS `idNotification`,`notification`.`Message` AS `Message`,`notification`.`NotificationFor` AS `NotificationFor`,`notification`.`ReadFlag` AS `ReadFlag`,`taskview`.`idTask` AS `Task_idTask` from (`notification` join `taskview` on((`notification`.`Task_idTask` = `taskview`.`idTask`))) where (`notification`.`StatusDelete` = 1) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

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
/*!50001 VIEW `userview` AS select `user`.`idUser` AS `idUser`,`user`.`Username` AS `Username`,`user`.`Password` AS `Password`,`user`.`FirstName` AS `FirstName`,`user`.`LastName` AS `LastName`,`user`.`Email` AS `Email`,`user`.`Telephone` AS `Telephone`,`user`.`Status` AS `Status`,`companyview`.`idCompany` AS `Company_idCompany`,`companyview`.`CompanyName` AS `CompanyName` from (`user` join `companyview` on((`user`.`Company_idCompany` = `companyview`.`idCompany`))) where (`user`.`StatusDelete` = 1) */;
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

-- Dump completed on 2022-02-28 17:23:07
