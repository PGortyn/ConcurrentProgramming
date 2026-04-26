using System.Windows.Input;
namespace Presentation.ViewModel;

public class RelayCommand : ICommand
{
    private readonly Action m_Action;
        
    public RelayCommand(Action action)
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
