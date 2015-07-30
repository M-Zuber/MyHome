SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

DROP SCHEMA IF EXISTS `myhome-test` ;
CREATE SCHEMA IF NOT EXISTS `myhome-test` DEFAULT CHARACTER SET utf8 ;
USE `myhome-test` ;

-- -----------------------------------------------------
-- Table `myhome-test`.`categoryview`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome-test`.`categoryview` ;

CREATE TABLE IF NOT EXISTS `myhome-test`.`categoryview` (
  `KEY` VARCHAR(45) NOT NULL,
  `VALUE` VARCHAR(45) NOT NULL)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `myhome-test`.`t_expenses_category`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome-test`.`t_expenses_category` ;

CREATE TABLE IF NOT EXISTS `myhome-test`.`t_expenses_category` (
  `ID` INT(11) NOT NULL,
  `NAME` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 16;


-- -----------------------------------------------------
-- Table `myhome-test`.`t_payment_methods`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome-test`.`t_payment_methods` ;

CREATE TABLE IF NOT EXISTS `myhome-test`.`t_payment_methods` (
  `ID` INT(11) NOT NULL,
  `NAME` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 11;


-- -----------------------------------------------------
-- Table `myhome-test`.`t_expenses`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome-test`.`t_expenses` ;

CREATE TABLE IF NOT EXISTS `myhome-test`.`t_expenses` (
  `ID` INT(10) NOT NULL,
  `AMOUNT` DOUBLE NOT NULL,
  `EXP_DATE` DATETIME NULL DEFAULT NULL,
  `CATEGORY` INT(11) NOT NULL,
  `METHOD` INT(11) NOT NULL,
  `COMMENTS` VARCHAR(200) NULL DEFAULT '""',
  PRIMARY KEY (`ID`),
  INDEX `PAYMENT_METHOD_idx` (`METHOD` ASC),
  INDEX `CATEGORY_NAME_idx` (`CATEGORY` ASC),
  CONSTRAINT `EXP_CATEGORY_NAME`
    FOREIGN KEY (`CATEGORY`)
    REFERENCES `myhome-test`.`t_expenses_category` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `EXP_PAYMENT_METHOD`
    FOREIGN KEY (`METHOD`)
    REFERENCES `myhome-test`.`t_payment_methods` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 306
COMMENT = '	';


-- -----------------------------------------------------
-- Table `myhome-test`.`t_incomes_category`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome-test`.`t_incomes_category` ;

CREATE TABLE IF NOT EXISTS `myhome-test`.`t_incomes_category` (
  `ID` INT(10) NOT NULL,
  `NAME` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `myhome-test`.`t_incomes`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome-test`.`t_incomes` ;

CREATE TABLE IF NOT EXISTS `myhome-test`.`t_incomes` (
  `ID` INT(10) NOT NULL,
  `AMOUNT` DOUBLE NOT NULL,
  `INC_DATE` DATETIME NULL DEFAULT NULL,
  `CATEGORY` INT(11) NOT NULL,
  `METHOD` INT(11) NOT NULL,
  `COMMENTS` VARCHAR(200) NULL DEFAULT '""',
  PRIMARY KEY (`ID`),
  INDEX `CATEGORY_NAME_idx` (`CATEGORY` ASC),
  INDEX `PAYMENT_METHOD_idx` (`METHOD` ASC),
  CONSTRAINT `INC_CATEGORY_NAME`
    FOREIGN KEY (`CATEGORY`)
    REFERENCES `myhome-test`.`t_incomes_category` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `INC_PAYMENT_METHOD`
    FOREIGN KEY (`METHOD`)
    REFERENCES `myhome-test`.`t_payment_methods` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 15;

USE `myhome-test` ;

-- -----------------------------------------------------
-- Placeholder table for view `myhome-test`.`viw`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myhome-test`.`viw` (`Entity id` INT, `Expense date` INT, `Amount` INT, `Category` INT, `Payment Method` INT, `Comments` INT);

-- -----------------------------------------------------
-- Placeholder table for view `myhome-test`.`viwin`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myhome-test`.`viwin` (`Income Date` INT, `Amount` INT, `Category` INT, `Payment Method` INT, `Comments` INT);

-- -----------------------------------------------------
-- function new_expense_id
-- -----------------------------------------------------

USE `myhome-test`;
DROP function IF EXISTS `myhome-test`.`new_expense_id`;

DELIMITER $$
USE `myhome-test`$$
CREATE DEFINER=`myhome-test`@`%` FUNCTION `new_expense_id`() RETURNS int(11)
BEGIN
declare newId int;
set newId = (select last_insert_id() from t_expenses);

if newId is null then
	set newId = 1;
end if;

RETURN newId;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- function new_expenses_category_id
-- -----------------------------------------------------

USE `myhome-test`;
DROP function IF EXISTS `myhome-test`.`new_expenses_category_id`;

DELIMITER $$
USE `myhome-test`$$
CREATE DEFINER=`myhome-test`@`%` FUNCTION `new_expenses_category_id`() RETURNS int(11)
BEGIN
declare newId int;
set newId = (select last_insert_id() from t_expenses_category);

if newId is null then
	set newId = 1;
end if;

RETURN newId;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- function new_income_category_id
-- -----------------------------------------------------

USE `myhome-test`;
DROP function IF EXISTS `myhome-test`.`new_income_category_id`;

DELIMITER $$
USE `myhome-test`$$
CREATE DEFINER=`myhome-test`@`%` FUNCTION `new_income_category_id`() RETURNS int(11)
BEGIN
declare newId int;
set newId = (select last_insert_id() from t_incomes_category);

if newId is null then
	set newId = 1;
end if;

RETURN newId;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- function new_income_id
-- -----------------------------------------------------

USE `myhome-test`;
DROP function IF EXISTS `myhome-test`.`new_income_id`;

DELIMITER $$
USE `myhome-test`$$
CREATE DEFINER=`myhome-test`@`%` FUNCTION `new_income_id`() RETURNS int(11)
BEGIN
declare newId int;
set newId = (select last_insert_id() from t_incomes);

if newId is null then
	set newId = 1;
end if;

RETURN newId;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- function new_payment_method_id
-- -----------------------------------------------------

USE `myhome-test`;
DROP function IF EXISTS `myhome-test`.`new_payment_method_id`;

DELIMITER $$
USE `myhome-test`$$
CREATE DEFINER=`myhome-test`@`%` FUNCTION `new_payment_method_id`() RETURNS int(11)
BEGIN
declare newId int;
set newId = (select last_insert_id() from t_payment_methods);

if newId is null then
	set newId = 1;
end if;

RETURN newId;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- View `myhome-test`.`viw`
-- -----------------------------------------------------
DROP VIEW IF EXISTS `myhome-test`.`viw` ;
DROP TABLE IF EXISTS `myhome-test`.`viw`;
USE `myhome-test`;
CREATE OR REPLACE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `myhome-test`.`viw` AS
    select 
		`ex`.`ID` AS `Entity id`,
        `ex`.`EXP_DATE` AS `Expense date`,
        `ex`.`AMOUNT` AS `Amount`,
        `excat`.`NAME` AS `Category`,
        `pay`.`NAME` AS `Payment Method`,
        `ex`.`COMMENTS` AS `Comments`
    from
        ((`myhome-test`.`t_expenses` `ex`
        join `myhome-test`.`t_expenses_category` `excat`)
        join `myhome-test`.`t_payment_methods` `pay`)
    where
        ((`excat`.`ID` = `ex`.`CATEGORY`)
            and (`pay`.`ID` = `ex`.`METHOD`));

-- -----------------------------------------------------
-- View `myhome-test`.`viwin`
-- -----------------------------------------------------
DROP VIEW IF EXISTS `myhome-test`.`viwin` ;
DROP TABLE IF EXISTS `myhome-test`.`viwin`;
USE `myhome-test`;
CREATE OR REPLACE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `myhome-test`.`viwin` AS
    select 
        `inc`.`INC_DATE` AS `Income Date`,
        `inc`.`AMOUNT` AS `Amount`,
        `inccat`.`NAME` AS `Category`,
        `pay`.`NAME` AS `Payment Method`,
        `inc`.`COMMENTS` AS `Comments`
    from
        ((`myhome-test`.`t_incomes` `inc`
        join `myhome-test`.`t_incomes_category` `inccat`)
        join `myhome-test`.`t_payment_methods` `pay`)
    where
        ((`inccat`.`ID` = `inc`.`CATEGORY`)
            and (`pay`.`ID` = `inc`.`METHOD`));

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
