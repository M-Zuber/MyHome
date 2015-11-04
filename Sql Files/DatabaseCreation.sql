SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

DROP SCHEMA IF EXISTS `myhome` ;
CREATE SCHEMA IF NOT EXISTS `myhome` DEFAULT CHARACTER SET utf8 ;
USE `myhome` ;

-- -----------------------------------------------------
-- Table `myhome`.`categoryview`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome`.`categoryview` ;

CREATE TABLE IF NOT EXISTS `myhome`.`categoryview` (
  `KEY` VARCHAR(45) NOT NULL,
  `VALUE` VARCHAR(45) NOT NULL)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `myhome`.`t_expenses_category`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome`.`t_expenses_category` ;

CREATE TABLE IF NOT EXISTS `myhome`.`t_expenses_category` (
  `ID` INT(11) NOT NULL,
  `NAME` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 16;


-- -----------------------------------------------------
-- Table `myhome`.`t_payment_methods`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome`.`t_payment_methods` ;

CREATE TABLE IF NOT EXISTS `myhome`.`t_payment_methods` (
  `ID` INT(11) NOT NULL,
  `NAME` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 11;


-- -----------------------------------------------------
-- Table `myhome`.`t_expenses`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome`.`t_expenses` ;

CREATE TABLE IF NOT EXISTS `myhome`.`t_expenses` (
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
    REFERENCES `myhome`.`t_expenses_category` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `EXP_PAYMENT_METHOD`
    FOREIGN KEY (`METHOD`)
    REFERENCES `myhome`.`t_payment_methods` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 306
COMMENT = '	';


-- -----------------------------------------------------
-- Table `myhome`.`t_incomes_category`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome`.`t_incomes_category` ;

CREATE TABLE IF NOT EXISTS `myhome`.`t_incomes_category` (
  `ID` INT(10) NOT NULL,
  `NAME` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `myhome`.`t_incomes`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome`.`t_incomes` ;

CREATE TABLE IF NOT EXISTS `myhome`.`t_incomes` (
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
    REFERENCES `myhome`.`t_incomes_category` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `INC_PAYMENT_METHOD`
    FOREIGN KEY (`METHOD`)
    REFERENCES `myhome`.`t_payment_methods` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 15;

USE `myhome` ;

-- -----------------------------------------------------
-- Placeholder table for view `myhome`.`viw`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myhome`.`viw` (`Entity id` INT, `Expense date` INT, `Amount` INT, `Category` INT, `Payment Method` INT, `Comments` INT);

-- -----------------------------------------------------
-- Placeholder table for view `myhome`.`viwin`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myhome`.`viwin` (`Income Date` INT, `Amount` INT, `Category` INT, `Payment Method` INT, `Comments` INT);

-- -----------------------------------------------------
-- function new_expense_id
-- -----------------------------------------------------

USE `myhome`;
DROP function IF EXISTS `myhome`.`new_expense_id`;

DELIMITER $$
USE `myhome`$$
CREATE DEFINER=`myhome`@`%` FUNCTION `new_expense_id`() RETURNS int(11)
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

USE `myhome`;
DROP function IF EXISTS `myhome`.`new_expenses_category_id`;

DELIMITER $$
USE `myhome`$$
CREATE DEFINER=`myhome`@`%` FUNCTION `new_expenses_category_id`() RETURNS int(11)
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

USE `myhome`;
DROP function IF EXISTS `myhome`.`new_income_category_id`;

DELIMITER $$
USE `myhome`$$
CREATE DEFINER=`myhome`@`%` FUNCTION `new_income_category_id`() RETURNS int(11)
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

USE `myhome`;
DROP function IF EXISTS `myhome`.`new_income_id`;

DELIMITER $$
USE `myhome`$$
CREATE DEFINER=`myhome`@`%` FUNCTION `new_income_id`() RETURNS int(11)
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

USE `myhome`;
DROP function IF EXISTS `myhome`.`new_payment_method_id`;

DELIMITER $$
USE `myhome`$$
CREATE DEFINER=`myhome`@`%` FUNCTION `new_payment_method_id`() RETURNS int(11)
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
-- View `myhome`.`viw`
-- -----------------------------------------------------
DROP VIEW IF EXISTS `myhome`.`viw` ;
DROP TABLE IF EXISTS `myhome`.`viw`;
USE `myhome`;
CREATE OR REPLACE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `myhome`.`viw` AS
    select 
		`ex`.`ID` AS `Entity id`,
        `ex`.`EXP_DATE` AS `Expense date`,
        `ex`.`AMOUNT` AS `Amount`,
        `excat`.`NAME` AS `Category`,
        `pay`.`NAME` AS `Payment Method`,
        `ex`.`COMMENTS` AS `Comments`
    from
        ((`myhome`.`t_expenses` `ex`
        join `myhome`.`t_expenses_category` `excat`)
        join `myhome`.`t_payment_methods` `pay`)
    where
        ((`excat`.`ID` = `ex`.`CATEGORY`)
            and (`pay`.`ID` = `ex`.`METHOD`));

-- -----------------------------------------------------
-- View `myhome`.`viwin`
-- -----------------------------------------------------
DROP VIEW IF EXISTS `myhome`.`viwin` ;
DROP TABLE IF EXISTS `myhome`.`viwin`;
USE `myhome`;
CREATE OR REPLACE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `myhome`.`viwin` AS
    select 
        `inc`.`INC_DATE` AS `Income Date`,
        `inc`.`AMOUNT` AS `Amount`,
        `inccat`.`NAME` AS `Category`,
        `pay`.`NAME` AS `Payment Method`,
        `inc`.`COMMENTS` AS `Comments`
    from
        ((`myhome`.`t_incomes` `inc`
        join `myhome`.`t_incomes_category` `inccat`)
        join `myhome`.`t_payment_methods` `pay`)
    where
        ((`inccat`.`ID` = `inc`.`CATEGORY`)
            and (`pay`.`ID` = `inc`.`METHOD`));

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
