using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class BallModel : INotifyPropertyChanged
    {
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private Vector2 m_Position;
        // private Vector2 m_Velocity;
        private float m_Radius;
        public float Diameter => m_Radius * 2;

        public BallModel(Vector2 pos, float r)
        {
            // pos = new Vector2(pos.X + r, pos.Y + r);
            m_Position = pos;
            m_Radius = r;
        }

        public double PositionX
        {
            get => m_Position.X;
            set
            {
                m_Position = new Vector2((float)value, m_Position.Y); 
                OnPropertyChanged(nameof(PositionX));
            }
        }
        
        public double PositionY
        {
            get => m_Position.Y;
            set
            {
                m_Position = new Vector2(m_Position.X, (float)value); 
                OnPropertyChanged(nameof(PositionY));
            }
        }

        public double Left => m_Position.X - m_Radius;
        public double Top => m_Position.Y - m_Radius;

        // public double VelocityX
        // {
        //     get => m_Velocity.X;
        //     set
        //     {
        //         m_Velocity = new Vector2((float)value, m_Velocity.Y); 
        //         OnPropertyChanged(nameof(VelocityX));
        //     }
        // }
        //
        // public double VelocityY
        // {
        //     get => m_Velocity.Y;
        //     set
        //     {
        //         m_Velocity = new Vector2(m_Velocity.X, (float)value); 
        //         OnPropertyChanged(nameof(VelocityY));
        //     }
        // }

    }
}
