-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 11, 2019 at 03:05 AM
-- Server version: 10.1.21-MariaDB
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `novel`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_emp` (IN `fname` VARCHAR(20), IN `lname` VARCHAR(20), IN `bday` DATETIME, OUT `empno` INT)  BEGIN INSERT INTO emp(first_name, last_name, birthdate) VALUES(fname, lname, DATE(bday)); SET empno = LAST_INSERT_ID(); END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `novel_infor`
--

CREATE TABLE `novel_infor` (
  `Id` varchar(10) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Link` varchar(255) NOT NULL,
  `Author` varchar(15) NOT NULL,
  `Chap_number` int(11) NOT NULL,
  `Rating` int(11) NOT NULL,
  `Viewer` int(11) NOT NULL,
  `Voting` int(11) NOT NULL,
  `Recommandation` int(11) NOT NULL,
  `image_link` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `novel_infor`
--

INSERT INTO `novel_infor` (`Id`, `Name`, `Link`, `Author`, `Chap_number`, `Rating`, `Viewer`, `Voting`, `Recommandation`, `image_link`) VALUES
('0001', 'Money', '#/', 'Lee', 10, 10, 100, 10, 10, NULL),
('0002', 'business', '#/', 'Lee', 10, 10, 10, 10, 10, NULL),
('0003', 'Industry', '#/', 'Lee', 10, 10, 10, 20, 10, NULL),
('0004', 'Home', '#/', 'Lee', 10, 10, 10, 20, 10, NULL),
('0005', 'Game', '#/', 'Lee', 10, 10, 10, 20, 10, NULL),
('0006', 'Girl', '#/', 'Lee', 10, 10, 10, 20, 10, NULL),
('0007', 'Powerfull', '#/', 'Lee', 10, 10, 10, 20, 10, NULL),
('0008', 'Fire World', '#/', 'Lee', 10, 10, 10, 10, 10, 'U'),
('0009', 'Champion World', '#/', 'Lee', 10, 10, 10, 10, 10, 'U'),
('0010', 'King of the Ring', '#/', 'Lee', 10, 10, 10, 10, 10, 'U'),
('0011', 'God of the Ring', '#/', 'Lee', 10, 10, 10, 10, 10, 'U');

-- --------------------------------------------------------

--
-- Table structure for table `user_infor`
--

CREATE TABLE `user_infor` (
  `ID` varchar(10) NOT NULL,
  `UserName` varchar(100) NOT NULL,
  `Password` varchar(200) NOT NULL,
  `Date_of_birth` date NOT NULL,
  `Image_profile` varchar(255) NOT NULL,
  `Coin` int(11) NOT NULL,
  `Level` int(11) NOT NULL,
  `Experience` int(11) NOT NULL,
  `Type` varchar(100) NOT NULL,
  `Power` int(11) NOT NULL,
  `Nick_name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user_infor`
--

INSERT INTO `user_infor` (`ID`, `UserName`, `Password`, `Date_of_birth`, `Image_profile`, `Coin`, `Level`, `Experience`, `Type`, `Power`, `Nick_name`) VALUES
('001', 'novel_book', '123456', '0000-00-00', '#', 100, 7, 200, 'Reader', 2000, 'Cat');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `novel_infor`
--
ALTER TABLE `novel_infor`
  ADD PRIMARY KEY (`Id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
