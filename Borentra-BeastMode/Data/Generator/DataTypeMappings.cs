namespace Borentra.Code
{
    /// <summary>
    /// Data Type Mappings
    /// </summary>
    public static class DataTypeMappings
    {
        #region Methods
        /// <summary>
        /// Data Type C#
        /// </summary>
        /// <param name="dataType">Data Type</param>
        /// <returns>Converted Data Type</returns>
        public static string DataTypeCSharp(string dataType)
        {
            switch (dataType)
            {
                case "varchar":
                case "nvarchar":
                case "ntext":
                case "string":
                    return "string";
                case "int":
                    return "int?";
                case "float":
                    return "double?";
                case "bigint":
                    return "long?";
                case "tinyint":
                    return "byte?";
                case "smallint":
                    return "short?";
                case "money":
                case "decimal":
                    return "decimal?";
                case "bit":
                    return "bool?";
                case "datetime2":
                case "smalldatetime":
                case "datetime":
                    return "DateTime?";
                case "uniqueidentifier":
                    return "Guid?";
                default:
                    return "object";
            }
        }

        /// <summary>
        /// Data Type Sql Server
        /// </summary>
        /// <param name="dataType">Data Type</param>
        /// <returns>Converted Data Type</returns>
        public static string DataTypeDbType(string dataType)
        {
            switch (dataType)
            {
                case "varchar":
                case "nvarchar":
                case "ntext":
                case "string":
                    return "DbType.String";
                case "int":
                    return "DbType.Int32";
                case "float":
                    return "DbType.Double";
                case "long":
                case "bigint":
                    return "DbType.Int64";
                case "tinyint":
                    return "DbType.Byte";
                case "smallint":
                    return "DbType.Int16";
                case "money":
                    return "DbType.Currency";
                case "decimal":
                    return "DbType.Decimal";
                case "bit":
                    return "DbType.Boolean";
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    return "DbType.DateTime";
                case "uniqueidentifier":
                    return "DbType.Guid";
                default:
                    return "DbType.Object";
            }
        }
        #endregion
    }
}