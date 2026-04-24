using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        private int? m_BallCount;
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
        
        private double m_WindowWidth = 800;
        private double m_WindowHeight = 450;

        public double WindowWidth
        {
            get => m_WindowWidth;
            set
            {
                m_WindowWidth = value;
                OnPropertyChanged();
                UpdateBorders();
            }
        }

        public double WindowHeight
        {
            get => m_WindowHeight;
            set
            {
                m_WindowHeight = value;
                OnPropertyChanged();
                UpdateBorders();
            }
        }
        
        public ICommand AddCommand { get; }
        public ICommand CreateCommand { get; }
        
        public ViewModel()
        {
            AddCommand = new CommandRelay(AddBalls);
            CreateCommand = new CommandRelay(CreateBalls);
        }
        
        private void AddBalls()
        {
        
        }

        private void CreateBalls()
        {
            
        }

        private void UpdateBorders()
        {
            
        }
        
    }
}
