   M S B A M L   `   `   `  ����G  CMySnipItTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MySnipItToolMySnipItTool  5   	   A%clr-namespace:MySnipItTool.PropertiesMySnipItTool.Properties  5   	   !    MySnipItTool.SettingsDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:MySnipItTool     5   	   <
properties%clr-namespace:MySnipItTool.Properties     5   	   -   5          � 
ResizeMode$  NoResize=�5       � Title$ Settings��5	   	   $	��180��6   $��332.927��6'   ���	#FFC7C7C7 � 	Resources 5         ToolBarButtonStyle �� 
TargetType �� Property �� Value��%w   &	        �� 5   
   " ��6+   .�5      ��  � ��
 ���� 6+   �� 5       � ��$ 0q�60   5      5      .��5      ��  �� Orientation$ Vertical=�6   .+�5   
   �� $��Settings��6    �� TextAlignment$ Center=�6%   $��20(�6<   �� 5   
   -   5        $��delayStackPanel��5      $ 
Horizontal=�6,   .+�5      �� $��	10,0,30,0q�6    � ToolTip$` ZThe time (in milliseconds) after the main window minimizes before the screenshot is taken.��5      $��Delay:��5      $��Center=�6&   �� 5      -   5        $��btnDecrease��5   B   $��Center=�6   $��0,0,10,0q�60   
������ 
������ $��0q�6�   .��5      �� $��Center=�6   $��0,0,0,0q�63   $��Images/down.ico��6D   $��20��6]   $��20��6i   5      �� 5      -   5        $��slider��5      $��Center=�6#   $��0,0,0,0q�6>   $	��128��6O   	 �� TickPlacement$	 BottomRight=�6[   
 � Minimum$
 0Q�6w    �� TickFrequency$	 250Q�6�    � Maximum$
 2000Q�6�   �� 5      -   5        $��btnIncrease��5   B   $��Center=�6   $��10,0,0,0q�60   
������ 
������ $��0q�6�   .��5      �� $��Center=�6   $��Images/up.ico��63   $��20��6J   $��20��6V   5      �� 5       -   5        $��lblDelay��5    /   $��Center=�6   ��6?   ��  �� ElementName$ slider�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=�5!      �� 5#   
   $��0,3,0,0q�6   $ 
Horizontal=�6&   .+�5$      �� $��10,0,0,0q�6   $~ xIf checked, the crosshairs are shown when a screen snippet is being taken. If unchecked, those crosshairs are not shown.��5%      $��Show Crosshairs:��6�   �� 5&      -   5         ��Name$ checkBoxCrosshairs��5&   F   $��Center=�6   $��
180,0,10,0q�62    J� RenderTransformOrigin$ -3.512,0.333&�6b   5'      �� 5(   
   $��0 5 0 0q�6   $ 
Horizontal=�6&   .+�5)      �� $��10 0 0 0q�6   $��Crosshairs Color��6*   5*      �� 5+   
   $��0,5,0,10q�6   $ 
Horizontal=�6'   .+�5,      �� -   5        $��	btnCancel��5,      $��50��6&   $��	190,0,0,0q�61   $��14(�6D   .��5-      ��  �� Padding$ 3q�6   $��Cancel��6(   $ -0.312,0.467&�66   6]   5.      �� 5/      -	   5        $��btnOk��5/      $��50��6"   $��	10,0,10,0q�6-   $��14(�6@   .��50      �� $ 3q�6   $��OK��6(   51      52      53      54      