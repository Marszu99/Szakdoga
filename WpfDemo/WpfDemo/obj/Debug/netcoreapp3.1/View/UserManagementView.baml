   M S B A M L   `   `   `  ����B  >WpfDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfDemoWpfDemo  5      +clr-namespace:WpfDemo.ViewWpfDemo.View  5      M ITimeSheet.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.ResourceTimeSheet.Resource 5      %    WpfDemo.View.UserManagementView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WpfDemo     5      +viewclr-namespace:WpfDemo.View     5      Ml10n<clr-namespace:TimeSheet.Resource;assembly=TimeSheet.Resource     5      
�������.��5      � $
��1510��6     � ColumnDefinitions  5   
   �� 5      $	��910 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   � $��0��6   $��0 0 0 0q�6    5      �� 5      $��35 �6    �� 5      $��* �6    .+�5      �� $��0��6   $��0��6)    �� Orientation$ 
Horizontal=�66   $��10 5 0 5q�6O   .+�5      �� -   5         ��Name$ NewUserButton��5      $
��Left=�6V   $	��6 0q�6r   $��1q�5      
���   � � Cursor$
 Hand��6=   ��5   0   �� *CreateUserCommand+y�5   K   �� *!UserManagementButtonsVisibility+ � ToolTip 5       !   WpfDemo.ResxStaticExtension  *l10n:Resources.CreateNewUser+.��5!      �� $\��VC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\create-user.png��6    � 	Resources 5"       �� 
TargetType �� Property	 �� Value��%^   '��       �� 5#      " ��6    .�5$      ��  �@��CornerRadius$	 4��6=   5%      5&      5'      �� 5)      -   5        $ ExportExcelButton��5)      $
��Left=�6]   $��10 0 0 0q�6x   $	��6 0q�6�   $��1q�5*      
���   �$
 Hand��6=   ��5)   4   �� *ExportToExcelCommand+y�5*   K   �� *!UserManagementButtonsVisibility+ 5+        *l10n:Resources.ExportToExcel+.��5,      �� $V��PC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\excel.png��6    5-      ��%^   '��       �� 5.      " ��6    .�5/      ��  �@��CornerRadius$	 4��6=   50      51      52      53      �� 56      $��0��6   $ 
Horizontal=�6&   $��Right=�6?   $��0 5 0 5q�6[   .+�57      ��  #  SearchTextBoxWithWaterMarkStyle#���   6\   $��0q�6�   ���#eee$	��165��58      $��25��6&   $��18(�62   $��Center=�6@   $
��Left=�6[   $��1 1 0 1q�6}   
���   �$��2 0 0 1q�59      $ IBeam��6[   ��57      �� *SearchValue+
 �� UpdateSourceTrigger$
 PropertyChanged=�
 � Tag 59   ,     *l10n:Resources.Search+.��6j    5:      ��%d   '��       �� 5;      " ��6    .�5<      ��  �@��CornerRadius$	 4 0 0 4��6=   5=      5>      .��6)    J� InputBindings 5?      �� 5@      
 �� Key$ Enter��6%    �� Command 61   �� *SearchingCommand+.��5A   -   5B      �� 5C        SearchButtonStyle#���  6>   $��0 1 1 1q�6j   
���   �$
 Hand��6�   ��6   �� *SearchingCommand+ 5D        *$"l10n:Resources.SearchButtonMessage+.��5E      �� $W��QC:\Marci\Egyetem\BLB-Soft\Szakdoga\Szakdoga\WpfDemo\WpfDemo\Components\search.png��6    5F      ��%d   '��       �� 5G      " ��6    .�5H      ��  �@��CornerRadius$	 0 4 4 0��6=   5I      5J      5K      5L          WpfDemo.View.ListUsersView  5N      -   5          Name$ ListUsersContent��5N   !   $��1��6;   $��10 0 0 0q�6H   
���   �$��1q�6n   5O         WpfDemo.View.UserView  5R   
   $��1��6   $��1��6%   $��Right=�65   $	��Top=�6Q   $��0 5 0 0q�6i   $��	0 35 16 0q�6{   y�5S      �� *SelectedUserVisibility+5T       5V        FocusVisual �� StrokeDashArray J� SnapsToDevicePixels  Button.Static.Background �� Color  Button.Static.Border  Button.Disabled.Background  Button.Disabled.Border  Button.Disabled.Foreground �� 
TargetType ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName ��Name ��Name ]� 
Conditions �� Property �� Value��%/  &	       &	 �     &	 �     &	 �     &	     &	 ?    &	 b    7  7  7  7  7  7  &	  ,    �� 5W   
   .�5X      ��  � ��	 5Y      �� 5Z      .R�5[      �� $��2q�6$   $	 1 2R�6/   $
 true��6E   $��1��6`   #��� ��6t   5\      5^      5_      �� 5`   
   $ 	#FFDDDDDD��6;   �� 5a   
   $ 	#FF707070��67   �� 5b   
   $ 	#FFF4F4F4��6=   �� 5c   
   $ 	#FFADB2B5��69   �� 5d   
   $ 	#FF838383��6=   �� 5e   
   " ��6*   .�5f      ��  �@�FocusVisualStyle8	   61   �� 5g       � ��8	  6+   �� 5h       � ��8	  6,   �� 5i       � ��#	 � ��6+   �� 5j       � ��$	 1q�60   �� 5k       � ��$	 Center=�6;   �� 5l       � ��$	 Center=�69   �� 5m       � ��$	 1q�6(   �� 5n       � ��	 5o      �� 5p      " ��6&   .R�5q      �� $ border��6!   #��z��61   #��z��6[   #��z��6�   $
 true��6�   ._�5r      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   # z 6�   #��z��6  5s       5t      P� 5u       �@��IsDefaulted$
 true��6=   . �5v   "   �� $ border��6@   #	 � ��6T    � ��5w      P� 5x       � {�$ false��6;   . �5y   "   �� $ border��6?   8	   � ��6S   �� 5z   "   $ border��6@   8	   � ��6T   �� 5{   "   $ contentPresenter��6K   8	   � ��6i   5|      5~      5�      5�      �� 5�   
   " ��68   .�5�      ��  � ��	 5�      �� 5�      " ��6&   .R�5�      � .+�5�      �� #��z��6%   #��z��6O   #��z��6�   $
 True��6�   �� 5�      $ PART_ContentHost��6+   $��0q�6E   $��Center=�6P   �� 5�      $ WaterMarkLabel��65   $��2 0 2 1q�6$   #��z 6M   $��Center=�6m   $y�	Collapsed=�5�   $   
�������$��1 0 0 0q�6M   5�       5�      ]� 5�       5�   "   �� 5�   &    � ��$  ��6@   ./�5�   "   �� $ WaterMarkLabel��6?    � y�$	 Visible=�6[   5�      P� 5�       � {�$ False��6;   . �5�   "   ��  � ��
	 �iii�6?   5�      5�      5�      5�      5�      5�      