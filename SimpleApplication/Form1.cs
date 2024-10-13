using CoordinateSystem;
using SimpleApplication.Models;
using CoordinateSystem.Privitives;

using Timer = System.Windows.Forms.Timer;

namespace SimpleApplication;

internal partial class Form1 : Form
{
    private readonly DoubleBufferedPanel _drawingPanel;
    private readonly Timer _animationTimer;
    private readonly IShapesModel _shapesModel;

    private readonly CircleModel _circleModel;

    public Form1(IShapesModel shapesModel)
    {
        _shapesModel = shapesModel;
        _circleModel = new CircleModel();

        InitializeComponent();
        Text = "Drawing Example";
        Size = new Size(800, 600);

        _drawingPanel = new DoubleBufferedPanel();
        _drawingPanel.Dock = DockStyle.Fill;
        _drawingPanel.BackColor = Color.White;
        _drawingPanel.Paint += DrawingPanel_Paint;
        _drawingPanel.SizeChanged += _drawingPanel_SizeChanged;
        Controls.Add(_drawingPanel);

        _animationTimer = new Timer();
        _animationTimer.Interval = 25; // Интервал в миллисекундах
        _animationTimer.Tick += AnimationTimer_Tick;
        _animationTimer.Start();
    }

    private void _drawingPanel_SizeChanged(object? sender, EventArgs e) => _circleModel.SetDrawPanelSize(width: _drawingPanel.Width, height: _drawingPanel.Height);

    private void DrawingPanel_Paint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        int panelCenterX = _drawingPanel.Width / 2;
        int panelCenterY = _drawingPanel.Height / 2;

        Pen pen = new Pen(Brushes.Gray);

        int l = 116;
        g.DrawEllipse(pen, (int) panelCenterX - l/2, (int) panelCenterY - l/2, l, l);

        DrawStrokes(g, _shapesModel, _drawingPanel);

        // Рисуем круг
        g.FillEllipse(Brushes.Red, (int)_circleModel.CirclePosition.X, (int)_circleModel.CirclePosition.Y, 50, 50);
    }

    private static void DrawStrokes(Graphics g, IShapesModel shapesModel, DoubleBufferedPanel panel)
    {
        IShape<GraphicSystem>[] shapes = shapesModel.Shapes.ToArray();
        if (!shapes.Any())
        {
            return;
        }

        Transform<GraphicSystem, DisplaySystem> tr = DisplayTransformPreparing();

        double minX = shapes.Select(s => s.Transform(tr)).Min(p => p.MinPoint.X);
        double minY = shapes.Select(s => s.Transform(tr)).Min(p => p.MinPoint.Y);
        double maxX = shapes.Select(s => s.Transform(tr)).Max(p => p.MaxPoint.X);
        double maxY = shapes.Select(s => s.Transform(tr)).Max(p => p.MaxPoint.Y);
        Point<GraphicSystem> strokesCenter = new Point<GraphicSystem>((minX + maxX) / 2, (minY + maxY) / 2);

        int panelCenterX = panel.Width / 2;
        int panelCenterY = panel.Height / 2;

        tr.AddShift(new Shift<DisplaySystem>(panelCenterX - strokesCenter.X, panelCenterY - strokesCenter.Y));

        Pen pen = new Pen(Color.Blue, 2);

        foreach (IShape<GraphicSystem> shape in shapes)
        {
            IShape<DisplaySystem> displayShape = shape.Transform(tr);

            switch (displayShape)
            {
                case Stroke<DisplaySystem> displayStroke:
                    int point1X = (int) displayStroke.Point1.X;
                    int point1Y = (int) displayStroke.Point1.Y;
                    int point2X = (int) displayStroke.Point2.X;
                    int point2Y = (int) displayStroke.Point2.Y;

                    g.DrawLine(pen, point1X, point1Y, point2X, point2Y);

                    break;

                case Rect<DisplaySystem> displayRect:
                    var mainDiagonal = displayRect.MainDiagonal;
                    var subDiagonal = displayRect.SubDiagonal;

                    List<PointF> vv =
                    [
                        new PointF() {X = (float) mainDiagonal.Point1.X, Y = (float) mainDiagonal.Point1.Y},
                        new PointF() {X = (float) subDiagonal.Point1.X, Y = (float) subDiagonal.Point1.Y},
                        new PointF() {X = (float) mainDiagonal.Point2.X, Y = (float) mainDiagonal.Point2.Y},
                        new PointF() {X = (float) subDiagonal.Point2.X, Y = (float) subDiagonal.Point2.Y},
                        new PointF() {X = (float) mainDiagonal.Point1.X, Y = (float) mainDiagonal.Point1.Y},
                    ];

                    g.DrawPolygon(pen, vv.ToArray());

                    break;
            }
        }
    }

    private static Transform<GraphicSystem, DisplaySystem> DisplayTransformPreparing()
    {
        Transform<GraphicSystem, DisplaySystem> tr = new Transform<GraphicSystem, DisplaySystem>();
        tr.AddFlipY();
        //tr.AddRotate(PI/4);
        return tr;
    }

    private void AnimationTimer_Tick(object? sender, EventArgs e)
    {
        _circleModel.TickProcess();

        _shapesModel.TickProcess();

        // Перерисовываем панель
        _drawingPanel.Invalidate(); // Это вызывает событие Paint для перерисовки панели
    }
}