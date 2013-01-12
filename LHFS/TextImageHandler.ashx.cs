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
	/// Zusammenfassungsbeschreibung für TextImageHandler
	/// </summary>
	/// 
	public class TextImageHandler : IHttpHandler {

		private string text, fontfile, fontstyle, horizontalalignment, verticalalignment, overflow;
		private Color color, backgroundcolor, glowcolor, shadowcolor;
		private short fontsize, width, height, textWidth, textHeight, textWidth2, textHeight2, padding, horizontalpadding, verticalpadding, rotation, glowwidth, shadowwidth, shadowX, shadowY, shadowalpha;
		private short colors;
		private bool transparency, backgroundtransparency;
		private const int maxComplexity = 500000;

		public bool IsReusable {
			get {
				return false;
			}
		}


		public void ProcessRequest(HttpContext context) {
			//if (context.Request.Headers["If-Modified-Since"] != null) {
			//    context.Response.StatusCode = 304;
			//    context.Response.StatusDescription = "Not Modified";
			//    context.Response.End();
			//}

			//try {
			//    if (context.Request.UrlReferrer != null && context.Request.UrlReferrer.Host != context.Request.Url.Host) {
			//        context.Response.ContentType = "image/gif";
			//        context.Response.BinaryWrite(new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61, 0x01, 0x00, 0x01, 0x00, 0x91, 0x02, 0x00, 0xFF, 0xFF, 0xFF, 0x0E, 0x0E, 0x0E, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x21, 0xFF, 0x0B, 0x4E, 0x45, 0x54, 0x53, 0x43, 0x41, 0x50, 0x45, 0x32, 0x2E, 0x30, 0x03, 0x01, 0x00, 0x00, 0x00, 0x21, 0xF9, 0x04, 0x05, 0x32, 0x00, 0x02, 0x00, 0x2C, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x02, 0x02, 0x4C, 0x01, 0x00, 0x21, 0xF9, 0x04, 0x05, 0x32, 0x00, 0x02, 0x00, 0x2C, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x02, 0x02, 0x44, 0x01, 0x00, 0x3B });
			//        context.Response.End();
			//    }
			//} catch { }

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
				fontfile = "hlr____-webfont.ttf";

			try { color = ColorTranslator.FromHtml("#" + context.Request.QueryString["color"] + context.Request.QueryString["c"]); } catch { color = Color.Black; }

			try { backgroundcolor = ColorTranslator.FromHtml("#" + context.Request.QueryString["backgroundcolor"] + context.Request.QueryString["bc"]); } catch { backgroundcolor = Color.White; }

			try { glowcolor = ColorTranslator.FromHtml("#" + context.Request.QueryString["glowcolor"]); } catch { glowcolor = Color.White; }

			try { glowwidth = short.Parse(context.Request.QueryString["glowwidth"]); } catch { if (context.Request.QueryString["glowcolor"] != null) glowwidth = 0; }

			try { shadowcolor = ColorTranslator.FromHtml("#" + context.Request.QueryString["shadowcolor"]); } catch { shadowcolor = Color.Transparent; }

			try { shadowalpha = short.Parse(context.Request.QueryString["shadowalpha"]); } catch { shadowalpha = 5; }

			shadowcolor = Color.FromArgb(shadowalpha, shadowcolor);

			try { shadowwidth = short.Parse(context.Request.QueryString["shadowwidth"]); } catch { if (context.Request.QueryString["shadowcolor"] != null) shadowwidth = 1; }

			try { shadowX = short.Parse(context.Request.QueryString["shadowX"]); } catch { if (context.Request.QueryString["shadowcolor"] != null) shadowX = 1; }

			try { shadowY = short.Parse(context.Request.QueryString["shadowY"]); } catch { if (context.Request.QueryString["shadowcolor"] != null) shadowY = 1; }

			try { rotation = short.Parse(context.Request.QueryString["rotation"] + context.Request.QueryString["r"]); } catch { rotation = 0; }

			try { fontsize = short.Parse(context.Request.QueryString["fontsize"] + context.Request.QueryString["size"] + context.Request.QueryString["s"]); } catch { fontsize = 12; }
			if (fontsize < 1 || fontsize > 512)
				fontsize = 12;

			int maxLength = 1 + maxComplexity / (int)Math.Pow(fontsize, 2);
			if (text.Length > maxLength)
				text = text.Substring(0, maxLength);

			try { width = short.Parse(context.Request.QueryString["width"] + context.Request.QueryString["w"]); } catch { }

			try { height = short.Parse(context.Request.QueryString["height"] + context.Request.QueryString["h"]); } catch { }

			try { padding = short.Parse(context.Request.QueryString["padding"] + context.Request.QueryString["p"]); } catch { }
			if (padding < 0)
				padding = 0;

			try { horizontalpadding = short.Parse(context.Request.QueryString["horizontalpadding"] + context.Request.QueryString["hp"]); } catch { }
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

			int factor = 1;

			transparency = context.Request.QueryString["transparency"] + context.Request.QueryString["t"] == "true" || context.Request.QueryString["transparency"] + context.Request.QueryString["t"] == "yes" || context.Request.QueryString["transparency"] + context.Request.QueryString["t"] == "1";
			backgroundtransparency = context.Request.QueryString["backgroundtransparency"] + context.Request.QueryString["bt"] != "false" && context.Request.QueryString["backgroundtransparency"] + context.Request.QueryString["bt"] != "no" && context.Request.QueryString["backgroundtransparency"] + context.Request.QueryString["bt"] != "0";

			horizontalalignment = context.Request.QueryString["horizontalalignment"] + context.Request.QueryString["alignment"] + context.Request.QueryString["textalign"] + context.Request.QueryString["ha"] + context.Request.QueryString["ta"];
			verticalalignment = context.Request.QueryString["verticalalignment"] + context.Request.QueryString["verticalalign"] + context.Request.QueryString["va"];

			StringFormat stringformat = StringFormat.GenericTypographic;
			stringformat.Alignment = horizontalalignment == "left" || horizontalalignment == "l" ? StringAlignment.Near : horizontalalignment == "right" || horizontalalignment == "r" ? StringAlignment.Far : StringAlignment.Center;
			stringformat.LineAlignment = verticalalignment == "top" || verticalalignment == "t" ? StringAlignment.Near : verticalalignment == "bottom" || verticalalignment == "b" ? StringAlignment.Far : StringAlignment.Center;
			stringformat.Trimming = overflow == "ellipsis" || overflow == "e" || overflow == "ellipsischaracter" || overflow == "ec" ? StringTrimming.EllipsisCharacter : overflow == "ellipsisword" || overflow == "ew" ? StringTrimming.EllipsisWord : overflow == "character" || overflow == "c" ? StringTrimming.Character : overflow == "word" || overflow == "w" ? StringTrimming.Word : StringTrimming.None;

			FontFamily fontfamily =
				//fontfile.Equals(string.Empty) || fontfile.IndexOf(".ttf") > 0 ? 
				PrivateFontFamily.FromTrueType(fontfile, context);
				//: new FontFamily(fontfile);

			Font font;

			try {
				font = new Font(fontfamily, fontsize * factor, FontStyle.Regular | (fontstyle.IndexOf("bold") >= 0 ? FontStyle.Bold : FontStyle.Regular) | (fontstyle.IndexOf("italic") >= 0 ? FontStyle.Italic : FontStyle.Regular) | (fontstyle.IndexOf("underline") >= 0 ? FontStyle.Underline : FontStyle.Regular), GraphicsUnit.Pixel);
			} catch (Exception e) {
				font = new Font(fontfamily, fontsize * factor, FontStyle.Bold | (fontstyle.IndexOf("bold") >= 0 ? FontStyle.Bold : FontStyle.Regular) | (fontstyle.IndexOf("italic") >= 0 ? FontStyle.Italic : FontStyle.Regular) | (fontstyle.IndexOf("underline") >= 0 ? FontStyle.Underline : FontStyle.Regular), GraphicsUnit.Pixel);
			}

			Bitmap tempbitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
			Graphics tempgraphics = Graphics.FromImage(tempbitmap);
			if (rotation != 0)
				tempgraphics.RotateTransform(rotation);


			float rot = (float)rotation / 180f * (float)Math.PI;
			SizeF textSizeF = tempgraphics.MeasureString(text, font, width > 1 ? width * factor : 32767, stringformat);
			textWidth = (short)(width > 0 ? width * factor : textSizeF.Width * Math.Abs(Math.Cos(rot)) + textSizeF.Height * Math.Abs(Math.Sin(rot)));
			textHeight = (short)(height > 0 ? height * factor : textSizeF.Width * Math.Abs(Math.Sin(rot)) + textSizeF.Height * Math.Abs(Math.Cos(rot)));
			textWidth2 = (short)(width > 0 ? width * factor : textSizeF.Width);
			textHeight2 = (short)(height > 0 ? height * factor : textSizeF.Height);

			tempgraphics.Dispose();
			tempbitmap.Dispose();

			Bitmap bitmap = new Bitmap(textWidth + padding * 2 * factor + horizontalpadding * 2 * factor, textHeight + padding * 2 * factor + verticalpadding * 2 * factor, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.Clear(Color.Transparent);

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = context.Request.QueryString["hinting"] == "1" || context.Request.QueryString["hinting"] == "true" ? TextRenderingHint.AntiAliasGridFit : TextRenderingHint.AntiAlias;
			graphics.TextContrast = 0;
			graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;

			graphics.TranslateTransform(textWidth / 2 + padding * factor + horizontalpadding * factor, textHeight / 2 + padding * factor + verticalpadding * factor);
			graphics.RotateTransform(rotation);

			if (shadowwidth > 0) {
				for (short posX = (short)-shadowwidth; posX <= shadowwidth; posX++)
					for (short posY = (short)-shadowwidth; posY <= shadowwidth; posY++)
						graphics.DrawString(text, font, new SolidBrush(shadowcolor), new Rectangle(-1 - textWidth2 / 2 + shadowX + posX, -1 - textHeight2 / 2 + shadowY + posY, textWidth2 + 2, textHeight2 + 2), stringformat);
			}

			if (glowwidth > 0) {
				graphics.DrawString(text, font, new SolidBrush(glowcolor), new Rectangle(-1 - textWidth2 / 2 - glowwidth, -1 - textHeight2 / 2 - glowwidth, textWidth2 + 2, textHeight2 + 2), stringformat);
				graphics.DrawString(text, font, new SolidBrush(glowcolor), new Rectangle(-1 - textWidth2 / 2 - glowwidth, -1 - textHeight2 / 2 + glowwidth, textWidth2 + 2, textHeight2 + 2), stringformat);
				graphics.DrawString(text, font, new SolidBrush(glowcolor), new Rectangle(-1 - textWidth2 / 2 + glowwidth, -1 - textHeight2 / 2 - glowwidth, textWidth2 + 2, textHeight2 + 2), stringformat);
				graphics.DrawString(text, font, new SolidBrush(glowcolor), new Rectangle(-1 - textWidth2 / 2 + glowwidth, -1 - textHeight2 / 2 + glowwidth, textWidth2 + 2, textHeight2 + 2), stringformat);
			}
			graphics.DrawString(text, font, new SolidBrush(color), new Rectangle(-1 - textWidth2 / 2, -1 - textHeight2 / 2, textWidth2 + 2, textHeight2 + 2), stringformat);

			fontfamily.Dispose();

			graphics.Flush();
			graphics.Dispose();

			Bitmap targetbitmap = bitmap;
			/*			Bitmap targetbitmap = new Bitmap(textWidth / factor + padding * 2 + horizontalpadding * 2, textHeight / factor + padding * 2 + verticalpadding * 2, PixelFormat.Format32bppArgb);
						Graphics targetgraphics = Graphics.FromImage(targetbitmap);
						targetgraphics.Clear(Color.Transparent);
						targetgraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
						targetgraphics.DrawImage(bitmap, new Rectangle(0, 0, textWidth / factor + padding * 2 + horizontalpadding * 2, textHeight / factor + padding * 2 + verticalpadding * 2));
						targetgraphics.Flush();
						targetgraphics.Dispose();
						bitmap.Dispose();*/

			if (!context.Response.IsClientConnected)
				context.Response.End();

			//			tempbitmap = ColorIndexedBitmap(targetbitmap, colors, color, backgroundcolor, transparency, backgroundtransparency);

			context.Response.ContentType = "image/png";
			MemoryStream stream = new MemoryStream();
			targetbitmap.Save(stream, ImageFormat.Png);

			targetbitmap.Dispose();


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