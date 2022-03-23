-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Мар 23 2022 г., 05:12
-- Версия сервера: 8.0.24
-- Версия PHP: 8.0.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `doplatform`
--

-- --------------------------------------------------------

--
-- Структура таблицы `course`
--

CREATE TABLE `course` (
  `Id` int NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Teacher_id` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `course`
--

INSERT INTO `course` (`Id`, `Name`, `Teacher_id`) VALUES
(2, 'web', 1),
(3, 'python', 1),
(7, '1c', 1),
(8, 'php', 1),
(25, 'ty', 1),
(26, 'java', 1),
(27, 'js', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `lecture`
--

CREATE TABLE `lecture` (
  `Id` int NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Theme` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Lecture_body` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Teacher_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `lecture`
--

INSERT INTO `lecture` (`Id`, `Name`, `Theme`, `Lecture_body`, `Teacher_id`) VALUES
(1, 'L1', 'theme of lecture 1', 'Lorem ipsum', 1),
(2, 'L2', 'theme of lecture 2', 'Lorem ipsum', 1),
(3, 'L3', 'theme of lecture 3', 'Lorem', 2),
(4, 'L4', NULL, NULL, 1);

-- --------------------------------------------------------

--
-- Структура таблицы `lecture_to_course`
--

CREATE TABLE `lecture_to_course` (
  `Id` int UNSIGNED NOT NULL,
  `Lecture_id` int NOT NULL,
  `Course_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `lecture_to_course`
--

INSERT INTO `lecture_to_course` (`Id`, `Lecture_id`, `Course_id`) VALUES
(1, 1, 7);

-- --------------------------------------------------------

--
-- Структура таблицы `student`
--

CREATE TABLE `student` (
  `Id` int NOT NULL,
  `Name` varchar(25) NOT NULL,
  `Lastname` varchar(25) NOT NULL,
  `Login` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `Course_id` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `student`
--

INSERT INTO `student` (`Id`, `Name`, `Lastname`, `Login`, `Password`, `Course_id`) VALUES
(1, 'student1', '', 'student1', '1', NULL),
(2, 's1', '', 's1', 's1', NULL),
(3, 's2', '', 's2', 's2', NULL);

-- --------------------------------------------------------

--
-- Структура таблицы `teacher`
--

CREATE TABLE `teacher` (
  `Id` int NOT NULL,
  `Name` varchar(25) NOT NULL,
  `Lastname` varchar(25) NOT NULL,
  `Login` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `teacher`
--

INSERT INTO `teacher` (`Id`, `Name`, `Lastname`, `Login`, `Password`) VALUES
(1, 'Bob', 'Anderson', 't1', 't1'),
(2, 'Alex', 'Bobson', 't2', 't2');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `course`
--
ALTER TABLE `course`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `Teacher_id` (`Teacher_id`);

--
-- Индексы таблицы `lecture`
--
ALTER TABLE `lecture`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `Teacher_id` (`Teacher_id`);

--
-- Индексы таблицы `lecture_to_course`
--
ALTER TABLE `lecture_to_course`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `Course_id` (`Course_id`,`Lecture_id`) USING BTREE,
  ADD KEY `Lecture_id` (`Lecture_id`);

--
-- Индексы таблицы `student`
--
ALTER TABLE `student`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `Course_id` (`Course_id`);

--
-- Индексы таблицы `teacher`
--
ALTER TABLE `teacher`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `course`
--
ALTER TABLE `course`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT для таблицы `lecture`
--
ALTER TABLE `lecture`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT для таблицы `lecture_to_course`
--
ALTER TABLE `lecture_to_course`
  MODIFY `Id` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT для таблицы `student`
--
ALTER TABLE `student`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT для таблицы `teacher`
--
ALTER TABLE `teacher`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `course`
--
ALTER TABLE `course`
  ADD CONSTRAINT `course_ibfk_1` FOREIGN KEY (`Teacher_id`) REFERENCES `teacher` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Ограничения внешнего ключа таблицы `lecture`
--
ALTER TABLE `lecture`
  ADD CONSTRAINT `lecture_ibfk_1` FOREIGN KEY (`Teacher_id`) REFERENCES `teacher` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `lecture_to_course`
--
ALTER TABLE `lecture_to_course`
  ADD CONSTRAINT `lecture_to_course_ibfk_1` FOREIGN KEY (`Course_id`) REFERENCES `course` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `lecture_to_course_ibfk_2` FOREIGN KEY (`Lecture_id`) REFERENCES `lecture` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `student`
--
ALTER TABLE `student`
  ADD CONSTRAINT `student_ibfk_1` FOREIGN KEY (`Course_id`) REFERENCES `course` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
