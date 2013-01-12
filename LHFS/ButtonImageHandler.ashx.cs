using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Web.Caching;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.IO;

namespace LHFS {
	/// <summary>
	/// Zusammenfassungsbeschreibung für ButtonImageHandler
	/// </summary>
	/// 
	public class ApplicationFont {
		PrivateFontCollection _privatefontcollection;

		public FontFamily Family {
			get { return _privatefontcollection == null ? FontFamily.GenericMonospace : (FontFamily)_privatefontcollection.Families[0]; }
		}

		public ApplicationFont(string filename) {
			try {
				if (HttpContext.Current.Application["ApplicationFont[" + filename + "]"] == null) {
					PrivateFontCollection privatefontcollection = new PrivateFontCollection();
					privatefontcollection.AddFontFile(HttpContext.Current.Server.MapPath(filename));
					HttpContext.Current.Application.Lock();
					HttpContext.Current.Application.Add("ApplicationFont[" + filename + "]", privatefontcollection);
					HttpContext.Current.Application.UnLock();
					_privatefontcollection = privatefontcollection;
				} else
					_privatefontcollection = (PrivateFontCollection)HttpContext.Current.Application["ApplicationFont[" + filename + "]"];
			} catch {
				_privatefontcollection = null;
			}
		}

		public ApplicationFont(string filename, bool create) {
			try {
				if (HttpContext.Current.Application["ApplicationFont[" + filename + "]"] == null || create) {
					PrivateFontCollection privatefontcollection = new PrivateFontCollection();
					privatefontcollection.AddFontFile(HttpContext.Current.Server.MapPath(filename));
					HttpContext.Current.Application.Lock();
					if (HttpContext.Current.Application["ApplicationFont[" + filename + "]"] != null)
						HttpContext.Current.Application.Remove("ApplicationFont[" + filename + "]");
					HttpContext.Current.Application.Add("ApplicationFont[" + filename + "]", privatefontcollection);
					HttpContext.Current.Application.UnLock();
					_privatefontcollection = privatefontcollection;
				} else
					_privatefontcollection = (PrivateFontCollection)HttpContext.Current.Application["ApplicationFont[" + filename + "]"];
			} catch {
				_privatefontcollection = null;
			}
		}
	}

	public class PrivateFontFamily {

		public static FontFamily FromTrueType(string filename, HttpContext context) {
			string filepath = context.Request.PhysicalApplicationPath + string.Format(ConfigurationManager.AppSettings["FontsPath"] + @"\" + filename.Replace("..", ""));

			if (context.Cache[filepath] == null) {
				PrivateFontCollection privatefontcollection = new PrivateFontCollection();
				privatefontcollection.AddFontFile(filepath);
				context.Cache.Insert(filepath, privatefontcollection, new CacheDependency(filepath));
				return privatefontcollection.Families[0];
			} else
				return ((PrivateFontCollection)context.Cache[filepath]).Families[0];

		}
	}

	public class ButtonImageHandler : IHttpHandler {

		private string text, fontfile, fontstyle, horizontalalignment, verticalalignment, overflow;
		private Color color, frombackgroundcolor, tobackgroundcolor, bordercolor, glowcolor, shadowcolor;
		private short fontsize, width, height, textWidth, textHeight, padding, horizontalpadding, verticalpadding, borderwidth, glowwidth, shadowwidth, shadowX, shadowY;
		private short colors;
		private bool transparency, backgroundtransparency;
		private const int maxComplexity = 500000;



		public bool IsReusable {
			get {
				return false;
			}
		}


		public void ProcessRequest(HttpContext context) {
			if (context.Request.Headers["If-Modified-Since"] != null) {
				context.Response.StatusCode = 304;
				context.Response.StatusDescription = "Not Modified";
				context.Response.End();
			}

			try {
				if (context.Request.UrlReferrer != null && context.Request.UrlReferrer.Host != context.Request.Url.Host) {
					context.Response.ContentType = "image/gif";
					context.Response.BinaryWrite(new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61, 0x01, 0x00, 0x01, 0x00, 0x91, 0x02, 0x00, 0xFF, 0xFF, 0xFF, 0x0E, 0x0E, 0x0E, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x21, 0xFF, 0x0B, 0x4E, 0x45, 0x54, 0x53, 0x43, 0x41, 0x50, 0x45, 0x32, 0x2E, 0x30, 0x03, 0x01, 0x00, 0x00, 0x00, 0x21, 0xF9, 0x04, 0x05, 0x32, 0x00, 0x02, 0x00, 0x2C, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x02, 0x02, 0x4C, 0x01, 0x00, 0x21, 0xF9, 0x04, 0x05, 0x32, 0x00, 0x02, 0x00, 0x2C, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x02, 0x02, 0x44, 0x01, 0x00, 0x3B });
					context.Response.End();
				}
			} catch { }

			string cacheKey = context.Server.UrlDecode(context.Request.Url.PathAndQuery);
			if (context.Cache[cacheKey] == null)
				cacheImage(cacheKey, context);

			byte[] bytes = (byte[])context.Cache[cacheKey];
			context.Response.Cache.SetCacheability(HttpCacheability.Public);
			context.Response.Cache.SetExpires(DateTime.Now.AddYears(1));
			context.Response.Cache.SetLastModified(DateTime.Now);
			context.Response.ContentType = "image/png";
			context.Response.AppendHeader("content-length", bytes.Length.ToString());
			context.Response.BinaryWrite(bytes);
			context.Response.Flush();
			context.Response.End();
		}

		private void cacheImage(string cacheKey, HttpContext context) {
			text = context.Request.QueryString["text"] + context.Request.QueryString["x"] + (context.Request.PathInfo.Length <= 1 ? string.Empty : context.Request.PathInfo.Substring(1));
			if (text == string.Empty)
				text = "text=?";
			text = text.Replace("{newline}", "\n").Replace("{colon}", ":").Replace("{amp}", "&");

			fontfile = context.Request.QueryString["fontfile"] + context.Request.QueryString["font"] + context.Request.QueryString["f"];
			if (fontfile == string.Empty)
				fontfile = "arial.ttf";

			try { color = ColorTranslator.FromHtml("#" + context.Request.QueryString["color"] + context.Request.QueryString["c"]); } catch { color = Color.FromArgb(238, 0, 0, 0); }

			try { frombackgroundcolor = ColorTranslator.FromHtml("#" + context.Request.QueryString["frombackgroundcolor"] + context.Request.QueryString["fbc"]); } catch { frombackgroundcolor = Color.FromArgb(255, 224, 223, 227); }
			try { tobackgroundcolor = ColorTranslator.FromHtml("#" + context.Request.QueryString["tobackgroundcolor"] + context.Request.QueryString["tbc"]); } catch { tobackgroundcolor = Color.FromArgb(255, 207, 206, 210); }

			try { bordercolor = ColorTranslator.FromHtml("#" + context.Request.QueryString["bordercolor"]); } catch { bordercolor = Color.White; }

			try { borderwidth = short.Parse(context.Request.QueryString["borderwidth"]); } catch { if (context.Request.QueryString["bordercolor"] != null) borderwidth = 1; }

			try { glowcolor = ColorTranslator.FromHtml("#" + context.Request.QueryString["glowcolor"]); } catch { glowcolor = Color.White; }

			try { glowwidth = short.Parse(context.Request.QueryString["glowwidth"]); } catch { if (context.Request.QueryString["glowcolor"] != null) glowwidth = 1; }

			try { shadowcolor = ColorTranslator.FromHtml("#" + context.Request.QueryString["shadowcolor"]); } catch { shadowcolor = Color.White; }

			try { shadowwidth = short.Parse(context.Request.QueryString["shadowwidth"]); } catch { if (context.Request.QueryString["shadowcolor"] != null) shadowwidth = 1; }

			try { shadowX = short.Parse(context.Request.QueryString["shadowX"]); } catch { if (context.Request.QueryString["shadowcolor"] != null) shadowX = 1; }

			try { shadowY = short.Parse(context.Request.QueryString["shadowY"]); } catch { if (context.Request.QueryString["shadowcolor"] != null) shadowY = 1; }


			try { fontsize = short.Parse(context.Request.QueryString["fontsize"] + context.Request.QueryString["size"] + context.Request.QueryString["s"]); } catch { fontsize = 13; }
			if (fontsize < 1 || fontsize > 512)
				fontsize = 12;

			int maxLength = 1 + maxComplexity / (int)Math.Pow(fontsize, 2);
			if (text.Length > maxLength)
				text = text.Substring(0, maxLength);

			try { width = short.Parse(context.Request.QueryString["width"] + context.Request.QueryString["w"]); } catch { }

			try { height = short.Parse(context.Request.QueryString["height"] + context.Request.QueryString["h"]); } catch { }

			try { padding = short.Parse(context.Request.QueryString["padding"] + context.Request.QueryString["p"]); } catch { padding = 4; }
			if (padding < 0)
				padding = 0;

			try { horizontalpadding = short.Parse(context.Request.QueryString["horizontalpadding"] + context.Request.QueryString["hp"]); } catch { horizontalpadding = 10; }
			if (horizontalpadding < 0)
				horizontalpadding = 0;

			try { verticalpadding = short.Parse(context.Request.QueryString["verticalpadding"] + context.Request.QueryString["vp"]); } catch { }
			if (verticalpadding < 0)
				verticalpadding = 0;

			try { colors = short.Parse(context.Request.QueryString["colors"] + context.Request.QueryString["cs"]); } catch { colors = 16; }
			if (colors < 2 || colors > 256)
				colors = 16;

			fontstyle = context.Request.QueryString["fontstyle"] + context.Request.QueryString["style"];

			overflow = context.Request.QueryString["overflow"] + context.Request.QueryString["o"];

			transparency = context.Request.QueryString["transparency"] + context.Request.QueryString["t"] == "true" || context.Request.QueryString["transparency"] + context.Request.QueryString["t"] == "yes" || context.Request.QueryString["transparency"] + context.Request.QueryString["t"] == "1";
			backgroundtransparency = context.Request.QueryString["backgroundtransparency"] + context.Request.QueryString["bt"] != "false" && context.Request.QueryString["backgroundtransparency"] + context.Request.QueryString["bt"] != "no" && context.Request.QueryString["backgroundtransparency"] + context.Request.QueryString["bt"] != "0";

			horizontalalignment = context.Request.QueryString["horizontalalignment"] + context.Request.QueryString["alignment"] + context.Request.QueryString["textalign"] + context.Request.QueryString["ha"] + context.Request.QueryString["ta"];
			verticalalignment = context.Request.QueryString["verticalalignment"] + context.Request.QueryString["verticalalign"] + context.Request.QueryString["va"];

			StringFormat stringformat = StringFormat.GenericTypographic;
			stringformat.Alignment = horizontalalignment == "left" || horizontalalignment == "l" ? StringAlignment.Near : horizontalalignment == "right" || horizontalalignment == "r" ? StringAlignment.Far : StringAlignment.Center;
			stringformat.LineAlignment = verticalalignment == "top" || verticalalignment == "t" ? StringAlignment.Near : verticalalignment == "bottom" || verticalalignment == "b" ? StringAlignment.Far : StringAlignment.Center;
			stringformat.Trimming = overflow == "ellipsis" || overflow == "e" || overflow == "ellipsischaracter" || overflow == "ec" ? StringTrimming.EllipsisCharacter : overflow == "ellipsisword" || overflow == "ew" ? StringTrimming.EllipsisWord : overflow == "character" || overflow == "c" ? StringTrimming.Character : overflow == "word" || overflow == "w" ? StringTrimming.Word : StringTrimming.None;

			FontFamily fontfamily = fontfile.Equals(string.Empty) || fontfile.IndexOf(".ttf") > 0 ? PrivateFontFamily.FromTrueType(fontfile, context) : new FontFamily(fontfile);

			//FontFamily fontfamily = new ApplicationFont(fontfile).Family;

			int factor = 2;
			Font font = new Font(fontfamily, fontsize * factor, FontStyle.Regular | (fontstyle.IndexOf("bold") >= 0 ? FontStyle.Bold : FontStyle.Regular) | (fontstyle.IndexOf("italic") >= 0 ? FontStyle.Italic : FontStyle.Regular) | (fontstyle.IndexOf("underline") >= 0 ? FontStyle.Underline : FontStyle.Regular), GraphicsUnit.Pixel);

			Bitmap tempbitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
			Graphics tempgraphics = Graphics.FromImage(tempbitmap);


			SizeF textSizeF = tempgraphics.MeasureString(text, font, width > 1 ? width * factor : 32767, stringformat);
			textWidth = (short)(width > 0 ? width * factor : textSizeF.Width);
			textHeight = (short)(height > 0 ? height * factor : textSizeF.Height);

			if (textWidth % factor > 0)
				textWidth += (short)(factor - textWidth % factor);
			if (textHeight % factor > 0)
				textHeight += (short)(factor - textHeight % factor);

			tempgraphics.Dispose();
			tempbitmap.Dispose();

			int w = textWidth / factor + padding * 2 + horizontalpadding * 2, h = textHeight / factor + padding * 2 + verticalpadding * 2;

			Bitmap bitmap = new Bitmap(w * factor, h * factor, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bitmap);

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = context.Request.QueryString["hinting"] == "1" || context.Request.QueryString["hinting"] == "true" ? TextRenderingHint.AntiAliasGridFit : TextRenderingHint.AntiAlias;
			graphics.TextContrast = 0;


			//			graphics.Clear(Color.White);

			graphics.DrawString(text, font, new SolidBrush(shadowcolor), new Rectangle(padding * factor + horizontalpadding * factor + factor - 1, padding * factor + verticalpadding * factor + factor - 3, textWidth + 2, textHeight + 2), stringformat);
			graphics.DrawString(text, font, new SolidBrush(color), new Rectangle(padding * factor + horizontalpadding * factor - 1, padding * factor + verticalpadding * factor - 3, textWidth + 2, textHeight + 2), stringformat);

			//			graphics.DrawLine(new SolidBrush(Color.FromArgb(100, 0, 0, 0)), 8, 8, textWidth + padding * 8 + horizontalpadding * 8, textHeight + padding * 8 + verticalpadding * 8);
			//			graphics.FillRectangle(new SolidBrush(Color.FromArgb(127, 0, 0, 0)), 8, 8, textWidth + padding * 8 + horizontalpadding * 8, textHeight + padding * 8 + verticalpadding * 8);
			//graphics.FillRectangle(new SolidBrush(Color.FromArgb(191, 0, 0, 0)), 4, 4, textWidth + padding * 8 + horizontalpadding * 8, textHeight + padding * 8 + verticalpadding * 8);
			//			graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 0, 0, 0)), 4, 4, textWidth + padding * 8 + horizontalpadding * 8, textHeight + padding * 8 + verticalpadding * 8);
			//			graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 145, 211)), 0, 0, textWidth + padding * 8 + horizontalpadding * 8, textHeight + padding * 8 + verticalpadding * 8);

			/*			graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 145, 211)), 16, 0, textWidth + padding * 8 + horizontalpadding * 8 - 32, textHeight + padding * 8 + verticalpadding * 8);
						graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 145, 211)), 0, 16, textWidth + padding * 8 + horizontalpadding * 8, textHeight + padding * 8 + verticalpadding * 8 - 32);
						graphics.FillPie(new SolidBrush(Color.FromArgb(255, 0, 145, 211)), 0, 0, 32, 32, 180, 90);
						graphics.FillPie(new SolidBrush(Color.FromArgb(255, 0, 145, 211)), w - 32, 0, 32, 32, 270, 90);
						graphics.FillPie(new SolidBrush(Color.FromArgb(255, 0, 145, 211)), 0, h - 32, 32, 32, 90, 90);
						graphics.FillPie(new SolidBrush(Color.FromArgb(255, 0, 145, 211)), w - 32, h - 32, 32, 32, 0, 90);
						graphics.DrawArc(new Pen(Color.FromArgb(136, 255, 255, 255), 4), 2, 2, 28, 28, 180, 90);
						graphics.DrawArc(new Pen(Color.FromArgb(136, 0, 0, 0), 4), w - 30, h - 30, 28, 28, 0, 90);
						graphics.DrawLine(new Pen(Color.FromArgb(102, 255, 255, 255), 4), 16, 2, w - 16, 2);
						graphics.DrawLine(new Pen(Color.FromArgb(102, 0, 0, 0), 4), 16, h - 2, w - 16, h - 2);
						graphics.DrawLine(new Pen(Color.FromArgb(102, 255, 255, 255), 4), 2, 16, 2, h - 16);
						graphics.DrawLine(new Pen(Color.FromArgb(102, 0, 0, 0), 4), w - 2, 16, w - 2, h - 16);
			*/
			//			graphics.DrawString (text, font, new SolidBrush(Color.FromArgb(255, 0, 145, 211)), new Rectangle(padding * factor + horizontalpadding * factor, padding * factor + verticalpadding * factor, textWidth + 2, textHeight + 2), stringformat);

			fontfamily.Dispose();

			graphics.Flush();
			graphics.Dispose();

			Bitmap targetbitmap = new Bitmap(w, h, PixelFormat.Format32bppArgb);
			Graphics targetgraphics = Graphics.FromImage(targetbitmap);

			targetgraphics.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(0, h), frombackgroundcolor, tobackgroundcolor), 0, 0, w, h);
			targetgraphics.DrawLine(new Pen(Color.FromArgb(102, 255, 255, 255), 1), 0, 0, w - 1, 0);
			targetgraphics.DrawLine(new Pen(Color.FromArgb(102, 0, 0, 0), 1), 0, h - 1, w - 1, h - 1);
			targetgraphics.DrawLine(new Pen(Color.FromArgb(102, 255, 255, 255), 1), 0, 0, 0, h - 1);
			targetgraphics.DrawLine(new Pen(Color.FromArgb(102, 0, 0, 0), 1), w - 1, 0, w - 1, h - 1);

			//			Pen p = new Pen(Color.FromArgb(170, bordercolor.R, bordercolor.G, bordercolor.B), borderwidth);
			Pen p = new Pen(new LinearGradientBrush(new Point(0, 0), new Point(0, h), Color.FromArgb(102, bordercolor.R, bordercolor.G, bordercolor.B), Color.FromArgb(204, bordercolor.R, bordercolor.G, bordercolor.B)), borderwidth * 2);
			//			p.Alignment = PenAlignment.Inset;
			if (borderwidth > 0)
				targetgraphics.DrawRectangle(p, 0, 0, w, h);

			targetgraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			targetgraphics.DrawImage(bitmap, new Rectangle(0, 0, w, h));
			targetgraphics.Flush();
			targetgraphics.Dispose();
			//			bitmap.Dispose();

			if (!context.Response.IsClientConnected)
				context.Response.End();

			//			tempbitmap = ColorIndexedBitmap(targetbitmap, colors, color, backgroundcolor, transparency, backgroundtransparency);

			context.Response.ContentType = "image/png";

			MemoryStream stream = new MemoryStream();
			targetbitmap.Save(stream, ImageFormat.Png);

			targetbitmap.Dispose();

			//			context.Response.ContentType = "image/gif";
			//			tempbitmap.Save(context.Response.OutputStream, ImageFormat.Gif);

			tempbitmap.Dispose();

			context.Cache.Insert(cacheKey, stream.ToArray(), null, DateTime.MaxValue, TimeSpan.FromDays(1));
		}

		protected ColorPalette GetColorPalette(short colors) {
			PixelFormat pixelformat;
			ColorPalette palette;
			Bitmap bitmap;

			pixelformat = colors > 16 ? PixelFormat.Format8bppIndexed : colors > 2 ? pixelformat = PixelFormat.Format4bppIndexed : PixelFormat.Format8bppIndexed;
			bitmap = new Bitmap(1, 1, pixelformat);
			palette = bitmap.Palette;

			bitmap.Dispose();

			return palette;
		}

		protected Bitmap ColorIndexedBitmap(Bitmap sourcebitmap, short colors, Color color, Color backgroundcolor, bool transparency, bool backgroundtransparency) {
			short Width = (short)sourcebitmap.Width;
			short Height = (short)sourcebitmap.Height;

			Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format8bppIndexed);

			ColorPalette palette = GetColorPalette(colors);

			for (short index = 0; index < colors; index++) {
				palette.Entries[index] = Color.FromArgb(255,
					backgroundcolor.R + (color.R - backgroundcolor.R) * index / (colors - 1),
					backgroundcolor.G + (color.G - backgroundcolor.G) * index / (colors - 1),
					backgroundcolor.B + (color.B - backgroundcolor.B) * index / (colors - 1));
			}
			if (transparency)
				palette.Entries[colors - 1] = Color.Transparent;
			if (backgroundtransparency)
				palette.Entries[0] = Color.Transparent;

			bitmap.Palette = palette;

			BitmapData bitmapdata;
			Rectangle rect = new Rectangle(0, 0, Width, Height);

			bitmapdata = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

			IntPtr pixels = bitmapdata.Scan0;

			int stride = (int)Math.Abs(bitmapdata.Stride);

			for (short row = 0; row < Height; row++) {
				for (short col = 0; col < Width; col++) {
					byte index = (byte)(sourcebitmap.GetPixel(col, row).R * (colors - 1) / 255);
					Marshal.WriteByte(bitmapdata.Scan0, row * stride + col, index);
				}
			}

			bitmap.UnlockBits(bitmapdata);

			return bitmap;
		}
	}
}