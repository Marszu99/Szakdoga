   M S B A M L   `   `   `  ����B  >WpfDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfDemoWpfDemo  5      7 clr-namespace:WpfDemo.ComponentsWpfDemo.Components  5          WpfDemo.View.RegisterView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WpfDemo     5      7
components clr-namespace:WpfDemo.Components     5      -   5        $��RegisterPage��5
      $	��830��5	      $
��1550��6   
�������.��5      � .+�5   
   � $��0 0 0 0q�6     � RowDefinitions  5      �� 5      $��85 �6    �� 5      $��* �6    �� 5      $	��100 �6    .+�5      �� $��Registration��6   $��0��6,   $
��Bold"�69   $��2��6L   $��Center=�6\   $��Center=�6y   $��0q�5      $��50(�6#   � 5      $��1��6     5      �� 5      $��* �6$   �� 5      $��* �6$   �� 5      $��* �6$   �� 5      $��* �6$   �� 5      $��* �6$    � ColumnDefinitions 5!      �� 5"      $��0.55* �6'   �� 5#      $
��0.9* �6'   �� 5$      $
��0.7* �6'   �� 5%      $
��0.9* �6'   �� 5&      $
��0.2* �6'   .+�5*      �� $��Username��6   $��0��6,   $
��Bold"�69   $��0��6L   $��Right=�6\   $��Center=�6x   $��0 0 20 0q�5+      $��40(�6.   �� 5,      $��Password��6   $��1��6,   $
��Bold"�69   $��0��6L   $��Right=�6\   $��Center=�6x   $��0 0 20 0q�5-      $��40(�6.   �� 5.      $��	Password2��6   $��2��6-   $
��Bold"�6:   $��0��6M   $��Right=�6]   $��Center=�6y   $��0 0 20 0q�5/      $��40(�6.   �� 50      $��	FirstName��6   $��3��6-   $
��Bold"�6:   $��0��6M   $��Right=�6]   $��Center=�6y   $��0 0 20 0q�51      $��40(�6.   �� 52      $��LastName��6   $��4��6,   $
��Bold"�69   $��0��6K   $��Right=�6[   $��Center=�6w   $��0 0 20 0q�53      $��40(�6.   �� 54      $��Email��6   $��0��6)   $
��Bold"�66   $��2��6H   $��Right=�6X   $��Center=�6t   $��0 0 20 0q�55      $��40(�6.   �� 56      $��Email2��6   $��1��6*   $
��Bold"�67   $��2��6I   $��Right=�6Y   $��Center=�6u   $��0 0 20 0q�57      $��40(�6.   �� 58      $��	Telephone��6   $��2��6-   $
��Bold"�6:   $��2��6L   $��Right=�6\   $��Center=�6x   $��0 0 20 0q�59      $��40(�6.   �� 5:      $��Company��6   $��3��6+   $
��Bold"�68   $��2��6J   $��Right=�6Z   $��Center=�6v   $��0 0 20 0q�5;      $��40(�6.   �� 5<      $��Company2��6   $��4��6,   $
��Bold"�69   $��2��6K   $��Right=�6[   $��Center=�6w   $��0 0 20 0q�5=      $��40(�6.   �� 5?      -   5         ��Name$ CreateUserUsername��5?      $��0��5@      $��1��6'   $��Center=�67   $��0q�6R   $��Stretch=�6]   $��32(�6{   ���#eee$	��2,0q�5A      $��1q�6(   
���   ���5?   6   �� *
Username+ �� ValidatesOnDataErrors$
 True�� �� UpdateSourceTrigger$ PropertyChanged=� ^� ToolTip 5A   P   �� *ErrorCollection[Username]+.��6�    � 	Resources 5B       �� 
TargetType �� Property	 �� Value��%^   '��       �� 5C      " ��6    .�5D      ��  �@��CornerRadius$	 4��6=   5E      5F      .��6)   5G      ,   &WpfDemo.Components.BindablePasswordBox  5I      -   5        
  Name$
 CreateUserPassword��5I   1   $��1��5J   1   $��1��6>   $��Center=�6N   $��0q�6i   $��Stretch=�6t   $��32(�6�   ���#eee   Password 5I   M   �� *
Password+$
 True��$ PropertyChanged=� �� Mode$ TwoWay=� 5K   C   �� *ErrorCollection[Password]+ 5L      ��%^   '��       �� 5M      " ��6    .�5N      ��  �@��CornerRadius$	 4��6=   5O      5P      5Q        5S      -   5        $
 CreateUserPassword2��5S   1   $��2��5T   1   $��1��6>   $��Center=�6N   $��0q�6i   $��Stretch=�6t   $��32(�6�   ���#eee 5S   N   �� *	Password2+$
 True��$ PropertyChanged=�$ TwoWay=� 5U   C   �� *ErrorCollection[Password2]+ 5V      ��%^   '��       �� 5W      " ��6    .�5X      ��  �@��CornerRadius$	 4��6=   5Y      5Z      5[      �� 5]      -   5        $ CreateUserFirstName��5]      $��3��5^      $��1��6'   $��Center=�67   $��0q�6R   $��Stretch=�6]   $��32(�6{   ���#eee$	��2,0q�5_      $��1q�6(   
���   ���5]   7   �� *	FirstName+$
 True��$ PropertyChanged=� 5_   P   �� *ErrorCollection[FirstName]+.��6�    5`      ��%^   '��       �� 5a      " ��6    .�5b      ��  �@��CornerRadius$	 4��6=   5c      5d      .��6)   5e      �� 5g      -   5        $ CreateUserLastName��5g      $��4��5h      $��1��6'   $��Center=�67   $��0q�6R   $��Stretch=�6]   $��32(�6{   ���#eee$	��2,0q�5i      $��1q�6(   
���   ���5g   6   �� *
LastName+$
 True��$ PropertyChanged=� 5i   P   �� *ErrorCollection[LastName]+.��6�    5j      ��%^   '��       �� 5k      " ��6    .�5l      ��  �@��CornerRadius$	 4��6=   5m      5n      .��6)   5o      �� 5q      -   5        $ CreateUserEmail��5q      $��0��5r      $��3��6'   $��Center=�67   $��0q�6R   $��Stretch=�6]   $��32(�6{   ���#eee$	��2,0q�5s      $��1q�6(   
���   ���5q   3   �� *Email+$
 True��$ PropertyChanged=� 5s   P   �� *ErrorCollection[Email]+.��6�    5t      ��%^   '��       �� 5u      " ��6    .�5v      ��  �@��CornerRadius$	 4��6=   5w      5x      .��6)   5y      �� 5{      -   5        $ CreateUserEmail2��5{      $��1��5|      $��3��6'   $��Center=�67   $��0q�6R   $��Stretch=�6]   $��32(�6{   ���#eee$	��2,0q�5}      $��1q�6(   
���   ���5{   4   �� *Email2+$
 True��$ PropertyChanged=� 5}   P   �� *ErrorCollection[Email2]+.��6�    5~      ��%^   '��       �� 5      " ��6    .�5�      ��  �@��CornerRadius$	 4��6=   5�      5�      .��6)   5�      �� 5�      -	   5        $ CreateUserTelephone��5�      $��2��5�      $��3��6'   $��Center=�67   $��0q�6R   $��Stretch=�6]   $��32(�6{   ���#eee$	��2,0q�5�      $��1q�6(   
���   ���5�   7   �� *	Telephone+$
 True��$ PropertyChanged=� 5�   P   �� *ErrorCollection[Telephone]+.��6�    5�      ��%^   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$	 4��6=   5�      5�      .��6)   5�      �� 5�      -
   5        $ CreateUserCompanyName��5�      $��3��5�      $��3��6'   $��Center=�67   $��0q�6R   $��Stretch=�6]   $��32(�6{   ���#eee$	��2,0q�5�      $��1q�6(   
���   ���5�   9   �� *CompanyName+$
 True��$ PropertyChanged=� 5�   P   �� *ErrorCollection[CompanyName]+.��6�    5�      ��%^   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$	 4��6=   5�      5�      .��6)   5�      �� 5�      -   5        $ CreateUserCompanyName2��5�      $��4��5�      $��3��6'   $��Center=�67   $��0q�6R   $��Stretch=�6]   $��32(�6{   ���#eee$	��2,0q�5�      $��1q�6(   
���   ���5�   :   �� *CompanyName2+$
 True��$ PropertyChanged=� 5�   P   �� *ErrorCollection[CompanyName2]+.��6�    5�      ��%^   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$	 4��6=   5�      5�      .��6)   5�      �� 5�      $��2��6   $��0��6(   $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��65   $
��Left=�5�   K   $��Center=�6f   $��60��6�   $��60��6�   y�6   �� *UsernameErrorIconVisibility+6�   �� 5�      $��2��6   $��1��6(   $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��65   $
��Left=�5�   K   $��Center=�6f   $��60��6�   $��60��6�   y�6   �� *PasswordErrorIconVisibility+6�   �� 5�      $��2��6   $��2��6(   $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��65   $
��Left=�5�   L   $��Center=�6g   $��60��6�   $��60��6�   y�6   �� *Password2ErrorIconVisibility+6�   �� 5�      $��2��6   $��3��6(   $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��65   $
��Left=�5�   L   $��Center=�6g   $��60��6�   $��60��6�   y�6   �� *FirstNameErrorIconVisibility+6�   �� 5�      $��2��6   $��4��6(   $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��65   $
��Left=�5�   K   $��Center=�6f   $��60��6�   $��60��6�   y�6   �� *LastNameErrorIconVisibility+6�   �� 5�      $��4��6   $��0��6(   $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��65   $
��Left=�5�   H   $��Center=�6c   $��60��6~   $��60��6�   y�6   �� *EmailErrorIconVisibility+6�   �� 5�      $��4��6   $��1��6(   $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��65   $
��Left=�5�   I   $��Center=�6d   $��60��6   $��60��6�   y�6   �� *Email2ErrorIconVisibility+6�   �� 5�      $��4��6   $��2��6(   $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��65   $
��Left=�5�   L   $��Center=�6g   $��60��6�   $��60��6�   y�6   �� *TelephoneErrorIconVisibility+6�   �� 5�      $��4��6   $��3��6(   $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��65   $
��Left=�5�   N   $��Center=�6i   $��60��6�   $��60��6�   y�6   �� * CompanyNameErrorIconVisibility+6�   �� 5�      $��4��6   $��4��6(   $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��65   $
��Left=�5�   O   $��Center=�6j   $��60��6�   $��60��6�   y�6   �� *!CompanyName2ErrorIconVisibility+6�   5�      �� 5�      -   5         ��Name$ CreateUserButton��5�      $��2��6X   $��4��6h   $��Register��6u   $��50��6�   $��30(�5�      $��Center=�6#   $��0q�6@   $	��6 3q�6K   $��1q�6Y   
���   � � Cursor$
 Hand��6�   ��5�   /   �� *RegisterAdminCommand+ 5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$	 4��69   5�      5�      5�      5�       � 5�   
   -   5          �Name$ RegisterContent��5�      $��0 0 0 0q�6)     � NavigationUIVisibility$ Hidden=�6:   5�      5�      