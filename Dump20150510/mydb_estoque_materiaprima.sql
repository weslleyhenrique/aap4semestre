-- MySQL dump 10.13  Distrib 5.6.23, for Win64 (x86_64)
--
-- Host: localhost    Database: mydb
-- ------------------------------------------------------
-- Server version	5.6.24-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `estoque_materiaprima`
--

DROP TABLE IF EXISTS `estoque_materiaprima`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `estoque_materiaprima` (
  `id_estoque_matprima` int(11) NOT NULL,
  `id_matprima` int(11) NOT NULL,
  `id_planocontas` int(11) NOT NULL,
  `data_estocagem` date DEFAULT NULL,
  `quant_minima` int(11) DEFAULT NULL,
  `quant_maxima` int(11) DEFAULT NULL,
  `quant_atual` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_estoque_matprima`),
  KEY `fk_Estoque_MateriaPrima_Materia-Prima1_idx` (`id_matprima`),
  KEY `fk_Estoque_MateriaPrima_Conta_Movimentação1_idx` (`id_planocontas`),
  CONSTRAINT `fk_Estoque_MateriaPrima_Conta_Movimentação1` FOREIGN KEY (`id_planocontas`) REFERENCES `plano_contas` (`id_planocontas`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Estoque_MateriaPrima_Materia-Prima1` FOREIGN KEY (`id_matprima`) REFERENCES `materia-prima` (`id_matprima`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estoque_materiaprima`
--

LOCK TABLES `estoque_materiaprima` WRITE;
/*!40000 ALTER TABLE `estoque_materiaprima` DISABLE KEYS */;
INSERT INTO `estoque_materiaprima` VALUES (3,123,1,'2015-05-10',1,10,5);
/*!40000 ALTER TABLE `estoque_materiaprima` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-05-10 22:33:40
