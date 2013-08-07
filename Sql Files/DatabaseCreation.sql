delimiter $$

CREATE DATABASE `myhome2013` /*!40100 DEFAULT CHARACTER SET utf8 */$$

delimiter $$

CREATE TABLE `categoryview` (
  `KEY` varchar(45) NOT NULL,
  `VALUE` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8$$

delimiter $$

CREATE TABLE `t_expenses` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `AMOUNT` double NOT NULL,
  `EXP_DATE` datetime DEFAULT NULL,
  `CATEGORY` int(11) unsigned NOT NULL,
  `METHOD` int(11) NOT NULL,
  `COMMENTS` varchar(200) DEFAULT '""',
  PRIMARY KEY (`ID`),
  KEY `PAYMENT_METHOD_idx` (`METHOD`),
  KEY `CATEGORY_NAME_idx` (`CATEGORY`),
  CONSTRAINT `EXP_CATEGORY_NAME` FOREIGN KEY (`CATEGORY`) REFERENCES `t_expenses_category` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `EXP_PAYMENT_METHOD` FOREIGN KEY (`METHOD`) REFERENCES `t_payment_methods` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='	'$$

delimiter $$

CREATE TABLE `t_expenses_category` (
  `ID` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `NAME` varchar(45) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8$$

delimiter $$

CREATE TABLE `t_incomes` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `AMOUNT` double NOT NULL,
  `INC_DATE` datetime DEFAULT NULL,
  `CATEGORY` int(11) NOT NULL,
  `METHOD` int(11) NOT NULL,
  `COMMENTS` varchar(200) DEFAULT '""',
  PRIMARY KEY (`ID`),
  KEY `CATEGORY_NAME_idx` (`CATEGORY`),
  KEY `PAYMENT_METHOD_idx` (`METHOD`),
  CONSTRAINT `INC_CATEGORY_NAME` FOREIGN KEY (`CATEGORY`) REFERENCES `t_incomes_category` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `INC_PAYMENT_METHOD` FOREIGN KEY (`METHOD`) REFERENCES `t_payment_methods` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8$$

delimiter $$

CREATE TABLE `t_incomes_category` (
  `ID` int(10) NOT NULL,
  `NAME` varchar(45) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8$$

delimiter $$

CREATE TABLE `t_payment_methods` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(45) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8$$

delimiter $$

CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `MyHome2013`@`%` 
    SQL SECURITY DEFINER
VIEW `viw` AS
    select 
        `ex`.`EXP_DATE` AS `Expense date`,
        `ex`.`AMOUNT` AS `Amount`,
        `excat`.`NAME` AS `Category`,
        `pay`.`NAME` AS `Payment Method`,
        `ex`.`COMMENTS` AS `Comments`
    from
        ((`t_expenses` `ex`
        join `t_expenses_category` `excat`)
        join `t_payment_methods` `pay`)
    where
        ((`excat`.`ID` = `ex`.`CATEGORY`)
            and (`pay`.`ID` = `ex`.`METHOD`))$$

delimiter $$

CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `MyHome2013`@`%` 
    SQL SECURITY DEFINER
VIEW `viw` AS
    select 
        `ex`.`EXP_DATE` AS `Expense date`,
        `ex`.`AMOUNT` AS `Amount`,
        `excat`.`NAME` AS `Category`,
        `pay`.`NAME` AS `Payment Method`,
        `ex`.`COMMENTS` AS `Comments`
    from
        ((`t_expenses` `ex`
        join `t_expenses_category` `excat`)
        join `t_payment_methods` `pay`)
    where
        ((`excat`.`ID` = `ex`.`CATEGORY`)
            and (`pay`.`ID` = `ex`.`METHOD`))$$

delimiter $$

CREATE DEFINER=`MyHome2013`@`%` FUNCTION `new_expense_id`() RETURNS int(11)
BEGIN
declare newId int;
set newId = (select last_insert_id() from t_expenses);

if newId is null then
	set newId = 1;
end if;

RETURN newId;

END$$

delimiter $$

CREATE DEFINER=`MyHome2013`@`%` FUNCTION `new_expenses_category_id`() RETURNS int(11)
BEGIN
declare newId int;
set newId = (select last_insert_id() from t_expenses_category);

if newId is null then
	set newId = 1;
end if;

RETURN newId;
END$$

delimiter $$

CREATE DEFINER=`MyHome2013`@`%` FUNCTION `new_income_category_id`() RETURNS int(11)
BEGIN
declare newId int;
set newId = (select last_insert_id() from t_incomes_category);

if newId is null then
	set newId = 1;
end if;

RETURN newId;
END$$

delimiter $$

CREATE DEFINER=`MyHome2013`@`%` FUNCTION `new_income_id`() RETURNS int(11)
BEGIN
declare newId int;
set newId = (select last_insert_id() from t_incomes);

if newId is null then
	set newId = 1;
end if;

RETURN newId;
END$$

delimiter $$

CREATE DEFINER=`MyHome2013`@`%` FUNCTION `new_payment_method_id`() RETURNS int(11)
BEGIN
declare newId int;
set newId = (select last_insert_id() from t_payment_methods);

if newId is null then
	set newId = 1;
end if;

RETURN newId;
END$$

