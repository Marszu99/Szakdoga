   M S B A M L   `   `   `  ����B  >WpfDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfDemoWpfDemo  5   	   M ITimeSheet.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.ResourceTimeSheet.Resource 5   	   7 clr-namespace:WpfDemo.ComponentsWpfDemo.Components  5   	       WpfDemo.View.LoginView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfDemo     5   	   Ml10n<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.Resource     5   	   7
components clr-namespace:WpfDemo.Components     5   	   -   5        $��Login��5
   	   
�������.��5      � .+�5   
   �   � ColumnDefinitions  5      �� 5      $
��0.5* �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $��* �6    �� 5      $��* �6    .+�5      �� $��0��6   $��1��6%   $��Worktime Registry��65   $��84(�6N   $��SemiBold"�6\   $��Italic$�6r   $
��left=�6�   $	��Top=�5      $��
-60 85 0 0q�60   $��High Tower Text)�6D   �� 5      $��0��6J   $��0��6Z   $
��Bold"�6g   $��Right=�6y   $��Bottom=�5      $��0 0 0 30q�63   $��55(�5      ��5      !   WpfDemo.ResxStaticExtension  *l10n:Resources.Username+5      �� 5      $��0��6J   $��1��6Z   $
��Bold"�6g   $��Right=�6y   $	��Top=�5      $��0 15 0 0q�60   $��55(�6B   ��5        *l10n:Resources.Password+5   B   �� 5!      -   5         ��Name$ LoginUsername��5!      $��0��6G   $��1��6T   $��Bottom=�6d   $
��Left=�6   $��	20 0 0 30q�5"      $	��2,0q�6)   $	��500��67   $��1q�6C   
���   �$��42(�6k   ���#eee��5!   -   �� *
Username+.��5"   �    � 	Resources 5#       �� 
TargetType �� Property �� Value��%^   '��       �� 5$      " ��6   .�5%      ��  �@��CornerRadius$ 4��69   5&      5'      .��6%   5(      ,   &WpfDemo.Components.BindablePasswordBox  5+      -   5          Name$ LoginPassword��5+   -   $��1��5,   -   $��1��6:   $��42(�6J   $	��500��6X   $��	20 25 0 0q�5-   -   $	��Top=�6@   $
��Left=�6X      Password 5+   D   �� *
Password+	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� Mode$
 TwoWay=�5.      �� 56      -   5         ��Name$ LoginButton��56      $��1��6}   $��1��6�   $��52��57      $��31(�6!   $��	20 0 0 80q�6/   $	��7 0q�6B   $
��Left=�6P   $��1q�6k   
���   � � Cursor$
 Hand��6�   ��56   *     *l10n:Resources.Login+��6\   �� *LoginCommand+ 58      ��%_   '��       �� 59      " ��6   .�5:      ��  �@��CornerRadius$ 10��69   5;      5<      5=      �� 5?      $��1��6   $��1��6&   $��
20 85 0 50q�66    �� Orientation$ 
Horizontal=�6J   $
��Left=�6c   $	��Top=�6~   .+�5@      �� -   5         ��Name$ LoginErrorIcon��5@      $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\error.png��60   $��50��6�   $��50��6�   $y�Hidden=�5A      $��0 0 0 0q�6,   �� 5B      -   5         ��Name$ LoginUserErrorMessage��5B      $��35(�6;   
���  ��6I   5C      5F       � 5G   
   -   5          �Name$ LoginWindow��5G      $��0 0 0 0q�6%     � NavigationUIVisibility$ Hidden=�66   5I       J� InputBindings 5K      �� 5L   
   
 �� Key$ Enter��6    �� Command 6!   �� *LoginCommand+5O      