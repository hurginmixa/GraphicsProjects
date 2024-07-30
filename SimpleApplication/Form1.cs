using CoordinateSystem;
using Timer = System.Windows.Forms.Timer;

namespace SimpleApplication;

public partial class Form1 : Form
{
    private readonly DoubleBufferedPanel _drawingPanel;
    private readonly Timer _animationTimer;
    private Point<DisplayCoordSystem> _circle = new(50, 50);
    private Shift<DisplayCoordSystem> _delta = new(2, 2);

    public Form1()
    {
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


        Transform<GraphicCoordSystem, DisplayCoordSystem> transform =
            new Transform<GraphicCoordSystem, DisplayCoordSystem>();

        Shift<DisplayCoordSystem> shift = transform * new Shift<GraphicCoordSystem>(12, 45);

        Point<GraphicCoordSystem> point = transform % (Point<DisplayCoordSystem>.Origin + shift);
    }

    private void DrawingPanel_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        // Рисуем отрезок
        Pen pen = new Pen(Color.Black, 2);
        g.DrawLine(pen, 10, 10, 200, 200);

        // Рисуем круг
        g.FillEllipse(Brushes.Red, (int) _circle.X, (int) _circle.Y, 50, 50);
    }

    private void AnimationTimer_Tick(object sender, EventArgs e)
    {
        // Изменяем координаты круга
        _circle += _delta;

        // Проверяем границы панели
        if (_circle.X < 0 || _circle.X + 50 > _drawingPanel.Width)
        {
            _delta = _delta.FlipX();
        }

        if (_circle.Y < 0 || _circle.Y + 50 > _drawingPanel.Height)
        {
            _delta = _delta.FlipY();
        }

        // Перерисовываем панель
        _drawingPanel.Invalidate(); // Это вызывает событие Paint для перерисовки панели
    }
}