   M S B A M L   `   `   `  ����G  CMySnipItTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MySnipItToolMySnipItTool  5      A%clr-namespace:MySnipItTool.PropertiesMySnipItTool.Properties  5      !    MySnipItTool.SettingsDialog   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:MySnipItTool     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      <
properties%clr-namespace:MySnipItTool.Properties     5      -   5          � Title$  Settings��5	      $��332.927��5
      $	��200��5      ���	#FFC7C7C7$��Segoe UI)�5       � 
ResizeMode$ NoResize=�5       � 	Resources 5         ToolBarButtonStyle �� 
TargetType �� Property �� Value��%w   &	        �� 5   
   " ��6+   .�5      ��  � ��
 ���� 6+   �� 5       � ��$ 0q�60   5      5      .��5      ��  �� Orientation$ Vertical=�6   .+�5   
   �� $��0,10,0,5q�5      $��20(�5      $��Settings��5       �� TextAlignment$ Center=�5      �� 5   
   �� 5    
   -   5        $��delayStackPanel��5!      $��0,10,0,0q�5"      $ 
Horizontal=�5#      .+�5$      �� $��	10,0,30,0q�5%      $��Center=�5&      $��Delay:��5'       � ToolTip$` ZThe time (in milliseconds) after the main window minimizes before the screenshot is taken.��5(      �� 5)      -   5        $��slider��5*      $	��128��5+      $��40,0,0,0q�5,      $��Center=�5-      	 �� IsSnapToTickEnabled	 . 
 � Maximum$

 2000Q�5/       � Minimum$ 0Q�50       �� TickFrequency$	 250Q�51       �� TickPlacement$ BottomRight=�52      �� 53      -   5        $��lblDelay��54      $��15,0,0,0q�55      $��Center=�56      $ Right=�58      ��57      ��  �� ElementName$ slider�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=�58      59      �� 5;   
   $��0,3,0,0q�6   $ 
Horizontal=�6&   .+�5<      �� $��10,0,0,0q�5=      $��Show Crosshairs:��5>      $~ xIf checked, the crosshairs are shown when a screen snippet is being taken. If unchecked, those crosshairs are not shown.��5?      �� 5@      -   5         ��Name$ checkBoxCrosshairs��5A      $��
180,0,10,0q�5B      $��Center=�5C       J� RenderTransformOrigin$ -3.512,0.333&�5D      5E      �� 5G   
   $��0,5,0,0q�6   $ 
Horizontal=�6&   .+�5H      �� $��10,0,0,0q�5I      $��Center=�5J      $��Crosshairs Color:��5K      �� 5L      -   5         ��Name$ btnCrosshairsColor��5M      $��20��5N      $��20��5O      $��	175,0,0,0q�5P      
���  ��
������ 5S      5T      �� 5U   
   $��0,5,0,10q�6   $ 
Horizontal=�6'   .+�5V      �� -   5        $��	btnCancel��5W      $��50��5X      $��	190,0,0,0q�5Y      $��14(�5[      .��5\      ��  �� Padding$ 3q�5]      $ -0.312,0.467&�5^      $��Cancel��5_      5`      �� 5a      -   5        $��btnOk��5b      $��50��5c      $��	10,0,10,0q�5d      $��14(�5f      .��5g      �� $ 3q�6   $��OK��6(   5h      5i      5j      5k      