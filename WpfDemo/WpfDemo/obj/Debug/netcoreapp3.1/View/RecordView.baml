   M S B A M L   `   `   `  ����B  >WpfDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfDemoWpfDemo  5      M ITimeSheet.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.ResourceTimeSheet.Resource 5          WpfDemo.View.RecordView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WpfDemo     5      Ml10n<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.Resource     5      -   5        $��RecordViewUserControl��5
      $	��665��5	      $	��540��6   
�������
���   �$��1q�6R   .��5      � $��0 0 0 0q�6     � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $
��1.7* �6    � RowDefinitions 5   
   �� 5      ��6   �� *(&SelectedRecord.RecordViewUserRowHeight+�� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $��* �6   .+�5   
   �� $��0��6B   $��0��6R   $
��Bold"�6_   $��Right=�6q   $��Center=�5      $��0 0 8 0q�6/   $��32(�6@   ��5      !   WpfDemo.ResxStaticExtension  *l10n:Resources.User+5   @   �� 5   
   $��0��6B   $��1��6R   $
��Bold"�6_   $��Right=�6q   $��Center=�5      $��0 0 8 0q�6/   $��32(�6@   ��5        *l10n:Resources.Task+5   @   �� 5   
   $��0��6B   $��2��6R   $
��Bold"�6_   $��Right=�6q   $��Center=�5      $��0 0 8 20q�6/   $��32(�6A   ��5        *l10n:Resources.Date+5   A   �� 5    
   $��0��6F   $��3��6V   $
��Bold"�6c   $��Right=�6u   $��Center=�5!      $��0 0 8 40q�6/   $��32(�6A   ��5         *l10n:Resources.Duration+5!   A   �� 5"   
   $��0��6E   $��4��6U   $
��Bold"�6b   $��Right=�6t   $	��Top=�5#      $��0 0 8 0q�6,   $��32(�6=   ��5"        *l10n:Resources.Comment+5#   =   �� 5%   
   $��0��6M   $��1��6Z   ���	#FFC7C7C7$��37��6�   $��26(�5&      $
��Left=�6    $
��Left=�6;   $��0 2 0 0q�6]   $	��2 0q�6n    �� 
IsReadOnly$
 True��6|   $��1q�5'      
���   �   TextBoxReadOnlyStyle#���   5(      ��5%      �� *SelectedRecord.User_Username+ �� Mode$ OneWay=�y�5'   :   �� *0.SelectedRecord.RecordViewUserTextBoxVisibility+5(      �� 5*   
   $��1��6   $��1��6%    �� Orientation$ 
Horizontal=�62   .+�5+      �� -   5         ��Name$ CreateRecordTask��5+      $	��2 0q�6�   $��0 2 0 0q�5,      ���#eee$	��250��6:   $��40��6F   $��26(�6R   $
��Left=�6`   $��Center=�6�     theComboBox#���  5.   _   ��5+   1   �� *SelectedRecord.ActiveTasks+ �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedItem 5-      �� *SelectedRecord.Task+ �� ValidatesOnDataErrors$
 True��$ PropertyChanged=�	 ^� ToolTip	 5.      �� *&$SelectedRecord.ErrorCollection[Task]+y�5/      �� *1/SelectedRecord.RecordViewTaskComboBoxVisibility+��50      �� 51      .R�52      �� ��6$   �� *Title+53      
 � 	Resources
 55       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%�   '��       �� 56      " ��6:    57      P� 58       �@J�IsMouseOver$
 true��6=   . �59   "   ��  �@�Cursor$
 Hand��6;   5:      5<      5=      5>      �� 5@      ���	#FFC7C7C7$��37��6X   $��26(�6d   $
��Left=�5A      $
��Left=�61   $��0 2 0 0q�6S   $	��2 0q�6d   $
 True��6r   $��1q�6�   
���   �#���   5B   p   ��5@      �� *SelectedRecord.Task.Title+y�5B   *   �� *0.SelectedRecord.RecordViewTaskTextboxVisibility+6p   �� 5D      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��75��5E   R   $��75��6]   y�6   �� *(&SelectedRecord.TaskErrorIconVisibility+6]   5F      �� 5I   
   $��1��6   $��2��6%   $ 
Horizontal=�62   $��0 0 0 20q�6K   .+�5J      (  "System.Windows.Controls.DatePicker  -   5          Name$ CreateRecordDate��5J         SelectedDateFormat$ Short=�5K      ���#eee$��38��6F   $	��165��6R   $��26(�6^   $	��3,2q�6l      FirstDayOfWeek$ Monday=�6z   $��0 2 0 0q�6�      SelectedDate 5J   3   �� *SelectedRecord.Date+$
 True��$ PropertyChanged=�y�5L      �� *31SelectedRecord.RecordViewDateDatePickerVisibility+	 5M      �� *&$SelectedRecord.ErrorCollection[Date]+
 5N      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �Name ��Name��%�  '        '  i    �� 5O      "  6   .�5Q      ��  � ��$ 0q�6<   �� 5R       � �� 5S      �� 5T   "   
dptext��"  6B   .R�5U   &   � .+�5V   *   � $ WatermarkContent��6/   #��z��5W   /   #��z��5X   /   .+�5Y   .   �� $ PART_ContentHost��6;   $��0q�6U   #��z��5Z   ;   #��z��5[   ;   5\   +   5]   '   5^   #   5`      5a      �� 5b      "  6   .�5c      ��  � ��
 �   �68    5d      P� 5e       �@J�IsMouseOver$
 True��6=   . �5f   "   ��  � �� �	#FF3C7FB16@   5h      P� 5i       �@J�IsKeyboardFocusWithin$
 True��6G   . �5j   "   ��  � �� �	#FF3C7FB16@   5k      5m      5�      5�      �� 5�      ���	#FFC7C7C7$��36��6m   $��26(�5�      $
��Left=�6$   $
��Left=�6?   $��0 2 0 0q�6a   $	��2 0q�6r   $
 True��6�   $��1q�6�   
���   �#���   5�   p   ��5�      �� *SelectedRecord.Date+ �� StringFormat$ 
yyyy.MM.dd��y�5�   *   �� *0.SelectedRecord.RecordViewDateTextBoxVisibility+6p   �� 5�      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��70��5�   R   $��70��6]   y�6   �� *(&SelectedRecord.DateErrorIconVisibility+6]   5�      �� 5�   
   $��1��6   $��3��6%   $ 
Horizontal=�62   $��0 0 0 40q�6K   .+�5�      �� -   5         ��Name$ CreateRecordDuration��5�      $��75��5�   `   $��36��6k   $��26(�6w   $��0 2 0 0q�6�   $��Center=�5�      $��1q�6:   
���   �#���   5�   h   ��5�   4   �� *SelectedRecord.DurationFormat+$
 True��$ 	LostFocus=���5�      �� *42SelectedRecord.RecordViewDurationCommentBackground+ 5�   b   �� *+)SelectedRecord.IsRecordViewValuesReadOnly+	 5�      �� *0.SelectedRecord.ErrorCollection[DurationFormat]+.��6�   
 5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 4��69   5�      5�      .��6%   5�      �� 5�      $ Vertical=�6   $��Center=�60   $��10 0 0 0q�6K   $��25��6]   $��40��6h   .+�5�      �� -   5         ��Name$ DurationAddButtton��5�      $��25��6k   $��15��6v   $��0 5 0 0q�5�      $��0q�6*    � Cursor$
 Hand��6>   $��0q�6L   
������  ! Add10MinToDurationButtonStyle#���  5�   [   ��5�   5   �� *#!SelectedRecord.DurationAddCommand+y�5�      �� *,*SelectedRecord.RecordViewButtonsVisibility+ � ToolTip 5�        *#!l10n:Resources.Add10MinToDuration+5�      �� 5�      -   5        $ DurationReduceButtton��5�      $��25��6q   $��15��6|   $��0 5 0 0q�5�      $��0q�6*   $
 Hand��6>   $��0q�6L   
������  ( $Subtract10MinFromDurationButtonStyle#���  5�   [   ��5�   8   �� *&$SelectedRecord.DurationReduceCommand+y�5�      �� *,*SelectedRecord.RecordViewButtonsVisibility+ 5�        **(l10n:Resources.Subtract10MinFromDuration+5�      5�      �� 5�      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��70��5�   V   $��70��6a   y�6   �� *,*SelectedRecord.DurationErrorIconVisibility+6a   5�      �� 5�   
   -   5        $ CreateRecordComment��5�      $��1��6|   $��4��6�   $��0 4 20 0q�5�   \   $	��2,1q�6n   $��1q�6|   
���   �$��Stretch=�5�      $��Stretch=�60   $��26(�6L    �� TextWrapping$
 Wrap=�6Z   " �� VerticalScrollBarVisibility$
 Auto=�6n    �� AcceptsReturn$
 True��6�   #���   5�   S   ��5�   /   �� *SelectedRecord.Comment+$ PropertyChanged=���5�      �� *42SelectedRecord.RecordViewDurationCommentBackground+ 5�      �� *+)SelectedRecord.IsRecordViewValuesReadOnly+.��6�   
 5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 4��65   5�      5�      .��6!   5�      �� 5�   
   $��1��6   $��5��6%   $ 
Horizontal=�62   .+�5�      �� -   5        $ CreateRecordButton��5�      $��42��5�      $��0q�6!   $	��6 3q�6,   $��24(�6:   $��1q�6H   
���   �$
 Hand��6p   ���	#FF66B2FD��5�   1     *l10n:Resources.Save+��6b   �� *SelectedRecord.SaveCommand+y�5�      �� *,*SelectedRecord.RecordViewButtonsVisibility+
 5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 4��69   5�      5�      5�      �� 5�      -	   5        $ CancelCreateRecordButton��5�      $��42��5�      $��30 0 0 0q�6!   $	��6 3q�63   $��24(�6A   $��1q�6O   
���   �$
 Hand��6w   ���	#FF66B2FD��5�   7     *l10n:Resources.Cancel+��6j   �� *(&SelectedRecord.CancelRecordViewCommand+
 5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 4��69   5�      5�      5�      5�      5�        J� InputBindings  5�      �� 5�   
   
! �� Key$! Enter��6   " �� Command" 6!   �� *SelectedRecord.SaveCommand+
 5�        ComboBoxToggleButton# ��Name$ �� CornerRadius% F�Name& �� Triggers' �� 
TargetName  ComboBoxTextBox( �� 	ClickMode) d� 	IsChecked* �� Path+ �� RelativeSource, J� IsHitTestVisible- �� SelectionBoxItem. �� SelectionBoxItemTemplate/ �� 
IsReadOnly0 �� IsDropDownOpen1 � AllowsTransparency2 J� SnapsToDevicePixels3 � ActualWidth4 �� MaxDropDownHeight5 K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation6   DirectionalNavigation  FocusVisual7 �� StrokeDashArray8 �� Stretch��%?  &	       &	 �    &	 !    7  7  &	 �    &	 ^    7  &	 �    7  &	  >    �� 5�   
   " d�6-   .�5�      ��  � �� 5�      �� 5�      " d�6&   .R�5�      �   5�      �� 5�   "   �� 5�   "   $��32 �63   .+�5�      �� $# Border��5�   !   $��2��5�   !   $$ 4��5�   !   #��z��5�   !   
���   �$��1q�5�   !   F� 5�      $% Arrow��5�   !   $��1��5�   !   #��z��5�   !   #��z��5�   !   $��Center=�5�   !   $��Center=�5�   !   "����        � 	=  	= � z     	5�   !   5�      & 5�      P� 5�       �@J�IsMouseOver$
 true��6J   . �5�   "   �� $' Border��6)    � �� �	#FF3C7FB16T   �� 5�   "   $' Arrow��6)    � ��
 ���� 6L   5�      P� 5�       �@d�	IsChecked$
 trueY�6H   . �5�   "   �� $' Border��6)    � �� �	#FF3C7FB16T   �� 5   "   $' Arrow��6)    � ��
 ���� 6L   5     P� 5      � {�$ False��6;   . �5  "   �� $' Border��6)    � �� �#EEEEEE6S   5     5     5     5	     �� 5
  
   " ��62   .R�5     �� $# PART_ContentHost��6   $
|�True��6/   5     �� 5  
   " ��6$   .�5     ��  � �� �#3336+   �� 5      � ��
 �����6,   �� 5      � ��
 �����6+   �� 5      �@J�SnapsToDevicePixels$
 true��64   �� 5      �@�OverridesDefaultStyle$
 true��66   �� 5      � ��$
 Auto=�6K   �� 5      � ��$
 Auto=�6I   �� 5      � ��$
 true��6>   �� 5      � ��$ 13(�6)   �� 5      � ��$	 150��6)   �� 5      � ��$ 35��6*   �� 5      � �� 5     �� 5     " ��6&   .R�5     � .+�5     d� $��ToggleButton��5   !   $
 Hand��5  !   #��z��5!  !   #��z��5"  !   #��z��5#  !   8��  $��2��5%  !   $|�false��5&  !   $( Press=�5(  !   ) 5'  !   �� $* IsDropDownOpen�$ TwoWay=�+ �� *TemplatedParent+5(  !   �� 5)     $��ContentSite��5*  %   $, False��5+  %   #��z- 5,  %   #��z. 5-  %   #��z��5.  %   $��	10,3,30,3q�5/  %   $��Center=�50  %   $
��Left=�51  %   �� 52     $ PART_EditableTextBox��6&   8�� $
��Left=�55  !   $��Center=�56  !   $��3,3,23,3q�57  !   $
|�True��58  !   $y�Hidden=�59  !   # z/ 5:  !   ��53  !   Z� 5:  !   � 5;     $��Popup��5<  !   $��Bottom=�5=  !   #��z0 5>  !   $
1 True��5?  !   $|�False��5@  !   $��Slide=�5A  !   .��5B  "   � $��DropDown��5C  %   $
2 True��5D  %   #��z3 5E  %   #��z4 5F  %   .+�5G  &   �� $# DropDownBorder��5I  %   $$ 4��5H  %   
�������$��1q�5K  %   ���#F6F6F65L  %   �� 5M  &   $��4,6,4,6q�63   $
2 True��6D   .��5N  *   �� $
5 True��65   $6 	Contained=�6H   5O  '   5P  #   5Q     5R     & 5S     P� 5T      �@��HasItems$ false��6:   . �5U  "   �� $' DropDownBorder��6)    � ��$ 95��6Z   5V     P� 5W      �@��
IsGrouping$
 true��6<   . �5X  "   ��  � ��$ false��6R   5Y     P� 5Z      �@��
IsEditable$
 true��6<   . �5[  "   ��  � ��$ false��6>   �� 5\  "   $' PART_EditableTextBox��6)    � y�$ Visible=�6a   �� 5]  "   $' ContentSite��6)    � y�$ Hidden=�6X   5^     5`     5b     5c     �� 5f  
   .�5g     ��  � �� 5h     �� 5i     .R�5j     �� $��2q�6$   $	7 1 2R�6/   $
2 true��6E   $��1��6`   #��� ��6t   5k     5m     5n     �� 5o  
   " ��66   .�5p     ��  �@�FocusVisualStyle8   61   �� 5q      � ��
 ���� 6+   �� 5r      � �� 5s     �� 5t     " ��6&   .R�5u     � $
 grid��6   .+�5v     F� $% ArrowUpPath��6#   
���   �$
8 Fill=�6E   "����    ��[ � � ��[ р�[     	6T   5w     & 5x     P� 5y      �@J�IsMouseOver$
 true��6=   . �5z  "   �� $
' grid��6?    � ��
 ���� 6Q   �� 5{  "   $' ArrowUpPath��6)    � ��
 ��� �6R   5|     P� 5}      � ��$
 true��6;   . �5~  "   �� $
' grid��6?    � ��
 ���� 6Q   �� 5  "   $' ArrowUpPath��6)    � ��
 ��� �6R   5�     P� 5�      � {�$ false��6;   . �5�  "   �� $
' grid��6?    � ��
 ���� 6Q   �� 5�  "   $' ArrowUpPath��6)    � �� �	#FFF4F4F46R   5�     5�     5�     5�     �� 5�  
   " ��6=   .�5�     ��  �@�FocusVisualStyle8   61   �� 5�      � ��
 ���� 6+   �� 5�      � �� 5�     �� 5�     " ��6&   .R�5�     � $
 grid��6   .+�5�     F� $% ArrowDownPath��6#   
���   �$
8 Fill=�6G   "����        р�[ ��[ � �     	6V   5�     & 5�     P� 5�      �@J�IsMouseOver$
 true��6=   . �5�  "   �� $
' grid��6?    � ��
 ���� 6Q   �� 5�  "   $' ArrowDownPath��6)    � ��
 ��� �6T   5�     P� 5�      � ��$
 true��6;   . �5�  "   �� $
' grid��6?    � ��
 ���� 6Q   �� 5�  "   $' ArrowDownPath��6)    � ��
 ��� �6T   5�     P� 5�      � {�$ false��6;   . �5�  "   �� $
' grid��6?    � ��
 ���� 6Q   �� 5�  "   $' ArrowDownPath��6)    � �� �	#FFF4F4F46T   5�     5�     5�     5�     �� 5�  
   " ��6-   .�5�     ��  � �� 5�     �� 5�     " ��6&   .R�5�     �� $# border��6!   #��z��61   #��z��6[   #��z��6�   $
2 True��6�   ._�5�     �� $ PART_ContentHost��6+   $|�false��6E   $��Hidden=�6W   $��Hidden=�6~   5�     & 5�     P� 5�      �@J�IsMouseOver$
 True��6=   . �5�  "   �� $' border��6@    � �� �	#FF3C7FB16T   5�     P� 5�      �@J�	IsFocused$
 True��6;   . �5�  "   �� $' border��6@    � �� �	#FF3C7FB16T   5�     P� 5�      �@��
IsReadOnly$
 True��6<   . �5�  "   �� $' border��6@    � ��
 �   �6T   5�     5�     5�     5�     5�     5�     