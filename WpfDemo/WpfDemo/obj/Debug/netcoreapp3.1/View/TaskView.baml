   M S B A M L   `   `   `  ����B  >WpfDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfDemoWpfDemo  5      M ITimeSheet.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.ResourceTimeSheet.Resource 5      5clr-namespace:WpfDemo.ViewModelWpfDemo.ViewModel  5          WpfDemo.View.TaskView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WpfDemo     5      Ml10n<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.Resource     5      -lclr-namespace:WpfDemo.ViewModel     5      -   5        $
��Task��5
      $	��665��6   $	��540��6'   
�������
���   �$��1q�6^   .��5      � $��0 0 0 0q�6     � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $
��1.5* �6    � RowDefinitions 5   
   �� 5      ��6   �� *$"SelectedTask.TaskViewUserRowHeight+�� 5      $��* �6   �� 5      $��2* �6   �� 5      $
��0.8* �6   �� 5      $
��0.8* �6   �� 5      $��0.75* �6   .+�5   
   �� $��0��6B   $��0��6R   $
��Bold"�6_   $��Right=�6q   $��Center=�5      $��0 20 8 0q�6/   $��32(�6A   ��5      !   WpfDemo.ResxStaticExtension  *l10n:Resources.User+5   A   �� 5   
   $��0��6C   $��1��6S   $
��Bold"�6`   $��Right=�6r   $��Center=�5      $��0 0 8 10q�6/   $��32(�6A   ��5        *l10n:Resources.Title+5   A   �� 5    
   $��0��6I   $��2��6Y   $
��Bold"�6f   $��Right=�6x   $	��Top=�5!      $��0 0 8 0q�6,   $��32(�6=   ��5         *l10n:Resources.Description+5!   =   �� 5"   
   $��0��6F   $��3��6V   $
��Bold"�6c   $��Right=�6u   $��Center=�5#      $��0 20 8 0q�6/   $��32(�6A   ��5"        *l10n:Resources.Deadline+5#   A   �� 5&   
   $��0��6D   $��4��6T   $
��Bold"�6a   $��Right=�6s   $��Center=�5'      $��0 0 8 0q�6/   $��32(�6@   ��5&        *l10n:Resources.Status+5'   @   �� 5)   
   $��1��6   $��0��6%    �� Orientation$ 
Horizontal=�62   $��0 20 0 0q�6K   .+�5*      �� -   5         ��Name$ CreateTaskUser��5*      $	��2 0q�6�   $��0q�5+      ���#eee$��40��64   $��26(�6@   $	��250��6N   $
��Left=�6Z   $��Center=�6|      theComboBox#���   5.   ]   ��5*   /   �� *SelectedTask.UserList+ �� UpdateSourceTrigger$ PropertyChanged=�y�5,      �� *-+SelectedTask.TaskViewUserComboBoxVisibility+ �� SelectedItem 5-      �� *SelectedTask.User+ �� ValidatesOnDataErrors$
 True��$ PropertyChanged=� ^� ToolTip 5.      �� *$"SelectedTask.ErrorCollection[User]+��5/      �� 50      .R�51      �� ��6$   �� *
Username+52       � 	Resources 54      	 �� 
TargetType
 �� Triggers P� Property P� Value �� Property �� Value��%�   '��       �� 55      "	 ��6:   
 56      P� 57       �@J�IsMouseOver$
 true��6=   . �58   "   ��  �@�Cursor$
 Hand��6;   59      5;      5<      5=      �� 5>      ���	#FFC7C7C7$��37��6Y   $��26(�5?      $
��Left=�6$   $
��Left=�6?   $��0 2 0 0q�6a   $	��2 0q�6r    �� 
IsReadOnly$
 True��6�   $��1q�5@      
���   �  TextBoxReadOnlyStyle#���  5A      ��5>      �� *SelectedTask.User.Username+y�5@   >   �� *,*SelectedTask.TaskViewUserTextBoxVisibility+5A      �� 5B      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��70��5C   P   $��70��6[   y�6   �� *&$SelectedTask.UserErrorIconVisibility+6[   5D      �� 5G   
   $��1��6   $��1��6%   $ 
Horizontal=�62   $��0 0 0 10q�6K   .+�5H      �� -   5         ��Name$ CreateTaskTitle��5H      ���#eee$	��250��5I      $��37��6"   $��26(�6.   $
��Left=�6<   $��0 2 0 0q�6W   $	��2 0q�6h   $��1q�6v   
���   ���5H   /   �� *SelectedTask.Title+$
 True��$ PropertyChanged=� 5J      �� *%#SelectedTask.ErrorCollection[Title]+y�5K      �� *64SelectedTask.TaskViewTitleDeadlineComboBoxVisibility+.��6b    5L      ��%^   '��       �� 5M      "	 ��6   .�5N      ��  �@��CornerRadius$ 4��69   5O      5P      .��6%   5Q      �� 5R      ���	#FFC7C7C7$��37��6Q   $��26(�5S      $
��Left=�6$   $
��Left=�6?   $��0 2 0 0q�6a   $	��2 0q�6r   $
 True��6�   $��1q�5T      
���   �#���  5U      ��5R      �� *SelectedTask.Title+y�5T   >   �� *53SelectedTask.TaskViewTitleDeadlineTextBoxVisibility+5U      �� 5V      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��70��5W   Q   $��70��6\   y�6   �� *'%SelectedTask.TitleErrorIconVisibility+6\   5X      �� 5[   
   -   5        $ CreateTaskDescription��5[      $��1��5\   V   $��2��6f   $��0 4 20 0q�6s   $	��2 1q�6�   $��Stretch=�5]      $��Stretch=�60   $��26(�6L    �� TextWrapping$
 Wrap=�6Z   " �� VerticalScrollBarVisibility$
 Auto=�6n    �� AcceptsReturn$
 True��6�   $��1q�5^      
���   �#���  5_      ��5[   1   �� *SelectedTask.Description+$ PropertyChanged=���5\      �� *.,SelectedTask.IsTaskViewDescriptionBackground+ 5^   :   �� *,*SelectedTask.IsTaskViewDescriptionReadOnly+.��5_   A    5`      ��%^   '��       �� 5a      "	 ��6   .�5b      ��  �@��CornerRadius$ 4��65   5c      5d      .��6!   5e      �� 5h   
   $��1��6   $��3��6%   $ 
Horizontal=�62   $��0 20 0 0q�6K   .+�5i      (  "System.Windows.Controls.DatePicker  -   5          Name$ CreateTaskDeadline��5i         SelectedDateFormat$ Short=�5j      ���#eee$��38��6F   $	��165��6R   $��26(�6^   $��0 2 0 0q�6l   $	��3 2q�6}      FirstDayOfWeek$ Monday=�6�      SelectedDate 5i   5   �� *SelectedTask.Deadline+$
 True��$ PropertyChanged=�y�5k      �� *64SelectedTask.TaskViewTitleDeadlineComboBoxVisibility+ 5l      �� *(&SelectedTask.ErrorCollection[Deadline]+ 5m      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �Name ��Name��%�  '        '  i    �� 5n      "	  6   .�5o      ��  � ��$ 0q�6<   �� 5p       � �� 5q      �� 5r   "   
dptext��"  6B   .R�5s   &   � .+�5t   *   � $ WatermarkContent��6/   #��z��5u   /   #��z��5v   /   .+�5w   .   �� $ PART_ContentHost��6;   $��0q�6U   #��z��5x   ;   #��z��5y   ;   5z   +   5{   '   5|   #   5~      5      �� 5�      "	  6   .�5�      ��  � ��
 �   �68   
 5�      P� 5�       �@J�IsMouseOver$
 True��6=   . �5�   "   ��  � �� �	#FF3C7FB16@   5�      P� 5�       �@J�IsKeyboardFocusWithin$
 True��6G   . �5�   "   ��  � �� �	#FF3C7FB16@   5�      5�      5�      5�      �� 5�      ���	#FFC7C7C7$��36��6o   $��26(�5�      $
��Left=�6$   $
��Left=�6?   $��0 2 0 0q�6a   $	��2 0q�6r   $
 True��6�   $��1q�5�      
���   �#���  5�      ��5�      �� *SelectedTask.Deadline+ �� StringFormat$ 
yyyy.MM.dd��y�5�   >   �� *53SelectedTask.TaskViewTitleDeadlineTextBoxVisibility+5�      �� 5�      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��70��5�   T   $��70��6_   y�6   �� **(SelectedTask.DeadlineErrorIconVisibility+6_   5�      �� 5�   
   $��1��6   $��4��6%   $ 
Horizontal=�62   .+�5�      �� -   5        $ CreateTaskStatus��5�      $	��2 0q�6c   $��0q�6q   $	��190��6|   $��40��5�      $��26(�6#   ���#eee$
��Left=�6C    �� SelectedValuePath$	 Key��5�       �� DisplayMemberPath$ Value��6/   #���   5�      ��5�   1   �� *SelectedTask.TaskStatuses+y�5�   e   �� */-SelectedTask.TaskViewStatusComboboxVisibility+ �� SelectedValue 5�      �� *SelectedTask.Status+$
 True��$ PropertyChanged=� 5�   I   �� *&$SelectedTask.ErrorCollection[Status]+ 5�      ��%�   '��       �� 5�      "	 ��6:   
 5�      P� 5�       �@J�IsMouseOver$
 true��6=   . �5�   "   ��  �@�Cursor$
 Hand��6;   5�      5�      5�      5�      �� 5�      ���	#FFC7C7C7$��37��6i   $��26(�5�      $
��Left=�6$   $
��Left=�6?   $��0 2 0 0q�6a   $	��2 0q�6r   $
 True��6�   $��1q�5�      
���   �#���  5�      ��5�      �� *SelectedTask.TaskStatusString+ �� Mode$ OneWay=�y�5�   >   �� *.,SelectedTask.TaskViewStatusTextBoxVisibility+5�      �� 5�      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��60��5�   R   $��60��6]   y�6   �� *(&SelectedTask.StatusErrorIconVisibility+6]   5�      �� 5�   
   $��1��6   $��5��6%   $ 
Horizontal=�62   $��0 0 0 18q�6K   .+�5�      �� -   5          ��Name$  CreateTaskForUserButton��5�      $��42��5�      $��0 0 0 0q�6!   $	��6 3q�62   $��24(�6@   $��1q�6N   
���   �! � Cursor$
! Hand��6v   ���	#FF66B2FD��5�   6     *l10n:Resources.Save+��6g   �� *SelectedTask.SaveCommand+ 5�      ��%^   '��       �� 5�      "	 ��6   .�5�      ��  �@��CornerRadius$ 4��69   5�      5�      5�      �� 5�      -   5        $#  CancelCreateTaskForUserButton��5�      $��42��5�      $��30 0 0 0q�6!   $	��6 3q�63   $��24(�6A   $��1q�6O   
���   �$
! Hand��6w   ���	#FF66B2FD��5�   <     *l10n:Resources.Cancel+��6o   �� *$"SelectedTask.CancelTaskViewCommand+ 5�      ��%^   '��       �� 5�      "	 ��6   .�5�      ��  �@��CornerRadius$ 4��69   5�      5�      5�      5�      5�      " J� InputBindings" 5�      �� 5�   
   
# �� Key$# Enter��6   $ �� Command$ 6!   �� *SelectedTask.SaveCommand+ 5�        ComboBoxToggleButton% ��Name& �� CornerRadius' F�Name( �� Triggers) �� 
TargetName  ComboBoxTextBox* �� 	ClickMode+ d� 	IsChecked, �� Path- �� RelativeSource. J� IsHitTestVisible/ �� SelectionBoxItem0 �� SelectionBoxItemTemplate1 �� 
IsReadOnly2 �� IsDropDownOpen3 � AllowsTransparency4 J� SnapsToDevicePixels5 � ActualWidth6 �� MaxDropDownHeight7 K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation8   DirectionalNavigation��%�  &	       &	 �    &	  !    7  7  &	 �    �� 5�   
   "	 d�6-   .�5�      ��  � �� 5�      �� 5�      " d�6&   .R�5�      �   5�      �� 5�   "   �� 5�   "   $��32 �63   .+�5�      �� $% Border��5�      $��2��5�      $& 4��5�      #��z��5�      
���   �$��1q�5�      F� 5�      $' Arrow��5�      $��1��5�      #��z��5�      #��z��5�      $��Center=�5�      $��Center=�5�      "����        � 	=  	= � z     	5�      5�      ( 5�      P� 5�       �@J�IsMouseOver$
 true��6J   . �5�   "   �� $) Border��6)    � �� �	#FF3C7FB16T   �� 5�   "   $) Arrow��6)    � ��
 ���� 6L   5�      P� 5�       �@d�	IsChecked$
 trueY�6H   . �5�   "   �� $) Border��6)    � �� �	#FF3C7FB16T   �� 5�   "   $) Arrow��6)    � ��
 ���� 6L   5�      P� 5�       � {�$ False��6;   . �5�   "   �� $) Border��6)    � �� �#EEEEEE6S   5�      5�      5�      5�      �� 5�   
   " ��62   .R�5�      �� $% PART_ContentHost��6   $
|�True��6/   5�      �� 5  
   "	 ��6$   .�5     ��  � �� �#3336+   �� 5      � ��
 �����6,   �� 5      � ��
 �����6+   �� 5      �@J�SnapsToDevicePixels$
 true��64   �� 5      �@�OverridesDefaultStyle$
 true��66   �� 5      � ��$
 Auto=�6K   �� 5      � ��$
 Auto=�6I   �� 5	      � ��$
 true��6>   �� 5
      � ��$ 13(�6)   �� 5      � ��$	 150��6)   �� 5      � ��$ 35��6*   �� 5      � �� 5     �� 5     " ��6&   .R�5     � .+�5     d� $��ToggleButton��5     $
! Hand��5     #��z��5     #��z��5     #��z��5     8��  $��2��5     $|�false��5     $* Press=�5     + 5     �� $, IsDropDownOpen�$ TwoWay=�- �� *TemplatedParent+5     �� 5     $��ContentSite��5  !   $. False��5  !   #��z/ 5   !   #��z0 5!  !   #��z��5"  !   $��	10,3,30,3q�5#  !   $��Center=�5$  !   $
��Left=�5%  !   �� 5&     $ PART_EditableTextBox��6&   8�� $
��Left=�5)     $��Center=�5*     $��3,3,23,3q�5+     $
|�True��5,     $y�Hidden=�5-     # z1 5.     ��5'     Z� 5.     � 5/     $��Popup��50     $��Bottom=�51     #��z2 52     $
3 True��53     $|�False��54     $��Slide=�55     .��56  "   � $��DropDown��57  !   $
4 True��58  !   #��z5 59  !   #��z6 5:  !   .+�5;  &   �� $% DropDownBorder��5=  !   $& 4��5<  !   
�������$��1q�5?  !   ���#F6F6F65@  !   �� 5A  &   $��4,6,4,6q�63   $
4 True��6D   .��5B  *   �� $
7 True��65   $8 	Contained=�6H   5C  '   5D  #   5E     5F     ( 5G     P� 5H      �@��HasItems$ false��6:   . �5I  "   �� $) DropDownBorder��6)    � ��$ 95��6Z   5J     P� 5K      �@��
IsGrouping$
 true��6<   . �5L  "   ��  � ��$ false��6R   5M     P� 5N      �@��
IsEditable$
 true��6<   . �5O  "   ��  � ��$ false��6>   �� 5P  "   $) PART_EditableTextBox��6)    � y�$ Visible=�6a   �� 5Q  "   $) ContentSite��6)    � y�$ Hidden=�6X   5R     5T     5V     5W     �� 5Z  
   "	 ��6-   .�5[     ��  � �� 5\     �� 5]     " ��6&   .R�5^     �� $% border��6!   #��z��61   #��z��6[   #��z��6�   $
4 True��6�   ._�5_     �� $ PART_ContentHost��6+   $|�false��6E   $��Hidden=�6W   $��Hidden=�6~   5`     ( 5a     P� 5b      �@J�IsMouseOver$
 True��6=   . �5c  "   �� $) border��6@    � �� �	#FF3C7FB16T   5d     P� 5e      �@J�	IsFocused$
 True��6;   . �5f  "   �� $) border��6@    � �� �	#FF3C7FB16T   5g     P� 5h      �@��
IsReadOnly$
 True��6<   . �5i  "   �� $) border��6@    � ��
 �   �6T   5j     5l     5n     5o     5p     5r     