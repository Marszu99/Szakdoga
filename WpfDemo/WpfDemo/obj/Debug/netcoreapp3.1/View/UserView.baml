   M S B A M L   `   `   `  ����B  >WpfDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfDemoWpfDemo  5   	   M ITimeSheet.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.ResourceTimeSheet.Resource 5   	       WpfDemo.View.UserView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfDemo     5   	   Ml10n<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.Resource     5   	   -   5        $
��User��5	   	   $	��540��6D   
�������
���   �$��1q�6{   ��6   �� *SelectedUser.UserViewHeight+.��5      �   � RowDefinitions  5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      ��6   �� * SelectedUser.UserViewRowHeight+�� 5      ��6   �� * SelectedUser.UserViewRowHeight+�� 5      ��6   �� * SelectedUser.UserViewRowHeight+�� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��1.4* �6   .+�5   
   �� $��0��6   $
��Bold"�6S   $��0��6e   $��Right=�6u   $��Center=�5      $��0q�6/   $��30(�6:   ��5   !   !   WpfDemo.ResxStaticExtension  *l10n:Resources.Username+y�5   H   �� *)'SelectedUser.UserViewUsernameVisibility+�� 5   
   $��0��6   $
��Bold"�6\   $��0��6n   $��Right=�6~   $��Center=�5      $��0q�6/   $��30(�6:   ��5   !     *" l10n:Resources.UsernameWithColon+y�5   H   �� *20SelectedUser.UserViewUsernameWithColonVisibility+�� 5   
   $
��Bold"�6�   $��0��5      $��Right=�6$   $��Center=�6@   $��0q�6[   $��30(�6f   ��5      �� *'%SelectedUser.UserViewFirstNameGridRow+��6O     *#!l10n:Resources.FirstNameWithColon+5   f   �� 5    
   $��2��6   $
��Bold"�6\   $��0��6n   $��Right=�6~   $��Center=�5!      $��0q�6/   $��30(�6:   ��5    !     *" l10n:Resources.LastNameWithColon+5!   :   �� 5"   
   $
��Bold"�6z   $��0��6�   $��Right=�5#      $��Center=�60   $��0q�6K   $��30(�6V   ��5"      �� *#!SelectedUser.UserViewEmailGridRow+��6K     *l10n:Resources.Email+y�5#   d   �� *&$SelectedUser.UserViewEmailVisibility+�� 5$   
   $
��Bold"�6�   $��0��6�   $��Right=�5%      $��Center=�60   $��0q�6K   $��30(�6V   ��5$      �� *#!SelectedUser.UserViewEmailGridRow+��6K     *l10n:Resources.EmailWithColon+y�5%   d   �� */-SelectedUser.UserViewEmailWithColonVisibility+�� 5&   
   $��4��6   $
��Bold"�6]   $��0��6o   $��Right=�6   $��Center=�5'      $��0q�6/   $��30(�6:   ��5&   !     *#!l10n:Resources.TelephoneWithColon+5'   :   �� 5*   
   $��0��6   $��1��6"    �� Orientation$ 
Horizontal=�62   .+�5+      �� -   5         ��Name$ UserUsername��5+      $��Center=�5,      $��Stretch=�61   $��26(�6O   $��10 5 5 0q�6]   $	��200��6p   
���   ���5+   ,   �� *SelectedUser.Username+ �� ValidatesOnDataErrors$
 True�� �� UpdateSourceTrigger$ PropertyChanged=���5-      �� *,*SelectedUser.UserViewValuesBorderThickness+��5.      �� *'%SelectedUser.UserViewValuesBackground+ �� 
IsReadOnly 6S   �� *'%SelectedUser.IsUserViewValuesReadOnly+ ^� ToolTip 5/      �� *(&SelectedUser.ErrorCollection[Username]+.��6`    � 	Resources 50      	 �� 
TargetType
 �� Property �� Value��%^   '��       �� 51      "	 ��6   .�52      �� 
 �@��CornerRadius$ 4��69   53      54      .��6%   55      �� 56      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��60��6n   $��60��6y   y�57      �� **(SelectedUser.UsernameErrorIconVisibility+58      �� 5;   
   -   5        $ UserFirstName��5;      $��1��5<   M   $��Stretch=�6]   $��Center=�5=      $��10 5 0 0q�6-   $��26(�6?   $��0q�6M   $
 True��5>   O   ��5;   )   �� *SelectedUser.FirstName+$ PropertyChanged=���5<      �� *'%SelectedUser.UserViewFirstNameGridRow+��5>      �� *'%SelectedUser.UserViewValuesBackground+{�5?      �� *&$SelectedUser.IsUserViewValuesEnabled+.��6M    5@      ��%^   '��       �� 5A      "	 ��6   .�5B      �� 
 �@��CornerRadius$ 4��65   5C      5D      .��6!   5E      �� 5G   
   -   5        $ UserLastName��5G      $��2��5H      $��1��6   $��Stretch=�6/   $��Center=�6M   $��10 5 0 0q�6h   $��26(�6z   $��0q�5I      $
 True��5J      ��5G   (   �� *SelectedUser.LastName+$ PropertyChanged=���5I   '   �� *'%SelectedUser.UserViewValuesBackground+{�5J   $   �� *&$SelectedUser.IsUserViewValuesEnabled+.��6_    5K      ��%^   '��       �� 5L      "	 ��6   .�5M      �� 
 �@��CornerRadius$ 4��65   5N      5O      .��6!   5P      �� 5R   
   $��1��6L   $ 
Horizontal=�6\   ��6   �� *#!SelectedUser.UserViewEmailGridRow+.+�5S      �� -   5        $ 	UserEmail��5S      $��Center=�5T   H   $��Stretch=�6c   $��10 1 0 0q�6�   $��26(�6�   
���   ���5S   )   �� *SelectedUser.Email+$
 True��$ PropertyChanged=���5T      �� *!SelectedUser.UserViewEmailWidth+��5U      �� *,*SelectedUser.UserViewValuesBorderThickness+��5V      �� *'%SelectedUser.UserViewValuesBackground+ 6S   �� *'%SelectedUser.IsUserViewValuesReadOnly+ 5W      �� *%#SelectedUser.ErrorCollection[Email]+.��6]    5X      ��%^   '��       �� 5Y      "	 ��6   .�5Z      �� 
 �@��CornerRadius$ 4��69   5[      5\      .��6%   5]      �� 5^      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��6   $��60��6n   $��60��6y   y�5_      �� *'%SelectedUser.EmailErrorIconVisibility+5`      �� 5c   
   -   5        $ UserTelephone��5c      $��4��5d      $��1��6   $��Stretch=�6/   $��Center=�6M   $��10 5 0 0q�6h   $��26(�6z   $��0q�5e      $
 True��5f      ��5c   )   �� *SelectedUser.Telephone+$ PropertyChanged=���5e   &   �� *'%SelectedUser.UserViewValuesBackground+{�5f   $   �� *&$SelectedUser.IsUserViewValuesEnabled+.��6_    5g      ��%^   '��       �� 5h      "	 ��6   .�5i      �� 
 �@��CornerRadius$ 4��65   5j      5k      .��6!   5l      �� 5o   
   $��1��6   $��5��6%   $ 
Horizontal=�62   .+�5p      �� -   5         ��Name$ SaveUserButton��5p      $��42��6�   $��10 0 0 0q�5q      $	��6 3q�6'   $��24(�65   $��1q�6C   
���   � � Cursor$
 Hand��6k   ���	#FF66B2FD��5p   -     *l10n:Resources.Save+��6^   �� *SelectedUser.SaveCommand+y�5r      �� *+)SelectedUser.UserViewSaveButtonVisibility+ 5s      ��%^   '��       �� 5t      "	 ��6   .�5u      �� 
 �@��CornerRadius$ 4��69   5v      5w      5x      �� 5z      -   5        $ CancelCreateUserButton��5z      $��42��5{      $��20 0 0 0q�6!   $	��6 3q�63   $��26(�6A   $��1q�6O   
���   �$
 Hand��6w   ���	#FF66B2FD��5z   5     *l10n:Resources.Cancel+��6h   �� *$"SelectedUser.CancelUserViewCommand+ 5|      ��%^   '��       �� 5}      "	 ��6   .�5~      �� 
 �@��CornerRadius$ 4��69   5      5�      5�      5�      5�       J� InputBindings 5�      �� 5�   
   
 �� Key$ Enter��6    �� Command 6!   �� *SelectedUser.SaveCommand+5�      