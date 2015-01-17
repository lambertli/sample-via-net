using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CodeGengeration
{
    class SqlGengerateSample : Executor
    {
        Person person = new Person { Name = "小李", Age = 28, Gender = Gender.Male, PersonWebsiteUrl = "http://zzk.cnblogs.com/s?w=GetTypeCode+enum&t='b" };

        [TestMethod]
        public void Test_Generate_Insert_Sql()
        {
            string sql = GenerateInsertSql("Persons", person, "CreatedBy");
            Console.WriteLine(sql);
        }

        [TestMethod]
        public void Test_Generate_Update_Sql()
        {
            string sql = GenerateUpdateSql("Persons", person, new string[] { "Name", "Age" }, "CreatedBy");
            Console.WriteLine(sql);
        }

        [TestMethod]
        public void Test_Generate_Delete_Sql()
        {
            string sql = GenerateDeleteSql("Persons", person, new string[] { "Name", "Age" });
            Console.WriteLine(sql);
        }

        //insert into [table](ColumnNames) values(ColumnValues) 
        private string GenerateInsertSql(string tableName, object entity, params string[] ignoreFields)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            SqlBuildModel model = CreateSqlBuldModel(tableName, entity, null, ignoreFields);
            if (model.Fields == null)
                return string.Empty;

            sqlBuilder.AppendFormat("insert into [{0}].[{1}](", model.Schema, model.TableName);
            sqlBuilder.Append(string.Join(", ", model.Fields.Select(m => string.Format("[{0}]", m.FieldName))));
            sqlBuilder.Append(") values(");
            sqlBuilder.Append(string.Join(", ", model.Fields.Select(m => string.Format("{0}{1}{0}", m.IsStringValue ? "'" : "", m.FieldValue.Replace("'", "''")))));
            sqlBuilder.Append(")");

            return sqlBuilder.ToString();
        }

        //update [table] set values where conditions
        private string GenerateUpdateSql(string tableName, object entity, string[] conditions, params string[] ignoreFields)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            SqlBuildModel model = CreateSqlBuldModel(tableName, entity, conditions, ignoreFields);
            if (model.Fields == null)
                return string.Empty;

            sqlBuilder.AppendFormat("update [{0}].[{1}] set ", model.Schema, model.TableName);
            sqlBuilder.Append(string.Join(", ", GenerateFieldStrings(model.Fields)));
            if (model.Conditions != null) 
            {
                sqlBuilder.AppendFormat(" where {0}", string.Join(" and ", GenerateFieldStrings(model.Conditions)));
            }
            return sqlBuilder.ToString();
        }

        //delete [table] where conditions
        private string GenerateDeleteSql(string tableName, object entity, string[] conditions) 
        {
            StringBuilder sqlBuilder = new StringBuilder();
            SqlBuildModel model = CreateSqlBuldModel(tableName, entity, conditions, null);
            sqlBuilder.AppendFormat("delete [{0}].[{1}] ", model.Schema, model.TableName);
            if (model.Conditions != null)
            {
                sqlBuilder.AppendFormat(" where {0}", string.Join(" and ", GenerateFieldStrings(model.Conditions)));
            }
            return sqlBuilder.ToString();
        }

        private string[] GenerateFieldStrings(IEnumerable<SqlColumn> columns, string joinSyblem="=")
        {
            return columns.Select(m => string.Format("[{0}] {3} {1}{2}{1}", m.FieldName, m.IsStringValue ? "'" : "", m.FieldValue.Replace("'", "''"), joinSyblem)).ToArray();
        }

        private SqlBuildModel CreateSqlBuldModel(string tableName, object entity, string[] whereFields, string[] ignoreFields)
        {
            SqlBuildModel model = new SqlBuildModel();
            if (tableName.IndexOf('.') != -1)
            {
                model.Schema = tableName.Split('.')[0];
                model.TableName = tableName.Split('.')[1];
            }
            else
            {
                model.TableName = tableName;
            }
            model.Fields = CreateSqlColumn(entity, ignoreFields);

            if (whereFields != null)
            {
                model.Conditions = model.Fields.Where(m => whereFields.Contains(m.FieldName));
            }
            return model;
        }

        private IEnumerable<SqlColumn> CreateSqlColumn(object entity, params string[] ignoreFields)
        {
            IList<SqlColumn> columns = new List<SqlColumn>();
            Type type = entity.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.CanRead && (ignoreFields == null || !ignoreFields.Contains(property.Name)))
                {
                    var sqlColumn = new SqlColumn();
                    sqlColumn.FieldName = property.Name;
                    sqlColumn.FieldValue = "null";
                    sqlColumn.IsStringValue = false;

                    object value = property.GetValue(entity);
                    if (value != null)
                    {
                        switch (Type.GetTypeCode(property.PropertyType))
                        {
                            case TypeCode.Boolean:
                                sqlColumn.IsStringValue = false;
                                sqlColumn.FieldValue = (bool)value ? "1" : "0";
                                break;
                            case TypeCode.String:
                            case TypeCode.Char:
                            case TypeCode.DateTime:
                                sqlColumn.IsStringValue = true;
                                sqlColumn.FieldValue = value.ToString();
                                break;
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                            case TypeCode.Int64:
                            case TypeCode.UInt16:
                            case TypeCode.UInt32:
                            case TypeCode.UInt64:
                            case TypeCode.Single:
                            case TypeCode.Double:
                            case TypeCode.Decimal:
                            case TypeCode.SByte:
                            case TypeCode.Byte:
                                sqlColumn.IsStringValue = false;
                                sqlColumn.FieldValue = value.ToString();
                                break;
                            case TypeCode.Object:
                            case TypeCode.DBNull:
                            case TypeCode.Empty:
                            default:
                                //ingore this type fields
                                continue;
                        }
                    }

                    columns.Add(sqlColumn);
                }
            }
            return columns;
        }
    }

    class SqlBuildModel
    {
        private string _schema = "dbo";
        public string Schema
        {
            get { return _schema; }
            set { _schema = value; }
        }

        public string TableName { get; set; }

        public IEnumerable<SqlColumn> Fields { get; set; }
        public IEnumerable<SqlColumn> Conditions { get; set; }
    }

    class SqlColumn
    {
        public string FieldName { get; set; }
        public bool IsStringValue { get; set; }
        public string FieldValue { get; set; }
    }
}
