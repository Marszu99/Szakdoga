   M S B A M L   `   `   `  ����B  >WpfDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfDemoWpfDemo  5   	   M ITimeSheet.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.ResourceTimeSheet.Resource 5   	   7 clr-namespace:WpfDemo.ComponentsWpfDemo.Components  5   	        WpfDemo.View.MyProfileView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfDemo     5   	   Ml10n<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.Resource     5   	   7
components clr-namespace:WpfDemo.Components     5   	     � Title$  Worktime Registry��5
   	   $
��1550��6#   $	��870��60   
������� � WindowStartupLocation$ Manual=�5   	    � 
ResizeMode$ CanMinimize=�6(    � Left$ -8��5   	   
 � Top$ 0��6    � Icon$S MC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\WR.png��5   	    � WindowStyle 5   A   �� *MyProfileViewWindowStyle+.��5      � $��5 0 5 0q�6    � ColumnDefinitions 5   
   �� 5      $
��1.1* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��50(�6W   $
��Bold"�6e   $��Verdana)�6w   $	��Top=�5      $��Center=�6,   $��100 40 280 0q�6I   $��65��6_   ��5   $   !   WpfDemo.ResxStaticExtension  *l10n:Resources.MyProfile+5   _   �� 5   
   $��0��6    �� Orientation$ Vertical=�6%   $
��Left=�6<   $	��250��6W   $��20 100 0 200q�6c   .+�5      �� $
��Bold"�6S   $��Right=�6e   $��Center=�6�   $��0 40 0 0q�5      $��34(�6*   ��5        *" l10n:Resources.UsernameWithColon+5   *   �� 5      $
��Bold"�6S   $��Right=�6e   $��Center=�6�   $��0 40 0 0q�5      $��34(�6*   ��5        *" l10n:Resources.PasswordWithColon+5   *   �� 5      $
��Bold"�6T   $��Right=�6f   $��Center=�6�   $��0 40 0 0q�5      $��34(�6*   ��5        *#!l10n:Resources.FirstNameWithColon+5   *   �� 5      $
��Bold"�6S   $��Right=�6e   $��Center=�6�   $��0 40 0 0q�5       $��34(�6*   ��5        *" l10n:Resources.LastNameWithColon+5    *   �� 5!      $
��Bold"�6P   $��Right=�6b   $��Center=�6~   $��0 40 0 0q�5"      $��34(�6*   ��5!        *l10n:Resources.EmailWithColon+5"   *   �� 5#      $
��Bold"�6T   $��Right=�6f   $��Center=�6�   $��0 40 0 0q�5$      $��34(�6*   ��5#        *#!l10n:Resources.TelephoneWithColon+5$   *   5%      �� 5'   
   $��0��6   $ Vertical=�6%   $
��Left=�6<   $	��500��6W   $��280 100 0 200q�6c   .+�5(      �� $
��Left=�6g   $��Center=�6�   $��0 40 0 0q�5)      $	��2,0q�6(   $��34(�66   
�������	 �� 
IsReadOnly$
	 True��6Z   $��0q�6l   ��5(      �� *CurrentLoggedUser.Username+
 �� UpdateSourceTrigger$
 PropertyChanged=�.��5)   �    � 	Resources 5*       �� 
TargetType �� Property �� Value��%^   '��       �� 5+      " ��6   .�5,      ��  �@��CornerRadius$ 4��69   5-      5.      .��6%    J� InputBindings 5/      �� 50      
 �� Key$ Enter��6!    �� Command 6-   �� *SaveChangedUserValuesCommand+.��51   )   52      �� 54      $��0 28 0 0q�6   $ 
Horizontal=�6+   .+�55      ,   &WpfDemo.Components.BindablePasswordBox  -   5          Name$ MyProfilePassword��55   1   $
��Left=�56   1   $��Center=�6L   $��34(�6g   $	��125��57   1   $	��440��6@   
�������   Password 55   L   �� *
Password+ �� ValidatesOnDataErrors$
 True��$
 PropertyChanged=� �� Mode$ TwoWay=���56   u   �� *#!MyProfileViewUserValuesBackground+{�57   O   �� *$"MyProfileViewUserPasswordIsEnabled+ ^� ToolTip 58   1   �� *ErrorCollection[Password]+ 59      ��%^   '��       �� 5:      " ��6    .�5;      ��  �@��CornerRadius$ 4��6=   5<      5=       5>      �� 5?      $ Enter��6%    61   �� *SaveChangedUserValuesCommand+5A      �� 5B      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��65��6r   $��65��6}   $��0 0 0 0q�5C   K   y�6   �� *PasswordErrorIconVisibility+6K   5D      �� 5F      $��0 22 0 0q�6   $ 
Horizontal=�6+   .+�5G      �� -   5         ��Name$ MyProfileFirstName��5G      $
��Left=�5H      $��Center=�65   $��34(�6P   $��55��5I   S   $	��440��6a   $	��2,0q�6p   ��5G   6   �� *	FirstName+$
 True��$
 PropertyChanged=���5H   ^   �� *#!MyProfileViewUserValuesBackground+	 5I      �� *#!MyProfileViewUserValuesIsReadOnly+��5J      �� *(&MyProfileViewUserValuesBorderThickness+��6]   �� *$"MyProfileViewUserValuesBorderBrush+ 5K      �� *ErrorCollection[FirstName]+.��6X    5L      ��%^   '��       �� 5M      " ��6    .�5N      ��  �@��CornerRadius$ 4��6=   5O      5P      .��6)    5Q      �� 5R      $ Enter��6%    61   �� *SaveChangedUserValuesCommand+.��5S   -   5T      �� 5U      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��65��6r   $��65��6}   $��0 0 0 0q�5V   L   y�6   �� *FirstNameErrorIconVisibility+6L   5W      �� 5Z      $��0 20 0 0q�6   $ 
Horizontal=�6+   .+�5[      �� -   5        $ MyProfileLastName��5[      $
��Left=�5\      $��Center=�65   $��34(�6P   $��55��5]   S   $	��440��6a   $	��2,0q�6p   ��5[   5   �� *
LastName+$
 True��$
 PropertyChanged=���5\   ^   �� *#!MyProfileViewUserValuesBackground+	 5]      �� *#!MyProfileViewUserValuesIsReadOnly+��5^      �� *(&MyProfileViewUserValuesBorderThickness+��6]   �� *$"MyProfileViewUserValuesBorderBrush+ 5_      �� *ErrorCollection[LastName]+.��6W    5`      ��%^   '��       �� 5a      " ��6    .�5b      ��  �@��CornerRadius$ 4��6=   5c      5d      .��6)    5e      �� 5f      $ Enter��6%    61   �� *SaveChangedUserValuesCommand+.��5g   -   5h      �� 5i      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��65��6r   $��65��6}   $��0 0 0 0q�5j   K   y�6   �� *LastNameErrorIconVisibility+6K   5k      �� 5m      $��0 20 0 0q�6   $ 
Horizontal=�6+   .+�5n      �� -   5        $ MyProfileEmail��5n      $
��Left=�5o      $��Center=�65   $��34(�6P   $	��140��5p   S   $	��440��6b   $	��2,0q�6q   ��5n   2   �� *Email+$
 True��$
 PropertyChanged=���5o   ^   �� *#!MyProfileViewUserValuesBackground+	 5p      �� *#!MyProfileViewUserValuesIsReadOnly+��5q      �� *(&MyProfileViewUserValuesBorderThickness+��6]   �� *$"MyProfileViewUserValuesBorderBrush+ 5r      �� *ErrorCollection[Email]+.��6T    5s      ��%^   '��       �� 5t      " ��6    .�5u      ��  �@��CornerRadius$ 4��6=   5v      5w      .��6)    5x      �� 5y      $ Enter��6%    61   �� *SaveChangedUserValuesCommand+.��5z   -   5{      �� 5|      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��65��6r   $��65��6}   $��0 0 0 0q�5}   H   y�6   �� *EmailErrorIconVisibility+6H   5~      �� 5�      $��0 22 0 0q�6   $ 
Horizontal=�6+   .+�5�      �� -   5        $ MyProfileTelephone��5�      $
��Left=�5�      $��Center=�65   $��34(�6P   $	��170��5�   S   $	��440��6b   $	��2,0q�6q   ��5�   6   �� *	Telephone+$
 True��$
 PropertyChanged=���5�   ^   �� *#!MyProfileViewUserValuesBackground+	 5�      �� *#!MyProfileViewUserValuesIsReadOnly+��5�      �� *(&MyProfileViewUserValuesBorderThickness+��6]   �� *$"MyProfileViewUserValuesBorderBrush+ 5�      �� *ErrorCollection[Telephone]+.��6X    5�      ��%^   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$ 4��6=   5�      5�      .��6)    5�      �� 5�      $ Enter��6%    61   �� *SaveChangedUserValuesCommand+.��5�   -   5�      �� 5�      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��65��6r   $��65��6}   $��0 0 0 0q�5�   L   y�6   �� *TelephoneErrorIconVisibility+6L   5�      5�      �� 5�   
   -   5         ��Name$ ChangeUserValuesButton��5�   T   $��0��6   $��40��5�      $��22(�6   $��Center=�6+   $��Bottom=�6H   $��100 0 300 150q�6c   $��4q�6z   $��1q�6�   
���   � � Cursor$
 Hand��5�   %   ��5�   !     *l10n:Resources.Change+��6t   �� *ChangeUserValuesCommand+y�5�   3   �� */-MyProfileViewChangeUserValuesButtonVisibility+ 5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 4��65   5�      5�      5�      �� 5�   
   -   5        $! SaveChangedUserValuesButton��5�   R   $��0��6   $��40��5�      $��22(�6   $��Center=�6+   $��Bottom=�6H   $��100 0 425 150q�6c   $��5q�6z   $��1q�6�   
���   ����	#FF66B2FD$
 Hand��5�   <   ��5�   !     *l10n:Resources.Save+��6w   �� *SaveChangedUserValuesCommand+y�5�   J   �� *-+MyProfileViewSaveAndCancelButtonsVisibility+ 5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 4��65   5�      5�      5�      �� 5�   
   -   5        $" CancelChangeUserValuesButton��5�   T   $��0��6   $��40��5�      $��22(�6   $��Center=�6+   $��Bottom=�6H   $��100 0 200 150q�6c   $��4q�6z   $��1q�6�   
���   ����	#FF66B2FD$
 Hand��5�   <   ��5�   !     *l10n:Resources.Cancel+��6z   �� *CancelChangeUserValuesCommand+y�5�   J   �� *-+MyProfileViewSaveAndCancelButtonsVisibility+ 5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 4��65   5�      5�      5�      � 5�   
   $��1��6    � RowDefinitions 5�      �� 5�      $��* �6    �� 5�      $��* �6    .+�5�      �� $ 
Horizontal=�6   $��5 30 0 0q�62   .+�5�      �� $[��UC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\to-do-list.png��6   $	��Top=�6w   $��0 4 0 0q�6�   $��35��5�      �� 5�      $��35(�6O   $
��Bold"�6]   $��
Constantia)�6o   $��40��6�   $	��Top=�6�   $��3 3 0 0q�5�      ��5�        *l10n:Resources.ToDoTasks+5�      5�      &   System.Windows.Controls.DataGrid  5�      -	   5          Name$ UserToDoTasksDataGrid��5�      $��	0 70 0 20q�6v   $
��Left=�6�   $	��700��5�         AutoGenerateColumns$ False��6#      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6?      	RowHeight$ 30Q�6O   
�������
���   �$��1q�6�   $
��True��5�      $
��Auto=�6<   $
��Auto=�6l      CanUserAddRows$ False��5�          
IsReadOnly$
  True��6.   $��20(�6@   $��1��6N   
���   ���5�   O   �� *MyToDoTaskList+!   RowStyle! 5�      �� 5�      )  #System.Windows.Controls.DataGridRow"  6   .�5�      ��  � �� �#eee67   �� 5�       � ��
 �   �67   " �� Triggers" 5�      P� 5�      # P� Property# �@ 
IsSelected$ P� Value$
$ true��6<   . �5�   "   ��  � ��
 �����6?   �� 5�   "    � ��
 �����6?   5�      P� 5�      # �@J�IsMouseOver$
$ true��6=   . �5�   "   ��  � ��
 �����6?   �� 5�   "    � ��
 �   �6?   5�      5�      %   	CellStyle% 5�      �� 5�      *  $System.Windows.Controls.DataGridCell"  6   .�5�      ��  � ��
 ���� 67   �� 5�       � ��
 ���� 68   �� 5�       �@�Cursor$
 Hand��63   5�      &   Columns& 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn'   Width$	' 175 6t   (   Header( 6)     *l10n:Resources.Title+1	  +System.Windows.Controls.DataGridBoundColumn) 	  Binding) 6Z   �� *Title+*   HeaderStyle* 5�      �� 5�      =
  7System.Windows.Controls.Primitives.DataGridColumnHeader" 
 6$   .�5�   "   ��  � ��$ Medium"�6?   �� 5�   "    �@�Cursor$
 Hand��6;   5�      5�        5�      $' * 6�   ( 6)     *l10n:Resources.Description+) 6`   �� *Description+* 5�      �� 5�      " 
 6$   .�5�   "   ��  � ��$ Medium"�6?   �� 5�   "    �@�Cursor$
 Hand��6;   5�      5�        5�      $	' 105 6�   ( 6)     *l10n:Resources.Deadline+) 6]   �� *
Deadline++ �� StringFormat$+ 
yyyy.MM.dd��* 5�      �� 5�      " 
 6$   .�5�   "   ��  � ��$ Medium"�6?   �� 5�   "    �@�Cursor$
 Hand��6;   5�      , 	  ElementStyle, 5     �� 5     " ��6$   .�5  "   ��  � ��$ Center=�6H   5     5     5     �� 5	     $��0��6   $��40(�6b   $��0 30 0 0q�6p   $��Center=�6�   $��Center=�6�   - J� Opacity$	- 0.3Q�6�   ��6%     *$"l10n:Resources.ToDoTaskListMessage+y�5
     �� *,*MyProfileViewToDoTaskListMessageVisibility+. � LayoutTransform. 5     �� 5     / �� Angle$/ 20Q�6&   5     �� 5     $��1��6   $ 
Horizontal=�6&   $��5 10 0 0q�6?   .+�5     �� $Z��TC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\done-list.png��6   $	��Top=�6v   $��3 5 0 0q�6�   $��35��5     �� 5     $��35(�6O   $
��Bold"�6]   $��
Constantia)�6o   $��40��6�   $	��Top=�6�   $��2 3 0 0q�5     ��5       *l10n:Resources.DoneTasks+5     5       5     -
   5        $ UserDoneTasksDataGrid��5     $��	0 50 0 20q�6v   $
��Left=�6�   $	��700��5     $ False��6#   $ * 6?   $ 30Q�6O   
�������
���   �$��1q�6�   $
��True��5     $
��Auto=�6<   $
��Auto=�6l   $ False��5     $
  True��6.   $��20(�6@   $��1��6N   $��1��6^   
���   ���5  O   �� *MyDoneTaskList+! 5     �� 5     "  6   .�5      ��  � �� �#eee67   �� 5!      � ��
 �   �67   " 5"     P� 5#     # �@ 
IsSelected$
$ true��6<   . �5$  "   ��  � ��
 �����6?   �� 5%  "    � ��
 �����6?   5&     P� 5'     # �@J�IsMouseOver$
$ true��6=   . �5(  "   ��  � ��
 �����6?   �� 5)  "    � ��
 �   �6?   5*     5,     % 5.     �� 5/     "  6   .�50     ��  � ��
 ���� 67   �� 51      � ��
 ���� 68   �� 52      �@�Cursor$
 Hand��63   53     & 56       57     $	' 175 6t   ( 6)     *l10n:Resources.Title+) 6Z   �� *Title+* 58     �� 59     " 
 6$   .�5:  "   ��  � ��$ Medium"�6?   �� 5;  "    �@�Cursor$
 Hand��6;   5<     5>       5?     $' * 6�   ( 6)     *l10n:Resources.Description+) 6`   �� *Description+* 5@     �� 5A     " 
 6$   .�5B  "   ��  � ��$ Medium"�6?   �� 5C  "    �@�Cursor$
 Hand��6;   5D     5F       5G     $	' 105 6�   ( 6)     *l10n:Resources.Deadline+) 6]   �� *
Deadline+$+ 
yyyy.MM.dd��* 5H     �� 5I     " 
 6$   .�5J  "   ��  � ��$ Medium"�6?   �� 5K  "    �@�Cursor$
 Hand��6;   5L     , 5N     �� 5O     " ��6$   .�5P  "   ��  � ��$ Center=�6H   5Q     5S     5U     �� 5V     $��1��6   $��40(�6b   $��0 30 0 0q�6p   $��Center=�6�   $��Center=�6�   $	- 0.3Q�6�   ��6%     *$"l10n:Resources.DoneTaskListMessage+y�5W     �� *,*MyProfileViewDoneTaskListMessageVisibility+. 5X     �� 5Y     $/ 20Q�6&   5[     5\     5]     5_     