   M S B A M L   `   `   `  ����B  >WpfDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfDemoWpfDemo  5      M ITimeSheet.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.ResourceTimeSheet.Resource 5           WpfDemo.View.ListUsersView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WpfDemo     5      Ml10n<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.Resource     5      -   5        $��ListAllUsers��5
      
�������.��5      �   � RowDefinitions  5   
   �� 5      $��* �6   .+�5   
   � -   5        $��frameworkElement��5       � DataContext 65   �� � 5   
   $��1��6   .+�5      ]� .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ ListUsersDataGrid��5      $	��900��5      $
��Left=�6'      AutoGenerateColumns$ False��6B      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6^      	RowHeight$ 35Q�6n   
�������$
��True��5      $
��Auto=�6@   $
��Auto=�6p      CanUserAddRows$ False��5         
IsReadOnly$
 True��62   $��18(�6D   
������ 
���   ���5   O   �� *
UserList+ �� SelectedItem 6p   �� *SelectedUser+	 J� InputBindings	 5      d� 5      
 d� MouseAction$
 LeftDoubleClicke�6'    �� Command 6E   �� *ShowUserProfilCommand+�� 5      
 �� Key$ Delete��6%    62   �� *DeleteCommand+   RowStyle 5       �� 5!       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    .�5"      ��  �� Property �@�ContextMenu �� Value 5#   "   �� 5$   &   y�62   �� *$"ListUsersViewContextMenuVisibility+ �� IsOpen 6l   �� * IsListUsersViewContextMenuOpen+ �� Mode$ OneWay=�.@�5%   *   m�  � Cursor$
 Hand��6�   $	��5 0q�6�      PopupMenuItem#���   6�    m� Command 63   �� *DataContext.DeleteCommand+ �� Source %  System.Windows.Markup.Reference  *frameworkElement+��5&   .   �� 5'   2    �� Orientation$ 
Horizontal=�6=   .+�5(   6   �� $W��QC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\delete.png��6<   $��20��6�   $	��5 0q�6�   �� 5)   6   $��19(�6p   $��1 0 3 0q�6~   ��6@   !   WpfDemo.ResxStaticExtension  *l10n:Resources.Delete+6~   5*   3   5,   +   5-   '   5/      �� 50       � �� �#eee6;   �� 51       � ��
 �   �6;    �� Triggers 52      P� 53   "    P� Property �@ 
IsSelected P� Value$
 true��6@   . �54   &   ��  � ��
 �����6C   �� 55   &    � ��
 �����6C   56   #   P� 57   "    �@J�IsMouseOver$
 true��6A   . �58   &   ��  � ��
 �����6C   �� 59   &    � ��
 �   �6C   5:   #   5<         	CellStyle 5>      �� 5?      *  $System.Windows.Controls.DataGridCell"  6    .�5@      ��  � ��
 ���� 6;   �� 5A       � ��
 ���� 6<   �� 5B       �@�Cursor$
 Hand��67   5C         Columns 5F      0  *System.Windows.Controls.DataGridTextColumn  5G      ,  &System.Windows.Controls.DataGridColumn   Width$ * 6~      FontSize$ 24Q�6�      Header 6-     *l10n:Resources.Username+1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6a   �� *
Username+    HeaderStyle  5H      �� 5I   "   =
  7System.Windows.Controls.Primitives.DataGridColumnHeader" 
 6(   .�5J   &   ��  � ��$ Medium"�6C   �� 5K   &    �@�Cursor$
 Hand��6?   5L   #   5N        5O      $ * 6�   $ 24Q�6�    6-     *l10n:Resources.FirstName+ 6b   �� *	FirstName+  5P      �� 5Q   "   " 
 6(   .�5R   &   ��  � ��$ Medium"�6C   �� 5S   &    �@�Cursor$
 Hand��6?   5T   #   5V        5W      $ * 6~   $ 24Q�6�    6-     *l10n:Resources.LastName+ 6a   �� *
LastName+  5X      �� 5Y   "   " 
 6(   .�5Z   &   ��  � ��$ Medium"�6C   �� 5[   &    �@�Cursor$
 Hand��6?   5\   #   5^      5`      5a      5b      5c      ! � 	Resources! 5e       ! MenuItem.Highlight.Background" �� Color  MenuItem.Highlight.Border  Menu.Disabled.Foreground * &MenuItem.Highlight.Disabled.Background & "MenuItem.Highlight.Disabled.Border  MenuItem.Selected.Border   MenuItem.Selected.Background  	Checkmark 	 Menu.Static.Foreground# �� BasedOn$ �� 
TargetType% ��Name& �� Triggers' �� 
TargetName( ]� 
Conditions) �� Property* �� Value��%�  &	       &	 #     &	 F     &	 i     &	 �     &	 �     &	 �     &	 �     &		 ^    &	  �    7m�7  7  7  7  7  �� 5f   
   $" 	#3D26A0DA��6@   �� 5g   
   $" 	#FF26A0DA��6<   �� 5h   
   $" 	#FF707070��6;   �� 5i   
   $" 	#0A000000��6I   �� 5j   
   $" 	#21000000��6E   �� 5k   
   $" 	#FF26A0DA��6;   �� 5l   
   $" 	#3D26A0DA��6?   �5m   
   UQF1 M 10.0,1.2 L 4.7,9.1 L 4.5,9.1 L 0,5.2 L 1.3,3.5 L 4.3,6.1L 8.3,0 L 10.0,1.2 Z�6x   �� 5n   
   $" 	#FF212121��69   �� 5o   
   8#   " m�6=   .�5p      ��  �@�OverridesDefaultStyle$
 True��66   �� 5q       � �� 5r      �� 5s      "$ m�6&   .R�5t      �� $% templateRoot��6!   ���#eee._�5u      �� $��Header��6/   5v      & 5w      P� 5x       66   Z�  �@m�IconP� 5y       �@m�	IsChecked$
 True��6;   P� 5z       �@m�IsHighlighted$
 True��6?   . �5{   "   �� $' templateRoot��6?   8   � ��6Y   �� 5|   "   $' templateRoot��6@   8   � ��6Z   5}      P� 5~       � {�$ False��6;   . �5   "   �� $' templateRoot��6K   8   � ��6e   5�      ]� 5�      ( 5�   "   �� 5�   &   ) �@m�IsHighlighted$
* True��6I   �� 5�   &   ) � {�$* False��6E   ./�5�   "   �� $' templateRoot��6?   8   � ��6Y   �� 5�   "   $' templateRoot��6@   8   � ��6Z   5�      5�      5�      5�      5�      5�      