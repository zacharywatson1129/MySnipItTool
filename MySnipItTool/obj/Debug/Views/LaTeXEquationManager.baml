   M S B A M L   `   `   `  ����G  CMySnipItTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:MySnipItTool.ViewsMySnipItTool.Views  5   	   B >WpfMath, Version=0.8.0.0, Culture=neutral, PublicKeyToken=nullD/clr-namespace:WpfMath.Controls;assembly=WpfMathWpfMath.Controls 5   	   -    'MySnipItTool.Views.LaTeXEquationManager   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:MySnipItTool.Views     5   	   Dcontrols/clr-namespace:WpfMath.Controls;assembly=WpfMath     5   	     � Title$  LaTeX Equation Manager��5	   	   $	��300��6(   $	��600��65   .��5
      �  � RowDefinitions 5   
   �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      �� 5      .+�5   
   �� $��2��6   5      %  WpfMath.Controls.FormulaControl  5   
   $��1��6"      Formula$* $\left(x^2 + 2 \cdot x + 2\right) = 0��6/   �� 5   
   -   5        $��1��5   4   $��0��6D   $��24(�6Q   $��Equation Text��6_   �� 5   
   -   5        $��Add To Canvas��5      $��1��6)   $��1��66   6F   �� 5   
   $��0��6   $��2��6   .@�5      �� $��Fraction��6   �� 5      $��
Power of 2��6   �� 5      $��
Power of 3��6   �� 5      $��Square Root��6   �� 5      $��	Cube Root��6   5       �� 5!   
   $��1��6   $��2��6   .��6.   Add To Equation6?   5"      5#      