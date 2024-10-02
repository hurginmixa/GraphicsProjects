using System.Security.Cryptography.Xml;
using CoordinateSystem;
using SimpleApplication.Models;
using Timer = System.Windows.Forms.Timer;

using static System.Math;
using System.Drawing;

namespace SimpleApplication;

internal partial class Form1 : Form
{
    private readonly DoubleBufferedPanel _drawingPanel;
    private readonly Timer _animationTimer;
    private readonly IStrokesModel _strokesModel;

    private Point<DisplaySystem> _circle = new(50, 50);
    private Shift<DisplaySystem> _delta = new(2, 2);

    public Form1(IStrokesModel strokesModel)
    {
        _strokesModel = strokesModel;

        InitializeComponent();
        Text = "Drawing Example";
        Size = new Size(800, 600);

        _drawingPanel = new DoubleBufferedPanel();
        _drawingPanel.Dock = DockStyle.Fill;
        _drawingPanel.BackColor = Color.White;
        _drawingPanel.Paint += DrawingPanel_Paint;

        Controls.Add(_drawingPanel);

        _animationTimer = new Timer();
        _animationTimer.Interval = 25; // Интервал в миллисекундах
        _animationTimer.Tick += AnimationTimer_Tick;
        _animationTimer.Start();
    }

    private void DrawingPanel_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        int panelCenterX = _drawingPanel.Width / 2;
        int panelCenterY = _drawingPanel.Height / 2;

        Pen pen = new Pen(Brushes.Gray);

        int l = 116;
        g.DrawEllipse(pen, (int) panelCenterX - l/2, (int) panelCenterY - l/2, l, l);

        DrawStrokes(g, _strokesModel, _drawingPanel);

        // Рисуем круг
        g.FillEllipse(Brushes.Red, (int) _circle.X, (int) _circle.Y, 50, 50);
    }

    private static void DrawStrokes(Graphics g, IStrokesModel strokesModel, DoubleBufferedPanel panel)
    {
        Stroke<GraphicSystem>[] strokes = strokesModel.Strokes.ToArray();
        if (!strokes.Any())
        {
            return;
        }

        Transform<GraphicSystem, DisplaySystem> tr = DisplayTransformPreparing();

        double minX = strokes.Select(s => tr *s).Min(p => Math.Min(p.Point1.X, p.Point2.X));
        double minY = strokes.Select(s => tr *s).Min(p => Math.Min(p.Point1.Y, p.Point2.Y));
        double maxX = strokes.Select(s => tr *s).Max(p => Math.Max(p.Point1.X, p.Point2.X));
        double maxY = strokes.Select(s => tr *s).Max(p => Math.Max(p.Point1.Y, p.Point2.Y));
        Point<GraphicSystem> strokesCenter = new Point<GraphicSystem>((minX + maxX) / 2, (minY + maxY) / 2);

        int panelCenterX = panel.Width / 2;
        int panelCenterY = panel.Height / 2;

        tr.AddShift(new Shift<DisplaySystem>(panelCenterX - strokesCenter.X, panelCenterY - strokesCenter.Y));

        Pen pen = new Pen(Color.Blue, 2);

        foreach (var stroke in strokes)
        {
            Stroke<DisplaySystem> displayStroke = tr * stroke;

            g.DrawLine(pen, (int)displayStroke.Point1.X, (int)displayStroke.Point1.Y, (int)displayStroke.Point2.X, (int)displayStroke.Point2.Y);
        }
    }

    private static Transform<GraphicSystem, DisplaySystem> DisplayTransformPreparing()
    {
        Transform<GraphicSystem, DisplaySystem> tr = new Transform<GraphicSystem, DisplaySystem>();
        tr.AddFlipY();
        tr.AddRotate(PI/4);
        return tr;
    }

    private void AnimationTimer_Tick(object sender, EventArgs e)
    {
        // Изменяем координаты круга
        _circle += _delta;

        // Проверяем границы панели
        if (_circle.X < 0 || _circle.X + 50 > _drawingPanel.Width)
        {
            Transform<DisplaySystem, DisplaySystem> transform = new Transform<DisplaySystem, DisplaySystem>();
            transform.AddFlipX();

            _delta = transform * _delta;
        }

        if (_circle.Y < 0 || _circle.Y + 50 > _drawingPanel.Height)
        {
            Transform<DisplaySystem, DisplaySystem> transform = new Transform<DisplaySystem, DisplaySystem>();
            transform.AddFlipY();

            _delta = transform * _delta;
        }

        _strokesModel.TickProcess();

        // Перерисовываем панель
        _drawingPanel.Invalidate(); // Это вызывает событие Paint для перерисовки панели
    }
}