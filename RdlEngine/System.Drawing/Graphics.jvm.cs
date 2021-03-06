
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Runtime.Remoting.Messaging;
using Android.Speech.Tts;

namespace System.Drawing
{
    using System;
    using droid = Android.Graphics;

    public class Graphics : IDisposable
    {
        private readonly bool ownCanvas;
        private readonly Android.Graphics.Paint paint;
        private readonly Android.Graphics.Canvas canvas;

        public Android.Graphics.PaintFlags Flags { get; set; }

        private Graphics()
        {
            this.paint = new droid.Paint();
            this.Flags = droid.PaintFlags.AntiAlias;
            this.ownCanvas = false;
        }

        private Graphics(Bitmap bitmap)
            : this()
        {
            this.canvas = new droid.Canvas(bitmap.ABitmap);
            this.ownCanvas = true;
        }

        public Graphics(Android.Graphics.Canvas canvas)
            : this()
        {
            this.canvas = canvas;
        }

        public static Graphics FromImage(Image image)
        {
            Bitmap bm = (Bitmap)image;

            Graphics g = new Graphics(bm);
            return g;
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.paint.Dispose();

            if (this.ownCanvas)
            {
                this.canvas.Dispose();
            }
        }

        #endregion

        public static Graphics FromBitmap(Bitmap bitmap)
        {
            return new Graphics(bitmap);
        }

        public void DrawImage(Bitmap bitmap, Rectangle target, Rectangle source, GraphicsUnit gu)
        {
            this.paint.Flags = 0;
            using (droid.Rect sa = source.ToRect())
            using (droid.Rect ta = target.ToRect())
            {
                this.canvas.DrawBitmap(bitmap.ABitmap, sa, ta, this.paint);
            }
        }

        public void DrawImage(Bitmap bitmap, int x, int y)
        {
            this.paint.Flags = 0;
            this.canvas.DrawBitmap(bitmap.ABitmap, x, y, this.paint);
        }

        public void DrawImage(Bitmap bitmap, int x, int y, Rectangle source, GraphicsUnit gu)
        {
            this.DrawImage(bitmap, new Rectangle(x, y, bitmap.Width, bitmap.Height), source, gu);
        }

        public void DrawImage(Image img, Rectangle rect)
        {
            throw new NotImplementedException();
        }

        public void DrawImage(Image img, RectangleF rect)
        {
            throw new NotImplementedException();
        }

        public void DrawImageUnscaled(Image image, int x, int y)
        {
            throw new NotImplementedException();
        }

        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            this.paint.Color = pen.Color.ToAColor();
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Stroke);
            this.paint.StrokeWidth = pen.Width;
            this.canvas.DrawLine(x1, y1, x2, y2, paint);
        }

        public void DrawLine(Pen pen, Point p1, Point p2)
        {
            this.paint.Color = pen.Color.ToAColor();
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Stroke);
            this.paint.StrokeWidth = pen.Width;
            this.canvas.DrawLine(p1.X, p1.Y, p2.X, p2.Y, paint);
        }

        public void DrawLines(Pen pen, Point[] p)
        {
            // Is this how DrawLines works?
            for (int i = 0; i < p.Length - 1; i++)
            {

                if (i == 0)
                {
                    DrawLine(pen, p[i], p[i]);
                }
                else
                {
                    DrawLine(pen, p[i - 1], p[i]);
                }
            }

        }


        public void DrawRectangle(Pen pen, float x1, float y1, float w, float h)
        {
            this.paint.Color = pen.Color.ToAColor();
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Stroke);
            this.paint.StrokeWidth = pen.Width;
            this.canvas.DrawRect(x1, y1, x1 + w, y1 + h, this.paint);
        }

        public void DrawRectangle(Pen pen, Rectangle rect)
        {
            this.paint.Color = pen.Color.ToAColor();
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Stroke);
            this.paint.StrokeWidth = pen.Width;
            this.canvas.DrawRect(rect.X, rect.Y, rect.X + 1, rect.Y + 1, this.paint);
        }


        public void DrawEllipse(Pen pen, float x, float y, float w, float h)
        {
            this.paint.Color = pen.Color.ToAColor();
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Stroke);
            this.paint.StrokeWidth = pen.Width;
            using (droid.RectF r = new RectangleF(x, y, w, h).ToRectF())
            {
                this.canvas.DrawOval(r, this.paint);
            }
        }

        public void DrawEllipse(Pen pen, Rectangle rect)
        {
            this.paint.Color = pen.Color.ToAColor();
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Stroke);
            this.paint.StrokeWidth = pen.Width;
            using (droid.RectF r = new RectangleF(rect.X, rect.Y, 1, 1).ToRectF())
            {
                this.canvas.DrawOval(r, this.paint);
            }
        }

        public void DrawEllipse(Pen pen, RectangleF rect)
        {
            this.paint.Color = pen.Color.ToAColor();
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Stroke);
            this.paint.StrokeWidth = pen.Width;
            using (droid.RectF r = new RectangleF(rect.X, rect.Y, 1, 1).ToRectF())
            {
                this.canvas.DrawOval(r, this.paint);
            }
        }

        public void DrawCurve(Pen pen, Point[] points, Single tension)
        {
            throw new NotImplementedException();
        }

        public void DrawCurve(Pen pen, PointF[] point, Single tension)
        {
            throw new NotImplementedException();
        }

        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension)
        {
            throw new NotImplementedException();
        }

        public void DrawPolygon(Pen pen, PointF[] points)
        {
            this.paint.Color = pen.Color.ToAColor();
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Stroke);
            this.paint.StrokeWidth = pen.Width;

            // TODO: test that this works
            droid.Path path = new droid.Path();
            foreach (PointF p in points)
            {
                path.LineTo(p.X, p.Y);
            }

            this.canvas.DrawPath(path, this.paint);
        }

        public void DrawPolygon(Pen pen, Point[] points)
        {
            this.paint.Color = pen.Color.ToAColor(); ;
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Stroke);
            this.paint.StrokeWidth = pen.Width;

            // TODO: test that this works
            droid.Path path = new droid.Path();
            foreach (Point p in points)
            {
                path.LineTo(p.X, p.Y);
            }

            this.canvas.DrawPath(path, this.paint);
        }


        public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void FillRectangle(Brush brush, float x1, float y1, float w, float h)
        {
            this.paint.Color = brush.Color.ToAColor();
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Fill);
            this.canvas.DrawRect(x1, y1, x1 + w, y1 + h, this.paint);
        }

        public void FillRectangle(Brush brush, RectangleF rect)
        {
            this.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void FillRectangle(Brush brush, Rectangle rect)
        {
            this.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void FillEllipse(Brush brush, RectangleF rect)
        {
            this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void FillEllipse(Brush brush, Rectangle rect)
        {
            this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void FillEllipse(Brush brush, float x, float y, float w, float h)
        {
            this.paint.Color = brush.Color.ToAColor();
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Fill);
            using (droid.RectF r = new RectangleF(x, y, w, h).ToRectF())
            {
                this.canvas.DrawOval(r, this.paint);
            }
        }

        public void FillPolygon(Brush brush, Point[] points)
        {
            this.paint.Color = brush.Color.ToAColor();
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Fill);

            // TODO: test that this works
            droid.Path path = new droid.Path();
            foreach (Point p in points)
            {
                path.LineTo(p.X, p.Y);
            }

            this.canvas.DrawPath(path, this.paint);
        }

        public void FillPolygon(Brush brush, PointF[] points)
        {

            this.paint.Color = brush.Color.ToAColor();
            this.paint.Flags = this.Flags;
            this.paint.SetStyle(droid.Paint.Style.Fill);

            // TODO: test that this works
            droid.Path path = new droid.Path();
            foreach (PointF p in points)
            {
                path.LineTo(p.X, p.Y);
            }

            this.canvas.DrawPath(path, this.paint);
        }

        public void FillPie(Brush brush, Rectangle rect, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void DrawString(string text, Font font, Brush brush, float x, float y)
        {
            this.paint.Color = brush.Color.ToAColor();
            this.paint.Flags = droid.PaintFlags.AntiAlias;

            this.paint.TextSize = font.Size;
            this.paint.SetTypeface(font.FontFamily.Typeface);
            this.paint.SetStyle(droid.Paint.Style.Fill);

            using (var fm = this.paint.GetFontMetrics())
            {
                this.canvas.DrawText(text, x, y - (fm.Top + fm.Bottom), this.paint);
            }
        }

        public void DrawString(string text, Font font, Brush brush, System.Drawing.PointF pos)
        {
            this.DrawString(text, font, brush, pos.X, pos.Y);
        }

        public void DrawString(string text, Font font, Brush brush, RectangleF rect)
        {
            this.DrawString(text, font, brush, rect.Location);
        }


        public void DrawString(string text, Font font, Brush brush, RectangleF rect, StringFormat format)
        {
            // TODO:  use the StringFormat parameter

            this.paint.Color = brush.Color.ToAColor();
            this.paint.Flags = droid.PaintFlags.AntiAlias;

            this.paint.TextSize = font.Size;
            this.paint.SetTypeface(font.FontFamily.Typeface);
            this.paint.SetStyle(droid.Paint.Style.Fill);

            using (var fm = this.paint.GetFontMetrics())
            {
                this.canvas.DrawText(text, rect.Location.X, rect.Location.Y - (fm.Top + fm.Bottom), this.paint);
            }

        }

        public void DrawString(string s, Font font, Brush brush, PointF layoutRectangle, StringFormat format)
        {
            throw new NotImplementedException();
        }


        public Size MeasureString(string text, Font font)
        {
            using (var p = new droid.Paint(this.paint))
            using (var bounds = new droid.Rect())
            using (var fm = p.GetFontMetrics())
            {
                p.TextSize = font.Size;
                p.SetTypeface(font.FontFamily.Typeface);
                p.SetStyle(droid.Paint.Style.Stroke);

                p.GetTextBounds(text, 0, text.Length, bounds);
                var width = bounds.Width();
                var height = -fm.Top + fm.Bottom;

                return new SizeF(width, height).ToSize();
            }
        }

        //TODO: review this function
        public Size MeasureString(String text, Font font, SizeF size, StringFormat format)
        {

            using (var p = new droid.Paint(this.paint))
            using (var bounds = new droid.Rect())
            using (var fm = p.GetFontMetrics())
            {
                p.TextSize = font.Size;
                p.SetTypeface(font.FontFamily.Typeface);
                p.SetStyle(droid.Paint.Style.Stroke);

                p.GetTextBounds(text, 0, text.Length, bounds);
                var width = bounds.Width();
                var height = -fm.Top + fm.Bottom;

                return new SizeF(width, height).ToSize();
            }
        }

        public SizeF MeasureString(string text, Font font, int width, StringFormat format)
        {
            using (var p = new droid.Paint(this.paint))
            using (var bounds = new droid.Rect())
            using (var fm = p.GetFontMetrics())
            {
                p.TextSize = font.Size;
                p.SetTypeface(font.FontFamily.Typeface);
                p.SetStyle(droid.Paint.Style.Stroke);

                p.GetTextBounds(text, 0, text.Length, bounds);
                var height = -fm.Top + fm.Bottom;

                return new SizeF(width, height);
            }
        }


        public void RotateTransform(float angle)
        {
            this.canvas.Rotate(angle);
        }

        public void TranslateTransform(float x, float y)
        {
            this.canvas.Translate(x, y);
        }

        public void Clear()
        {
            this.Clear(Color.Transparent);
        }

        public void Clear(Color fill)
        {
            this.canvas.DrawARGB(fill.A, fill.R, fill.G, fill.B);
        }

        public Drawing2D.SmoothingMode SmoothingMode { get; set; }

        public Drawing2D.InterpolationMode InterpolationMode { get; set; }

        public Drawing2D.PixelOffsetMode PixelOffsetMode { get; set; }

        public Drawing2D.CompositingQuality CompositingQuality { get; set; }

        public void ReleaseHdc(IntPtr hdc)
        {
        }

        public void AddMetafileComment(byte[] data)
        {
            throw new NotImplementedException();
        }

        public System.IntPtr GetHdc()
        {
            throw new NotImplementedException();
        }


        public delegate bool EnumerateMetafileProc(
            Imaging.EmfPlusRecordType recordType,
            int flags,
            int dataSize,
            IntPtr data,
            Imaging.PlayRecordCallback callbackData
            );

        public GraphicsUnit PageUnit { get; set; }


        public float DpiX
        {
            get
            {
                return this.canvas.Density;
            }
        }

        public float DpiY
        {
            get
            {
                return this.canvas.Density;
            }
        }


        public Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat)
        {

            using (var _p = new droid.Paint(this.paint))
            using (var _path = new droid.Path())
            using (var _fm = _p.GetFontMetrics())
            {
                _p.TextSize = font.Size;
                _p.SetTypeface(font.FontFamily.Typeface);
                _p.SetStyle(droid.Paint.Style.Stroke);
                _p.GetTextPath(text, 0, text.Length, 0, 0, _path);

                var _rectf = new droid.RectF();
                _path.ComputeBounds(_rectf, true);

                return new Region[] { new Region(_rectf) };

            }

        }


        public void ScaleTransform(float sx, float sy)
        {
            throw new NotImplementedException();
        }

        public Region Clip
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Drawing2D.Matrix Transform
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void ResetTransform()
        {
            throw new NotImplementedException();
        }


        public void EnumerateMetafile(Imaging.Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

    }
}