using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;

namespace SinAmplitudeAPI.Utils
{
    public static class Drawer
    {
        public static void DrawGrid(ref Image image, float xMax, float yMax, string fontPath)
        {
            image.Mutate(imgContext =>
            {
                var pen = Pens.Solid(Color.Gray);

                var points = new PointF[4]
                {
                    new PointF(50, 30),
                    new PointF(850, 30),
                    new PointF(850, 630),
                    new PointF(50, 630)
                };

                imgContext.DrawPolygon(pen, points);

                FontCollection collection = new();
                FontFamily family = collection.Add(fontPath);
                Font font = family.CreateFont(10);

                for (int i = 1; i < 10; i++)
                {
                    float y = 30 + i * 60;
                    var gorizontalLine = new PointF[2]
                    {
                        new PointF(50, y),
                        new PointF(850, y)
                    };

                    imgContext.DrawText(Math.Round(yMax - i * yMax / 10 * 2, 1).ToString(), font, Color.White, new PointF(20, y - 5));
                    imgContext.DrawLine(pen, gorizontalLine);
                }

                for (int i = 1; i < 10; i++)
                {
                    float x = 50 + i * 80;
                    var vertiaclLine = new PointF[2]
                    {
                        new PointF(x, 30),
                        new PointF(x, 630)
                    };
                    imgContext.DrawLine(pen, vertiaclLine);
                    imgContext.DrawText(Math.Round(i * xMax / 10, 1).ToString(), font, Color.White, new PointF(x - 10, 640));
                }

                imgContext.DrawText("0", font, Color.White, new PointF(50, 640));
                imgContext.DrawText(yMax.ToString(), font, Color.Wheat, new PointF(20, 25));
                imgContext.DrawText(xMax.ToString(), font, Color.White, new PointF(840, 640));
            });
        }

        public static void DrawSinWave(ref Image image, SineWaveModel model)
        {
            double w = 2 * Math.PI * model.SignalFrequancy / model.SamplingFrequency;
            double scaleX = 800 / model.PeriodCount;
            double scaleY = 600 / model.Amplitude / 2;

            int rest = 0;

            switch (model.PeriodCount % 3)
            {
                case 0:
                    rest++;
                    break;
                case 2:
                    rest += 2;
                    break;
                default:
                    break;
            }

            var points = new PointF[model.PeriodCount + rest];
            for (int i = 0; i < model.PeriodCount + rest; i++)
            {
                points[i] = new()
                {
                    X = (float)(50 + i * scaleX),
                    Y = (float)(-model.Amplitude * Math.Sin(i * w) * scaleY + 330)
                };
            }

            image.Mutate(imgContext =>
            {
                Pen pen = Pens.Solid(Color.Red);
                imgContext.DrawBeziers(pen, points);
            });
        }
    }
}
