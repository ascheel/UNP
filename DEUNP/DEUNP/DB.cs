using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace DEUNP
{
	class DB
	{
		private SqliteConnection db;
		public string filename = null;
		public string timestampFormat = null;
		public DB(string filename)
		{
			this.filename = filename;
			this.timestampFormat = "yyyy-MM-dd HH:mm:ss.fff";
		}

		~DB()
		{
			this.db.Close();
		}

		private void setDB()
		{
			var builder = new SqliteConnectionStringBuilder();
			builder.DataSource = this.filename;
			builder.Mode = SqliteOpenMode.ReadWriteCreate;

			this.db = new SqliteConnection(builder.ConnectionString);
			this.db.Open();
			this.prepDB();
		}

		private string toDateTimeString(DateTime dt)
		{
			//"YYYY-MM-DD HH:MM:SS.SSS"
			return dt.ToString(this.timestampFormat);
		}
		
		private DateTime fromDateTimeString(string dateTimeString)
		{
			return DateTime.ParseExact(dateTimeString, this.timestampFormat, new CultureInfo("en-US"));
		}

		private void prepDB()
		{
			string sql;
			using (var transaction = this.db.BeginTransaction())
			{
				sql = @"
				CREATE TABLE IF NOT EXISTS token (
					user TEXT,
					token TEXT UNIQUE,
					expires TIMESTAMP,
					created TIMESTAMP
				)";
			}
		}
	}
}
