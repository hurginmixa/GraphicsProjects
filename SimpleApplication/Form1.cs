using Timer = System.Windows.Forms.Timer;

namespace SimpleApplication;

public partial class Form1 : Form
{
    private readonly DoubleBufferedPanel _drawingPanel;
    private readonly Timer _animationTimer;
    private int _circleX;
    private int _circleY;
    private int _deltaX = 2;
    private int _deltaY = 2;

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

        _circleX = 50;
        _circleY = 50;
    }

    private void DrawingPanel_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        // Рисуем отрезок
        Pen pen = new Pen(Color.Black, 2);
        g.DrawLine(pen, 10, 10, 200, 200);

        // Рисуем круг
        g.FillEllipse(Brushes.Red, _circleX, _circleY, 50, 50);
    }

    private void AnimationTimer_Tick(object sender, EventArgs e)
    {
        // Изменяем координаты круга
        _circleX += _deltaX;
        _circleY += _deltaY;

        // Проверяем границы панели
        if (_circleX < 0 || _circleX + 50 > _drawingPanel.Width)
        {
            _deltaX = -_deltaX;
        }

        if (_circleY < 0 || _circleY + 50 > _drawingPanel.Height)
        {
            _deltaY = -_deltaY;
        }

        // Перерисовываем панель
        _drawingPanel.Invalidate(); // Это вызывает событие Paint для перерисовки панели
    }
}