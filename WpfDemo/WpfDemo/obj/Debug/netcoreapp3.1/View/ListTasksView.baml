   M S B A M L   `   `   `  ����B  >WpfDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfDemoWpfDemo  5      M ITimeSheet.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.ResourceTimeSheet.Resource 5           WpfDemo.View.ListTasksView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WpfDemo     5      Ml10n<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.Resource     5      -   5          >�Name$  ListAllTasks��5
      
�������.��5      �  � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   � -   5        $��frameworkElement��5       � DataContext 65   �� � 5   
   $��1��6   .+�5      ]� .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ ListTasksDataGrid��5      $	��900��5      $
��Left=�6'      AutoGenerateColumns$ False��6B      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6^      	RowHeight$ 30Q�6n   
�������$
��True��5      $
��Auto=�6@   $
��Auto=�6p      CanUserAddRows$ False��5         
IsReadOnly$
 True��62   $��20(�6D   
������ 
���   ���5   O   �� *
TaskList+	 �� SelectedItem	 6p   �� *SelectedTask+
 J� InputBindings
 5      �� 5      
 �� Key$ Delete��6%    �� Command 62   �� *DeleteCommand+�� 5      $ T��6%    6-   �� *SpentTimeWithCommand+d� 5       d� MouseAction$ 	LeftClicke�6'    6?   �� *HasReadCommand+   RowStyle 5!      �� 5"       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    .�5#      ��  �� Property �@�ContextMenu �� Value 5$   "   �� 5%   &   .@�5'   *   m�  � Cursor$
 Hand��6�   $	��5 0q�6�      PopupMenuItem#���   6�    m� Command 63   �� *" DataContext.SpentTimeWithCommand+ �� Source %  System.Windows.Markup.Reference  *frameworkElement+��5(   .   �� 5)   2    �� Orientation$ 
Horizontal=�6=   .+�5*   6   �� $U��OC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\time.png��6<   $��20��6�   $	��5 0q�6�   �� 5+   6   $��19(�6s   $��1 0 3 0q�6�   ��6@   !   WpfDemo.ResxStaticExtension  *l10n:Resources.SpentTime+6�   5,   3   5.   +   m� 5/   *   $
 Hand��6�   $	��5 0q�6�   #���   51   3    5/   3   �� *DataContext.DeleteCommand+   *frameworkElement+y�50   3   �� *1/DataContext.ListTasksViewDeleteHeaderVisibility+   *frameworkElement+��52   .   �� 53   2   $ 
Horizontal=�6=   .+�54   6   �� $W��QC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\delete.png��6<   $��20��6�   $	��5 0q�6�   �� 55   6   $��19(�6p   $��1 0 3 0q�6~   ��6@     *l10n:Resources.Delete+6~   56   3   58   +   59   '   5;      �� 5<       �@�ToolTip 68   �� *NotificationText+�� 5=       � �� 6;   �� *ListTasksBackground+�� 5>       � ��
 �   �6;    �� Triggers 5?      P� 5@   "    P� Property �@ 
IsSelected P� Value$
 true��6@   . �5A   &   ��  � ��
 �����6C   �� 5B   &    � ��
 �����6C   5C   #   P� 5D   "    �@J�IsMouseOver$
 true��6A   . �5E   &   ��  � ��
 �����6C   �� 5F   &    � ��
 �   �6C   5G   #   5I         	CellStyle 5K      �� 5L      *  $System.Windows.Controls.DataGridCell"  6    .�5M      ��  � ��
 ���� 6;   �� 5N       � ��
 ���� 6<   �� 5O       �@�Cursor$
 Hand��67   5P         Columns 5S      0  *System.Windows.Controls.DataGridTextColumn  5T      ,  &System.Windows.Controls.DataGridColumn   Width$	 170 6      FontSize$ 20Q�6�      Header 6-     *l10n:Resources.User+1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6]   �� *User_Username+   
Visibility 5U   -   �� *)'DataContext.ListTasksViewUserVisibility+   *frameworkElement+    HeaderStyle  5V      �� 5W   "   =
  7System.Windows.Controls.Primitives.DataGridColumnHeader" 
 6(   .�5X   &   ��  � ��$ Medium"�6C   �� 5Y   &    �@�Cursor$
 Hand��6?   5Z   #   5\        5]      $	 175 6x   $ 20Q�6�    6-     *l10n:Resources.Title+ 6^   �� *Title+  5^      �� 5_   "   " 
 6(   .�5`   &   ��  � ��$ Medium"�6C   �� 5a   &    �@�Cursor$
 Hand��6?   5b   #   5d        5e      $ * 6�   $ 20Q�6�    6-     *l10n:Resources.Description+ 6d   �� *Description+  5f      �� 5g   "   " 
 6(   .�5h   &   ��  � ��$ Medium"�6C   �� 5i   &    �@�Cursor$
 Hand��6?   5j   #   5l        5m      $	 108 5n   -   $ 20Q�69    5m   -     *l10n:Resources.Deadline+ 6a   �� *
Deadline+! �� StringFormat$! 
yyyy.MM.dd��  5o      �� 5p   "   " 
 6(   .�5q   &   ��  � ��$ Medium"�6C   �� 5r   &    �@�Cursor$
 Hand��6?   5s   #   " 	  ElementStyle" 5u      �� 5v   "   " ��6(   .�5w   &   ��  � ��$ Center=�6L   5x   #   5z        5{      $	 120 6�   $ 20Q�6�    6-     *l10n:Resources.Status+ 6_   �� *TaskStatusString+  5|      �� 5}   "   " 
 6(   .�5~   &   ��  � ��$ Medium"�6C   �� 5   &    �@�Cursor$
 Hand��6?   5�   #   5�      5�      5�      �� 5�      $��50(�6W   $��0 0 0 0q�6e   $��Center=�6v   $��Center=�6�   # J� Opacity$	# 0.3Q�6�   ��6     *&$l10n:Resources.TaskListNoTaskMessage+y�5�      �� *(&ListTasksViewTaskListMessageVisibility+$ � LayoutTransform$ 5�      �� 5�      % �� Angle$% 32Q�6&   5�      5�      5�      & � 	Resources& 5�       ! MenuItem.Highlight.Background' �� Color  MenuItem.Highlight.Border  Menu.Disabled.Foreground * &MenuItem.Highlight.Disabled.Background & "MenuItem.Highlight.Disabled.Border  MenuItem.Selected.Border   MenuItem.Selected.Background  	Checkmark 	 Menu.Static.Foreground( �� BasedOn) �� 
TargetType* ��Name+ �� Triggers, �� 
TargetName- ]� 
Conditions. �� Property/ �� Value��%�  &	       &	 #     &	 F     &	 i     &	 �     &	 �     &	 �     &	 �     &		 ^    &	  �    7m�7  7  7  7  7  �� 5�   
   $' 	#3D26A0DA��6@   �� 5�   
   $' 	#FF26A0DA��6<   �� 5�   
   $' 	#FF707070��6;   �� 5�   
   $' 	#0A000000��6I   �� 5�   
   $' 	#21000000��6E   �� 5�   
   $' 	#FF26A0DA��6;   �� 5�   
   $' 	#3D26A0DA��6?   �5�   
   UQF1 M 10.0,1.2 L 4.7,9.1 L 4.5,9.1 L 0,5.2 L 1.3,3.5 L 4.3,6.1L 8.3,0 L 10.0,1.2 Z�6x   �� 5�   
   $' 	#FF212121��69   �� 5�   
   8(   " m�6=   .�5�      ��  �@�OverridesDefaultStyle$
 True��66   �� 5�       � �� 5�      �� 5�      ") m�6&   .R�5�      �� $* templateRoot��6!   ���#eee._�5�      �� $��Header��6/   5�      + 5�      P� 5�       66   Z�  �@m�IconP� 5�       �@m�	IsChecked$
 True��6;   P� 5�       �@m�IsHighlighted$
 True��6?   . �5�   "   �� $, templateRoot��6?   8   � ��6Y   �� 5�   "   $, templateRoot��6@   8   � ��6Z   5�      P� 5�       � {�$ False��6;   . �5�   "   �� $, templateRoot��6K   8   � ��6e   5�      ]� 5�      - 5�   "   �� 5�   &   . �@m�IsHighlighted$
/ True��6I   �� 5�   &   . � {�$/ False��6E   ./�5�   "   �� $, templateRoot��6?   8   � ��6Y   �� 5�   "   $, templateRoot��6@   8   � ��6Z   5�      5�      5�      5�      5�      5�      