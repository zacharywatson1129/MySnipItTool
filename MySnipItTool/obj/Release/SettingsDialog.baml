   M S B A M L   `   `   `  ���� G  CMySnipItTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MySnipItToolMySnipItTool  A%clr-namespace:MySnipItTool.PropertiesMySnipItTool.Properties  !    MySnipItTool.SettingsDialog   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:MySnipItTool     <
properties%clr-namespace:MySnipItTool.Properties     -     � 
ResizeMode$  NoResize=� � Title$ Settings��$	��180��$��332.927�����	#FFC7C7C7 � 	Resources    ToolBarButtonStyle �� 
TargetType �� Property �� Value��%D   &	        �� " ��.���  � ��
 ���� ��  � ��$ 0q�.����  �� Orientation$ Vertical=�.+��� $��Settings�� �� TextAlignment$ Center=�$��20(��� -   $��delayStackPanel��$ 
Horizontal=�.+��� $��	10,0,30,0q� � ToolTip$` ZThe time (in milliseconds) after the main window minimizes before the screenshot is taken.��$��Delay:��$��Center=��� -   $��btnDecrease��$��Center=�$��0,0,10,0q�
������ 
������ $��0q�.���� $��Center=�$��0,0,0,0q�$��Images/down.ico��$��20��$��20���� -   $��slider��$��Center=�$��0,0,0,0q�$	��128��	 �� TickPlacement$	 BottomRight=�
 � Minimum$
 0Q� �� TickFrequency$	 250Q� � Maximum$
 2000Q��� -   $��btnIncrease��$��Center=�$��10,0,0,0q�
������ 
������ $��0q�.���� $��Center=�$��Images/up.ico��$��20��$��20���� -   $��lblDelay��$��Center=�����  �� ElementName$ slider�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=��� $��0,3,0,0q�$ 
Horizontal=�.+��� $��10,0,0,0q�$~ xIf checked, the crosshairs are shown when a screen snippet is being taken. If unchecked, those crosshairs are not shown.��$��Show Crosshairs:���� -    ��Name$ checkBoxCrosshairs��$��Center=�$��
180,0,10,0q� J� RenderTransformOrigin$ -3.512,0.333&��� $��0 5 0 0q�$ 
Horizontal=�.+��� $��10 0 0 0q�$��Crosshairs Color���� $��0,5,0,10q�$ 
Horizontal=�.+��� -   $��	btnCancel��$��50��$��	190,0,0,0q�$��14(�.����  �� Padding$ 3q�$��Cancel��$ -0.312,0.467&��� -	   $��btnOk��$��50��$��	10,0,10,0q�$��14(�.���� $ 3q�$��OK��