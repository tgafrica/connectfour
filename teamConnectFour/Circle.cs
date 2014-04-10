using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace teamConnectFour
{
   class Circle
   {
      private int diameter;
      private int locX = 0, locY = 0;
      private Ellipse circle = null;
      private Color owner;

      public Circle(int diameter, Color cirColor)
      {
         this.diameter = diameter;
         this.owner = cirColor;
      }

      public void SetLocation(int xCoord, int yCoord)
      {
         this.locX = xCoord;
         this.locY = yCoord;
      }

      public void SetColor(Color cirColor)
      {
         this.owner = cirColor;
      }

      public Color GetColor()
      {
         return owner;
      }

      public void Draw(Canvas canvas)
      {
         if (this.circle != null)
            canvas.Children.Remove(this.circle);
         else
            this.circle = new Ellipse();
         this.circle.Height = this.diameter;
         this.circle.Width = this.diameter;
         Canvas.SetBottom(this.circle, this.locY);
         Canvas.SetLeft(this.circle, this.locX);
         SolidColorBrush brush = new SolidColorBrush(owner);
         this.circle.Fill = brush;
         canvas.Children.Add(this.circle);
      }
   }
}