using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;

namespace LHFS {
	/// <summary>
	/// Zusammenfassungsbeschreibung für ImageHandler
	/// </summary>
	public class ImageHandler : IHttpHandler {

		private Color backgroundcolor;
		private int scaleup, scaledown;

		public void ProcessRequest(HttpContext context) {

			byte[] bytes;

			if (context.Request.Url.PathAndQuery.Contains("Gallery")) {

				if (context.Request.Headers["If-Modified-Since"] != null) {
					context.Response.StatusCode = 304;
					context.Response.StatusDescription = "Not Modified";
					context.Response.End();
				}

				string cacheKey = context.Server.UrlDecode(context.Request.Url.PathAndQuery);
				if (context.Cache[cacheKey] == null) {
					MemoryStream stream = getImage(context);
					context.Cache.Insert(cacheKey, stream.ToArray(), null, DateTime.MaxValue, TimeSpan.FromDays(1));
				}

				bytes = (byte[])context.Cache[cacheKey];
				context.Response.Cache.SetCacheability(HttpCacheability.Public);
				context.Response.Cache.SetExpires(DateTime.Now.AddDays(1));
				context.Response.Cache.SetLastModified(DateTime.Now);
				
			} else {

				MemoryStream stream = getImage(context);
				bytes = stream.ToArray();
				context.Response.Cache.SetCacheability(HttpCacheability.Public);
				context.Response.Cache.SetExpires(DateTime.Now.AddSeconds(1));
				context.Response.Cache.SetLastModified(DateTime.Now);
			}

			context.Response.ContentType = "image/jpeg";
			context.Response.AppendHeader("content-length", bytes.Length.ToString());
			context.Response.BinaryWrite(bytes);
			context.Response.Flush();
			context.Response.End();
		}

		private MemoryStream getImage(HttpContext context) {


			System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
			
			int width, height, size;
			long quality = 66;
			bool clip = false, isUserContent = false;
			try { isUserContent = int.Parse(context.Request.QueryString["usercontent"]) == 1; } catch { isUserContent = false; }
			try { scaleup = int.Parse(context.Request.QueryString["scaleup"] + context.Request.QueryString["su"]); } catch { scaleup = 1; }
			try { scaledown = int.Parse(context.Request.QueryString["scaledown"] + context.Request.QueryString["sd"]); } catch { scaledown = 1; }
			try { width = int.Parse(context.Request.QueryString["width"] + context.Request.QueryString["w"]); } catch { width = 0; }
			try { height = int.Parse(context.Request.QueryString["height"] + context.Request.QueryString["h"]); } catch { height = 0; }
			try { size = int.Parse(context.Request.QueryString["size"] + context.Request.QueryString["s"]); } catch { size = 0; }
			try { quality = int.Parse(context.Request.QueryString["quality"] + context.Request.QueryString["q"]); } catch { }
			try { backgroundcolor = ColorTranslator.FromHtml("#" + context.Request.QueryString["backgroundcolor"] + context.Request.QueryString["bc"]); } catch { backgroundcolor = Color.Transparent; }
			try { clip = bool.Parse(context.Request.QueryString["clip"] + context.Request.QueryString["cl"]); } catch { }

			string file;
			
			if(isUserContent) {
				file = context.Request.MapPath("") + @"\" + ConfigurationManager.AppSettings["UserContentBasePath"] + @"\" + context.Request.QueryString["file"];
			} else {
				file = context.Request.MapPath("") + @"\" + ConfigurationManager.AppSettings["ImageBasePath"] + @"\" + context.Request.QueryString["file"];
			}
			
			ImageCodecInfo myImageCodecInfo;
			EncoderParameter myEncoderParameter;
			EncoderParameters myEncoderParameters;
			myImageCodecInfo = GetEncoderInfo("image/jpeg");
			myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
			myEncoderParameters = new EncoderParameters();
			myEncoderParameters.Param[0] = myEncoderParameter;

			MemoryStream stream = new MemoryStream();

			if (File.Exists(file)) {
				Bitmap b = new Bitmap(file);
				Bitmap thumb = GetThumbnail(b, width, height, size, clip);
				b.Dispose();
				
				thumb.Save(stream, myImageCodecInfo, myEncoderParameters);
				thumb.Dispose();
			}

			return stream;

		}

		private Bitmap GetThumbnail(Bitmap b, int maxw, int maxh, int s, bool clip) {
			int w = 0, h = 0;
			double maxratio = 0, ratio = 1;
			int clipW = 0, clipH = 0;

			if (maxw != 0 && maxh != 0 && !clip)
				maxratio = Math.Min(((float)maxw) / b.Size.Width, ((float)maxh) / b.Size.Height);
			else if (maxw != 0 && maxh != 0 && clip)
				maxratio = Math.Max(((float)maxw) / b.Size.Width, ((float)maxh) / b.Size.Height);
			else if (maxw != 0)
				maxratio = ((float)maxw) / b.Size.Width;
			else if (maxh != 0)
				maxratio = ((float)maxh) / b.Size.Height;

			//	ratio = maxratio;

			if (s != 0)
				ratio = Math.Sqrt(((float)s) / b.Size.Width / b.Size.Height);

			if (maxratio != 0 && ratio > maxratio)
				ratio = maxratio;

			if (scaleup != 1 || scaledown != 1)
				ratio = 1 * (float)scaleup / (float)scaledown;

			if (maxw > 0 && clip && (int)(b.Size.Width * ratio) > maxw)
				clipW = (int)(b.Size.Width * ratio - maxw);
			if (maxh > 0 && clip && (int)(b.Size.Height * ratio) > maxh)
				clipH = (int)(b.Size.Height * ratio - maxh);

			w = (int)(b.Size.Width * ratio);
			h = (int)(b.Size.Height * ratio);

			Bitmap bmpTarget = new Bitmap(w - clipW, h - clipH);

			Graphics grfxThumb = Graphics.FromImage(bmpTarget);

			if (backgroundcolor != Color.Transparent)
				grfxThumb.Clear(backgroundcolor);

			// Set the interpolation mode
			// Bicubic				: Specifies bicubic interpolation
			// Bilinear				: Specifies bilinear interpolation
			// Default				: Specifies default mode
			// High					: Specifies high quality interpolation
			// HighQualityBicubic	: Specifies high quality bicubic interpolation ****************** THIS IS THE HIGHEST QUALITY
			// HighQualityBilinear	: Specifies high quality bilinear interpolation 
			// Invalid				: Equivalent to the Invalid element of the QualityMode enumeration
			// Low					: Specifies low quality interpolation
			// NearestNeighbor		: Specifies nearest-neighbor interpolation
			grfxThumb.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

			grfxThumb.DrawImage(b, new Rectangle(-clipW / 2, -clipH / 2, w, h));

			// Save the new image
			return (bmpTarget);

			//return((Bitmap) b.GetThumbnailImage(w, h, null, IntPtr.Zero));
		}

		private static ImageCodecInfo GetEncoderInfo(String mimeType) {

			int j;

			ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

			for (j = 0; j < encoders.Length; ++j) {
				if (encoders[j].MimeType == mimeType)  {
					return encoders[j];
				}
			}

			return null;
		}

		public bool IsReusable {
			get {
				return false;
			}
		}
	}
}