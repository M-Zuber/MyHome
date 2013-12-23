SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

DROP SCHEMA IF EXISTS `mydb` ;
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
DROP SCHEMA IF EXISTS `myhome2013` ;
CREATE SCHEMA IF NOT EXISTS `myhome2013` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`table1`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`table1` ;

CREATE TABLE IF NOT EXISTS `mydb`.`table1` (
)
ENGINE = InnoDB;

USE `myhome2013` ;

-- -----------------------------------------------------
-- Table `myhome2013`.`categoryview`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome2013`.`categoryview` ;

CREATE TABLE IF NOT EXISTS `myhome2013`.`categoryview` (
  `KEY` VARCHAR(45) NOT NULL,
  `VALUE` VARCHAR(45) NOT NULL)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `myhome2013`.`t_expenses_category`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome2013`.`t_expenses_category` ;

CREATE TABLE IF NOT EXISTS `myhome2013`.`t_expenses_category` (
  `ID` INT(11) UNSIGNED NOT NULL AUTO_INCREMENT,
  `NAME` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 16
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `myhome2013`.`t_payment_methods`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome2013`.`t_payment_methods` ;

CREATE TABLE IF NOT EXISTS `myhome2013`.`t_payment_methods` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `NAME` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 11
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `myhome2013`.`t_expenses`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome2013`.`t_expenses` ;

CREATE TABLE IF NOT EXISTS `myhome2013`.`t_expenses` (
  `ID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `AMOUNT` DOUBLE NOT NULL,
  `EXP_DATE` DATETIME NULL DEFAULT NULL,
  `CATEGORY` INT(11) UNSIGNED NOT NULL,
  `METHOD` INT(11) NOT NULL,
  `COMMENTS` VARCHAR(200) NULL DEFAULT '""',
  PRIMARY KEY (`ID`),
  INDEX `PAYMENT_METHOD_idx` (`METHOD` ASC),
  INDEX `CATEGORY_NAME_idx` (`CATEGORY` ASC),
  CONSTRAINT `EXP_CATEGORY_NAME`
    FOREIGN KEY (`CATEGORY`)
    REFERENCES `myhome2013`.`t_expenses_category` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `EXP_PAYMENT_METHOD`
    FOREIGN KEY (`METHOD`)
    REFERENCES `myhome2013`.`t_payment_methods` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 306
DEFAULT CHARACTER SET = utf8
COMMENT = '	';


-- -----------------------------------------------------
-- Table `myhome2013`.`t_incomes_category`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome2013`.`t_incomes_category` ;

CREATE TABLE IF NOT EXISTS `myhome2013`.`t_incomes_category` (
  `ID` INT(10) NOT NULL,
  `NAME` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `myhome2013`.`t_incomes`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `myhome2013`.`t_incomes` ;

CREATE TABLE IF NOT EXISTS `myhome2013`.`t_incomes` (
  `ID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
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
    REFERENCES `myhome2013`.`t_incomes_category` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `INC_PAYMENT_METHOD`
    FOREIGN KEY (`METHOD`)
    REFERENCES `myhome2013`.`t_payment_methods` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 15
DEFAULT CHARACTER SET = utf8;

USE `myhome2013` ;

-- -----------------------------------------------------
-- Placeholder table for view `myhome2013`.`viw`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myhome2013`.`viw` (`Expense date` INT, `Amount` INT, `Category` INT, `Payment Method` INT, `Comments` INT);

-- -----------------------------------------------------
-- Placeholder table for view `myhome2013`.`viwin`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `myhome2013`.`viwin` (`Income Date` INT, `Amount` INT, `Category` INT, `Payment Method` INT, `Comments` INT);

-- -----------------------------------------------------
-- function new_expense_id
-- -----------------------------------------------------

USE `myhome2013`;
DROP function IF EXISTS `myhome2013`.`new_expense_id`;

DELIMITER $$
USE `myhome2013`$$
CREATE DEFINER=`MyHome2013`@`%` FUNCTION `new_expense_id`() RETURNS int(11)
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

USE `myhome2013`;
DROP function IF EXISTS `myhome2013`.`new_expenses_category_id`;

DELIMITER $$
USE `myhome2013`$$
CREATE DEFINER=`MyHome2013`@`%` FUNCTION `new_expenses_category_id`() RETURNS int(11)
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

USE `myhome2013`;
DROP function IF EXISTS `myhome2013`.`new_income_category_id`;

DELIMITER $$
USE `myhome2013`$$
CREATE DEFINER=`MyHome2013`@`%` FUNCTION `new_income_category_id`() RETURNS int(11)
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

USE `myhome2013`;
DROP function IF EXISTS `myhome2013`.`new_income_id`;

DELIMITER $$
USE `myhome2013`$$
CREATE DEFINER=`MyHome2013`@`%` FUNCTION `new_income_id`() RETURNS int(11)
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

USE `myhome2013`;
DROP function IF EXISTS `myhome2013`.`new_payment_method_id`;

DELIMITER $$
USE `myhome2013`$$
CREATE DEFINER=`MyHome2013`@`%` FUNCTION `new_payment_method_id`() RETURNS int(11)
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
-- View `myhome2013`.`viw`
-- -----------------------------------------------------
DROP VIEW IF EXISTS `myhome2013`.`viw` ;
DROP TABLE IF EXISTS `myhome2013`.`viw`;
USE `myhome2013`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`MyHome2013`@`%` SQL SECURITY DEFINER VIEW `myhome2013`.`viw` AS select `ex`.`EXP_DATE` AS `Expense date`,`ex`.`AMOUNT` AS `Amount`,`excat`.`NAME` AS `Category`,`pay`.`NAME` AS `Payment Method`,`ex`.`COMMENTS` AS `Comments` from ((`myhome2013`.`t_expenses` `ex` join `myhome2013`.`t_expenses_category` `excat`) join `myhome2013`.`t_payment_methods` `pay`) where ((`excat`.`ID` = `ex`.`CATEGORY`) and (`pay`.`ID` = `ex`.`METHOD`));

-- -----------------------------------------------------
-- View `myhome2013`.`viwin`
-- -----------------------------------------------------
DROP VIEW IF EXISTS `myhome2013`.`viwin` ;
DROP TABLE IF EXISTS `myhome2013`.`viwin`;
USE `myhome2013`;
CREATE  OR REPLACE ALGORITHM=UNDEFINED DEFINER=`MyHome2013`@`%` SQL SECURITY DEFINER VIEW `myhome2013`.`viwin` AS select `inc`.`INC_DATE` AS `Income Date`,`inc`.`AMOUNT` AS `Amount`,`inccat`.`NAME` AS `Category`,`pay`.`NAME` AS `Payment Method`,`inc`.`COMMENTS` AS `Comments` from ((`myhome2013`.`t_incomes` `inc` join `myhome2013`.`t_incomes_category` `inccat`) join `myhome2013`.`t_payment_methods` `pay`) where ((`inccat`.`ID` = `inc`.`CATEGORY`) and (`pay`.`ID` = `inc`.`METHOD`));

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
