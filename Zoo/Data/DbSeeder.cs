using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Zoo.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(DbContext dbContext)
        {
            var conn = (SqlConnection)dbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            // ⛔ PROVJERA: ako već ima podataka, NE SEEDAJ OPET
            using (var checkCmd = new SqlCommand(
                "SELECT COUNT(1) FROM dbo.Vrsta WHERE hr_naziv = N'Lav';",
                conn))
            {
                var alreadySeeded = (int)await checkCmd.ExecuteScalarAsync() > 0;

                if (alreadySeeded)
                {
                    return;
                }
            }

            var sqlFilePath = Path.Combine(AppContext.BaseDirectory, "SeedData.sql");

            if (!File.Exists(sqlFilePath))
            {
                return;
            }

            var sql = await File.ReadAllTextAsync(sqlFilePath);

            var batches = SplitSqlBatches(sql);

            foreach (var batch in batches)
            {
                if (string.IsNullOrWhiteSpace(batch))
                {
                    continue;
                }

                using var cmd = new SqlCommand(batch, conn);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private static List<string> SplitSqlBatches(string sql)
        {
            var parts = sql.Split(
                new[] { "\r\nGO\r\n", "\nGO\n", "\r\nGO\n", "\nGO\r\n" },
                StringSplitOptions.RemoveEmptyEntries);

            return parts.ToList();
        }
    }
}
