﻿using gamon;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace GlucoMan
{
    internal  partial class DL_Sqlite : DataLayer
    {
        internal  override int GetNextPrimaryKey()
        {
            return GetNextTablePrimaryKey("GlucoseRecords", "IdGlucoseRecord");
        }
        internal  override List<GlucoseRecord> GetGlucoseRecords(
            DateTime? InitialInstant, DateTime? FinalInstant)
        {
            List<GlucoseRecord> list = new List<GlucoseRecord>(); 
            try
            {
                DbDataReader dRead;
                DbCommand cmd;
                using (DbConnection conn = Connect())
                {
                    string query = "SELECT *" +
                        " FROM GlucoseRecords";
                    if (InitialInstant != null && FinalInstant != null)
                    {   // add WHERE clause
                        query += " WHERE Timestamp BETWEEN " + ((DateTime)InitialInstant).ToString("YYYY-MM-DD") +
                            " AND " + ((DateTime)FinalInstant).ToString("YYYY-MM-DD"); 
                    }
                    query += " ORDER BY Timestamp DESC";
                    cmd = new SqliteCommand(query);
                    cmd.Connection = conn;
                    dRead = cmd.ExecuteReader();
                    while (dRead.Read())
                    {
                        GlucoseRecord g = GetGlucoseRecordFromRow(dRead);
                        list.Add(g);
                    }
                    dRead.Dispose();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                Common.LogOfProgram.Error("Sqlite_GlucoseMeasurement | ReadGlucoseMeasurements", ex);
            }
            return list; 
        }
        internal override GlucoseRecord GetOneGlucoseRecord(int? IdGlucoseRecord)
        {
            GlucoseRecord gr = new GlucoseRecord();
            try
            {
                DbDataReader dRead;
                DbCommand cmd;
                using (DbConnection conn = Connect())
                {
                    string query = "SELECT *" +
                        " FROM GlucoseRecords";
                    query += " WHERE idGlucoseRecord=" + IdGlucoseRecord; 
                    query += ";";
                    cmd = new SqliteCommand(query);
                    cmd.Connection = conn;
                    dRead = cmd.ExecuteReader();
                    while (dRead.Read())
                    {
                        gr = GetGlucoseRecordFromRow(dRead);
                    }
                    dRead.Dispose();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                Common.LogOfProgram.Error("Sqlite_GlucoseMeasurement | ReadGlucoseMeasurements", ex);
            }
            return gr;
        }
        internal  override List<GlucoseRecord> GetLastTwoGlucoseMeasurements()
        {
            List<GlucoseRecord> list = new List<GlucoseRecord>();
            try
            {
                DbDataReader dRead;
                DbCommand cmd;
                using (DbConnection conn = Connect())
                {
                    string query = "SELECT *" +
                        " FROM GlucoseRecords";
                    query += " ORDER BY Timestamp DESC LIMIT 2";
                    cmd = new SqliteCommand(query);
                    cmd.Connection = conn;
                    dRead = cmd.ExecuteReader();
                    while (dRead.Read())
                    {
                        GlucoseRecord g = GetGlucoseRecordFromRow(dRead);
                        list.Add(g);
                    }
                    dRead.Dispose();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                Common.LogOfProgram.Error("Sqlite_GlucoseMeasurement | ReadGlucoseMeasurements", ex);
            }
            return list;
        }
        internal GlucoseRecord GetGlucoseRecordFromRow(DbDataReader Row)
        {
            GlucoseRecord gr = new GlucoseRecord();
            try
            {
                gr.IdGlucoseRecord = Safe.Int(Row["IdGlucoseRecord"]);
                gr.Timestamp = Safe.DateTime(Row["Timestamp"]);
                gr.GlucoseValue.Double = Safe.Double(Row["GlucoseValue"]);
                gr.GlucoseString = Safe.String(Row["GlucoseString"]);
                gr.IdDevice = Safe.String(Row["IdDevice"]);
                gr.IdTypeOfGlucoseMeasurement =  Safe.String(Row["IdTypeOfGlucoseMeasurement"]);
                gr.IdTypeOfGlucoseMeasurementDevice = Safe.String(Row["IdTypeOfGlucoseMeasurementDevice"]);
                gr.IdModelOfMeasurementSystem = Safe.String(Row["IdModelOfMeasurementSystem"]);
                gr.IdDocumentType = Safe.Int(Row["IdDocumentType"]);
                gr.Notes = Safe.String(Row["Notes"]);
            }
            catch (Exception ex)
            {
                Common.LogOfProgram.Error("Sqlite_GlucoseMeasurement | GetGlucoseRecordFromRow", ex);
            }
            return gr;
        }
        internal  override void SaveGlucoseMeasurements(List<GlucoseRecord> List)
        {
            try
            {
                foreach (GlucoseRecord rec in List)
                {
                    SaveOneGlucoseMeasurement(rec);
                }
            }
            catch (Exception ex)
            {
                Common.LogOfProgram.Error("Sqlite_GlucoseMeasurement | SaveGlucoseMeasurements", ex);
            }
        }
        internal  override long? SaveOneGlucoseMeasurement(GlucoseRecord GlucoseMeasurement)
        {
            try
            {
                if (GlucoseMeasurement.IdGlucoseRecord == null || GlucoseMeasurement.IdGlucoseRecord == 0)
                {
                    GlucoseMeasurement.IdGlucoseRecord = GetNextPrimaryKey();
                    // INSERT new record in the table
                    InsertGlucoseMeasurement(GlucoseMeasurement);                         
                }
                else
                {   // GlucoseMeasurement.IdGlucoseRecord exists
                    UpdateGlucoseMeasurement(GlucoseMeasurement); 
                }
                return GlucoseMeasurement.IdGlucoseRecord;
            }
            catch (Exception ex)
            {
                Common.LogOfProgram.Error("Sqlite_GlucoseMeasurement | SaveGlucoseMeasurements", ex);
                return null; 
            }
        }
        private long? UpdateGlucoseMeasurement(GlucoseRecord Measurement)
        {
            try { 
                using (DbConnection conn = Connect())
                {
                    DbCommand cmd = conn.CreateCommand();
                    string query = "UPDATE GlucoseRecords SET " +
                    "GlucoseValue=" + SqliteHelper.Double(Measurement.GlucoseValue) + "," +
                    "Timestamp=" + SqliteHelper.Date(Measurement.Timestamp) + "," +
                    "GlucoseString=" + SqliteHelper.String(Measurement.GlucoseString) + "," +
                    "IdTypeOfGlucoseMeasurement=" + SqliteHelper.String(Measurement.IdTypeOfGlucoseMeasurement) + "," +
                    "IdTypeOfGlucoseMeasurementDevice=" + SqliteHelper.String(Measurement.IdTypeOfGlucoseMeasurementDevice) + "," +
                    "IdModelOfMeasurementSystem=" + SqliteHelper.String(Measurement.IdModelOfMeasurementSystem) + "," +
                    "IdDevice=" + SqliteHelper.String(Measurement.IdDevice) + "," +
                    "IdDocumentType=" + SqliteHelper.Int(Measurement.IdDocumentType) + "," +
                    "Notes=" + SqliteHelper.String(Measurement.Notes) + ""; 
                    query += " WHERE IdGlucoseRecord=" + SqliteHelper.Int(Measurement.IdGlucoseRecord);
                    query += ";";
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                return Measurement.IdGlucoseRecord;
            }
            catch (Exception ex)
            {
                Common.LogOfProgram.Error("Sqlite_GlucoseMeasurement | SaveGlucoseMeasurements", ex);
                return null;
            }
        }
        private long? InsertGlucoseMeasurement(GlucoseRecord Measurement)
        {
            try
            {
                using (DbConnection conn = Connect())
                {
                    DbCommand cmd = conn.CreateCommand();
                    string query = "INSERT INTO GlucoseRecords" +
                    "(" +
                    "IdGlucoseRecord,GlucoseValue,Timestamp,GlucoseString," +
                    "IdTypeOfGlucoseMeasurement,IdTypeOfGlucoseMeasurementDevice,IdModelOfMeasurementSystem," +
                    "IdDevice,IdDocumentType,Notes";
                    query += ")VALUES (" +
                    SqliteHelper.Int(Measurement.IdGlucoseRecord) + "," +
                    SqliteHelper.Double(Measurement.GlucoseValue) + "," +
                    SqliteHelper.Date(Measurement.Timestamp) + "," +
                    SqliteHelper.String(Measurement.GlucoseString) + "," +
                    SqliteHelper.String(Measurement.IdTypeOfGlucoseMeasurement) + "," +
                    SqliteHelper.String(Measurement.IdTypeOfGlucoseMeasurementDevice) + "," +
                    SqliteHelper.String(Measurement.IdModelOfMeasurementSystem) + "," +
                    SqliteHelper.String(Measurement.IdDevice) + "," +
                    SqliteHelper.Int(Measurement.IdDocumentType) + "," +
                    SqliteHelper.String(Measurement.Notes) + ")";
                    query += ";";
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                return Measurement.IdGlucoseRecord;
            }
            catch (Exception ex)
            {
                Common.LogOfProgram.Error("Sqlite_GlucoseMeasurement | InsertIntoGlucoseMeasurement", ex);
                return null;
            }
        }
        internal override void DeleteOneGlucoseMeasurement(GlucoseRecord gr)
        {
            try
            {
                using (DbConnection conn = Connect())
                {
                    DbCommand cmd = conn.CreateCommand();
                    string query = "DELETE FROM GlucoseRecords" +
                    " WHERE IdGlucoseRecord=" + gr.IdGlucoseRecord;  
                    query += ";";
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                Common.LogOfProgram.Error("Sqlite_GlucoseMeasurement | DeleteOneGlucoseMeasurement", ex);
            }
        }
    }
}
