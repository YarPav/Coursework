-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Июн 26 2022 г., 15:54
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
-- База данных: `doplatform-release`
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

-- --------------------------------------------------------

--
-- Структура таблицы `estimation`
--

CREATE TABLE `estimation` (
  `Id` int NOT NULL,
  `test_id` int NOT NULL,
  `student_id` int NOT NULL,
  `mark` int NOT NULL,
  `max_mark` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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

-- --------------------------------------------------------

--
-- Структура таблицы `lecture_to_course`
--

CREATE TABLE `lecture_to_course` (
  `Id` int UNSIGNED NOT NULL,
  `Lecture_id` int NOT NULL,
  `Course_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `student`
--

CREATE TABLE `student` (
  `Id` int NOT NULL,
  `Name` varchar(25) NOT NULL,
  `Lastname` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Login` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `Course_id` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `student_to_course`
--

CREATE TABLE `student_to_course` (
  `Id` int NOT NULL,
  `Student_id` int NOT NULL,
  `Course_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

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
(1, 'Иван', 'Иванов', 'ivanivan', '123');

-- --------------------------------------------------------

--
-- Структура таблицы `test`
--

CREATE TABLE `test` (
  `Id` int NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Theme` varchar(45) DEFAULT NULL,
  `test_time` int DEFAULT NULL,
  `Teacher_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `test_answer`
--

CREATE TABLE `test_answer` (
  `Id` int NOT NULL,
  `answer_body` text,
  `is_true_answer` tinyint(1) NOT NULL DEFAULT '0',
  `question_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `test_question`
--

CREATE TABLE `test_question` (
  `Id` int NOT NULL,
  `question_body` text,
  `test_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `test_to_course`
--

CREATE TABLE `test_to_course` (
  `Id` int NOT NULL,
  `Test_id` int NOT NULL,
  `Course_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `course`
--
ALTER TABLE `course`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `course_ibfk_1` (`Teacher_id`);

--
-- Индексы таблицы `estimation`
--
ALTER TABLE `estimation`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `test_id` (`test_id`),
  ADD KEY `student_id` (`student_id`);

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
-- Индексы таблицы `student_to_course`
--
ALTER TABLE `student_to_course`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `Student_id` (`Student_id`),
  ADD KEY `Course_id` (`Course_id`);

--
-- Индексы таблицы `teacher`
--
ALTER TABLE `teacher`
  ADD PRIMARY KEY (`Id`);

--
-- Индексы таблицы `test`
--
ALTER TABLE `test`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `Teacher_id` (`Teacher_id`);

--
-- Индексы таблицы `test_answer`
--
ALTER TABLE `test_answer`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `question_id` (`question_id`);

--
-- Индексы таблицы `test_question`
--
ALTER TABLE `test_question`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `test_id` (`test_id`);

--
-- Индексы таблицы `test_to_course`
--
ALTER TABLE `test_to_course`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `Test_id` (`Test_id`),
  ADD KEY `Course_id` (`Course_id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `course`
--
ALTER TABLE `course`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT для таблицы `estimation`
--
ALTER TABLE `estimation`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT для таблицы `lecture`
--
ALTER TABLE `lecture`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT для таблицы `lecture_to_course`
--
ALTER TABLE `lecture_to_course`
  MODIFY `Id` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT для таблицы `student`
--
ALTER TABLE `student`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT для таблицы `student_to_course`
--
ALTER TABLE `student_to_course`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT для таблицы `teacher`
--
ALTER TABLE `teacher`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `test`
--
ALTER TABLE `test`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT для таблицы `test_answer`
--
ALTER TABLE `test_answer`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT для таблицы `test_question`
--
ALTER TABLE `test_question`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT для таблицы `test_to_course`
--
ALTER TABLE `test_to_course`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `course`
--
ALTER TABLE `course`
  ADD CONSTRAINT `course_ibfk_1` FOREIGN KEY (`Teacher_id`) REFERENCES `teacher` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `estimation`
--
ALTER TABLE `estimation`
  ADD CONSTRAINT `estimation_ibfk_1` FOREIGN KEY (`test_id`) REFERENCES `test` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `estimation_ibfk_2` FOREIGN KEY (`student_id`) REFERENCES `student` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

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

--
-- Ограничения внешнего ключа таблицы `student_to_course`
--
ALTER TABLE `student_to_course`
  ADD CONSTRAINT `student_to_course_ibfk_1` FOREIGN KEY (`Course_id`) REFERENCES `course` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `student_to_course_ibfk_2` FOREIGN KEY (`Student_id`) REFERENCES `student` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `test`
--
ALTER TABLE `test`
  ADD CONSTRAINT `test_ibfk_1` FOREIGN KEY (`Teacher_id`) REFERENCES `teacher` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `test_answer`
--
ALTER TABLE `test_answer`
  ADD CONSTRAINT `test_answer_ibfk_1` FOREIGN KEY (`question_id`) REFERENCES `test_question` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `test_question`
--
ALTER TABLE `test_question`
  ADD CONSTRAINT `test_question_ibfk_1` FOREIGN KEY (`test_id`) REFERENCES `test` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `test_to_course`
--
ALTER TABLE `test_to_course`
  ADD CONSTRAINT `test_to_course_ibfk_1` FOREIGN KEY (`Course_id`) REFERENCES `course` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `test_to_course_ibfk_2` FOREIGN KEY (`Test_id`) REFERENCES `test` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
