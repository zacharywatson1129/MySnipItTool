   M S B A M L   `   `   `  ���� G  CMySnipItTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MySnipItToolMySnipItTool  A%clr-namespace:MySnipItTool.ViewModelsMySnipItTool.ViewModels      MySnipItTool.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:MySnipItTool     5vms%clr-namespace:MySnipItTool.ViewModels     -   $��16(�
�������  � Title$  MySnipItTool��$	��500��$	��800��$��Segoe UI)� � Icon$  /Views/mysnipittool-64.ico�� � 	Resources    ToolBarButtonStyle �� 
TargetType �� Property �� Value  DrawingModeButtonStyle �� BasedOn 	 hello � FillRule �� 
StartPoint	 �� EndPoint
 �� 
TargetType �� 
Background � Cursor �� Bullet �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName��%=  &	        &	 :     7d�&	 �     '	� h    �� " ��.���  � ��
 ���� ��  � ��$ 0q��� " 	�8   .���  � ��
 ���� ��  � ��$ 0q���  � ��$ 50����  � ��$ 50����  � ��$ 50����  � ��$ 50��F� 
���   �$��1�����#CCCCFF��� $ EvenOdd=�.���� $ 10,10&�$
	 10,0&��� $ 10,10&�$	 50,30&��� $ 10,10&�$	 50,30&��� " 	�.���  � �� �� "
 	�.R��� 
 �����$
 Hand�� � $��16��$��16��.+��� $��
RadioOuter��
������ 
�������$��2q�$ 2���� $��	RadioMark��$ 0��$��4q����	#FFADADAD$y�Hidden=�._��� $��3,1,0,0q����	#FF3E3E3E$��Calibri)�$��12(���  P�  �@d�	IsChecked$
 trueY�. ��� $ 	RadioMark�� � y�$ Visible=��� $ 
RadioOuter�� � �� �	#FFADADAD.���  � RowDefinitions �� $
��auto ��� $��35��$��60���� $��* ��� $
��auto � � ColumnDefinitions �� $��50���� .+�o� $��Center=�$��0��$��2��$��0q����	#FFF1F1F1$��25��$��22��$��16(�$��Source Sans Pro)�.@�m� $
��File��.@�m� $	��New��.@�m� -   $��Full Screenshot��m� -   $��Screen Snippet���� m� -   $��menuItemSave��$
��Save���� m� -   $��Settings���� m� -   $��Close��m� -   $��Minimize to Taskbar��m� $
��Edit��.@�m� -   $��menuItemCopy��$
��Copy��m� -	   $��menuItemDrawing��$��Drawing��.@�m� -
   $��Color��m� -   $��penShapeMenuItem��$��	Pen Shape��.@�	� -   $��radioBtnRound��.��Round	� -   $��radioBtnSquare��.��Squarem� -   $��drawingThicknessMenuItem��$��Drawing Thickness��.@�	� -   $��radioBtnSkinny��.��Skinny	� -   $��radioBtnNormal��.��Normal	� -   $��radioBtnThick��.��Thickm� $��About��.@�m� -   $��menuItemAboutMySnipItTool��$��About MySnipItTool��c� -   $��toolbar��$ Arrow��$��2��$��0��$��1�� `� IsLocked . ���	#FFF1F1F1$��0q�$��18(�$��55��.@��� -   $��btnFullScreenshot��#��[  $��55��.���  �� �� .+��� $��48��$��48��$��0,0,2,0q�$��Center=�$+��%../Images/Icons/fullscreenshot-48.png���� $��WrapWithOverflow=�$��1��$��Center=�$��Full Screenshot��$��90�� �� TextAlignment$ Center=��� -   $��btnScreenSnippet��.���  �� �� .+��� $��0,0,2,0q�$��Center=�$*��$../Images/Icons/screensnippet-48.png���� $��1��$��Center=�$��Screen Snippet��$��75��$��WrapWithOverflow=�$ Center=�a� -   $��basicToolsPanel�� �� Orientation$ 
Horizontal=�$y�Hidden=�.+��� -   $��btnCopy��#��[  .���� $!��../Images/Icons/copy-48.png���� -   $��btnSave��$��Center=�#��[  .���� $!��../Images/Icons/save-48.png���� -   $��btnDeleteTab��#��[  .���� $"��../Images/Icons/trash-48.png��a� -    a�Name$ drawingToolsPanel��$ Vertical=�$y�Visible=�$��2��$��0��$��2��.+�	� -   $��	btnEraser��#��[ .���� $"��../Images/Icons/erase-48.png��	� -   $��btnFreeDraw��#��[ .���� $��../Images/pencil48.png��$��40��$��40��	� -   $��btnLine��#��[ .���� 
���   �
���   �$��3��	 �� X1$ 0��	 �� Y1$ 35��	 �� X2$ 35��	 �� Y2$ 0��	� -   $��btnRectangle��#��[ $��50��$��50��.���� $��35��$��25��
���   �
�������	� -   $��	btnCircle��#��[ .��A� $��35��$��35��
���   �
�������	� -    $��btnText��#��[ .���� $!��../Images/Icons/text-64.png���� -!   $��btnColor��$��Stretch=�
���   �
������ $��40��$��40��$��2,4,2,2q��� -"   $��
tabControl��$��1��$��2��$��2��
������ $y�Visible=�