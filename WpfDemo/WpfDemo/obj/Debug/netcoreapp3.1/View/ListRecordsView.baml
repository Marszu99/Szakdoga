   M S B A M L   `   `   `  ����B  >WpfDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfDemoWpfDemo  5      M ITimeSheet.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.ResourceTimeSheet.Resource 5      "    WpfDemo.View.ListRecordsView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WpfDemo     5      Ml10n<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.Resource     5      -   5          >�Name$  ListAllRecords��5
      
�������.��5      �  � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   � -   5        $��frameworkElement��5       � DataContext 65   �� � 5   
   $��1��6   .+�5      ]� .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ ListRecordsDataGrid��5      $	��900��5      $
��Left=�6'      AutoGenerateColumns$ False��6B      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6^      	RowHeight$ 30Q�6n   
�������$
��True��5      $
��Auto=�6@   $
��Auto=�6p      CanUserAddRows$ False��5         
IsReadOnly$
 True��62   $��20(�6D   
������ 
���   ���5   Q   �� *
RecordList+	 �� SelectedItem	 6t   �� *SelectedRecord+
 J� InputBindings
 5      �� 5      
 �� Key$ Delete��6%    �� Command 62   �� *DeleteCommand+   RowStyle 5      �� 5        �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    .�5!      ��  �� Property �@�ContextMenu �� Value 5"   "   �� 5#   &   y�62   �� *&$ListRecordsViewContextMenuVisibility+ �� IsOpen 6n   �� *" IsListRecordsViewContextMenuOpen+ �� Mode$ OneWay=�.@�5$   *   m�  � Cursor$
 Hand��6�   $	��5 0q�6�      PopupMenuItem#���   6�    m� Command 63   �� *DataContext.DeleteCommand+ �� Source %  System.Windows.Markup.Reference  *frameworkElement+��5%   .   �� 5&   2    �� Orientation$ 
Horizontal=�6=   .+�5'   6   �� $W��QC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\delete.png��6<   $��20��6�   $	��5 0q�6�   �� 5(   6   $��19(�6p   $��1 0 3 0q�6~   ��6@   !   WpfDemo.ResxStaticExtension  *l10n:Resources.Delete+6~   5)   3   5+   +   5,   '   5.      �� 5/       � �� �#eee6;   �� 50       � ��
 �   �6;    �� Triggers 51      P� 52   "    P� Property �@ 
IsSelected P� Value$
 true��6@   . �53   &   ��  � ��
 �����6C   �� 54   &    � ��
 �����6C   55   #   P� 56   "    �@J�IsMouseOver$
 true��6A   . �57   &   ��  � ��
 �����6C   �� 58   &    � ��
 �   �6C   59   #   5;         	CellStyle 5=      �� 5>      *  $System.Windows.Controls.DataGridCell"  6    .�5?      ��  � ��
 ���� 6;   �� 5@       � ��
 ���� 6<   �� 5A       �@�Cursor$
 Hand��67   5B         Columns 5E      0  *System.Windows.Controls.DataGridTextColumn  5F      ,  &System.Windows.Controls.DataGridColumn   Width$	 170 6      FontSize$ 20Q�6�      Header 6-     *l10n:Resources.User+1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6]   �� *User_Username+    
Visibility  5G   -   �� *+)DataContext.ListRecordsViewUserVisibility+   *frameworkElement+!   HeaderStyle! 5H      �� 5I   "   =
  7System.Windows.Controls.Primitives.DataGridColumnHeader" 
 6(   .�5J   &   ��  � ��$ Medium"�6C   �� 5K   &    �@�Cursor$
 Hand��6?   5L   #   5N        5O      $	 175 6|   $ 20Q�6�    6-     *l10n:Resources.Task+ 6]   �� *
Task_Title+! 5P      �� 5Q   "   " 
 6(   .�5R   &   ��  � ��$ Medium"�6C   �� 5S   &    �@�Cursor$
 Hand��6?   5T   #   5V        5W      $	 108 6�   $ 20Q�5X   -    5W   -     *l10n:Resources.Date+ 6]   �� *Date+" �� StringFormat$" 
yyyy.MM.dd��! 5Y      �� 5Z   "   " 
 6(   .�5[   &   ��  � ��$ Medium"�6C   �� 5\   &    �@�Cursor$
 Hand��6?   5]   #   # 	  ElementStyle# 5_      �� 5`   "   " ��6(   .�5a   &   ��  � ��$ Center=�6L   5b   #   5d        5e      $	 100 6�   $ 20Q�6�    6-     *l10n:Resources.Duration+ 6a   �� *DurationFormat+! 5f      �� 5g   "   " 
 6(   .�5h   &   ��  � ��$ Medium"�6C   �� 5i   &    �@�Cursor$
 Hand��6?   5j   #   # 5l      �� 5m   "   " ��6(   .�5n   &   ��  � ��$ Center=�6L   5o   #   5q        5r      $ * 6|   $ 20Q�6�    6-     *l10n:Resources.Comment+ 6`   �� *	Comment+! 5s      �� 5t   "   " 
 6(   .�5u   &   ��  � ��$ Medium"�6C   �� 5v   &    �@�Cursor$
 Hand��6?   5w   #   5y      5{      5|      �� 5}      $��50(�6[   $��0 0 0 0q�6i   $��Center=�6z   $��Center=�6�   $ J� Opacity$	$ 0.3Q�6�   ��6     **(l10n:Resources.RecordListNoRecordMessage+y�5~      �� *,*ListRecordsViewRecordListMessageVisibility+% � LayoutTransform% 5      �� 5�      & �� Angle$& 32Q�6&   5�      5�      5�      ' � 	Resources' 5�       ! MenuItem.Highlight.Background( �� Color  MenuItem.Highlight.Border  Menu.Disabled.Foreground * &MenuItem.Highlight.Disabled.Background & "MenuItem.Highlight.Disabled.Border  MenuItem.Selected.Border   MenuItem.Selected.Background  	Checkmark 	 Menu.Static.Foreground) �� BasedOn* �� 
TargetType+ ��Name, �� Triggers- �� 
TargetName. ]� 
Conditions/ �� Property0 �� Value��%�  &	       &	 #     &	 F     &	 i     &	 �     &	 �     &	 �     &	 �     &		 ^    &	  �    7m�7  7  7  7  7  �� 5�   
   $( 	#3D26A0DA��6@   �� 5�   
   $( 	#FF26A0DA��6<   �� 5�   
   $( 	#FF707070��6;   �� 5�   
   $( 	#0A000000��6I   �� 5�   
   $( 	#21000000��6E   �� 5�   
   $( 	#FF26A0DA��6;   �� 5�   
   $( 	#3D26A0DA��6?   �5�   
   UQF1 M 10.0,1.2 L 4.7,9.1 L 4.5,9.1 L 0,5.2 L 1.3,3.5 L 4.3,6.1L 8.3,0 L 10.0,1.2 Z�6x   �� 5�   
   $( 	#FF212121��69   �� 5�   
   8)   " m�6=   .�5�      ��  �@�OverridesDefaultStyle$
 True��66   �� 5�       � �� 5�      �� 5�      "* m�6&   .R�5�      �� $+ templateRoot��6!   ���#eee._�5�      �� $��Header��6/   5�      , 5�      P� 5�       66   Z�  �@m�IconP� 5�       �@m�	IsChecked$
 True��6;   P� 5�       �@m�IsHighlighted$
 True��6?   . �5�   "   �� $- templateRoot��6?   8   � ��6Y   �� 5�   "   $- templateRoot��6@   8   � ��6Z   5�      P� 5�       � {�$ False��6;   . �5�   "   �� $- templateRoot��6K   8   � ��6e   5�      ]� 5�      . 5�   "   �� 5�   &   / �@m�IsHighlighted$
0 True��6I   �� 5�   &   / � {�$0 False��6E   ./�5�   "   �� $- templateRoot��6?   8   � ��6Y   �� 5�   "   $- templateRoot��6@   8   � ��6Z   5�      5�      5�      5�      5�      5�      