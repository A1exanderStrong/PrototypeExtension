CREATE DATABASE  IF NOT EXISTS `prototype` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `prototype`;
-- MySQL dump 10.13  Distrib 8.0.28, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: prototype
-- ------------------------------------------------------
-- Server version	8.0.28

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
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (1,'3д модель'),(2,'Анимация'),(4,'Визуальные эффекты'),(3,'Звуковые эффекты'),(9,'Материалы и текстуры'),(7,'Окружение'),(8,'Плагины'),(5,'Пользовательский интерфейс'),(6,'Шаблоны игр');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchases`
--

DROP TABLE IF EXISTS `purchases`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `purchases` (
  `resource_id` int NOT NULL,
  `user_id` int NOT NULL,
  `purchase_date` date NOT NULL,
  `cost` double DEFAULT NULL,
  PRIMARY KEY (`resource_id`,`user_id`),
  KEY `resource_idx` (`resource_id`),
  KEY `user_idx` (`user_id`),
  CONSTRAINT `resource` FOREIGN KEY (`resource_id`) REFERENCES `resources` (`id`),
  CONSTRAINT `user` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchases`
--

LOCK TABLES `purchases` WRITE;
/*!40000 ALTER TABLE `purchases` DISABLE KEYS */;
INSERT INTO `purchases` VALUES (1,3,'2025-02-12',500),(1,21,'2025-03-11',500),(1,22,'2025-04-02',600),(2,6,'2025-01-12',700),(3,7,'2025-03-10',499),(4,7,'2025-03-10',899),(4,22,'2025-04-02',899),(9,21,'2025-03-10',699),(10,6,'2025-01-12',2099),(10,22,'2025-03-10',2099),(12,3,'2025-03-11',2099),(17,22,'2025-04-02',695),(21,22,'2025-03-11',299),(24,24,'2025-03-17',599),(33,3,'2025-04-02',599),(42,11,'2025-04-02',499),(43,11,'2025-04-02',399);
/*!40000 ALTER TABLE `purchases` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `resources`
--

DROP TABLE IF EXISTS `resources`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `resources` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `description` varchar(800) NOT NULL,
  `picture` varchar(200) DEFAULT NULL,
  `price` float NOT NULL,
  `category` int NOT NULL,
  `created_by` int NOT NULL,
  `publication_date` date NOT NULL,
  `blob_picture` longblob,
  PRIMARY KEY (`id`),
  KEY `category_idx` (`category`),
  KEY `created_by_idx` (`created_by`),
  CONSTRAINT `category` FOREIGN KEY (`category`) REFERENCES `categories` (`id`) ON UPDATE CASCADE,
  CONSTRAINT `created_by` FOREIGN KEY (`created_by`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=44 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `resources`
--

LOCK TABLES `resources` WRITE;
/*!40000 ALTER TABLE `resources` DISABLE KEYS */;
INSERT INTO `resources` VALUES (1,'Blockout Starter Pack','Этот набор ресурсов включает в себя несколько примитивов, модульные ресурсы, реквизит, материалы и декали, которые облегчают блокировку уровня в Unreal Engine.','https://media.fab.com/image_previews/gallery_images/d53680c7-5ef0-4532-9388-2d767cad0779/e6ed8506-fa5c-4121-860c-705b55e7ea5a.jpg',600,1,6,'2025-02-24',NULL),(2,'MC Sample Pack','Это коллекция из более чем 100 реалистичных анимаций, которые придадут жизни и правдоподобия персонажам вашего проекта. Мы получили удовольствие от выступлений в этом наборе! Множество вариаций - от колдовства с помощью Книги заклинаний до шатания в пьяном виде, игры на пианино или даже катания на воображаемой лошади (из классического британского фильма).','https://media.fab.com/image_previews/gallery_images/f367acb5-e4ff-4d0a-abd6-36a8701279d7/b5c54668-a011-4e83-805c-81a089685027.jpg',1099,2,7,'2025-02-24',NULL),(3,'Sound Phenomenon Fantasy Orchestra','Коллекция оркестровой музыки, созданной для создания настроения в играх в жанре фэнтези и мрачного фэнтези. Включено 9 треков.','https://media.fab.com/image_previews/gallery_images/43e75514-7d47-4dd5-95eb-72b41ac02512/12749ac6-9fab-4695-82aa-34ca73a6a070.jpg',500,3,8,'2025-02-19',NULL),(4,'Realistic Starter VFX Pack Vol 2','Этот пакет призван облегчить вам жизнь, когда вы начинаете свой собственный проект, и содержит все эффекты, необходимые для начала разработки и создания прототипов.','https://media.fab.com/image_previews/gallery_images/20cb4247-9a93-41c3-b66a-4e0bdcbe75df/17a2ffc9-a22e-4fc1-aa60-0db69a8e45e4.jpg',899,4,10,'2023-05-18',NULL),(5,'Essential Elements SFX Pack','Essential Elements SFX - это коллекция из 8 типов звуковых материалов и обработанных эффектов для повышения четкости, которая также подходит для экспериментального звукового дизайна. Тип материалов: Воздух -Электричество - Огонь - Лед - Металл - Камни - Вода - Дерево.','https://media.fab.com/image_previews/gallery_images/7b1e705f-d8f1-40ba-9ad9-3777da07097c/6458d1c5-5d65-4317-bbfa-7919206e7be8.jpg',399,3,9,'2023-05-17',NULL),(6,'Fire VDB Pack Loop','Fire VDB Effects Pack - это обширная коллекция высококачественных циклических огненных эффектов.','https://media.fab.com/image_previews/gallery_images/34ac62c3-13df-4546-8dda-b32d842e920a/02ca05e7-ac48-43a1-8bb4-1b535b688d5e.jpg',1199,4,9,'2024-07-21',NULL),(7,'Simple Inventory System','Этот плагин предоставляет простое и эффективное решение для управления предметами в ваших играх. Он позволяет игрокам собирать, хранить и упорядочивать предметы, чтобы вы могли легко взаимодействовать с их инвентарем. Идеально подходящий для проектов, требующих отслеживания предметов, этот плагин упрощает сбор, использование и управление ресурсами в ваших играх, обеспечивая при этом прочную основу для более расширенной функциональности.','https://media.fab.com/image_previews/gallery_images/c32d056a-1401-4376-a508-6b834300b1b4/746460c5-f00f-4e36-b67f-b456e5887fea.jpg',1599,8,21,'2022-12-01',NULL),(8,'Primitive Characters (Pack)','В наборе вы найдете мужчину, женщину, ребенка, дедушку и бабушку. У персонажа немного изменилось выражение лица и немного оружия.','https://media.fab.com/image_previews/gallery_images/d24db3f8-fa19-404b-8548-55a93037b7b7/e80045cd-5a06-4495-adcf-79292806ab78.jpg',1399,1,24,'2025-01-03',NULL),(9,'Essentials Icon Pack - PC','В этом наборе более 1500 значков кнопок для ПК. В настоящее время доступно 3 темы оформления в 6 различных разрешениях.','https://media.fab.com/image_previews/gallery_images/6dfb8418-b014-420c-9a9d-0962dda93fb0/ea113f92-c1fd-4510-9c21-bf572866e1ed.jpg',699,5,11,'2025-01-04',NULL),(10,'TAR-21 Assault Rifle','Высококачественная модель автомата с 4K PBR текстурами для максимальной детализации','https://media.fab.com/image_previews/gallery_images/978640d0-b03a-4877-a7c0-1ad8923858ad/80520f02-3d34-43d2-85e9-42fd14ef6aad.jpg',2099,1,23,'2025-01-08',NULL),(11,'Viking','Игровая 3d-модель персонажа викинга была создана в соответствии с моей концепцией и анимирована для unreal engine. Эта модель идеально подходит для фэнтезийных и RPG игр.','https://media.fab.com/image_previews/gallery_images/9f5c0251-8861-4bc0-a527-0538816f55d7/c9590d31-08a4-426c-b70b-3b8d8cdf7381.jpg',3099,1,25,'2025-01-09',NULL),(12,'Character Interaction Add-On Vol.01','Набор анимаций взаимодействия персонажа с различными объектами','https://media.fab.com/image_previews/gallery_images/be4021cf-622a-410c-ab95-82d50eb3aab7/75eaf6ff-1e69-47ab-add2-8e188708d3c1.jpg',2099,2,9,'2025-04-01',NULL),(13,'Mega Spear Animation Pack','Пакет содержит 219 анимаций в 10 ключевых позах. (зеркальных версий нет) и еще несколько в разработке','https://media.fab.com/image_previews/gallery_images/189c937c-291e-4848-b41a-f09ced3bfea7/1cde8277-85c3-4cad-88bf-be2078b908aa.jpg',2199,2,34,'2025-04-04',NULL),(14,'UEFN Manny Zombie Animation Sample Pack','Манекен UEFN с простым набором анимациий для зомби.','https://media.fab.com/image_previews/gallery_images/9011865a-6343-4226-8431-bfcafdefbb7a/029aab4d-cc70-4b4c-a33a-d4fc216029cf.jpg',1099,2,32,'2024-02-01',NULL),(15,'Various Attitudes animations (Motion Cast)','В этом наборе анимаций мы хотели предложить несколько простых анимаций, в которых персонаж остается на месте и реагирует по-разному. Персонаж не будет двигаться на протяжении всей анимации.','https://media.fab.com/image_previews/gallery_images/957f7ec8-1e48-49e2-955f-dd386f4ce6b2/b7bd21e5-06ec-4073-a17b-5a8ae950425f.jpg',700,2,33,'2024-02-06',NULL),(16,'Quantum Modular Character','Используя наши продукты, вы сможете: ','https://media.fab.com/image_previews/gallery_images/9386ab54-4466-4414-8f9c-403fee64a225/d645e715-d7a7-4817-9a51-a6f0a162b013.jpg',900,1,31,'2024-02-06',NULL),(17,'Medieval Helmet ( 25 pcs )','Мы предлагаем отличное решение для ваших игр, анимационных фильмов, рекламных роликов, VR-проектов и симуляторов. В процессе работы вы можете просмотреть наш магазин в поисках лучших 3d-объектов и сред','https://media.fab.com/image_previews/gallery_images/779cf71b-24b5-4531-8e64-842488379f43/bdd032a7-5129-40e8-b7af-aa430d1b492d.jpg',695,1,34,'2024-01-05',NULL),(18,'Helmet Pack 2','Мы предлагаем отличное решение для ваших игр, анимационных фильмов, рекламных роликов, VR-проектов и симуляторов. В процессе работы вы можете просмотреть наш магазин в поисках лучших 3d-объектов и сред','https://media.fab.com/image_previews/gallery_images/b3e7b435-851b-49ad-9a2d-1df54095e622/de0b06ff-1546-4655-b820-fed0df9bdb2c.jpg',695,1,34,'2024-03-15',NULL),(19,'Old West VOL.1 - Interior Furniture','Этот проект включает в себя все изображенное, включая все ресурсы, карты и материалы, созданные на движке Unreal Engine. Каждый ресурс был создан с учетом реалистичных визуальных эффектов AAA-качества, стиля и бюджета.','https://media.fab.com/image_previews/gallery_images/af926c15-9892-4510-82c2-87395d8aa0ed/f1fa42a6-35c5-473c-acfd-5b129b8e5b6e.jpg',459,1,29,'2025-01-01',NULL),(20,'Cinematic Earth Project','Всем привет, я делюсь с вами своим самым первым проектом Unreal на FAB. Пожалуйста, имейте в виду, что это ранний релиз, и со временем появятся новые обновления. Прежде всего, я хочу поблагодарить NASA Earth Observatory и НАСА за все текстуры, которые я загрузил и отредактировал. Описание не является окончательным и будет обновляться по мере добавления дополнительных функций в проект','https://media.fab.com/image_previews/gallery_images/f689230f-322c-440c-9273-3ab39debd196/f68ffbe2-7741-4ec8-86a1-363e029fdae4.jpg',599,7,30,'2025-02-03',NULL),(21,'Saloon Interior','Загляните выпить в бар с этим модульным набором аксессуаров для интерьера салона, который включает в себя все - от барных стульев и столов до винтажных вывесок и украшений. Создавайте захватывающие и реалистичные сцены в салоне, которые перенесут вашу аудиторию в прошлое.','https://media.fab.com/image_previews/gallery_images/e4637b98-7aa0-4a3d-8a6c-8ef1b6a9ac03/c6c87aec-d4ea-4cdf-a7a2-cf132f9f9f47.jpg',299,7,30,'2024-12-03',NULL),(24,'Telegraph Room','Вдохновленный старым фильмом, я создал эту старую телеграфную комнату. При поддержке Nanite и Lumen.','https://media.fab.com/image_previews/gallery_images/a158c751-d5d9-49a6-ba5f-f4bf6bda0019/4031ba69-1f79-4206-84ff-7c55a7e9a92d.jpg',599,7,24,'2024-12-23',NULL),(25,'TPP Template Replicated','Реплицированный шаблон от третьего лица с основными функциями передвижения.Этот шаблон поставляется с базовым прицелом (без реквизита).У всех игроков есть возможность переключать ориентацию и перемещение.','https://media.fab.com/image_previews/gallery_images/d1951938-e5be-4212-91fc-5ffcb4b12bce/f7b42915-3f29-46c8-a70c-c5a81cfb7f21.jpg',599,6,2,'2025-04-02',NULL),(26,'Black Hole Fragment Shader','Это черная дыра, полностью созданная с использованием фрагментарного шейдерного кода и некоторых чертежей материалов. Для расчета кривизны света вокруг черной дыры используются приближенные математические уравнения Эйнштейна. Также поставляется с множеством различных настраиваемых параметров','https://media.fab.com/image_previews/gallery_images/47adea6e-c1dc-4c2b-adb1-de58440d98b5/a29e15ef-1f26-468e-a262-3742259e39d3.jpg',299,9,6,'2024-11-09',NULL),(27,'MLS Props Texturing','Текстурирование MLS Props (MLSPT) - это система текстурирования на основе слоев, разработанная в Unreal Engine, которая позволяет текстурировать 3D-модель (Props); для этой цели в ней есть различные слои материалов (ML) и слои смешивания материалов (MLB), которые были разработаны для охвата различных процессов и получения набор текстур, то есть для окончательного выпекания. Этот процесс выполняется послойно, очень похоже на популярное программное обеспечение для текстурирования','https://media.fab.com/image_previews/gallery_images/2adb6485-0e2d-4842-ae5c-22b2d1e9bf5b/27200c66-e783-42fa-9f34-e52bd60575ab.jpg',399,9,34,'2024-10-21',NULL),(33,'Advanced Realistic Glass','Современное реалистичное стекло - это превосходный материал, созданный для обеспечения исключительной реалистичности и гибкости. Благодаря широкому набору опций настройки, вы можете создавать различные варианты внешнего вида стекла: от прозрачного до матового или с узорчатыми моделями. Вы можете настроить непрозрачность, шероховатость и отражение для создания различных типов стекла, включая матовое, текстурированное и узорчатое. Контролируйте параметры IOR и Френеля для точной настройки отражений и преломлений. Вы можете добавить нормали загрязнения / дефектов или искажений для повышения реалистичности.','https://media.fab.com/image_previews/gallery_images/1cf2f96b-92b9-44cf-aaf0-3af7aa3cd505/c7965c24-1f6b-4744-bf32-c19ac35ae92d.jpg',599,9,21,'2024-12-01',NULL),(34,'Machine Rooms','Звуки заводского оборудования и жужжание. Синтетический звуковой эффект идеально подходит для любой научно-фантастической среды, где требуется низкий уровень шума двигателя.','https://media.fab.com/image_previews/gallery_images/30a58109-9d9a-4298-918f-853ea28b13d5/32b870bc-bca5-45ce-abaf-e7f2209856d5.jpg',399,3,9,'2024-12-01',NULL),(35,'Gold Diamond - Midnight Assault Sample Pack','Midnight Assault - это эпическая инструментальная армейская тактическая песня, созданная Габриэле Гилоди, которая может быть использована для любой темы войны / Армии / ЧВК / Военных / сражений. В нем есть приятные взрывные хиты, очень насыщенные синтезаторы, ностальгическое пианино, скрипка и струнные, которые пользователь может использовать по своему усмотрению.','https://media.fab.com/image_previews/gallery_images/6f8fb2a7-82b9-4438-99c8-6be5844da7de/f2db11e9-19ed-4af1-9cb1-e4e796d64df2.jpg',299,3,22,'2024-12-03',NULL),(38,'Small Sound Kit','The Small Sound Kit - это коллекция высококачественных звуков, идеально подходящих для создания атмосферных и интерактивных звуковых ландшафтов в ваших проектах. Эта библиотека содержит разнообразные звуки в нескольких категориях','https://media.fab.com/image_previews/gallery_images/f0b5b699-907c-43a7-abff-b3d66226cad2/8039e830-dee3-49df-a575-598f0577ffb4.jpg',399,3,2,'2025-04-02',NULL),(39,'Footsteps Mini Sound Pack','Footsteps Mini Sound Pack - это коллекция из 70 высококачественных звуков шагов, идеально подходящих для игровых проектов, видео и других мультимедийных приложений. В этом наборе представлены звуки шагов по различным поверхностям, что позволяет придать реалистичность движениям персонажей и взаимодействию с окружающей средой.','https://media.fab.com/image_previews/gallery_images/961c88ba-9486-40e1-8190-910f5cfc35d2/3af825a7-6cbd-43d6-93bc-7e85a64bc3fc.jpg',399,3,14,'2024-12-13',NULL),(40,'Caves and Dungeons','\"Пещеры и подземелья\" - это коллекция из 14 закольцованных атмосферных треков, идеально подходящих для фэнтезийных приключенческих видеоигр. Каждый из этих захватывающих и леденящих душу треков был тщательно проработан, чтобы передать богатые нюансы темных мест, где искатели приключений испытывают свою смелость. Спустись в мрачные Чертоги Проклятых, преодолей великую Змеиную пропасть или отыщи сокровища, которые лежат глубоко в Железной гробнице Котара, если осмелишься','https://media.fab.com/image_previews/gallery_images/5fa8fe88-1767-465f-afe6-679e0667b2d6/e89a17f1-f251-4cb7-ad72-66f72c2dd273.jpg',399,3,16,'2024-12-14',NULL),(41,'LASER GUNSHOTS: SFX PACK','Раскройте всю мощь футуристической войны с помощью LASER GUNSHOTS: SFX PACK 1. Этот тщательно разработанный набор звуковых эффектов включает в себя широкий спектр современных цифровых звуков выстрелов, которые идеально подходят для имитации различных видов футуристического оружия. Разрабатываете ли вы приключенческую игру в стиле киберпанк, научно-фантастический шутер или игру о вторжении инопланетян, эти высококачественные звуки погрузят ваших игроков в мир продвинутых сражений.','https://media.fab.com/image_previews/gallery_images/2bd7515b-565f-4786-8dbf-b7dda5916bb1/45593ead-1d32-4d8b-8309-3068ad5354ec.jpg',399,3,31,'2024-12-10',NULL),(42,'Cyber Punk Music v11','Окунитесь в сердце футуристических звуковых ландшафтов с \"CyberPunk Music V11\", тщательно разработанным набором инструментов, призванным придать вашим проектам непревзойденный прилив энергии и интенсивности. Погрузите свою аудиторию в наполненный адреналином мир киберпанка с помощью разнообразной подборки треков, каждый из которых тщательно подобран, чтобы передать суть антиутопических пейзажей и высокотехнологичных сражений.','https://media.fab.com/image_previews/gallery_images/fa3ac25f-93f4-43d0-873f-242aba921524/00b6d285-470d-48a9-8362-44d633bcb034.jpg',499,3,21,'2024-11-11',NULL),(43,'Horror Sci Fi Music Pack','этот саундтрек был создан с использованием оркестровых инструментов, синтезаторов и элементов звукового оформления.В этих треках сочетаются страх, энергия и атональность. Это делает их идеальными для научно-фантастических игр в жанре хоррор. Если вы ищете ужасающие саундтреки для своего проекта, например, к культовым научно-фантастическим видеоиграм в жанре хоррор','https://media.fab.com/image_previews/gallery_images/14376bdb-895c-4a1d-85a6-22e65cf0f963/07e6327b-55c3-4ffd-b1d4-0fae2f099337.jpg',399,3,12,'2024-12-09',NULL);
/*!40000 ALTER TABLE `resources` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `resources_requests`
--

DROP TABLE IF EXISTS `resources_requests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `resources_requests` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `description` varchar(500) NOT NULL,
  `picture` varchar(500) DEFAULT NULL,
  `price` float NOT NULL,
  `category` int NOT NULL,
  `request_date` date NOT NULL,
  `blob_picture` longblob,
  `author` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `category_idx` (`category`),
  KEY `category_requests_idx` (`category`),
  KEY `request_author_idx` (`author`),
  CONSTRAINT `category_requests` FOREIGN KEY (`category`) REFERENCES `categories` (`id`),
  CONSTRAINT `request_author` FOREIGN KEY (`author`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `resources_requests`
--

LOCK TABLES `resources_requests` WRITE;
/*!40000 ALTER TABLE `resources_requests` DISABLE KEYS */;
/*!40000 ALTER TABLE `resources_requests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'Администратор'),(2,'Менеджер'),(3,'Пользователь');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `login` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `registration_date` date NOT NULL,
  `role` int NOT NULL,
  `name` varchar(400) NOT NULL,
  `phone` varchar(13) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `login_UNIQUE` (`login`),
  KEY `role_idx` (`role`),
  CONSTRAINT `role` FOREIGN KEY (`role`) REFERENCES `roles` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin','8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918','admin@mail.com','2025-02-25',1,'Иван','8903564321365'),(2,'manager','6ee4a469cd4e91053847f5d3fcb61dbcc91e8f0ef10be7748da4c4a1ba382d17','manager@mail.com','2023-12-12',2,'Дмитрий','8912345465675'),(3,'user','04f8996da763b7a969b1028ee3007569eaf3a635486ddab211d512c85b9df8fb','user@mail.com','2021-11-11',3,'Геннадий','8235446645533'),(4,'admin23','6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b','admin23@mail.com','2025-03-12',1,'Сергей','8930534674353'),(6,'user0','3f92107747fcccc58db838122c14149b1c6e5a81ad7f45b91f1674017f03090f','user0@mail.com','2023-12-12',3,'Александр','8834545640565'),(7,'user1','0a041b9462caa4a31bac3567e0b6e6fd9100787db2ab433d96f6d178cabfce90','user1@mail.com','2023-12-16',3,'Алексей','8902353772992'),(8,'user2','6025d18fe48abd45168528f18a82e265dd98d421a7084aa09f61b341703901a3','user2@mail.com','2023-11-19',3,'Олег','8902352757629'),(9,'user3','5860faf02b6bc6222ba5aca523560f0e364ccd8b67bee486fe8bf7c01d492ccb','user3@mail.com','2024-11-19',3,'Тимофей','8235035646665'),(10,'user4','5269ef980de47819ba3d14340f4665262c41e933dc92c1a27dd5d01b047ac80e','user4@mail.com','2025-02-25',3,'Ярослав','8934067237868'),(11,'user5','5a39bead318f306939acb1d016647be2e38c6501c58367fdb3e9f52542aa2442','user5@mail.com','2023-05-09',3,'Алексей','8901237547438'),(12,'user6','ecb48a1cc94f951252ec462fe9ecc55c3ef123fadfe935661396c26a45a5809d','user6@mail.com','2023-05-05',3,'Сергей','8900235776776'),(13,'admin1','25f43b1486ad95a1398e3eeb3d83bc4010015fcc9bedb35b432e00298d5021f7','admin1@mail.com','2023-05-07',1,'Семён','8903457238895'),(14,'admin2','1c142b2d01aa34e9a36bde480645a57fd69e14155dacfab5a3f9257b77fdc8d8','admin2@mail.com','2023-05-08',1,'Виктория','8902376574384'),(15,'admin3','4fc2b5673a201ad9b1fc03dcb346e1baad44351daa0503d5534b4dfdcc4332e0','admin3@mail.com','2023-05-06',1,'Мария','8908767900003'),(16,'manager1','380f9771d2df8566ce2bd5b8ed772b0bb74fd6457fb803ab2d267c394d89c750','manager1@mail.com','2023-06-06',2,'Степан','8907668756823'),(17,'manager2','9d05b6092d975b0884c6ba7fadb283ced03da9822ebbd13cc6b6d1855a6495ec','manager2@mail.com','2023-05-06',2,'Роман','8908995948944'),(18,'manager3','42385b24804a6609a2744d414e0bf945704427b256ab79144b9ba93f278dbea7','manager3@mail.com','2023-05-12',2,'Полина','8075576654545'),(19,'manager4','e3c0f6e574f2e758a4d9d271fea62894230126062d74fd6d474e2046837f9bce','manager4@mail.com','2023-05-29',2,'Вероника','8999087666444'),(20,'manager5','60c6fc387428b43201be7da60da59934acb080b254e4eebead657b54154fbeb1','manager5@mail.com','2023-05-28',2,'Полина','8908766646444'),(21,'user7','3268151e52d97b4cacf97f5b46a5c76c8416e928e137e3b3dc447696a29afbaa','user7@mail.com','2025-02-28',3,'Юрий','8900237656431'),(22,'user8','f60afa4989a7db13314a2ab9881372634b5402c30ba7257448b13fa388de1b78','user8@mail.com','2023-03-01',3,'Григорий','8990034574757'),(23,'user9','0fb8d3c5dfaf81a387bf0ba439ab40e6343d2155fb4ddf6978a52d9b9ea8d0f8','user9@mail.com','2023-03-12',3,'Алексей','8980447655655'),(24,'user10','5bbf1a9e0de062225a1bb7df8d8b3719591527b74950810f16b1a6bc6d7bd29b','user10@mail.com','2023-01-03',3,'Татьяна','8902878598555'),(25,'user11','81115e31e22a5801b197750ec12d7a51ad693aa017ecc8bca033cbd500a928b6','user11@mail.com','2023-01-02',3,'Фёдор','8900857437645'),(26,'user12','bd35283fe8fcfd77d7c05a8bf2adb85c773281927e12c9829c72a9462092f7c4','user12@mail.com','2025-02-03',3,'Юрий','8904376575555'),(27,'user13','1834e148b518a43a37e04a4e4fbcee1eb845de6ee5a3f04fe9fb749f9695be42','user13@mail.com','2024-06-07',3,'Анастасия','8900457587545'),(28,'user14','daf7996f88742675acb3d0f85a8069d02fdf1c4dc2026de7f01a0ba7e65922fb','user14@mail.com','2023-05-28',3,'Алексей','8908153457435'),(29,'user15','2b8b66f64b605318593982b059a08dae101c0bdf5d8cb882a0891241a704f46c','user15@mail.com','2023-01-28',3,'Никита','8976556535311'),(30,'user16','4de4153595c0977d2389d0880547bd3aa60871e906ce52a26648d8ca0a157e5c','user16@mail.com','2023-03-28',3,'Григорий','8939457576568'),(31,'user17','2a60ff641c890283b1d070f827cf9c0cce004769c2a2dc7136bc6d290477275c','user17@mail.com','2024-05-28',3,'Алексей','8900876745455'),(32,'user18','ebc835d1b43e63d1ba35af810da3a23e4f8a04cf680f1718c2fefb1ee77fcecf','user18@mail.com','2023-01-29',3,'Фёдор','8902755644211'),(33,'user19','0b6ecb3aa9b23589fb9e314b46c832d977e597228c1e358fcc564bd8ba733195','user19@mail.com','2025-01-01',3,'Юрий','8903564783567'),(34,'user20','7febe54e79096749ac43dc6c2e3e5d4dc768993d1f3102889257c9cab7934ec7','user20@mail.com','2023-01-01',3,'Татьяна','8900457354755');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-04 15:39:58
