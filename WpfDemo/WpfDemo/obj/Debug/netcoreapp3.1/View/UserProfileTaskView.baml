   M S B A M L   `   `   `  ����B  >WpfDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfDemoWpfDemo  5   	   M ITimeSheet.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.ResourceTimeSheet.Resource 5   	   &     WpfDemo.View.UserProfileTaskView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfDemo     5   	   Ml10n<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.Resource     5   	     � Title$  Worktime Registry��5	   	   $	��600��6#   $	��560��60   
������� � WindowStartupLocation$ Manual=�5
   	    � Left$	 652��6(   
 � Top$	 100��63    � 
ResizeMode$ CanMinimize=�6=    � Icon$S MC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\WR.png��5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��1.5* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6K   $��0��6X   $
��Bold"�6h   $��Right=�6z   $��Center=�5      $��0 5 8 0q�6/   $��32(�6@   ��5      !   WpfDemo.ResxStaticExtension  *l10n:Resources.UserWithColon+5   @   �� 5   
   $��1��6C   $��0��6P   $
��Bold"�6`   $��Right=�6r   $��Center=�5      $��0 0 8 15q�6/   $��32(�6A   ��5        *l10n:Resources.Title+5   A   �� 5    
   $��2��6I   $��0��6V   $
��Bold"�6f   $��Right=�6x   $	��Top=�5!      $��0 0 8 0q�6,   $��32(�6=   ��5         *l10n:Resources.Description+5!   =   �� 5"   
   $��3��6F   $��0��6S   $
��Bold"�6c   $��Right=�6u   $��Center=�5#      $��0 20 8 0q�6/   $��32(�6A   ��5"        *l10n:Resources.Deadline+5#   A   �� 5$   
   $��4��6D   $��0��6Q   $
��Bold"�6a   $��Right=�6s   $��Center=�5%      $��0 0 8 0q�6/   $��32(�6@   ��5$        *l10n:Resources.Status+y�5%   N   �� *%#UserProfileTaskViewStatusVisibility+�� 5&   
   $��4��6M   $��0��6Z   $
��Bold"�6j   $��Right=�6|   $��Center=�5'      $��0 0 8 0q�6/   $��32(�6@   ��5&        * l10n:Resources.StatusWithColon+y�5'   N   �� *.,UserProfileTaskViewStatusWithColonVisibility+�� 5*   
   -   5         ��Name$ AddTaskUser��5*      $��1��6M   $��0��6]   
������ $��38��5+      $��28(�6   $
��Left=�6,   $
��Left=�6G   $��	-3 10 0 0q�6i   $��0q�6|   	 �� 
IsReadOnly$
	 True��6�   $��0q�6�   ��5*   '   �� *CurrentUser.Username+.��5+   �   
 � 	Resources
 5,       �� 
TargetType �� Property �� Value��%^   '��       �� 5-      " ��6   .�5.      ��  �@��CornerRadius$ 4��65   5/      50      .��6!   51      �� 54   
   $��1��6   $��1��6%    �� Orientation$ 
Horizontal=�62   $��0 0 0 15q�6K   .+�55      �� -   5        $ AddTaskTitle��55      ���#eee$	��200��56      $��37��6"   $��26(�6.   $
��Left=�6<   $��0 2 0 0q�6^   $	��2 0q�6o   $��1q�57      
���   ���55   ,   �� *Title+ �� ValidatesOnDataErrors$
 True�� �� UpdateSourceTrigger$ PropertyChanged=� ^� ToolTip 57   >   �� *ErrorCollection[Title]+.��6x   
 58      ��%^   '��       �� 59      " ��6   .�5:      ��  �@��CornerRadius$ 4��69   5;      5<      .��6%   5=      �� 5>      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��65��5?   D   $��65��6O   y�6   �� *TitleErrorIconVisibility+6O   5@      �� 5C   
   -   5        $ AddTaskDescription��5C      ���#eee$��1��6�   $��2��6�   $��0 4 20 0q�5D      $	��2 1q�6$   $��Stretch=�62   $��26(�6P    �� TextWrapping$
 Wrap=�6^   " �� VerticalScrollBarVisibility$
 Auto=�6r    �� AcceptsReturn$
 True��5E      $��1q�6'   
���   ���5C   .   �� *Description+$ PropertyChanged=�.��5E   O   
 5F      ��%^   '��       �� 5G      " ��6   .�5H      ��  �@��CornerRadius$ 4��65   5I      5J      .��6!   5K      �� 5M   
   $��1��6   $��3��6%   $ 
Horizontal=�62   $��0 20 0 0q�6K   .+�5N      (  "System.Windows.Controls.DatePicker  -   5          Name$ AddTaskDeadline��5N         SelectedDateFormat$ Short=�5O      ���#eee$��38��6F   $	��165��6R   $��26(�6^   $��0 2 0 0q�6l   $	��3,2q�6}      FirstDayOfWeek$ Monday=�6�      SelectedDate 5N   2   �� *
Deadline+$
 True��$ PropertyChanged=� 5P      �� *ErrorCollection[Deadline]+
 5Q      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �Name ��Name �� Triggers P� Property P� Value��%�  '        '  i    �� 5R      "  6   .�5S      ��  � ��$ 0q�6<   �� 5T       � �� 5U      �� 5V   "   
dptext��"  6B   .R�5W   &   � .+�5X   *   � $ WatermarkContent��6/   #��z��5Y   /   #��z��5Z   /   .+�5[   .   �� $ PART_ContentHost��6;   $��0q�6U   #��z��5\   ;   #��z��5]   ;   5^   +   5_   '   5`   #   5b      5c      �� 5d      "  6   .�5e      ��  � ��
 �   �68    5f      P� 5g       �@J�IsMouseOver$
 True��6=   . �5h   "   ��  � �� �	#FF3C7FB16@   5i      P� 5j       �@J�IsKeyboardFocusWithin$
 True��6G   . �5k   "   ��  � �� �	#FF3C7FB16@   5l      5n      5o      5p      �� 5q      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��70��5r   G   $��70��6R   y�6   �� *DeadlineErrorIconVisibility+6R   5s      �� 5u   
   $��1��6   $��4��6%   $ 
Horizontal=�62   .+�5v      �� -   5         ��Name$ CreateTaskStatus��5v      $	��2,1q�6V   $	��190��6d   $��40��6p   $��26(�6|   ���#eee$
��Left=�5w   )     �� SelectedValuePath$	  Key��5y      ! �� DisplayMemberPath$! Value��6/      theComboBox#���   5z   R   ��5v   1   �� *TaskStatuses+y�5w   K   �� *-+UserProfileTaskViewStatusComboboxVisibility+" �� SelectedValue" 5x      �� *Status+$
 True��$ PropertyChanged=� 5z      �� *ErrorCollection[Status]+
 5{      ��%�   '��       �� 5|      " ��6:    5}      P� 5~       �@J�IsMouseOver$
 true��6=   . �5   "   ��  �@�Cursor$
 Hand��6;   5�      5�      5�      5�      �� 5�      
������ $��40��6^   $��28(�6j   $
��Left=�5�      $
��Left=�61   $��-3 5 0 0q�6S   $��0q�6e   $
	 True��6q   $��0q�6�   ��5�      �� *TaskStatusString+# �� Mode$# OneWay=�y�5�      �� *,*UserProfileTaskViewStatusTextBoxVisibility+�� 5�      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��60��5�   E   $��60��6P   y�6   �� *StatusErrorIconVisibility+6P   5�      �� 5�   
   $��1��6   $��5��6%   $ 
Horizontal=�62   .+�5�      �� -   5        $ ��Name$$ SaveTaskToUserButton��5�      $��42��5�      $
��Left=�6!   $��0q�6<   $��4q�6G   $��24(�6S   $��1q�6a   
���   ����	#FF66B2FD% � Cursor$
% Hand��6�   ��5�   3     *l10n:Resources.Save+��6d   �� *SaveCommand+
 5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 4��69   5�      5�      5�      �� 5�      -   5        $$ BackToListButton��5�      $��42��5�      $��30 0 0 0q�6!   $	��5 3q�63   $��24(�6A   $��1q�6O   
���   ����	#FF66B2FD$
% Hand��6�   ��5�   /     *l10n:Resources.Cancel+��6b   �� *ExitWindowCommand+
 5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 4��69   5�      5�      5�      5�      5�      & J� InputBindings& 5�      �� 5�   
   
' �� Key$' Enter��6   ( �� Command( 6!   �� *SaveCommand+�� 5�   
   $	' Esc��6   ( 6   �� *ExitWindowCommand+
 5�        ComboBoxToggleButton) ��Name* �� CornerRadius+ F�Name, �� Triggers- �� 
TargetName  ComboBoxTextBox. �� 	ClickMode/ d� 	IsChecked0 �� Path1 �� RelativeSource2 J� IsHitTestVisible3 �� SelectionBoxItem4 �� SelectionBoxItemTemplate5 �� 
IsReadOnly6 �� IsDropDownOpen7 � AllowsTransparency8 J� SnapsToDevicePixels9 � ActualWidth: �� MaxDropDownHeight; K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation<   DirectionalNavigation��%�  &	       &	 �    &	  !    7  7  �� 5�   
   " d�6-   .�5�      ��  � �� 5�      �� 5�      " d�6&   .R�5�      �  5�      �� 5�   "   �� 5�   "   $��32 �63   .+�5�      �� $) Border��5�   !   $��2��5�   !   $* 4��5�   !   #��z��5�   !   
���   �$��1q�5�   !   F� 5�      $+ Arrow��5�   !   $��1��5�   !   #��z��5�   !   #��z��5�   !   $��Center=�5�   !   $��Center=�5�   !   "����        � 	=  	= � z     	5�   !   5�      , 5�      P� 5�       �@J�IsMouseOver$
 true��6J   . �5�   "   �� $- Border��6)    � �� �	#FF3C7FB16T   �� 5�   "   $- Arrow��6)    � ��
 ���� 6L   5�      P� 5�       �@d�	IsChecked$
 trueY�6H   . �5�   "   �� $- Border��6)    � �� �	#FF3C7FB16T   �� 5�   "   $- Arrow��6)    � ��
 ���� 6L   5�      P� 5�       � {�$ False��6;   . �5�   "   �� $- Border��6)    � �� �#EEEEEE6S   5�      5�      5�      5�      �� 5�   
   " ��62   .R�5�      �� $) PART_ContentHost��6   $
|�True��6/   5�      �� 5�   
   " ��6$   .�5�      ��  � �� �#3336+   �� 5�       � ��
 �����6,   �� 5�       � ��
 �����6+   �� 5�       �@J�SnapsToDevicePixels$
 true��64   �� 5�       �@�OverridesDefaultStyle$
 true��66   �� 5�       � ��$
 Auto=�6K   �� 5�       � ��$
 Auto=�6I   �� 5�       � ��$
 true��6>   �� 5�       � ��$ 13(�6)   �� 5�       � ��$	 150��6)   �� 5�       � ��$ 35��6*   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      � .+�5�      d� $��ToggleButton��5�   !   $
% Hand��5�   !   #��z��5�   !   #��z��5�   !   #��z��5�   !   8��  $��2��5�   !   $|�false��5�   !   $. Press=�5�   !   / 5�   !   �� $0 IsDropDownOpen�$# TwoWay=�1 �� *TemplatedParent+5�   !   �� 5�      $��ContentSite��5�   !   $2 False��5�   !   #��z3 5�   !   #��z4 5�   !   #��z��5�   !   $��	10,3,30,3q�5�   !   $��Center=�5�   !   $
��Left=�5   !   �� 5     $ PART_EditableTextBox��6&   8�� $
��Left=�5  !   $��Center=�5  !   $��3,3,23,3q�5  !   $
|�True��5  !   $y�Hidden=�5  !   #	 z5 5	  !   ��5  !   Z� 5	  !   � 5
     $��Popup��5  !   $��Bottom=�5  !   #��z6 5  !   $
7 True��5  !   $|�False��5  !   $��Slide=�5  !   .��5  "   � $��DropDown��5  %   $
8 True��5  %   #��z9 5  %   #��z: 5  %   .+�5  &   �� $) DropDownBorder��5  )   $* 4��5  )   
�������$��1q�5  )   ���#F6F6F65  )   �� 5  &   $��4,6,4,6q�63   $
8 True��6D   .��5  *   �� $
; True��65   $< 	Contained=�6H   5  '   5  #   5      5!     , 5"     P� 5#      �@��HasItems$ false��6:   . �5$  "   �� $- DropDownBorder��6)    � ��$ 95��6Z   5%     P� 5&      �@��
IsGrouping$
 true��6<   . �5'  "   ��  � ��$ false��6R   5(     P� 5)      �@��
IsEditable$
 true��6<   . �5*  "   ��  � ��$ false��6>   �� 5+  "   $- PART_EditableTextBox��6)    � y�$ Visible=�6a   �� 5,  "   $- ContentSite��6)    � y�$ Hidden=�6X   5-     5/     51     52     53     55     