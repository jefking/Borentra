namespace Borentra.DataAccessLayer
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class ExtensionMethods
    {
        #region Borentra.DataAccessLayer.IStoredProc
        public static T CallObject<T>(this IStoredProc proc)
            where T : new()
        {
            var obj = default(T);

            using (var ds = proc.Execute())
            {
                obj = ds.LoadObject<T>();
            }

            return obj;
        }
        public static IList<T> CallObjects<T>(this IStoredProc proc)
            where T : new()
        {
            IList<T> obj = null;

            using (var ds = proc.Execute())
            {
                obj = ds.LoadObjects<T>();
            }

            return obj;
        }
        /// <summary>
        /// Execute IStored Proc
        /// </summary>
        /// <param name="proc">Procedure</param>
        /// <returns>Data Set</returns>
        public static DataSet Execute(this IStoredProc proc)
        {
            DataSet dataSet = null;
            var database = DatabaseFactory.CreateDatabase();
            using (var command = proc.BuildCommand(database))
            {
                dataSet = database.ExecuteDataSet(command);
            }

            return dataSet;
        }

        /// <summary>
        /// Execute Non-Query
        /// </summary>
        /// <param name="proc">Procedure To Execute</param>
        /// <returns>rows affected</returns>
        public static int ExecuteNonQuery(this IStoredProc proc)
        {
            var rowsAffected = 0;
            var database = DatabaseFactory.CreateDatabase();
            using (var command = proc.BuildCommand(database))
            {
                rowsAffected = database.ExecuteNonQuery(command);
            }

            return rowsAffected;
        }

        /// <summary>
        /// Build Command
        /// </summary>
        /// <param name="proc">Procedure</param>
        /// <param name="database">Database</param>
        /// <returns>Database Command</returns>
        public static DbCommand BuildCommand(this IStoredProc proc, Database database)
        {
            if (null == database)
            {
                throw new ArgumentNullException("database");
            }

            var command = database.GetStoredProcCommand(proc.StoredProc);
            foreach (var prop in proc.GetProperties())
            {
                var mapper = prop.GetAttribute<DataMapperAttribute>();

                if (mapper != null)
                {
                    var value = prop.GetValue(proc, null);
                    database.AddInParameter(command, mapper.ParameterName, mapper.DatabaseType, value);
                }
            }

            return command;
        }

        /// <summary>
        /// Fill Stored Proc Parameters
        /// </summary>
        /// <param name="proc">Stored Procedure</param>
        /// <param name="values">Values</param>
        public static void Fill(this IStoredProc proc, IDictionary<string, object> values)
        {
            if (null != values)
            {
                var properties = proc.GetProperties();
                foreach (var property in properties)
                {
                    if (null != property && property.CanWrite && values.ContainsKey(property.Name))
                    {
                        var value = values[property.Name];
                        if (null == value)
                        {
                            property.SetValue(proc, null, null);
                        }
                        else if (value.GetType() == property.PropertyType)
                        {
                            property.SetValue(proc, value, null);
                        }
                        else
                        {
                            var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            var safeValue = Convert.ChangeType(value, t);
                            property.SetValue(proc, safeValue, null);
                        }
                    }
                }
            }
        }
        #endregion

        #region System.Data.DataRow
        /// <summary>
        /// Get Value
        /// </summary>
        /// <typeparam name="T">Type Value</typeparam>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        /// <param name="defaultValue">Default Value</param>
        /// <returns>Value</returns>
        public static T Get<T>(this DataRow row, string column, T defaultValue = default(T))
        {
            if (null == row)
            {
                throw new ArgumentNullException("row");
            }

            if (string.IsNullOrWhiteSpace(column))
            {
                throw new ArgumentOutOfRangeException("column");
            }

            try
            {
                if (null != row.Table && null != row.Table.Columns && row.Table.Columns.Contains(column))
                {
                    return (T)Convert.ChangeType(row[column], typeof(T));
                }
            }
            catch (InvalidCastException)
            {
                // logger.Log(ice, EventTypes.Warning);
            }

            return defaultValue;
        }
        #endregion

        #region System.Data.IDataRecord
        /// <summary>
        /// Get Value
        /// </summary>
        /// <typeparam name="T">Type Value</typeparam>
        /// <param name="record">Row</param>
        /// <param name="column">Column</param>
        /// <param name="defaultValue">Default Value</param>
        /// <returns>Value</returns>
        public static T Get<T>(this IDataRecord record, string column, T defaultValue = default(T))
        {
            if (null == record)
            {
                throw new ArgumentNullException("record");
            }
            if (string.IsNullOrWhiteSpace(column))
            {
                throw new ArgumentOutOfRangeException("column");
            }

            try
            {
                if (record[column].IsValid())
                {
                    return (T)Convert.ChangeType(record[column], typeof(T));
                }
            }
            catch (IndexOutOfRangeException)
            {
                //   logger.Log(iorex, EventTypes.Information);
            }
            catch (ArgumentException)
            {
                // logger.Log(aex, EventTypes.Warning);
            }

            return defaultValue;
        }
        #endregion

        #region System.Data.DataSet
        /// <summary>
        /// Load Object from Data Set
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="ds">Data Set</param>
        /// <param name="action">Action</param>
        /// <returns>Objects</returns>
        public static IList<T> LoadObjects<T>(this DataSet ds, ActionFlags action = ActionFlags.Load)
            where T : new()
        {
            var values = new List<T>();
            foreach (DataTable table in ds.Tables)
            {
                values.AddRange(table.LoadObjects<T>(action));
            }

            return values;
        }

        /// <summary>
        /// Load Object from Data Set
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="ds">Data Set</param>
        /// <param name="action">Action</param>
        /// <returns>Object</returns>
        public static T LoadObject<T>(this DataSet ds, ActionFlags action = ActionFlags.Load)
            where T : new()
        {
            var data = ds.LoadObjects<T>(action);
            return null != data && 0 < data.Count ? data[0] : default(T);
        }
        #endregion

        #region System.Data.DataTable
        /// <summary>
        /// Load Object from Data Table
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="table">Data Table</param>
        /// <param name="action">Action</param>
        /// <returns>Object</returns>
        public static T LoadObject<T>(this DataTable table, ActionFlags action = ActionFlags.Load)
            where T : new()
        {
            var type = typeof(T);
            T value = Activator.CreateInstance<T>();
            Type createType;
            var isEnumerable = type.IsGenericType && value is IEnumerable;

            if (isEnumerable)
            {
                createType = type.GetGenericArguments()[0];
            }
            else
            {
                value = default(T);
                createType = type;
            }

            var list = value as IList;
            var columns = table.Columns.ToArray();
            foreach (DataRow row in table.Rows)
            {
                var instance = Activator.CreateInstance(createType);

                instance.Fill(columns, row.ItemArray, action);

                if (isEnumerable)
                {
                    if (null != list)
                    {
                        list.Add(instance);
                    }
                }
                else
                {
                    return (T)instance;
                }
            }

            return value;
        }

        /// <summary>
        /// Load Object from Data Table
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="table">Data Table</param>
        /// <param name="action">Action</param>
        /// <returns>Object</returns>
        public static IList<T> LoadObjects<T>(this DataTable table, ActionFlags action = ActionFlags.Load)
            where T : new()
        {
            return table.LoadObject<List<T>>(action);
        }
        #endregion

        #region System.Data.DataColumnCollection
        /// <summary>
        /// Data Column Collection to Array
        /// </summary>
        /// <param name="columns">Columns</param>
        /// <returns>Column Name Array</returns>
        public static string[] ToArray(this DataColumnCollection columns)
        {
            var cols = new string[columns.Count];
            for (var i = 0; i < cols.Length; i++)
            {
                cols[i] = columns[i].ColumnName;
            }

            return cols;
        }
        #endregion
    }
}