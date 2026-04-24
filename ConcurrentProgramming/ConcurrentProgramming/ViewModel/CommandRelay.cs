using System.Windows.Input;
namespace Presentation.ViewModel;

public class CommandRelay : ICommand
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
