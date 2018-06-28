using System.Collections.Generic;
using System.Configuration;
using Lte.Parameters.Entities.Dt;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace Lte.Parameters.Concrete
{
    //db.ExecuteCommand("UPDATE Products SET QuantityPerUnit = {0} WHERE ProductID = {1}", "24 boxes", 5);
    [Database(Name = "masterTest")]
    public class MasterTestContext : DataContext
    {
        private static readonly MappingSource _mappingSource = new AttributeMappingSource();

        public MasterTestContext()
            : base(
                ConfigurationManager.ConnectionStrings["MasterTest"].ConnectionString,
                _mappingSource)
        {

        }
        
        [Function(Name = "dbo.sp_get4GFileContents")]
        public ISingleResult<FileRecord4G> Get4GFileContents([Parameter(DbType = "varchar(max)")] string tableName)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), "[" + tableName + "]");
            return (ISingleResult<FileRecord4G>) result?.ReturnValue;
        }

        [Function(Name = "dbo.sp_getVolteFileContents")]
        public ISingleResult<FileRecordVolte> GetVolteFileContents([Parameter(DbType = "varchar(max)")] string tableName)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), "[" + tableName + "]");
            return (ISingleResult<FileRecordVolte>)result?.ReturnValue;
        }

        [Function(Name = "dbo.sp_get4GFileContentsRasterConsidered")]
        public ISingleResult<FileRecord4G> Get4GFileContents([Parameter(DbType = "varchar(max)")] string tableName,
            [Parameter(DbType = "Int")] int rasterNum)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), "[" + tableName + "]", rasterNum);
            return (ISingleResult<FileRecord4G>)result?.ReturnValue;
        }

        [Function(Name = "dbo.sp_getVolteFileContentsRasterConsidered")]
        public ISingleResult<FileRecordVolte> GetVolteFileContents([Parameter(DbType = "varchar(max)")] string tableName,
            [Parameter(DbType = "Int")] int rasterNum)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), "[" + tableName + "]", rasterNum);
            return (ISingleResult<FileRecordVolte>)result?.ReturnValue;
        }

        [Function(Name = "dbo.sp_get3GFileContents")]
        public ISingleResult<FileRecord3G> Get3GFileContents([Parameter(DbType = "varchar(max)")] string tableName)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), "[" + tableName + "]");
            return (ISingleResult<FileRecord3G>)result?.ReturnValue;
        }

        [Function(Name = "dbo.sp_get3GFileContentsRasterConsidered")]
        public ISingleResult<FileRecord3G> Get3GFileContents([Parameter(DbType = "varchar(max)")] string tableName,
            [Parameter(DbType = "Int")] int rasterNum)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), "[" + tableName + "]", rasterNum);
            return (ISingleResult<FileRecord3G>)result?.ReturnValue;
        }

        [Function(Name = "dbo.sp_get2GFileContents")]
        public ISingleResult<FileRecord2G> Get2GFileContents([Parameter(DbType = "varchar(max)")] string tableName)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo) (MethodBase.GetCurrentMethod())), "[" + tableName + "]");
            return (ISingleResult<FileRecord2G>)result?.ReturnValue;
        }

        [Function(Name = "dbo.sp_get2GFileContentsRasterConsidered")]
        public ISingleResult<FileRecord2G> Get2GFileContents([Parameter(DbType = "varchar(max)")] string tableName,
            [Parameter(DbType = "Int")] int rasterNum)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), "[" + tableName + "]", rasterNum);
            return (ISingleResult<FileRecord2G>)result?.ReturnValue;
        }

        public IEnumerable<string> GetTables()
        {
            return ExecuteQuery<string>("SELECT Name FROM SysObjects Where XType='U' ORDER BY Name");
        }
    }
}
