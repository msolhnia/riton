using application.Contract.Api.Interface;
using application.Contract.Infrastructure;
using application.Settings;
using ClosedXML.Excel;
using domain.Entity;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Service
{
    public class TTMSService: ITTMSService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly ITTMSRepository _ttmsRepository;
        private readonly string _uploadsPath;
        private readonly int _chunkSize;

        public TTMSService(
            IBackgroundJobClient backgroundJobClient, 
            ITTMSRepository ttmsRepository,
            IConfiguration configuration,
            IOptions<HangFireSettings> options)
        {
            _backgroundJobClient = backgroundJobClient;
            _ttmsRepository = ttmsRepository;
            _uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(_uploadsPath);
            _chunkSize = options.Value.ChunkSize;
        }

        public async Task UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            var filePath = Path.Combine(_uploadsPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                DateTime uploadTime = DateTime.Now;
                Console.WriteLine($"File uploaded successfully at {uploadTime:hh:mm:ss tt}");
            }

            _backgroundJobClient.Enqueue(() => ProcessExcelFile(filePath));
        }
        public async Task ProcessExcelFile(string filePath)
        {
            DateTime processingStartTime = DateTime.Now;
            Console.WriteLine($"Processing started at {processingStartTime:hh:mm:ss tt}");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var rows = ReadExcelFile(filePath);           
            var chunks = SplitIntoChunks(rows, _chunkSize);

            foreach (var chunk in chunks)
            {
                _backgroundJobClient.Enqueue(() => ProcessChunkAsync(chunk));
            }
            stopwatch.Stop();
            var processingEndTime = DateTime.Now;
            Console.WriteLine($"Processing ended at {processingEndTime:hh:mm:ss tt}");
            Console.WriteLine($"Total time spent: {stopwatch.Elapsed.TotalSeconds} seconds");
        }

        public async Task ProcessChunkAsync(List<TTMS> chunk)
        {
            await _ttmsRepository.AddRangeAsync(chunk);
        }

        private List<TTMS> ReadExcelFile(string filePath)
        {
            var ttmsList = new List<TTMS>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);  
                var rowCount = worksheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= rowCount; row++)  
                {
                    var ttms = new TTMS
                    {
                        Sarjam = worksheet.Cell(row, 1).GetBoolean(),             
                        IsHagholAmalKari = worksheet.Cell(row, 2).GetBoolean(),
                        KalaType = worksheet.Cell(row, 3).GetValue<int?>(),
                        KalaKhadamatName = worksheet.Cell(row, 4).GetString(),
                        KalaCode = worksheet.Cell(row, 5).GetValue<int?>(),
                        BargashtType = worksheet.Cell(row, 6).GetBoolean(),
                        Price = worksheet.Cell(row, 7).GetValue<decimal?>(),
                        MaliatArzeshAfzoodeh = worksheet.Cell(row, 8).GetString(),
                        AvarezArzeshAfzoodeh = worksheet.Cell(row, 9).GetString(),
                        SayerAvarez = worksheet.Cell(row, 10).GetValue<decimal>(),
                        TakhfifPrice = worksheet.Cell(row, 11).GetValue<decimal>(),
                        HCKharidarTypeCode = worksheet.Cell(row, 12).GetValue<int?>(),
                        FactorNo = worksheet.Cell(row, 13).GetString(),
                        FactorDate = worksheet.Cell(row, 14).GetString(),
                        SanadNO = worksheet.Cell(row, 15).GetValue<long?>(),
                        SanadDate = worksheet.Cell(row, 16).GetString(),
                        CreateDate = DateTime.Now,  
                        IsDelete = false
                    };

                    ttmsList.Add(ttms);
                }
            }

            return ttmsList;
        }

        private IEnumerable<List<TTMS>> SplitIntoChunks(List<TTMS> rows, int chunkSize)
        {
            for (int i = 0; i < rows.Count; i += chunkSize)
            {
                yield return rows.Skip(i).Take(chunkSize).ToList();
            }
        }
    }
}
