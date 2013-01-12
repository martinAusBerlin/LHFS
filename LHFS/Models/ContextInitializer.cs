using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.IO;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Text;

namespace LHFS.Models {
	public class ContextInitializer : DropCreateDatabaseIfModelChanges<LHFSContext>  {

		protected override void Seed(LHFSContext context)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Users\martinM10\Documents\Visual Studio 2010\Projects\LHFS\LHFS\SQL");
			FileInfo[] fileInfos = directoryInfo.GetFiles("*.sql");

			StreamReader file;

			foreach(FileInfo info in fileInfos.Where(t => !t.Name.StartsWith("_")).OrderBy(t => t.Name)) {

				file = new StreamReader(info.FullName, Encoding.UTF8);
				string fileContent = file.ReadToEnd();

				context.Database.ExecuteSqlCommand(fileContent);
			}

			base.Seed(context);
		}

	}
}