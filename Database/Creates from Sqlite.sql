BEGIN TRANSACTION;
DROP TABLE IF EXISTS 'InsulinInjection';
CREATE TABLE IF NOT EXISTS 'InsulinInjection' (
	'idInsulinInjection'	INT NOT NULL,
	'descInsulinInjection'	VARCHAR(45),
	'amount'	VARCHAR(45),
	PRIMARY KEY('idInsulinInjection')
);
DROP TABLE IF EXISTS 'ModelsOfMeasurementSystem';
CREATE TABLE IF NOT EXISTS 'ModelsOfMeasurementSystem' (
	'IdModelOfMeasurementSystem'	INT NOT NULL,
	'Name'	VARCHAR(45),
	PRIMARY KEY('IdModelOfMeasurementSystem')
);
DROP TABLE IF EXISTS 'InsulineInjections';
CREATE TABLE IF NOT EXISTS 'InsulineInjections' (
	'IdInsulineInjection'	INT NOT NULL,
	'Timestamp'	VARCHAR(45),
	'InsulinValue'	DOUBLE,
	'InjectionPositionX'	INT,
	'InjectionPositionY'	INT,
	'IdTypeOfInjection'	INT,
	'IdTypeOfInsulinSpeed'	INT,
	'IdTypeOfInsulinInjection'	INT,
	'InsulinString'	VARCHAR(45),
	PRIMARY KEY('IdInsulineInjection')
);
DROP TABLE IF EXISTS 'BolusCalculations';
CREATE TABLE IF NOT EXISTS 'BolusCalculations' (
	'IdBolusCalculation'	INT NOT NULL,
	'Timestamp'	DATETIME,
	'TotalInsulinForMeal'	DOUBLE,
	'CalculatedChoToEat'	DOUBLE,
	'BolusInsulinDueToChoOfMeal'	DOUBLE,
	'BolusInsulinDueToCorrectionOfGlucose'	DOUBLE,
	'TargetGlucose'	DOUBLE,
	'InsulinCorrectionSensitivity'	DOUBLE,
	'TypicalBolusMorning'	DOUBLE,
	'TypicalBolusMidday'	DOUBLE,
	'TypicalBolusEvening'	DOUBLE,
	'TypicalBolusNight'	DOUBLE,
	'TotalDailyDoseOfInsulin'	DOUBLE,
	'ChoInsulinRatioBreakfast'	DOUBLE,
	'ChoInsulinRatioLunch'	DOUBLE,
	'ChoInsulinRatioDinner'	DOUBLE,
	'GlucoseBeforeMeal'	DOUBLE,
	'GlucoseToBeCorrected'	DOUBLE,
	'FactorOfInsulinCorrectionSensitivity'	DOUBLE,
	PRIMARY KEY('IdBolusCalculation')
);
DROP TABLE IF EXISTS 'Alarms';
CREATE TABLE IF NOT EXISTS 'Alarms' (
	'idAlarm'	INT NOT NULL,
	'TimeStart'	DATETIME,
	'TimeAlarm'	DATETIME,
	'Interval'	DOUBLE,
	'Duration'	DOUBLE,
	'IsRepeated'	TINYINT,
	'IsEnabled'	TINYINT,
	PRIMARY KEY('idAlarm')
);
DROP TABLE IF EXISTS 'GlucoseRecords';
CREATE TABLE IF NOT EXISTS 'GlucoseRecords' (
	'IdGlucoseRecord'	INT NOT NULL,
	'GlucoseValue'	DOUBLE,
	'Timestamp'	DATETIME,
	'GlucoseString'	VARCHAR(45),
	'IdTypeOfGlucoseMeasurement'	INT,
	'IdTypeOfGlucoseMeasurementDevice'	INT,
	'IdModelOfMeasurementSystem'	INT,
	'IdDevice'	VARCHAR(45),
	'IdDocumentType'	INT,
	'Notes'	VARCHAR(255),
	PRIMARY KEY('IdGlucoseRecord')
);
DROP TABLE IF EXISTS 'Meals';
CREATE TABLE IF NOT EXISTS 'Meals' (
	'IdMeal'	INT NOT NULL,
	'IdTypeOfMeal'	INT,
	'TimeBegin'	DATETIME,
	'TimeEnd'	DATETIME,
	'Carbohydrates'	DOUBLE,
	'AccuracyOfChoEstimate'	DOUBLE,
	'IdBolusCalculation'	TEXT,
	'IdGlucoseRecord'	INT,
	'IdQualitativeAccuracyCHO'	INT,
	PRIMARY KEY('IdMeal')
);
DROP TABLE IF EXISTS 'FoodsInMeals';
CREATE TABLE IF NOT EXISTS 'FoodsInMeals' (
	'IdFoodInMeal'	INT NOT NULL,
	'IdMeal'	INT,
	'IdFood'	INT,
	'CarbohydratesGrams'	DOUBLE,
	'CarbohydratesPercent'	INTEGER,
	'Quantity'	DOUBLE,
	'AccuracyOfChoEstimate'	DOUBLE,
	'QualitativeAccuracy'	INT,
	'Name'	TEXT,
	'Field10'	INTEGER,
	PRIMARY KEY('IdFoodInMeal')
);
DROP TABLE IF EXISTS 'Foods';
CREATE TABLE IF NOT EXISTS 'Foods' (
	'IdFood'	INT NOT NULL,
	'Name'	VARCHAR(15),
	'Description'	VARCHAR(256),
	'Energy'	DOUBLE,
	'TotalFats'	DOUBLE,
	'SaturatedFats'	DOUBLE,
	'Carbohydrates'	DOUBLE NOT NULL,
	'Sugar'	DOUBLE,
	'Fibers'	INTEGER,
	'Proteins'	INTEGER,
	'Salt'	DOUBLE,
	'Potassium'	DOUBLE,
	'GlycemicIndex'	DOUBLE,
	PRIMARY KEY('IdFood')
);
DROP TABLE IF EXISTS 'InsulinDrugs';
CREATE TABLE IF NOT EXISTS 'InsulinDrugs' (
	'IdInsulinDrugs'	INTEGER NOT NULL,
	'Name'	VARCHAR(30),
	'InsulinSpeed'	DOUBLE,
	PRIMARY KEY('IdInsulinDrugs')
);
DROP TABLE IF EXISTS 'HypoPredictions';
CREATE TABLE IF NOT EXISTS 'HypoPredictions' (
	'IdHypoPrediction'	INT NOT NULL,
	'PredictedTime'	DATETIME,
	'AlarmTime'	DATETIME,
	'GlucoseSlope'	DOUBLE,
	'HypoGlucoseTarget'	INT,
	'GlucoseLast'	DOUBLE,
	'GlucosePrevious'	DOUBLE,
	'Interval'	VARCHAR(10),
	'DatetimeLast'	DATETIME,
	'DatetimePrevious'	DATETIME,
	PRIMARY KEY('IdHypoPrediction')
);
DROP TABLE IF EXISTS 'Parameters';
CREATE TABLE IF NOT EXISTS 'Parameters' (
	'IdParameters'	INTEGER NOT NULL,
	'TargetGlucose'	INTEGER,
	'GlucoseBeforeMeal'	INTEGER,
	'ChoToEat'	INTEGER,
	'ChoInsulinRatioBreakfast'	DOUBLE,
	'ChoInsulinRatioLunch'	DOUBLE,
	'ChoInsulinRatioDinner'	DOUBLE,
	'TypicalBolusMorning'	DOUBLE,
	'TypicalBolusMidday'	DOUBLE,
	'TypicalBolusEvening'	DOUBLE,
	'TypicalBolusNight'	DOUBLE,
	'TotalDailyDoseOfInsulin'	DOUBLE,
	'FactorOfInsulinCorrectionSensitivity'	DOUBLE,
	'InsulinCorrectionSensitivity'	DOUBLE,
	'Hypo_GlucoseTarget'	DOUBLE,
	'Hypo_GlucoseLast'	DOUBLE,
	'Hypo_GlucosePrevious'	DOUBLE,
	'Hypo_HourLast'	DOUBLE,
	'Hypo_HourPrevious'	DOUBLE,
	'Hypo_MinuteLast'	DOUBLE,
	'Hypo_MinutePrevious'	DOUBLE,
	'Hypo_AlarmAdvanceTime'	DOUBLE,
	'Hypo_FutureSpanMinutes'	DOUBLE,
	'Hit_ChoAlreadyTaken'	DOUBLE,
	'Hit_ChoOfFood'	DOUBLE,
	'Hit_TargetCho'	DOUBLE,
	PRIMARY KEY('IdParameters' AUTOINCREMENT)
);
COMMIT;
