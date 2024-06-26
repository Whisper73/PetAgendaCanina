-- MySQL Script generated by MySQL Workbench
-- Fri May 10 20:19:35 2024
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema petagenda
-- -----------------------------------------------------
-- DROP SCHEMA IF EXISTS `petagenda` ;

-- -----------------------------------------------------
-- Schema petagenda
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `petagenda` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci ;
USE `petagenda` ;

-- -----------------------------------------------------
-- Table `petagenda`.`tipopelo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`tipopelo` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Pelo` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;


-- -----------------------------------------------------
-- Table `petagenda`.`tamanomascota`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`tamanomascota` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Tamano` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;


-- -----------------------------------------------------
-- Table `petagenda`.`categoriamascota`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`categoriamascota` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Id_TipoPelo` INT NULL DEFAULT NULL,
  `Id_TamanoMascota` INT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `categoriamascota_ibfk_1`
    FOREIGN KEY (`Id_TipoPelo`)
    REFERENCES `petagenda`.`tipopelo` (`Id`),
  CONSTRAINT `categoriamascota_ibfk_2`
    FOREIGN KEY (`Id_TamanoMascota`)
    REFERENCES `petagenda`.`tamanomascota` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;

CREATE INDEX `Id_TipoPelo` ON `petagenda`.`categoriamascota` (`Id_TipoPelo` ASC) VISIBLE;

CREATE INDEX `Id_TamanoMascota` ON `petagenda`.`categoriamascota` (`Id_TamanoMascota` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `petagenda`.`mascota`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`mascota` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Raza` VARCHAR(255) NULL DEFAULT NULL,
  `Nombre` VARCHAR(255) NULL DEFAULT NULL,
  `Id_CategoriaMascota` INT NULL DEFAULT NULL,
  `FechNacim` DATE NULL DEFAULT NULL,
  `NivelTemperamento` INT NULL DEFAULT NULL,
  `Observaciones` TEXT NULL DEFAULT NULL,
  `FechaRegistro` DATE NULL DEFAULT NULL,
  `EstaBorrado` TINYINT(1) NULL DEFAULT '0',
  `FechaBorrado` DATE NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `mascota_ibfk_1`
    FOREIGN KEY (`Id_CategoriaMascota`)
    REFERENCES `petagenda`.`categoriamascota` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;

CREATE INDEX `Id_CategoriaMascota` ON `petagenda`.`mascota` (`Id_CategoriaMascota` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `petagenda`.`persona`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`persona` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Documento` VARCHAR(255) NULL DEFAULT NULL,
  `Nombre` VARCHAR(255) NULL DEFAULT NULL,
  `Apellido` VARCHAR(255) NULL DEFAULT NULL,
  `Num_Telefono` VARCHAR(255) NULL DEFAULT NULL,
  `Correo` VARCHAR(255) NULL DEFAULT NULL,
  `Direccion` VARCHAR(255) NULL DEFAULT NULL,
  `FechaRegistro` DATE NULL DEFAULT NULL,
  `EstaBorrado` TINYINT(1) NULL DEFAULT '0',
  `FechaBorrado` DATE NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 13
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;


-- -----------------------------------------------------
-- Table `petagenda`.`cliente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`cliente` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Id_Persona` INT NULL DEFAULT NULL,
  `Nivel` INT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `cliente_ibfk_1`
    FOREIGN KEY (`Id_Persona`)
    REFERENCES `petagenda`.`persona` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;

CREATE INDEX `Id_Persona` ON `petagenda`.`cliente` (`Id_Persona` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `petagenda`.`cliente_mascota`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`cliente_mascota` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Id_Cliente` INT NULL DEFAULT NULL,
  `Id_Mascota` INT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `cliente_mascota_ibfk_1`
    FOREIGN KEY (`Id_Mascota`)
    REFERENCES `petagenda`.`mascota` (`Id`),
  CONSTRAINT `cliente_mascota_ibfk_2`
    FOREIGN KEY (`Id_Cliente`)
    REFERENCES `petagenda`.`cliente` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;

CREATE INDEX `Id_Mascota` ON `petagenda`.`cliente_mascota` (`Id_Mascota` ASC) VISIBLE;

CREATE INDEX `Id_Cliente` ON `petagenda`.`cliente_mascota` (`Id_Cliente` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `petagenda`.`estadocita`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`estadocita` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Estado` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;


-- -----------------------------------------------------
-- Table `petagenda`.`cita`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`cita` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Id_Cliente_Mascota` INT NULL DEFAULT NULL,
  `Fecha` DATETIME NULL DEFAULT NULL,
  `Id_EstadoCita` INT NULL DEFAULT NULL,
  `EstaEliminada` TINYINT(1) NULL DEFAULT '0',
  `FechaEliminada` DATE NULL DEFAULT NULL,
  `Observacion` TEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `cita_ibfk_1`
    FOREIGN KEY (`Id_Cliente_Mascota`)
    REFERENCES `petagenda`.`cliente_mascota` (`Id`),
  CONSTRAINT `cita_ibfk_2`
    FOREIGN KEY (`Id_EstadoCita`)
    REFERENCES `petagenda`.`estadocita` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;

CREATE INDEX `Id_Cliente_Mascota` ON `petagenda`.`cita` (`Id_Cliente_Mascota` ASC) VISIBLE;

CREATE INDEX `Id_EstadoCita` ON `petagenda`.`cita` (`Id_EstadoCita` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `petagenda`.`servicio`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`servicio` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `InicioVigencia` DATE NULL DEFAULT NULL,
  `FinVigencia` DATE NULL DEFAULT NULL,
  `Descripcion` VARCHAR(255) NULL DEFAULT NULL,
  `ValorServicioBase` FLOAT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;


-- -----------------------------------------------------
-- Table `petagenda`.`cita_servicio`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`cita_servicio` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Id_Cita` INT NULL DEFAULT NULL,
  `Id_Servicio` INT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `cita_servicio_ibfk_1`
    FOREIGN KEY (`Id_Cita`)
    REFERENCES `petagenda`.`cita` (`Id`),
  CONSTRAINT `cita_servicio_ibfk_2`
    FOREIGN KEY (`Id_Servicio`)
    REFERENCES `petagenda`.`servicio` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;

CREATE INDEX `Id_Cita` ON `petagenda`.`cita_servicio` (`Id_Cita` ASC) VISIBLE;

CREATE INDEX `Id_Servicio` ON `petagenda`.`cita_servicio` (`Id_Servicio` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `petagenda`.`promocion`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`promocion` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Descripcion` VARCHAR(255) NULL DEFAULT NULL,
  `Descuento` DOUBLE NULL DEFAULT NULL,
  `EstaActiva` TINYINT(1) NULL DEFAULT '1',
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;


-- -----------------------------------------------------
-- Table `petagenda`.`estadopago`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`estadopago` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Estado` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;


-- -----------------------------------------------------
-- Table `petagenda`.`metodopago`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`metodopago` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Tipo` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;


-- -----------------------------------------------------
-- Table `petagenda`.`factura`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`factura` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `FechaEmision` DATE NULL DEFAULT NULL,
  `Id_Cliente` INT NULL DEFAULT NULL,
  `Id_Cita` INT NULL DEFAULT NULL,
  `Id_Promocion` INT NULL DEFAULT NULL,
  `IVA` FLOAT NULL DEFAULT NULL,
  `Total` FLOAT NULL DEFAULT NULL,
  `Id_EstadoPago` INT NULL DEFAULT NULL,
  `Id_MetodoPago` INT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `factura_ibfk_1`
    FOREIGN KEY (`Id_Cliente`)
    REFERENCES `petagenda`.`cliente` (`Id`),
  CONSTRAINT `factura_ibfk_2`
    FOREIGN KEY (`Id_Cita`)
    REFERENCES `petagenda`.`cita` (`Id`),
  CONSTRAINT `factura_ibfk_3`
    FOREIGN KEY (`Id_Promocion`)
    REFERENCES `petagenda`.`promocion` (`Id`),
  CONSTRAINT `factura_ibfk_4`
    FOREIGN KEY (`Id_EstadoPago`)
    REFERENCES `petagenda`.`estadopago` (`Id`),
  CONSTRAINT `factura_ibfk_5`
    FOREIGN KEY (`Id_MetodoPago`)
    REFERENCES `petagenda`.`metodopago` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;

CREATE INDEX `Id_Cliente` ON `petagenda`.`factura` (`Id_Cliente` ASC) VISIBLE;

CREATE INDEX `Id_Cita` ON `petagenda`.`factura` (`Id_Cita` ASC) VISIBLE;

CREATE INDEX `Id_Promocion` ON `petagenda`.`factura` (`Id_Promocion` ASC) VISIBLE;

CREATE INDEX `Id_EstadoPago` ON `petagenda`.`factura` (`Id_EstadoPago` ASC) VISIBLE;

CREATE INDEX `Id_MetodoPago` ON `petagenda`.`factura` (`Id_MetodoPago` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `petagenda`.`servicio_categoriamascota`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`servicio_categoriamascota` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Id_Servicio` INT NULL DEFAULT NULL,
  `Id_CategoriaMascota` INT NULL DEFAULT NULL,
  `PorcentajeAumentoBase` FLOAT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `servicio_categoriamascota_ibfk_1`
    FOREIGN KEY (`Id_Servicio`)
    REFERENCES `petagenda`.`servicio` (`Id`),
  CONSTRAINT `servicio_categoriamascota_ibfk_2`
    FOREIGN KEY (`Id_CategoriaMascota`)
    REFERENCES `petagenda`.`categoriamascota` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;

CREATE INDEX `Id_Servicio` ON `petagenda`.`servicio_categoriamascota` (`Id_Servicio` ASC) VISIBLE;

CREATE INDEX `Id_CategoriaMascota` ON `petagenda`.`servicio_categoriamascota` (`Id_CategoriaMascota` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `petagenda`.`detalle_factura`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`detalle_factura` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Id_Factura` INT NULL DEFAULT NULL,
  `Id_Servicio_CategoriaMascota` INT NULL DEFAULT NULL,
  `Preciounitario` FLOAT NULL DEFAULT NULL,
  `SubTotal` FLOAT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `detalle_factura_ibfk_1`
    FOREIGN KEY (`Id_Factura`)
    REFERENCES `petagenda`.`factura` (`Id`),
  CONSTRAINT `detalle_factura_ibfk_2`
    FOREIGN KEY (`Id_Servicio_CategoriaMascota`)
    REFERENCES `petagenda`.`servicio_categoriamascota` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;

CREATE INDEX `Id_Factura` ON `petagenda`.`detalle_factura` (`Id_Factura` ASC) VISIBLE;

CREATE INDEX `Id_Servicio_CategoriaMascota` ON `petagenda`.`detalle_factura` (`Id_Servicio_CategoriaMascota` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `petagenda`.`empleado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `petagenda`.`empleado` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Id_Persona` INT NULL DEFAULT NULL,
  `NivelPermisos` INT NULL DEFAULT '0',
  `EsAdmin` TINYINT(1) NULL DEFAULT '0',
  `Contrasena` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `empleado_ibfk_1`
    FOREIGN KEY (`Id_Persona`)
    REFERENCES `petagenda`.`persona` (`Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_general_ci;

CREATE INDEX `Id_Persona` ON `petagenda`.`empleado` (`Id_Persona` ASC) VISIBLE;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
