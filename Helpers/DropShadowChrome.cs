using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UserRegister.Helpers
{
    public sealed class DropShadowChrome : Decorator
    {
        // Fields
        private Brush[] _brushes;
        private static Brush[] _commonBrushes;
        private static CornerRadius _commonCornerRadius;
        private static double _commonBlur;
        private static object _resourceAccess = new object();
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(DropShadowChrome), new FrameworkPropertyMetadata(Color.FromArgb(0x71, 0, 0, 0), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(DropShadowChrome.ClearBrushes)));
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(DropShadowChrome), new FrameworkPropertyMetadata(new CornerRadius(), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(DropShadowChrome.ClearBrushes)), new ValidateValueCallback(DropShadowChrome.IsCornerRadiusValid));
        public static readonly DependencyProperty BlurProperty = DependencyProperty.Register("Blur", typeof(double), typeof(DropShadowChrome), new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(DropShadowChrome.ClearBrushes)));
        private const double ShadowDepth = 5.0;
        private const int TopLeft = 0;
        private const int Top = 1;
        private const int TopRight = 2;
        private const int Left = 3;
        private const int Center = 4;
        private const int Right = 5;
        private const int BottomLeft = 6;
        private const int Bottom = 7;
        private const int BottomRight = 8;

        // Methods
        private static void ClearBrushes(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((DropShadowChrome)o)._brushes = null;
        }

        private static Brush[] CreateBrushes(Color c, CornerRadius cornerRadius, double blur)
        {
            GradientStopCollection stops2;
            GradientStopCollection stops3;
            GradientStopCollection stops4;
            GradientStopCollection stops5;
            Brush[] brushArray = new Brush[9];
            brushArray[4] = new SolidColorBrush(c);
            brushArray[4].Freeze();
            GradientStopCollection gradientStopCollection = CreateStops(c, 0.0, blur);
            LinearGradientBrush brush = new LinearGradientBrush(gradientStopCollection, new Point(0.0, 1.0), new Point(0.0, 0.0));
            brush.Freeze();
            brushArray[1] = brush;
            LinearGradientBrush brush2 = new LinearGradientBrush(gradientStopCollection, new Point(1.0, 0.0), new Point(0.0, 0.0));
            brush2.Freeze();
            brushArray[3] = brush2;
            LinearGradientBrush brush3 = new LinearGradientBrush(gradientStopCollection, new Point(0.0, 0.0), new Point(1.0, 0.0));
            brush3.Freeze();
            brushArray[5] = brush3;
            LinearGradientBrush brush4 = new LinearGradientBrush(gradientStopCollection, new Point(0.0, 0.0), new Point(0.0, 1.0));
            brush4.Freeze();
            brushArray[7] = brush4;
            if (cornerRadius.TopLeft == 0.0)
            {
                stops2 = gradientStopCollection;
            }
            else
            {
                stops2 = CreateStops(c, cornerRadius.TopLeft, blur);
            }
            RadialGradientBrush brush5 = new RadialGradientBrush(stops2);
            brush5.RadiusX = 1.0;
            brush5.RadiusY = 1.0;
            brush5.Center = new Point(1.0, 1.0);
            brush5.GradientOrigin = new Point(1.0, 1.0);
            brush5.Freeze();
            brushArray[0] = brush5;
            if (cornerRadius.TopRight == 0.0)
            {
                stops3 = gradientStopCollection;
            }
            else if (cornerRadius.TopRight == cornerRadius.TopLeft)
            {
                stops3 = stops2;
            }
            else
            {
                stops3 = CreateStops(c, cornerRadius.TopRight, blur);
            }
            RadialGradientBrush brush6 = new RadialGradientBrush(stops3);
            brush6.RadiusX = 1.0;
            brush6.RadiusY = 1.0;
            brush6.Center = new Point(0.0, 1.0);
            brush6.GradientOrigin = new Point(0.0, 1.0);
            brush6.Freeze();
            brushArray[2] = brush6;
            if (cornerRadius.BottomLeft == 0.0)
            {
                stops4 = gradientStopCollection;
            }
            else if (cornerRadius.BottomLeft == cornerRadius.TopLeft)
            {
                stops4 = stops2;
            }
            else if (cornerRadius.BottomLeft == cornerRadius.TopRight)
            {
                stops4 = stops3;
            }
            else
            {
                stops4 = CreateStops(c, cornerRadius.BottomLeft, blur);
            }
            RadialGradientBrush brush7 = new RadialGradientBrush(stops4);
            brush7.RadiusX = 1.0;
            brush7.RadiusY = 1.0;
            brush7.Center = new Point(1.0, 0.0);
            brush7.GradientOrigin = new Point(1.0, 0.0);
            brush7.Freeze();
            brushArray[6] = brush7;
            if (cornerRadius.BottomRight == 0.0)
            {
                stops5 = gradientStopCollection;
            }
            else if (cornerRadius.BottomRight == cornerRadius.TopLeft)
            {
                stops5 = stops2;
            }
            else if (cornerRadius.BottomRight == cornerRadius.TopRight)
            {
                stops5 = stops3;
            }
            else if (cornerRadius.BottomRight == cornerRadius.BottomLeft)
            {
                stops5 = stops4;
            }
            else
            {
                stops5 = CreateStops(c, cornerRadius.BottomRight, blur);
            }
            RadialGradientBrush brush8 = new RadialGradientBrush(stops5);
            brush8.RadiusX = 1.0;
            brush8.RadiusY = 1.0;
            brush8.Center = new Point(0.0, 0.0);
            brush8.GradientOrigin = new Point(0.0, 0.0);
            brush8.Freeze();
            brushArray[8] = brush8;
            return brushArray;
        }

        private static GradientStopCollection CreateStops(Color c, double cornerRadius, double blur)
        {
            double num = 1.0 / (cornerRadius + blur);
            blur /= 5;
            GradientStopCollection stops = new GradientStopCollection();
            stops.Add(new GradientStop(c, (0.5 * blur + cornerRadius) * num));
            Color color = c;
            color.A = (byte)(0.74336 * c.A);
            stops.Add(new GradientStop(color, (1.5 * blur + cornerRadius) * num));
            color.A = (byte)(0.38053 * c.A);
            stops.Add(new GradientStop(color, (2.5 * blur + cornerRadius) * num));
            color.A = (byte)(0.12389 * c.A);
            stops.Add(new GradientStop(color, (3.5 * blur + cornerRadius) * num));
            color.A = (byte)(0.02654 * c.A);
            stops.Add(new GradientStop(color, (4.5 * blur + cornerRadius) * num));
            color.A = 0;
            stops.Add(new GradientStop(color, (5.0 * blur + cornerRadius) * num));
            stops.Freeze();
            return stops;
        }

        private Brush[] GetBrushes(Color c, CornerRadius cornerRadius)
        {
            if (_commonBrushes == null)
            {
                lock (_resourceAccess)
                {
                    if (_commonBrushes == null)
                    {
                        _commonBrushes = CreateBrushes(c, cornerRadius, Blur);
                        _commonCornerRadius = cornerRadius;
                        _commonBlur = Blur;
                    }
                }
            }
            if ((c == ((SolidColorBrush)_commonBrushes[Center]).Color) && (cornerRadius == _commonCornerRadius) && (Blur == _commonBlur))
            {
                this._brushes = null;
                return _commonBrushes;
            }
            if (this._brushes == null)
            {
                this._brushes = CreateBrushes(c, cornerRadius, Blur);
            }
            return this._brushes;
        }

        private static bool IsCornerRadiusValid(object value)
        {
            CornerRadius radius = (CornerRadius)value;
            return ((((((radius.TopLeft >= 0.0) && (radius.TopRight >= 0.0)) && ((radius.BottomLeft >= 0.0) && (radius.BottomRight >= 0.0))) && ((!double.IsNaN(radius.TopLeft) && !double.IsNaN(radius.TopRight)) && (!double.IsNaN(radius.BottomLeft) && !double.IsNaN(radius.BottomRight)))) && ((!double.IsInfinity(radius.TopLeft) && !double.IsInfinity(radius.TopRight)) && !double.IsInfinity(radius.BottomLeft))) && !double.IsInfinity(radius.BottomRight));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            CornerRadius cornerRadius = this.CornerRadius;
            Rect rect = new Rect(new Point(0, 0), new Size(base.RenderSize.Width, base.RenderSize.Height));
            Color c = this.Color;
            if (((rect.Width > 0.0) && (rect.Height > 0.0)) && (c.A > 0))
            {
                double width = (rect.Right - rect.Left) - Blur * 2;
                double height = (rect.Bottom - rect.Top) - Blur * 2;
                double num3 = Math.Min((double)(width * 0.5), (double)(height * 0.5));
                cornerRadius.TopLeft = Math.Min(cornerRadius.TopLeft, num3);
                cornerRadius.TopRight = Math.Min(cornerRadius.TopRight, num3);
                cornerRadius.BottomLeft = Math.Min(cornerRadius.BottomLeft, num3);
                cornerRadius.BottomRight = Math.Min(cornerRadius.BottomRight, num3);
                Brush[] brushes = this.GetBrushes(c, cornerRadius);
                double top = rect.Top + Blur;
                double left = rect.Left + Blur;
                double right = rect.Right - Blur;
                double bottom = rect.Bottom - Blur;
                double[] guidelinesX = new double[] { left, left + cornerRadius.TopLeft, right - cornerRadius.TopRight, left + cornerRadius.BottomLeft, right - cornerRadius.BottomRight, right };
                double[] guidelinesY = new double[] { top, top + cornerRadius.TopLeft, top + cornerRadius.TopRight, bottom - cornerRadius.BottomLeft, bottom - cornerRadius.BottomRight, bottom };
                drawingContext.PushGuidelineSet(new GuidelineSet(guidelinesX, guidelinesY));
                cornerRadius.TopLeft += Blur;
                cornerRadius.TopRight += Blur;
                cornerRadius.BottomLeft += Blur;
                cornerRadius.BottomRight += Blur;
                Rect rectangle = new Rect(rect.Left, rect.Top, cornerRadius.TopLeft, cornerRadius.TopLeft);
                drawingContext.DrawRectangle(brushes[0], null, rectangle);
                double num8 = guidelinesX[2] - guidelinesX[1];
                if (num8 > 0.0)
                {
                    Rect rect3 = new Rect(guidelinesX[1], rect.Top, num8, Blur);
                    drawingContext.DrawRectangle(brushes[1], null, rect3);
                }
                Rect rect4 = new Rect(guidelinesX[2], rect.Top, cornerRadius.TopRight, cornerRadius.TopRight);
                drawingContext.DrawRectangle(brushes[2], null, rect4);
                double num9 = guidelinesY[3] - guidelinesY[1];
                if (num9 > 0.0)
                {
                    Rect rect5 = new Rect(rect.Left, guidelinesY[1], Blur, num9);
                    drawingContext.DrawRectangle(brushes[3], null, rect5);
                }
                double num10 = guidelinesY[4] - guidelinesY[2];
                if (num10 > 0.0)
                {
                    Rect rect6 = new Rect(guidelinesX[5], guidelinesY[2], Blur, num10);
                    drawingContext.DrawRectangle(brushes[5], null, rect6);
                }
                Rect rect7 = new Rect(rect.Left, guidelinesY[3], cornerRadius.BottomLeft, cornerRadius.BottomLeft);
                drawingContext.DrawRectangle(brushes[6], null, rect7);
                double num11 = guidelinesX[4] - guidelinesX[3];
                if (num11 > 0.0)
                {
                    Rect rect8 = new Rect(guidelinesX[3], guidelinesY[5], num11, Blur);
                    drawingContext.DrawRectangle(brushes[7], null, rect8);
                }
                Rect rect9 = new Rect(guidelinesX[4], guidelinesY[4], cornerRadius.BottomRight, cornerRadius.BottomRight);
                drawingContext.DrawRectangle(brushes[8], null, rect9);
                if (((cornerRadius.TopLeft == Blur) && (cornerRadius.TopLeft == cornerRadius.TopRight)) && ((cornerRadius.TopLeft == cornerRadius.BottomLeft) && (cornerRadius.TopLeft == cornerRadius.BottomRight)))
                {
                    Rect rect10 = new Rect(guidelinesX[0], guidelinesY[0], width, height);
                    drawingContext.DrawRectangle(brushes[4], null, rect10);
                }
                else
                {
                    PathFigure figure = new PathFigure();
                    if (cornerRadius.TopLeft > Blur)
                    {
                        figure.StartPoint = new Point(guidelinesX[1], guidelinesY[0]);
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[1], guidelinesY[1]), true));
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[0], guidelinesY[1]), true));
                    }
                    else
                    {
                        figure.StartPoint = new Point(guidelinesX[0], guidelinesY[0]);
                    }
                    if (cornerRadius.BottomLeft > Blur)
                    {
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[0], guidelinesY[3]), true));
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[3], guidelinesY[3]), true));
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[3], guidelinesY[5]), true));
                    }
                    else
                    {
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[0], guidelinesY[5]), true));
                    }
                    if (cornerRadius.BottomRight > Blur)
                    {
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[4], guidelinesY[5]), true));
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[4], guidelinesY[4]), true));
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[5], guidelinesY[4]), true));
                    }
                    else
                    {
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[5], guidelinesY[5]), true));
                    }
                    if (cornerRadius.TopRight > Blur)
                    {
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[5], guidelinesY[2]), true));
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[2], guidelinesY[2]), true));
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[2], guidelinesY[0]), true));
                    }
                    else
                    {
                        figure.Segments.Add(new LineSegment(new Point(guidelinesX[5], guidelinesY[0]), true));
                    }
                    figure.IsClosed = true;
                    figure.Freeze();
                    PathGeometry geometry = new PathGeometry();
                    geometry.Figures.Add(figure);
                    geometry.Freeze();
                    drawingContext.DrawGeometry(brushes[4], null, geometry);
                }
                drawingContext.Pop();
            }
        }

        // Properties
        public Color Color
        {
            get
            {
                return (Color)base.GetValue(ColorProperty);
            }
            set
            {
                base.SetValue(ColorProperty, value);
            }
        }

        public CornerRadius CornerRadius
        {
            get
            {
                return (CornerRadius)base.GetValue(CornerRadiusProperty);
            }
            set
            {
                base.SetValue(CornerRadiusProperty, value);
            }
        }

        public double Blur
        {
            get
            {
                return (double)base.GetValue(BlurProperty);
            }
            set
            {
                base.SetValue(BlurProperty, value);
            }
        }
    }


}
