namespace Borentra
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class ExtensionMethods
    {
        #region System.DateTime
        public static string ToRFC822Format(this DateTime date)
        {
            return string.Format("{0} UT", date.ToUniversalTime().ToString("ddd, dd MMM yyyy HH:mm:ss"));
        }
        public static string ToStringExact(this DateTime date)
        {
            return date.ToString("O");
        }
        #endregion

        #region System.Object
        /// <summary>
        /// Is the object a valid one
        /// </summary>
        /// <param name="obj">the object to validate</param>
        /// <returns>true if valid, false otherwise</returns>
        public static bool IsValid(this object obj)
        {
            return obj != null && obj != DBNull.Value;
        }

        /// <summary>
        /// Get Properties
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Property Info</returns>
        public static PropertyInfo[] GetProperties(this object value)
        {
            var t = value.GetType();
            return t.GetProperties();
        }

        /// <summary>
        /// Parameters
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Parameters</returns>
        public static string[] Parameters(this object value)
        {
            var properties = value.GetProperties();
            return (from property in properties
                    where property.CanWrite
                    select property.Name).ToArray();
        }

        /// <summary>
        /// Get Attribute
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Attribute</returns>
        public static T GetAttribute<T>(this object value)
            where T : Attribute
        {
            var t = value.GetType();
            var attributes = t.GetCustomAttributes(false);
            return attributes.GetAttribute<T>();
        }

        /// <summary>
        /// Get Value Mapping of Object and parameters
        /// </summary>
        /// <param name="value">Object with Values</param>
        /// <param name="parameters">Stored Procedure Parameters</param>
        /// <param name="action">Action</param>
        /// <returns>Mapped Parameters to Values</returns>
        public static IDictionary<string, object> ValueMapping(this object value, string[] parameters, ActionFlags action = ActionFlags.Execute)
        {
            if (null == parameters)
            {
                throw new ArgumentNullException("parameters");
            }

            var row = new Dictionary<string, object>(parameters.Length);
            var columnHash = new HashSet<string>(parameters);
            var properties = value.GetProperties();

            foreach (var property in properties)
            {
                foreach (var actionName in property.GetAttributes<Borentra.DataAccessLayer.ActionNameAttribute>())
                {
                    if (null != actionName && actionName.Action == action)
                    {
                        var name = actionName.Name.Replace("@", string.Empty);
                        if (!columnHash.Contains(name) && !row.ContainsKey(name))
                        {
                            row.Add(name, property.GetValue(value, null));
                        }
                    }
                }

                if (columnHash.Contains(property.Name))
                {
                    row.Add(property.Name, property.GetValue(value, null));
                }
            }

            return row;
        }

        /// <summary>
        /// Fill Object with values from the data store
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="value">Value</param>
        /// <param name="columns">Columns</param>
        /// <param name="values">Values</param>
        /// <param name="action">Action</param>
        public static void Fill(this object value, string[] columns, object[] values, ActionFlags action = ActionFlags.Load)
        {
            if (null == columns)
            {
                throw new ArgumentNullException("columns");
            }

            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

            if (columns.Length != values.Length)
            {
                throw new ArgumentException("Columns don't match values.");
            }

            var properties = value.GetProperties();

            var propertyDictionary = new Dictionary<string, PropertyInfo>(properties.Count());
            foreach (var property in properties)
            {
                propertyDictionary.Add(property.Name, property);

                foreach (var actionName in property.GetAttributes<Borentra.DataAccessLayer.ActionNameAttribute>())
                {
                    if (null != actionName && actionName.Action == action && !propertyDictionary.ContainsKey(actionName.Name))
                    {
                        propertyDictionary.Add(actionName.Name, property);
                    }
                }
            }

            for (int i = 0; i < columns.Length; i++)
            {
                if (propertyDictionary.ContainsKey(columns[i]))
                {
                    var property = propertyDictionary[columns[i]];
                    if (null != property)
                    {
                        property.Set(value, values[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Map properties from one object to another
        /// </summary>
        /// <typeparam name="T">Type of Secondary Object</typeparam>
        /// <param name="from">From</param>
        /// <param name="to">To</param>
        /// <returns>Object</returns>
        public static T Map<T>(this object from, T to)
        {
            if (null == from)
            {
                throw new ArgumentNullException("from");
            }

            if (null == to)
            {
                throw new ArgumentNullException("to");
            }

            var properties = from.ValueMapping(from.Parameters());
            to.Fill(properties.Keys.ToArray(), properties.Values.ToArray());
            return to;
        }

        /// <summary>
        /// Map properties from one object to another
        /// </summary>
        /// <typeparam name="T">Type of Secondary Object</typeparam>
        /// <param name="from">From</param>
        /// <returns>Object</returns>
        public static T Map<T>(this object from)
        {
            if (null == from)
            {
                throw new ArgumentNullException("from");
            }

            return from.Map<T>(Activator.CreateInstance<T>());
        }

        public static T Deserialize<T>(this byte[] value)
        {
            T returnValue;
            using (var ms = new MemoryStream())
            {
                ms.Write(value, 0, value.Length);
                ms.Position = 0;
                var bf = new BinaryFormatter();
                returnValue = (T)bf.Deserialize(ms);
            }

            return returnValue;
        }

        /// <summary>
        /// Serializes an Object
        /// </summary>
        /// <param name="value">object to Serialize</param>
        /// <returns>bytes</returns>
        public static byte[] Serialize(this object value)
        {
            byte[] bytes;
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, value);
                bytes = ms.ToArray();
            }

            return bytes;
        }

        public static string ToBase64(this object value)
        {
            var bytes = value.Serialize();
            return Convert.ToBase64String(bytes);
        }
        #endregion

        #region System.Object[]
        /// <summary>
        /// Get Attribute
        /// </summary>
        /// <typeparam name="T">Attribute Type</typeparam>
        /// <param name="attributes">Attributes</param>
        /// <returns>Attribute of Type</returns>
        public static T GetAttribute<T>(this object[] attributes)
            where T : Attribute
        {
            return (T)(from item in attributes
                       where item.GetType() == typeof(T)
                       select item).FirstOrDefault();
        }
        #endregion

        #region System.Byte[]
        public static string GetHexadecimal(this byte[] value)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                sb.Append(value[i].ToString("x2", CultureInfo.InvariantCulture));
            }

            return sb.ToString();
        }
        #endregion

        #region System.Reflection.PropertyInfo
        /// <summary>
        /// Get Attribute from Property Info
        /// </summary>
        /// <typeparam name="T">Attribute</typeparam>
        /// <param name="value">Value</param>
        /// <returns>Attribute</returns>
        public static T GetAttribute<T>(this PropertyInfo value)
            where T : Attribute
        {
            var attributes = value.GetCustomAttributes(false);

            return attributes.GetAttribute<T>();
        }

        /// <summary>
        /// Get Attribute
        /// </summary>
        /// <typeparam name="T">Attribute Type</typeparam>
        /// <param name="attributes">Attributes</param>
        /// <returns>Attribute of Type</returns>
        public static IEnumerable<T> GetAttributes<T>(this PropertyInfo property)
            where T : Attribute
        {
            var enumeration = new List<T>();

            foreach (var obj in property.GetCustomAttributes(false))
            {
                if (obj.GetType() == typeof(T))
                {
                    enumeration.Add((T)obj);
                }
            }

            return enumeration;
        }

        /// <summary>
        /// Set Value of Property
        /// </summary>
        /// <param name="property">Property</param>
        /// <param name="value">Value</param>
        public static void Set(this PropertyInfo property, object owner, object value = null)
        {
            if (null == owner)
            {
                throw new ArgumentNullException("owner");
            }
            else
            {
                if (property.CanWrite)
                {
                    object ptr = null;
                    if (DBNull.Value != value && null != value)
                    {
                        if (property.PropertyType.BaseType == typeof(Enum))
                        {
                            ptr = Enum.ToObject(property.PropertyType, value);
                        }
                        else if (value as IConvertible != null)
                        {
                            var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            ptr = Convert.ChangeType(value, t);
                        }
                        else
                        {
                            ptr = value;
                        }
                    }

                    property.SetValue(owner, ptr, null);
                }
            }
        }
        #endregion

        #region System.String
        public static DateTime DateTimeExact(this string value)
        {
            return DateTime.ParseExact(value, "O", CultureInfo.InvariantCulture);
        }
        public static T FromBase64<T>(this string value)
        {
            var data = Convert.FromBase64String(value);
            return data.Deserialize<T>();
        }
        /// <summary>
        /// Trim the given string if it is not null
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>Trimmed string if specified, null if not</returns>
        public static string TrimIfNotNull(this string str)
        {
            return str != null ? str.Trim() : null;
        }

        /// <summary>
        /// First Part
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitter"></param>
        /// <returns></returns>
        public static string FirstPart(this string str, char splitter = ' ')
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            else if (!str.Contains(splitter))
            {
                return str;
            }
            else
            {
                return str.Split(splitter)[0];
            }
        }
        public static byte[] FromHex(this string value)
        {
            var length = value.Length;
            var bytes = new char[length / 2][];
            var bits = value.ToCharArray();
            for (uint i = 0; i < length / 2; i++)
            {
                bytes[i] = new char[] { bits[i * 2], bits[(i * 2) + 1] };
            }

            var converted = new byte[bytes.LongLength];
            for (long i = 0; i < bytes.LongLength; i++)
            {
                converted[i] = byte.Parse(bytes[i][0].ToString() + bytes[i][1], NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
            }

            return converted;
        }
        #endregion

        #region System.Guid
        public static Guid? ToNullable(this Guid value)
        {
            return value == Guid.Empty ? (Guid?)null : value;
        }
        #endregion
    }
}