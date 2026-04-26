using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Converters;
using System.Windows.Threading;
using Data;
using Logic;
using Presentation.Model;

namespace Presentation.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly DispatcherTimer m_Timer;

        private int? m_BallCount = 5;
        public int? BallCount
        {
            get => m_BallCount;
            set
            {
                m_BallCount = value;
                OnPropertyChanged(nameof(BallCount));
            }
        }

        public ObservableCollection<BallModel> BallsCollection { get; } = new();
        private ILogicManager m_LogicManager;
        
        private float m_CanvasWidth = 800;
        public float CanvasWidth
        {
            get => m_CanvasWidth;
            set
            {
                m_CanvasWidth = value;
                OnPropertyChanged();
                UpdateBorders();
            }
        }

        private float m_CanvasHeight = 450;
        public float CanvasHeight
        {
            get => m_CanvasHeight;
            set
            {
                m_CanvasHeight = value;
                OnPropertyChanged();
                UpdateBorders();
            }
        }

        private float m_Margin = 10;
        public float Margin => m_Margin;

        private float m_UIAreaHeight = 50;
        public float UIAreaHeight => m_UIAreaHeight;
        
        public ICommand AddCommand { get; }
        public ICommand CreateCommand { get; }
        
        public ViewModel()
        {
            m_LogicManager = new LogicManager(m_CanvasWidth, m_CanvasHeight);
            m_LogicManager.OnBallsUpdated += OnLogicUpdated;
            m_Timer = new DispatcherTimer();
            m_Timer.Interval = TimeSpan.FromMilliseconds(16);
            m_Timer.Tick += OnTick;
            m_Timer.Start();
            
            AddCommand = new RelayCommand(AddBalls);
            CreateCommand = new RelayCommand(CreateBalls);
        }
        
        private void AddBalls()
        {
            m_LogicManager.AddBalls(m_BallCount.HasValue ? m_BallCount.Value : 0);
        }

        private void CreateBalls()
        {
            m_LogicManager.AddBalls(m_BallCount.HasValue ? m_BallCount.Value : 0, true);
        }

        private void UpdateBorders()
        {
            if (m_LogicManager != null)
            {
                m_LogicManager.UpdateSize(m_CanvasWidth, m_CanvasHeight);
            }
        }

        private void OnTick(object? sender, EventArgs e)
        {
            m_LogicManager.Update();
        }

        private void OnLogicUpdated(object? sender, EventArgs e)
        {
            BallsCollection.Clear();
            List<Ball> Balls = m_LogicManager.GetBalls();
            foreach (Ball ball in Balls)
            {
                BallsCollection.Add(new BallModel(ball.Position, ball.Radius));
            }
        }
        
        public void OnCanvasSizeChanged(double w, double h)
        {
            m_CanvasWidth = (float)w;
            m_CanvasHeight = (float)h;
            UpdateBorders();
        }
    }
}
