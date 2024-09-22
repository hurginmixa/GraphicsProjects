using System.Security.Cryptography.Xml;
using CoordinateSystem;
using SimpleApplication.Models;
using Timer = System.Windows.Forms.Timer;

using static System.Math;
using System.Drawing;

namespace SimpleApplication;

public partial class Form1 : Form
{
    private readonly DoubleBufferedPanel _drawingPanel;
    private readonly Timer _animationTimer;
    private readonly AstrixModel _astrixModel = new(160, 7);

    private Point<DisplaySystem> _circle = new(50, 50);
    private Shift<DisplaySystem> _delta = new(2, 2);

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
    }

    private void DrawingPanel_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        // Рисуем отрезок
        Pen pen = new Pen(Color.Black, 2);

        Point<DisplaySystem> p1 = new Point<DisplaySystem>(50, 50);
        Point<DisplaySystem> p2 = new Point<DisplaySystem>(200, 200);
        g.DrawLine(pen, (int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y);

        DrawAstrix(g);

        Transform<DisplaySystem, DisplaySystem> tr = new Transform<DisplaySystem, DisplaySystem>();
        tr.AddShift(new Shift<DisplaySystem>(100, 0));
        tr.AddStretch(1.5);
        //tr.AddRotate(PI / 20);

        pen = new Pen(Color.Magenta, 2);

        for (int i = 0; i < 4; i++)
        {
            var p1new = tr * p1;
            g.DrawLine(pen, (int)p1.X, (int)p1.Y, (int)p1new.X, (int)p1new.Y);
            
            var p2new = tr * p2;
            g.DrawLine(pen, (int)p2.X, (int)p2.Y, (int)p2new.X, (int)p2new.Y);

            p1 = p1new;
            p2 = p2new;
            g.DrawLine(pen, (int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y);
        }

        // Рисуем круг
        g.FillEllipse(Brushes.Red, (int) _circle.X, (int) _circle.Y, 50, 50);
    }

    private void DrawAstrix(Graphics g)
    {
        Transform<GraphicSystem, DisplaySystem> tr = new Transform<GraphicSystem, DisplaySystem>();

        int dispCenterX = _drawingPanel.Width / 2;
        int dispCenterY = _drawingPanel.Height / 2;
        tr.AddRotate(PI);
        tr.AddShift(new Shift<DisplaySystem>(dispCenterX, dispCenterY));

        var dispCenter = tr * new Point<GraphicSystem>(0,0);

        Pen pen = new Pen(Color.Blue, 2);

        foreach (Point<GraphicSystem> point in _astrixModel.Points)
        {
            Point<DisplaySystem> displayPoint = tr * point;

            g.DrawLine(pen, (int)dispCenter.X, (int)dispCenter.Y, (int)displayPoint.X, (int)displayPoint.Y);
        }

        Point<DisplaySystem> pFirst = tr * _astrixModel.Points.First();
        Point<DisplaySystem> pCurrent = pFirst;

        foreach (Point<GraphicSystem> point in _astrixModel.Points.Skip(1))
        {
            Point<DisplaySystem> pNext = tr * point;

            g.DrawLine(pen, (int)pCurrent.X, (int)pCurrent.Y, (int)pNext.X, (int)pNext.Y);

            pCurrent = pNext;
        }
        
        g.DrawLine(pen, (int)pCurrent.X, (int)pCurrent.Y, (int)pFirst.X, (int)pFirst.Y);
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

        _astrixModel.AddTick();

        // Перерисовываем панель
        _drawingPanel.Invalidate(); // Это вызывает событие Paint для перерисовки панели
    }
}