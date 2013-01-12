using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace LHFS.Utils {
	public class FileHelper {

		/// <summary>
		/// Copies the contents of input to output. Doesn't close either stream.
		/// </summary>
		private static void CopyStream(Stream input, Stream output) {
			byte[] buffer = new byte[8 * 1024];
			int len;
			while ((len = input.Read(buffer, 0, buffer.Length)) > 0) {
				output.Write(buffer, 0, len);
			}
		}

		public static bool FileExists(string path, string fileName) {
			return System.IO.File.Exists(path + fileName);
		}

		public static void WriteFile(string path, string fileName, Stream fileStream) {

			if (!Directory.Exists(path)) {
				Directory.CreateDirectory(path);
			}

			using (Stream file = System.IO.File.OpenWrite(path + fileName)) {
				CopyStream(fileStream, file);
			}

		}
	}
}