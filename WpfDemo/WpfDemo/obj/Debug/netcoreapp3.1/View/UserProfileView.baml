   M S B A M L   `   `   `  ����B  >WpfDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfDemoWpfDemo  5   	   M ITimeSheet.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.ResourceTimeSheet.Resource 5   	   "    WpfDemo.View.UserProfileView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfDemo     5   	   Ml10n<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.Resource     5   	   b ^Syncfusion.SfChart.WPF, Version=20.1310.0.47, Culture=neutral, PublicKeyToken=3d67ed1f87d44c892
syncfusion!http://schemas.syncfusion.com/wpf  5   	     � Title$  Worktime Registry��5
   	   $
��1550��6#   $	��870��60   
������� � WindowStartupLocation$ Manual=�5   	    � 
ResizeMode$ CanMinimize=�6(    � Left$ -8��5   	   
 � Top$ 0��6    � Icon$S MC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\WR.png��5   	   .��5      � $��	10 0 10 5q�6    � RowDefinitions 5   
   �� 5      $��0.35* �6   �� 5      $��2* �6   .+�5      �� $��0��6   $
��Bold"�6%   $��Verdana)�67   $��38(�6L   $��Center=�6Z   $	��Top=�6w   $��0 6 0 0q�5      �� 5      �6   �� *CurrentUser.Username+ �� UpdateSourceTrigger$ PropertyChanged=� �� 5      �6   !   WpfDemo.ResxStaticExtension  * l10n:Resources.UsernameProfile+5      � 5   
   $��0��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��7 0 0 0q�62   .+�5      �� $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\tasks.png��6   $��Bottom=�6r   $��0 0 0 1q�6�   $��39��6�   �� 5      $
��Bold"�6J   $��34(�6\   $��
-7 0 0 -10q�6j   $��50��6~   $��Bottom=�6�   $��
Constantia)�6�   ��6     *l10n:Resources.Tasks+6�   5       �� 5"      $��	0 0 807 1q�6   $��Bottom=�6,   $��Right=�6G   $ 
Horizontal=�6c   $��27��6|   .+�5#      (  "System.Windows.Controls.DatePicker  ���#eee	   SelectedDateFormat$	 Short=�5$      $	��120��68   $��25��6D   $��18(�6P   $��0q�6^   $��2 0 0 0q�6i   
   FirstDayOfWeek$
 Monday=�6{      SelectedDate 5#      �� *DeadlineFrom+$ PropertyChanged=� � 	Resources 5%      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value �� 
TargetType �Name ��Name �� Triggers P� Property P� Value��%�  '        '  i    �� 5&      "  6    .�5'      ��  � ��$ 0q�6@   �� 5(       � �� 5)   "   �� 5*   &   
dptext��"  6F   .R�5+   *   � .+�5,   .   � $ WatermarkContent��63   #��z��5-   3   #��z��5.   3   .+�5/   2   �� $ PART_ContentHost��6?   $��0q�6Y   #��z��50   ?   #��z��51   ?   52   /   53   +   54   '   56      57      �� 58      "  6    .�59      ��  � ��
 �   �6<    5:      P� 5;   "    �@J�IsMouseOver$
 True��6A   . �5<   &   ��  � �� �	#FF3C7FB16D   5=   #   P� 5>   "    �@J�IsKeyboardFocusWithin$
 True��6K   . �5?   &   ��  � �� �	#FF3C7FB16D   5@   #   5B      5C      5D      �� 5E      $��-��6   $��0 0 0 0q�6%   $��Center=�66   $��Center=�6Q   $��20(�6n     5F      ���#eee$	 Short=�5G      $	��120��68   $��25��6D   $��18(�6P   $��0 0 5 0q�6^   $��2 0 0 0q�6o   $
 Monday=�6�    5F      �� *
DeadlineTo+$ PropertyChanged=� 5H      ��%�  '        '  i    �� 5I      "  6    .�5J      ��  � ��$ 0q�6@   �� 5K       � �� 5L   "   �� 5M   &   
dptext��"  6F   .R�5N   *   � .+�5O   .   � $ WatermarkContent��63   #��z��5P   3   #��z��5Q   3   .+�5R   2   �� $ PART_ContentHost��6?   $��0q�6Y   #��z��5S   ?   #��z��5T   ?   5U   /   5V   +   5W   '   5Y      5Z      �� 5[      "  6    .�5\      ��  � ��
 �   �6<    5]      P� 5^   "    �@J�IsMouseOver$
 True��6A   . �5_   &   ��  � �� �	#FF3C7FB16D   5`   #   P� 5a   "    �@J�IsKeyboardFocusWithin$
 True��6K   . �5b   &   ��  � �� �	#FF3C7FB16D   5c   #   5e      5f      5g      �� 5i       #  SearchTextBoxWithWaterMarkStyle#���   6d   $��0 0 0 1q�6�   ���#eee$	��140��5j   ,   $��18(�68   $
��Left=�6F   $��1 1 0 1q�6h   
���   �$��2 0 0 1q�5k       � Cursor$ IBeam��6[   ��5i      �� *SearchTaskListValue+$ PropertyChanged=�
 � Tag 5k   ,     *l10n:Resources.Search+.��6j    5l      ��%d   '��       �� 5m      " ��6    .�5n      ��  �@��CornerRadius$ 4 0 0 4��6=   5o      5p      .��6)    J� InputBindings 5q      �� 5r      
 �� Key$ Enter��6%    �� Command 61   �� *SearchingTaskListCommand+.��5s   -   5t      �� 5u        SearchButtonStyle#���  6F   $��0 1 1 1q�6r   
���   �$
 Hand��6�   $��0 0 0 1q�5v      ��5u      �� *SearchingTaskListCommand+ � ToolTip 5v   *     *$"l10n:Resources.SearchButtonMessage+.��5w      �� $W��QC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\search.png��6    5x      ��%d   '��       �� 5y      " ��6    .�5z      ��  �@��CornerRadius$ 0 4 4 0��6=   5{      5|      5}      �� 5~      $��1q�6B   
���   �$
 Hand��6j   $��5 0 0 1q�6x   ��6   �� *ResetTaskListCommand+ 5        *l10n:Resources.ResetList+.��5�      �� $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\reset.png��6    5�      ��%^   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$ 4��6=   5�      5�      5�      5�      �� 5�      $ 
Horizontal=�6   $��
822 0 0 -5q�62   .+�5�      �� $W��QC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\record.png��6   $��Bottom=�6s   $��0 0 0 4q�6�   $��41��6�   �� 5�      $
��Bold"�6L   $��34(�6^   $��	-7 0 0 -5q�6l   $��Bottom=�6   $��
Constantia)�6�   ��6     *l10n:Resources.Records+6�   5�      �� 5�      $��0 0 2 1q�6   $��Bottom=�6*   $��Right=�6E   $ 
Horizontal=�6a   .+�5�      �� $ Vertical=�6   .+�5�        ���#eee$	 Short=�5�   !   $	��120��6<   $��25��6H   $��18(�6T   $��0q�6b   $��2 0 0 0q�6m   $
 Monday=�6    5�   !   �� *
DateFrom+$ PropertyChanged=� 5�      ��%�  '        '  i    �� 5�      "  6$   .�5�   "   ��  � ��$ 0q�6D   �� 5�   "    � �� 5�   &   �� 5�   *   
dptext��"  6J   .R�5�   .   � .+�5�   2   � $ WatermarkContent��67   #��z��5�   3   #��z��5�   3   .+�5�   6   �� $ PART_ContentHost��6C   $��0q�6]   #��z��5�   ?   #��z��5�   ?   5�   3   5�   /   5�   +   5�   #   5�      �� 5�      "  6$   .�5�   "   ��  � ��
 �   �6@    5�   "   P� 5�   &    �@J�IsMouseOver$
 True��6E   . �5�   *   ��  � �� �	#FF3C7FB16H   5�   '   P� 5�   &    �@J�IsKeyboardFocusWithin$
 True��6O   . �5�   *   ��  � �� �	#FF3C7FB16H   5�   '   5�      5�      5�        5�      ���#eee$	 Short=�5�   !   $	��120��6<   $��25��6H   $��18(�6T   $��0 3 0 0q�6b   $��2 0 0 0q�6s   $
 Monday=�6�    5�   !   �� *DateTo+$ PropertyChanged=� 5�      ��%�  '        '  i    �� 5�      "  6$   .�5�   "   ��  � ��$ 0q�6D   �� 5�   "    � �� 5�   &   �� 5�   *   
dptext��"  6J   .R�5�   .   � .+�5�   2   � $ WatermarkContent��67   #��z��5�   3   #��z��5�   3   .+�5�   6   �� $ PART_ContentHost��6C   $��0q�6]   #��z��5�   ?   #��z��5�   ?   5�   3   5�   /   5�   +   5�   #   5�      �� 5�      "  6$   .�5�   "   ��  � ��
 �   �6@    5�   "   P� 5�   &    �@J�IsMouseOver$
 True��6E   . �5�   *   ��  � �� �	#FF3C7FB16H   5�   '   P� 5�   &    �@J�IsKeyboardFocusWithin$
 True��6O   . �5�   *   ��  � �� �	#FF3C7FB16H   5�   '   5�      5�      5�      5�      �� 5�      $ Vertical=�6   $��5 0 5 0q�64   .+�5�      �� ���#eee$��55��6s   $��19(�6~   $��0 0 0 1q�6�   $��0 -1 0 0q�6�   $��Center=�5�      $��1q�6B   
���   �$��25��6j   ��5�      �� *DurationFromFormat+$ 	LostFocus=�.��5�   v    5�      ��%^   '��       �� 5�      " ��6$   .�5�   "   ��  �@��CornerRadius$ 4��6A   5�      5�      .��6-   5�      �� 5�      ���#eee$��55��6q   $��19(�6|   $��0 2 0 1 q�6�   $��0 -1 0 0q�6�   $��Center=�5�      $��1q�6B   
���   �$��25��6j   ��5�      �� *DurationToFormat+$ 	LostFocus=�.��5�   v    5�      ��%^   '��       �� 5�      " ��6$   .�5�   "   ��  �@��CornerRadius$ 4��6A   5�      5�      .��6-   5�      5�      �� 5�      #���   6f   $��0 0 0 1q�6�   ���#eee$	��140��5�      $��27��6&   $��18(�62   $
��Left=�6@   $��Bottom=�6b   $��1 1 0 1q�6}   
���   �$��2 0 0 1q�5�      $ IBeam��6[   ��5�      �� *SearchRecordListValue+$ PropertyChanged=� 5�   ,     *l10n:Resources.Search+.��6j    5�      ��%d   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$ 4 0 0 4��6=   5�      5�      .��6)    5�      �� 5�      $ Enter��6%    61   �� *SearchingRecordListCommand+.��5�   -   5�      �� 5�      #���  6H   $��27��6t   $��Bottom=�6�   $��0 1 1 1q�5�      
���   �$
 Hand��6G   $��0 0 0 1q�6U   ��5�      �� *SearchingRecordListCommand+ 5�   f     *$"l10n:Resources.SearchButtonMessage+.��5�      �� $W��QC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\search.png��6    5�      ��%d   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$ 0 4 4 0��6=   5�      5�      5      �� 5     $��27��6D   $��Bottom=�6P   $��1q�6k   
���   �$
 Hand��6�   $��5 0 0 1q�5     ��5     �� *ResetRecordListCommand+ 5  *     *l10n:Resources.ResetList+.��5     �� $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\reset.png��6    5     ��%^   '��       �� 5     " ��6    .�5     ��  �@��CornerRadius$ 4��6=   5     5     5	     5
     5     � 5  
   $��2��6    � ColumnDefinitions 5     �� 5     $��* �6#   �� 5     $��95 �6#   �� 5     $��* �6#    5     �� 5     $��* �6    �� 5     $
��1.2* �6    .+�5     �� -   5         ��Name$ AddTaskToUserTaskButton��5     $��0��6Z   $��1��6g   $��	6 2.5 0 0q�5     $	��5 0q�6(   $��32��66   $
��Left=�6B   $��Center=�6]   $	��Top=�6�   $��1q�5     
���   �$
 Hand��6=   ��5  6   �� *ShowTaskCommand+y�5  K   �� *AddTaskButtonVisibility+ 5       *l10n:Resources.CreateNewTask+.��5     �� $\��VC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\create-task.png��6    5     ��%^   '��       �� 5     " ��6   .�5      ��  �@��CornerRadius$ 4��69   5!     5"     5#     � 5%     -   5        $��frameworkElement��5%      � DataContext 69   �� &   System.Windows.Controls.DataGrid  5(     -   5          Name$ UserTasksDataGrid��5(     $
��Left=�5)     $	��710��62       AutoGenerateColumns$  False��6>   !   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$! * 6Z   
�������
���   �$��1q�5*     $
��True��6+   $
��Auto=�6P   $
��Auto=�5+     "   CanUserAddRows$" False��6I   #   
IsReadOnly$
# True��6`   $��20(�6r   $��0 0 0 20q�6�   $��0��5,     $��0��6$   $��2��64   $   	RowHeight$$ 30Q�6H   
���   ���5(  K   �� *
TaskList+% �� SelectedItem% 6l   �� *SelectedTask+ 5.     d� 5/     & d� MouseAction$& 	LeftClicke�6#    6;   �� *ShowTaskCommand+�� 50     $ Delete��6!    6.   �� *DeleteCommand+�� 51     $ T��6!    6)   �� *SpentTimeWithCommand+'   RowStyle' 54     �� 55     )  #System.Windows.Controls.DataGridRow"  6   .�56     ��  �@�ContextMenu 57     �� 58  "   .@�5:  &   m� $
 Hand��6�   $	��5 0q�6�     PopupMenuItem#���  6�   ( m� Command( 6/   �� *" DataContext.SpentTimeWithCommand+) �� Source) %  System.Windows.Markup.Reference  *frameworkElement+��5;  *   �� 5<  .   $ 
Horizontal=�69   .+�5=  2   �� $U��OC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\time.png��68   $��20��6�   $	��5 0q�6�   �� 5>  2   $��19(�6o   $��1 0 3 0q�6}   ��6<     *l10n:Resources.SpentTime+6}   5?  /   5A  '   m� 5B  &   $
 Hand��6�   $	��5 0q�6�   #���  5D  3   ( 5B  /   �� *DataContext.DeleteCommand+)   *frameworkElement+y�5C  3   �� *75DataContext.UserProfileViewTasksContextMenuVisibility+)   *frameworkElement+��5E  *   �� 5F  .   $ 
Horizontal=�69   .+�5G  2   �� $W��QC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\delete.png��68   $��20��6�   $	��5 0q�6�   �� 5H  2   $��19(�6l   $��1 0 3 0q�6z   ��6<     *l10n:Resources.Delete+6z   5I  /   5K  '   5L  #   5N     �� 5O      � �� �#eee67   �� 5P      � ��
 �   �67    5Q     P� 5R      �@ 
IsSelected$
 true��6<   . �5S  "   ��  � ��
 �����6?   �� 5T  "    � ��
 �����6?   5U     P� 5V      �@J�IsMouseOver$
 true��6=   . �5W  "   ��  � ��
 �����6?   �� 5X  "    � ��
 �   �6?   5Y     5[     *   	CellStyle* 5]     �� 5^     *  $System.Windows.Controls.DataGridCell"  6   .�5_     ��  � ��
 ���� 67   �� 5`      � ��
 ���� 68   �� 5a      �@�Cursor$
 Hand��63   5b     +   Columns+ 5e     0	  *System.Windows.Controls.DataGridTextColumn	  5f     ,
  &System.Windows.Controls.DataGridColumn, 
  Width$	, 160 6t   - 
  Header- 6)     *l10n:Resources.Title+1  +System.Windows.Controls.DataGridBoundColumn.   Binding. 6Z   �� *Title+/ 
  HeaderStyle/ 5g     �� 5h     =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6$   .�5i  "   ��  � ��$ Medium"�6?   �� 5j  "    �@�Cursor$
 Hand��6;   5k     5m     	  5n     $, * 6�   - 6)     *l10n:Resources.Description+. 6`   �� *Description+/ 5o     �� 5p     "  6$   .�5q  "   ��  � ��$ Medium"�6?   �� 5r  "    �@�Cursor$
 Hand��6;   5s     5u     	  5v     $	, 105 6�   - 6)     *l10n:Resources.Deadline+. 6]   �� *
Deadline+0 �� StringFormat$0 
yyyy.MM.dd��/ 5w     �� 5x     "  6$   .�5y  "   ��  � ��$ Medium"�6?   �� 5z  "    �@�Cursor$
 Hand��6;   5{     1   ElementStyle1 5}     �� 5~     " ��6$   .�5  "   ��  � ��$ Center=�6H   5�     5�     	  5�     $	, 120 6�   - 6)     *l10n:Resources.Status+. 6[   �� *TaskStatusString+/ 5�     �� 5�     "  6$   .�5�  "   ��  � ��$ Medium"�6?   �� 5�  "    �@�Cursor$
 Hand��6;   5�     5�     5�     �� 5�     $��0��6   $��0��6%   $��40(�6x   $��0 0 0 0q�6�   $��Center=�6�   $��Center=�6�   2 J� Opacity$	2 0.3Q�6�   ��65     **(l10n:Resources.TaskListUserNoTaskMessage+y�5�     �� **(UserProfileViewTaskListMessageVisibility+3 � LayoutTransform3 5�     �� 5�     4 �� Angle$4 16Q�6&   5�     '  !Syncfusion.UI.Xaml.Charts.SfChart  5�     $��1��6!   $��0��6.   
�������$��25(�6�   $��Medium"�5�  !   $��0q�65   
���   �$��1q�6T   )  #Syncfusion.UI.Xaml.Charts.ChartBase5   Header5 5�  U     * l10n:Resources.DurationOnTasks+6   Series.6 5�     )  #Syncfusion.UI.Xaml.Charts.PieSeries  /  )Syncfusion.UI.Xaml.Charts.ChartSeriesBase7   XBindingPath$7 Title��6P   6  0Syncfusion.UI.Xaml.Charts.AccumulationSeriesBase8   YBindingPath$8 Duration��6e   9   ShowTooltip$
9 True��6}   ,  &Syncfusion.UI.Xaml.Charts.ChartTooltip:   TooltipMargin$: 15q�5�  '   ;   ShowDuration$
; 3000��6R   2  ,Syncfusion.UI.Xaml.Charts.CircularSeriesBase<   	GroupMode$< Value=�6~   =   LabelPosition$= Inside=�5�  '   >   ItemsSource> 5�  '   �� *TaskListForChart+/  )Syncfusion.UI.Xaml.Charts.AdornmentSeries?   AdornmentsInfo? 5�     2  ,Syncfusion.UI.Xaml.Charts.ChartAdornmentInfo  5�     6  0Syncfusion.UI.Xaml.Charts.ChartAdornmentInfoBase@   FontSize$@ 14Q�68   A   	ShowLabel$
A True��6F   B   SegmentLabelContent$B 
Percentage=�6W   C   TooltipTemplateC 5�     �� 5�     .R�5�     �� $ 
Horizontal=�6)   .+�5�  "   �� 
�������$��Medium"�6[   $��13(�6o   $��Center=�5�  ,   $��Center=�6I   ��5�  ,   �� *
Item.Title+5�  I   �� 5�  "   $��: ��6,   
�������$��Medium"�6I   $��13(�6]   $��Center=�6k   $��Center=�5�  ,   �� 5�  "   
�������$��Medium"�6d   $��13(�6x   $��Center=�5�  ,   $��Center=�6I   ��5�  ,   �� *Item.DurationFormat+5�  I   5�     5�     5�     D   LegendD 5�     +  %Syncfusion.UI.Xaml.Charts.ChartLegend  5�     $��18(�6-   E   LegendPosition$E Inside=�6;   F   Orientation$F Vertical=�6S   $��Right=�6j   $	��Top=�5�  -   G   
IconHeight$G 15Q�6E   H   	IconWidth$H 15Q�6U   5�     �� 5�     $��1��6   $��0��6%   $��40(�6x   $��0 0 0 0q�6�   $��Center=�6�   $��Center=�6�   $	2 0.3Q�6�   ��65     **(l10n:Resources.TaskListUserNoTaskMessage+y�5�     �� **(UserProfileViewTaskListMessageVisibility+3 5�     �� 5�     $4 24Q�6&   5�     �� 5�     $��1��6   $��0��6$   $	��2 0q�6�   $��1 1 0 0q�5�     ���	#FFE5E5E5$��30��6?   $	��120��6K   $��16(�6W   $
��Left=�6e   $	��Top=�5�     $
��Left=�6/   I �� SelectedValuePath$	I Key��5�     J �� DisplayMemberPath$J Value��6/     theComboBox#���  6I   ��5�  4   �� *!ChartTimeSorterListForPieSeries+$ PropertyChanged=�K �� SelectedValueK 5�  J   �� *ChartTimeSorterForPieSeries+$ PropertyChanged=� 5�     ��%�   '��       �� 5�     " ��6:    5�     P� 5�      �@J�IsMouseOver$
 true��6=   . �5�  "   ��  �@�Cursor$
 Hand��6;   5�     5�     5�     5�       5�     -   5        $ UserRecordsDataGrid��5�     $  False��6p   
�������$	��697��5�     $
��Left=�6#   
���   �$��1q�6R   $
��True��6f   $
��Auto=�6�   $
��Auto=�5�     $" False��6I   $
# True��6`   $��20(�6r   $��0��5�     $��2��6$   $��2��64   $��	13 0 0 20q�6H   $$ 30Q�6[   
���   ���5�  M   �� *
RecordList+' 5�     �� 5�     "  6   .�5�     ��  � �� �#eee67   �� 5�      � ��
 �   �67    5�     P� 5�      �@ 
IsSelected$
 true��6<   . �5�  "   ��  � ��
 �����6?   �� 5�  "    � ��
 �����6?   5�     P� 5�      �@J�IsMouseOver$
 true��6=   . �5�  "   ��  � ��
 �����6?   �� 5�  "    � ��
 �   �6?   5�     5�     * 5�     �� 5�     "  6   .�5�     ��  � ��
 ���� 67   �� 5�      � ��
 ���� 68   �� 5�      �@�Cursor$
 Hand��63   5�     + 5�     	  5�     $	, 105 6�   - 6)     *l10n:Resources.Date+. 6Y   �� *Date+$0 
yyyy.MM.dd��/ 5�     �� 5�     "  6$   .�5�  "   ��  � ��$ Medium"�6?   �� 5�  "    �@�Cursor$
 Hand��6;   5�     1 5�     �� 5�     " ��6$   .�5�  "   ��  � ��$ Center=�6H   5�     5�     	  5�     $
, Auto 6�   - 6)     *l10n:Resources.Duration+. 6]   �� *DurationFormat+/ 5�     �� 5�     "  6$   .�5�  "   ��  � ��$ Medium"�6?   �� 5�  "    �@�Cursor$
 Hand��6;   5�     1 5�     �� 5�     " ��6$   .�5�  "   ��  � ��$ Center=�6H   5�     5�     	  5�     $, * 6x   - 6)     *l10n:Resources.Comment+. 6\   �� *	Comment+/ 5�     �� 5      "  6$   .�5  "   ��  � ��$ Medium"�6?   �� 5  "    �@�Cursor$
 Hand��6;   5     5     5     �� 5     $��0��6   $��2��6%   $��40(�6g   $��0 0 0 0q�6u   $��Center=�6�   $��Center=�6�   $	2 0.3Q�6�   ��65   �� *" UserProfileViewRecordListMessage+y�5	     �� *,*UserProfileViewRecordListMessageVisibility+3 5
     �� 5     $4 16Q�6&   5       5     -   5        $��MyChart��5  !   $��1��60   $��2��6=   $��25(�6�   $��Medium"�6�   
�������$��6 0 0 0q�5  8   
���   �$��1q�6]   5 5  M     *'%l10n:Resources.TaskRecordingDurations+L   PrimaryAxisL 5     ,  &Syncfusion.UI.Xaml.Charts.CategoryAxis  5     )  #Syncfusion.UI.Xaml.Charts.ChartAxisM   LabelFormat$M 
yyyy.MM.dd��6^   $��15(�6w   $��Normal"�6�   N   HeaderN 6.     *l10n:Resources.Date+O   HeaderStyleO 5     *  $Syncfusion.UI.Xaml.Charts.LabelStyle  5     P   FontSize$P 18Q�64   Q   
Foreground$Q Black��6B   5     R   SecondaryAxisR 5     ,  &Syncfusion.UI.Xaml.Charts.TimeSpanAxis  5     -   5        $��15(�5  b   $��Normal"�6p   S   IntervalS 01:00:006�   N 6.     *l10n:Resources.Duration+O 5       5     $P 18Q�64   $Q Black��6B   5     .6 5      ,  &Syncfusion.UI.Xaml.Charts.ColumnSeries  $
7 Date��6U   ,  &Syncfusion.UI.Xaml.Charts.XyDataSeriesT   YBindingPath$T DurationDouble��6i   $
9 True��6�   $: 15q�5!  *   $
; 3000��6U   U   SegmentSpacing$	U 0.7Q�6�   > 5   *   �� *RecordListForChart+C 5,     �� 5-     .R�5.     �� 
�������$��Medium"�6`   $��13(�6t   $��Center=�5/  ,   $��Center=�6I   ��5.  (   �� *Item.DurationFormat+5/  I   50     52     53     �� 54     $��1��6   $��2��6%   $��38(�6g   $��60 0 0 0q�6u   $��Center=�6�   $��Center=�6�   $	2 0.3Q�6�   ��65   �� *" UserProfileViewRecordListMessage+y�55     �� *,*UserProfileViewRecordListMessageVisibility+3 56     �� 57     $4 24Q�6&   59     �� 5<     $��1��6   $��2��6$   $	��2 0q�6�   $��13 1 0 0q�5=     ���	#FFE5E5E5$��30��6@   $	��120��6L   $��16(�6X   $
��Left=�6f   $	��Top=�5>     $
��Left=�6/   $	I Key��5?     $J Value��6/   #���  6I   ��5<  4   �� *#!ChartTimeSorterListForColumSeries+$ PropertyChanged=�K 5>  J   �� *ChartTimeSorterForColumSeries+$ PropertyChanged=� 5@     ��%�   '��       �� 5A     " ��6:    5B     P� 5C      �@J�IsMouseOver$
 true��6=   . �5D  "   ��  �@�Cursor$
 Hand��6;   5E     5G     5H     5I     5J     5K      5M      ! MenuItem.Highlight.BackgroundV �� Color  MenuItem.Highlight.Border  Menu.Disabled.Foreground * &MenuItem.Highlight.Disabled.Background & "MenuItem.Highlight.Disabled.Border 	 MenuItem.Selected.Border  
 MenuItem.Selected.Background  	Checkmark  Menu.Static.ForegroundW �� BasedOnX ��NameY �� TriggersZ �� 
TargetName[ ]� 
Conditions\ �� Property] �� Value  FocusVisual^ �� StrokeDashArray_ J� SnapsToDevicePixels  Button.Static.Background  Button.Static.Border  Button.Disabled.Background  Button.Disabled.Border  Button.Disabled.Foreground` ��Namea ��Name  ComboBoxToggleButtonb �� CornerRadiusc F�Name  ComboBoxTextBoxd �� 	ClickModee d� 	IsCheckedf �� Pathg �� Modeh �� RelativeSourcei J� IsHitTestVisiblej �� SelectionBoxItemk �� SelectionBoxItemTemplatel ��Namem �� 
IsReadOnlyn �� 
IsReadOnlyo �� IsDropDownOpenp � AllowsTransparencyq � ActualWidthr �� MaxDropDownHeights K� IsItemsHost-  'System.Windows.Input.KeyboardNavigationt   DirectionalNavigation��%j  &	       &	 #     &	 F     &	 i     &	 �     &		 �     &	
 �     &	 �     &	 ^    &	 �    7m�7  7  7  7  7  &	     &	 �    &	 �    &	      &	 #    &	 F    &	 i    7  7  7  7  7  7  &	  3
    &	 �    &	 �    &	 �    7  7  �� 5O  
   $V 	#3D26A0DA��6@   �� 5P  
   $V 	#FF26A0DA��6<   �� 5Q  
   $V 	#FF707070��6;   �� 5R  
   $V 	#0A000000��6I   �� 5S  
   $V 	#21000000��6E   �� 5T  
   $V 	#FF26A0DA��6;   �� 5U  
   $V 	#3D26A0DA��6?   �5V  
   UQF1 M 10.0,1.2 L 4.7,9.1 L 4.5,9.1 L 0,5.2 L 1.3,3.5 L 4.3,6.1L 8.3,0 L 10.0,1.2 Z�6x   �� 5W  
   $V 	#FF212121��69   �� 5X  
   8W   " m�6=   .�5Y     ��  �@�OverridesDefaultStyle$
 True��66   �� 5Z      � �� 5[     �� 5\     " m�6&   .R�5]     �� $X templateRoot��6!   ���#eee._�5^     �� $��Header��6/   5_     Y 5`     P� 5a      66   Z�  �@m�IconP� 5b      �@m�	IsChecked$
 True��6;   P� 5c      �@m�IsHighlighted$
 True��6?   . �5d  "   �� $Z templateRoot��6?   8   � ��6Y   �� 5e  "   $Z templateRoot��6@   8   � ��6Z   5f     P� 5g      � {�$ False��6;   . �5h  "   �� $Z templateRoot��6K   8   � ��6e   5i     ]� 5j     [ 5k  "   �� 5l  &   \ �@m�IsHighlighted$
] True��6I   �� 5m  &   \ � {�$] False��6E   ./�5o  "   �� $Z templateRoot��6?   8   � ��6Y   �� 5p  "   $Z templateRoot��6@   8   � ��6Z   5q     5s     5u     5v     �� 5x  
   .�5y     ��  � �� 5z     �� 5{     .R�5|     �� $��2q�6$   $	^ 1 2R�6/   $
_ true��6E   $��1��6`   #��� ��6t   5}     5     5�     �� 5�  
   $V 	#FFDDDDDD��6;   �� 5�  
   $V 	#FF707070��67   �� 5�  
   $V 	#FFF4F4F4��6=   �� 5�  
   $V 	#FFADB2B5��69   �� 5�  
   $V 	#FF838383��6=   �� 5�  
   " ��6*   .�5�     ��  �@�FocusVisualStyle8   61   �� 5�      � ��8  6+   �� 5�      � ��8  6,   �� 5�      � ��# � ��6+   �� 5�      � ��$ 1q�60   �� 5�      � ��$ Center=�6;   �� 5�      � ��$ Center=�69   �� 5�      � ��$ 1q�6(   �� 5�      � �� 5�     �� 5�     " ��6&   .R�5�     �� $X border��6!   #��z��61   #��z��6[   #��z��6�   $
_ true��6�   ._�5�     �� $` contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   #_ z_ 6�   #��z��6  5�     Y 5�     P� 5�      �@��IsDefaulted$
 true��6=   . �5�  "   �� $Z border��6@   # � ��6T    � ��5�     P� 5�      � {�$ false��6;   . �5�  "   �� $Z border��6?   8   � ��6S   �� 5�  "   $Z border��6@   8   � ��6T   �� 5�  "   $Z contentPresenter��6K   8   � ��6i   5�     5�     5�     5�     �� 5�  
   " ��68   .�5�     ��  � �� 5�     �� 5�     " ��6&   .R�5�     � .+�5�     �� #��z��6%   #��z��6O   #��z��6�   $
_ True��6�   �� 5�     $ PART_ContentHost��6+   $��0q�6E   $��Center=�6P   �� 5�     $a WaterMarkLabel��65   $��2 0 2 1q�6$   #��z 6M   $��Center=�6m   $y�	Collapsed=�5�  $   
�������$��1 0 0 0q�6M   5�     Y 5�     ]� 5�     [ 5�  "   �� 5�  &   \ � ��$]  ��6@   ./�5�  "   �� $Z WaterMarkLabel��6?    � y�$ Visible=�6[   5�     P� 5�      � {�$ False��6;   . �5�  "   ��  � ��
 �iii�6?   5�     5�     5�     5�     �� 5�  
   " d�6-   .�5�     ��  � �� 5�     �� 5�     " d�6&   .R�5�     �  5�     �� 5�  "   �� 5�  "   $��32 �63   .+�5�     �� $X Border��5�     $��2��5�     $b 4 0 4 0��5�     #��z��5�     
���   �$��0 0 1 1q�5�     F� 5�     $c Arrow��5�     $��1��5�     #��z��5�     #��z��5�     $��Center=�5�     $��Center=�5�     "����        � 	=  	= � z     	5�     5�     Y 5�     P� 5�      �@J�IsMouseOver$
 true��6J   . �5�  "   �� $Z Border��6)    � �� �	#FF3C7FB16T   �� 5�  "   $Z Arrow��6)    � ��
 ���� 6L   5�     P� 5�      �@d�	IsChecked$
 trueY�6H   . �5�  "   �� $Z Border��6)    � �� �	#FF3C7FB16T   �� 5�  "   $Z Arrow��6)    � ��
 ���� 6L   5�     P� 5�      � {�$ False��6;   . �5�  "   �� $Z Border��6)    � �� �#EEEEEE6S   5�     5�     5�     5�     �� 5�  
   " ��62   .R�5�     �� $X PART_ContentHost��6   $
|�True��6/   5�     �� 5�  
   " ��6$   .�5�     ��  � �� �#3336+   �� 5�      � ��
 �����6,   �� 5�      � ��
 �����6+   �� 5�      �@J�SnapsToDevicePixels$
 true��64   �� 5�      �@�OverridesDefaultStyle$
 true��66   �� 5�      � ��$
 Auto=�6K   �� 5�      � ��$
 Auto=�6I   �� 5�      � ��$
 true��6>   �� 5�      � ��$ 13(�6)   �� 5�      � ��$ 15��6*   �� 5�      � �� 5�     �� 5      " ��6&   .R�5     � .+�5     d� $��ToggleButton��5     $
 Hand��5     #��z��5     #��z��5     #��z��5     8��  $��2��5	     $|�false��5
     $d Press=�5     e 5     �� $f IsDropDownOpen�$g TwoWay=�h �� *TemplatedParent+5     �� 5     $��ContentSite��5  !   $i False��5  !   #��zj 5  !   #��zk 5  !   #��z��5  !   $��	10,3,30,3q�5  !   $��Center=�5  !   $
��Left=�5  !   �� 5     $l PART_EditableTextBox��6&   8�� $
��Left=�5     $��Center=�5     $��3,3,23,3q�5     $
|�True��5     $y�Hidden=�5     #n zm 5     ��5     Z� 5     � 5      $��Popup��5!     $��Bottom=�5"     #��zo 5#     $
p True��5$     $|�False��5%     $��Slide=�5&     .��5'  "   � $��DropDown��5(  !   $
_ True��5)  !   #��zq 5*  !   #��zr 5+  !   .+�5,  &   �� $X DropDownBorder��5.  !   $b 4��5-  !   
�������$��1q�50  !   ���#F6F6F651  !   �� 52  &   $��4,6,4,6q�63   $
_ True��6D   .��53  *   �� $
s True��65   $t 	Contained=�6H   54  '   55  #   56     57     Y 58     P� 59      �@��HasItems$ false��6:   . �5:  "   �� $Z DropDownBorder��6)    � ��$ 95��6Z   5;     P� 5<      �@��
IsGrouping$
 true��6<   . �5=  "   ��  � ��$ false��6R   5>     P� 5?      �@��
IsEditable$
 true��6<   . �5@  "   ��  � ��$ false��6>   �� 5A  "   $Z PART_EditableTextBox��6)    � y�$ Visible=�6a   �� 5B  "   $Z ContentSite��6)    � y�$ Hidden=�6X   5C     5E     5G     5H     5I     5J     