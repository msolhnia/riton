using application.Contract.Infrastructure;
using domain.Entity;
using infrastructure.Data.Persist;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Data.Repository
{
    public class TTMSRepository: ITTMSRepository
    {
        private readonly ritonContext _context;

        public TTMSRepository(ritonContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<TTMS> rows)
        {
            var dataTable = ConvertToDataTable(rows);

            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                await connection.OpenAsync();

                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "TTMS";  

                   
                    bulkCopy.ColumnMappings.Add("Sarjam", "Sarjam");
                    bulkCopy.ColumnMappings.Add("IsHagholAmalKari", "IsHagholAmalKari");
                    bulkCopy.ColumnMappings.Add("KalaType", "KalaType");
                    bulkCopy.ColumnMappings.Add("KalaKhadamatName", "KalaKhadamatName");
                    bulkCopy.ColumnMappings.Add("KalaCode", "KalaCode");
                    bulkCopy.ColumnMappings.Add("BargashtType", "BargashtType");
                    bulkCopy.ColumnMappings.Add("Price", "Price");
                    bulkCopy.ColumnMappings.Add("MaliatArzeshAfzoodeh", "MaliatArzeshAfzoodeh");
                    bulkCopy.ColumnMappings.Add("AvarezArzeshAfzoodeh", "AvarezArzeshAfzoodeh");
                    bulkCopy.ColumnMappings.Add("SayerAvarez", "SayerAvarez");
                    bulkCopy.ColumnMappings.Add("TakhfifPrice", "TakhfifPrice");
                    bulkCopy.ColumnMappings.Add("HCKharidarTypeCode", "HCKharidarTypeCode");
                    bulkCopy.ColumnMappings.Add("FactorNo", "FactorNo");
                    bulkCopy.ColumnMappings.Add("FactorDate", "FactorDate");
                    bulkCopy.ColumnMappings.Add("SanadNO", "SanadNO");
                    bulkCopy.ColumnMappings.Add("SanadDate", "SanadDate");
                    bulkCopy.ColumnMappings.Add("CreateDate", "CreateDate");
                    bulkCopy.ColumnMappings.Add("CreateBy", "CreateBy");
                    bulkCopy.ColumnMappings.Add("IsDelete", "IsDelete");

                    await bulkCopy.WriteToServerAsync(dataTable);
                }
            }
        }

        private DataTable ConvertToDataTable(IEnumerable<TTMS> rows)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Sarjam", typeof(bool));
            dataTable.Columns.Add("IsHagholAmalKari", typeof(bool));
            dataTable.Columns.Add("KalaType", typeof(int));
            dataTable.Columns.Add("KalaKhadamatName", typeof(string));
            dataTable.Columns.Add("KalaCode", typeof(int));
            dataTable.Columns.Add("BargashtType", typeof(bool));
            dataTable.Columns.Add("Price", typeof(decimal));
            dataTable.Columns.Add("MaliatArzeshAfzoodeh", typeof(string));
            dataTable.Columns.Add("AvarezArzeshAfzoodeh", typeof(string));
            dataTable.Columns.Add("SayerAvarez", typeof(decimal));
            dataTable.Columns.Add("TakhfifPrice", typeof(decimal));
            dataTable.Columns.Add("HCKharidarTypeCode", typeof(int));
            dataTable.Columns.Add("FactorNo", typeof(string));
            dataTable.Columns.Add("FactorDate", typeof(string));
            dataTable.Columns.Add("SanadNO", typeof(long));
            dataTable.Columns.Add("SanadDate", typeof(string));
            dataTable.Columns.Add("CreateDate", typeof(DateTime));
            dataTable.Columns.Add("CreateBy", typeof(string));
            dataTable.Columns.Add("IsDelete", typeof(bool));

            foreach (var row in rows)
            {
                dataTable.Rows.Add(
                    row.Sarjam,
                    row.IsHagholAmalKari,
                    row.KalaType,
                    row.KalaKhadamatName,
                    row.KalaCode,
                    row.BargashtType,
                    row.Price,
                    row.MaliatArzeshAfzoodeh,
                    row.AvarezArzeshAfzoodeh,
                    row.SayerAvarez,
                    row.TakhfifPrice,
                    row.HCKharidarTypeCode,
                    row.FactorNo,
                    row.FactorDate,
                    row.SanadNO,
                    row.SanadDate,
                    row.CreateDate,
                    row.CreateBy,
                    row.IsDelete
                );
            }

            return dataTable;
        }
        //public async Task AddRangeAsync(IEnumerable<TTMS> rows)
        //{
        //    await _context.TTMS.AddRangeAsync(rows);
        //    await _context.SaveChangesAsync();
        //}
    }
}
