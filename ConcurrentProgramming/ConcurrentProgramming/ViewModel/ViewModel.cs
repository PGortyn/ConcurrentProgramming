using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    internal class CommandRelay : ICommand
    {
        private readonly Action m_Action;
        
        public CommandRelay(Action action)
        {
            m_Action = action;
        }
        
        public bool CanExecute(object? parameter)
        {
            //TODO 
            return true;
        }
        public void Execute(object? parameter)
        {
            m_Action();
        }
        public event EventHandler? CanExecuteChanged;
    }

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
                OnPropertyChanged();
            }
        }
        public ICommand AddCommand { get; }
        public ICommand CreateCommand { get; }
        
        public ViewModel()
        {
            AddCommand = new CommandRelay(AddBalls);
            CreateCommand = new CommandRelay(CreateBalls);
        }
        
        // protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        // {
        //     if (EqualityComparer<T>.Default.Equals(field, value))
        //         return false;
        //     field = value;
        //     OnPropertyChanged(propertyName);
        //     return true;
        // }
        
        private void AddBalls()
        {
        
        }

        private void CreateBalls()
        {
            
        }
        
        
        
    }
}
