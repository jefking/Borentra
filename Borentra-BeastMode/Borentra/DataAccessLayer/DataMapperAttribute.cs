﻿namespace Borentra.DataAccessLayer
{
    using System;
    using System.Data;

    /// <summary>
    /// Data Mapper Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class DataMapperAttribute : Attribute
    {
        #region Members
        /// <summary>
        /// Parameter Name
        /// </summary>
        private readonly string parameterName;

        /// <summary>
        /// Database Type
        /// </summary>
        private readonly DbType type;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the DataMapperAttribute class
        /// </summary>
        /// <param name="parameterName">Parameter Name</param>
        /// <param name="type">Database Type</param>
        public DataMapperAttribute(string parameterName, DbType type)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentException("parameterName");
            }

            this.parameterName = parameterName;
            this.type = type;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Parameter Name
        /// </summary>
        public string ParameterName
        {
            get
            {
                return this.parameterName;
            }
        }

        /// <summary>
        /// Gets the Database Type
        /// </summary>
        public DbType DatabaseType
        {
            get
            {
                return this.type;
            }
        }
        #endregion
    }
}