using RazorEngine;
using RazorEngine.Templating;
using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.CodeGengeration
{
    class SqlGengerateSample : Executor
    {
        Person person = new Person { Name = "小李", Age = 28, Gender = Gender.Female, PersonWebsiteUrl = "http://zzk.cnblogs.com/s?w=GetTypeCode+enum&t='b" };

        [TestMethod]
        public void Test_Generate_Insert_Sql()
        {
            string sql = SqlGengerator.BuildInsertSql(person, "Persons", new string[] { "CreatedBy" });
            Console.WriteLine(sql);
        }

        [TestMethod]
        public void Test_Generate_Update_Sql()
        {
            string sql = SqlGengerator.BuildUpdateSql(person, "Persons", new string[] { "Name", "Age" }, new string[] { "Name", "Age" });
            Console.WriteLine(sql);
        }

        [TestMethod]
        public void Test_Generate_Upsert_Sql()
        {
            string sql = SqlGengerator.BuildUpsertSql(person, "Persons", new string[] { "Name", "Age" }, new string[] { "Name", "Age" });
            Console.WriteLine(sql);
        }

        [TestMethod]
        public void Test_Generate_Delete_Sql()
        {
            string sql = SqlGengerator.BuildDeletedSql(person, "Persons", new string[] { "Name", "Age" });
            Console.WriteLine(sql);
        }
    }

    /// <summary>
    /// Sample sql script gengeration
    /// </summary>
    static class SqlGengerator 
    {
        //insert into [table](ColumnNames) values(ColumnValues) 
        public static string BuildInsertSql(object entity, string tableName, string[] ignoreFields) 
        {
            string template = @"insert into @Model.TableName (@Model.InsertNameString) values (@Raw(Model.InsertValueString))";
            SqlBuildViewModel model = new SqlBuildViewModel(entity, tableName, null, ignoreFields);
            return Engine.Razor.RunCompile(template, "insert_template", null, model);
        }

        //update [table] set values where [conditions]
        public static string BuildUpdateSql(object entity, string tableName, string[] findFields, string[] ignoreFields)
        {
            string template = @"update @Model.TableName set @Raw(Model.UpdateString) where @Raw(Model.WhereString)";
            SqlBuildViewModel model = new SqlBuildViewModel(entity, tableName, findFields, ignoreFields);
            return Engine.Razor.RunCompile(template, "update_template", null, model);
        }

        //if exists(select 1 from [table] where [conditions])
        //begin
        //  update [table] set values where conditions
        //end
        //else
        //  insert into [table](ColumnNames) values(ColumnValues) 
        //end
        public static string BuildUpsertSql(object entity, string tableName, string[] findFields, string[] ignoreFields)
        {
            string template =@"
if exists(select 1 from @Model.TableName where  @Raw(Model.WhereString))
begin
    update @Model.TableName set @Raw(Model.UpdateString) where @Raw(Model.WhereString)
end
else
    insert into @Model.TableName (@Model.InsertNameString) values(@Raw(Model.InsertValueString))
end
";
            SqlBuildViewModel model = new SqlBuildViewModel(entity, tableName, findFields, ignoreFields);
            return Engine.Razor.RunCompile(template, "upsert_template", null, model);
        }

        //delete [table] where [conditions]
        public static string BuildDeletedSql(object entity, string tableName, string[] findFields)
        {
            string template = @"delete @Model.TableName where @Raw(Model.WhereString)";
            SqlBuildViewModel model = new SqlBuildViewModel(entity, tableName, findFields, null);
            return Engine.Razor.RunCompile(template, "delete_template", null, model);
        }

    }

    public class SqlBuildViewModel
    {
        object entity;
        string tableName;
        string[] findFields;
        string[] ignoreFields;
        List<dynamic> entityFields;

        public SqlBuildViewModel(object entity, string tableName, string[] findFields, string[] ignoreFields)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            this.entity = entity;
            this.tableName = tableName;
            this.findFields = findFields;
            this.ignoreFields = ignoreFields;
            this.entityFields = new List<dynamic>();

            InitTableName();
            InitFileds();
        }

        public string TableName { get; private set; }
        public string InsertNameString
        {
            get
            {
                return string.Join(", ", entityFields.Where(m => !m.IsIgnored).Select(m => string.Format("[{0}]", m.FieldName)).ToArray());
            }
        }
        public string InsertValueString
        {
            get
            {
                return string.Join(", ", entityFields.Where(m => !m.IsIgnored).Select(m => string.Format("{0}{1}{0}", m.IsStringValue ? "'" : "", m.FieldValue.Replace("'", "''"))).ToArray());
            }
        }
        public string UpdateString
        {
            get
            {
                return string.Join(", ", entityFields.Where(m => !m.IsIgnored).Select(m => string.Format("[{0}] {3} {1}{2}{1}", m.FieldName, m.IsStringValue ? "'" : "", m.FieldValue.Replace("'", "''"), "=")).ToArray());
            }
        }
        public string WhereString
        {
            get
            {
                return string.Join(" and ", entityFields.Where(m => m.IsFindField).Select(m => string.Format("[{0}] {3} {1}{2}{1}", m.FieldName, m.IsStringValue ? "'" : "", m.FieldValue.Replace("'", "''"), "=")).ToArray());
            }
        }

        private void InitTableName()
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                this.TableName = string.Format("[dbo].[{0}]", entity.GetType().Name);
            }
            else
            {
                if (tableName.IndexOf('.') != -1)
                {
                    this.TableName = string.Format("[{0}].[{1}]", tableName.Split('.')[0], tableName.Split('.')[1]);
                }
                else
                {
                    this.TableName = string.Format("[dbo].[{0}]", tableName);
                }
            }
        }

        private void InitFileds()
        {
            Type type = entity.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    dynamic column = new ExpandoObject();
                    column.FieldName = property.Name;
                    column.FieldValue = "null";
                    column.IsStringValue = false;
                    column.IsIgnored = ignoreFields != null && ignoreFields.Contains(property.Name);
                    column.IsFindField = findFields != null && findFields.Contains(property.Name);

                    object value = property.GetValue(entity);
                    switch (Type.GetTypeCode(property.PropertyType))
                    {
                        case TypeCode.Boolean:
                            if (value != null) column.FieldValue = (bool)value ? "1" : "0";
                            break;
                        case TypeCode.String:
                        case TypeCode.Char:
                        case TypeCode.DateTime:
                            if (value != null)
                            {
                                column.IsStringValue = true;
                                column.FieldValue = value.ToString();
                            }
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
                            if (value != null) column.FieldValue = property.PropertyType.IsEnum ? ((int)value).ToString() : value.ToString();
                            break;
                        case TypeCode.Object:
                        case TypeCode.DBNull:
                        case TypeCode.Empty:
                        default:
                            //ingore this type fields
                            continue;
                    }
                    this.entityFields.Add(column);
                }
            }
        }
    }
}
