namespace Borentra.Generator
{
    using Borentra.Code;
    using Borentra.Code.Template;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;

    public class Program
    {
        #region Members
        /// <summary>
        /// SQL Statement
        /// </summary>
        private const string sqlStatement = @"SELECT DISTINCT
parm.name AS [Parameter]
, LOWER(typ.name) AS [DataType]
, SPECIFIC_SCHEMA AS [Schema]
, SPECIFIC_NAME AS [StoredProcedure]
, CASE parm.max_length WHEN -1 THEN 2147483647 ELSE parm.max_length END AS [MaxLength]
FROM sys.procedures sp LEFT OUTER JOIN sys.parameters parm ON sp.object_id = parm.object_id
INNER JOIN [information_schema].[routines] ON routine_type = 'PROCEDURE' AND ROUTINE_NAME not like 'sp_%diagram%'
	 AND ROUTINE_NAME not like 'sp_%diagram%'
	 AND sp.name = SPECIFIC_NAME
LEFT OUTER JOIN sys.types typ ON parm.system_type_id = typ.system_type_id
	AND typ.name <> 'sysname'
ORDER BY SPECIFIC_NAME, SPECIFIC_SCHEMA";
        #endregion

        #region Methods
        /// <summary>
        /// Program Start Method
        /// </summary>
        /// <param name="args">Arguments</param>
        public static void Main(string[] args)
        {
            var reader = new System.Configuration.AppSettingsReader();
            var connectionString = reader.GetValue("ConnectionString", typeof(string)) as string;
            var folder = reader.GetValue("Directory", typeof(string)) as string;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var execute = new SqlCommand(sqlStatement, connection))
                {
                    using (var dataAdaptor = new SqlDataAdapter(execute))
                    {
                        using (var dataSet = new DataSet())
                        {
                            dataAdaptor.Fill(dataSet, "Schema");

                            var table = dataSet.Tables[0];
                            var procs = (from DataRow data in table.Rows
                                         select new { Name = data["StoredProcedure"].ToString(), Schema = data["Schema"].ToString() }).Distinct();

                            var manifest = new Dictionary<string, Definition>(procs.Count());
                            foreach (var schemas in procs)
                            {
                                var proc = new Definition()
                                {
                                    Name = schemas.Name,
                                    Preface = schemas.Schema,
                                };

                                proc.Variables = from DataRow data in table.Rows
                                                 where data["StoredProcedure"].ToString() == proc.Name
                                                    && data["Schema"].ToString() == proc.Preface
                                                    && !string.IsNullOrWhiteSpace(data["Parameter"].ToString())
                                                    && !string.IsNullOrWhiteSpace(data["DataType"].ToString())
                                                 select new Variable()
                                                            {
                                                                DataType = data["DataType"].ToString(),
                                                                Name = data["Parameter"].ToString(),
                                                                MaxLength = data["MaxLength"] is DBNull ? 0 : Convert.ToInt32(data["MaxLength"])
                                                            };

                                manifest.Add(string.Format("{0}{1}", proc.Name, proc.Preface), proc);
                            }

                            var template = new Dal()
                            {
                                Manifest = manifest,
                            };

                            File.WriteAllText(string.Format("{0}Generated.cs", folder), template.TransformText());
                        }
                    }
                }
            }
        }
        #endregion
    }
}