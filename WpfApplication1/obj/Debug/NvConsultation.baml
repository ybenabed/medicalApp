   M S B A M L   `   `   `  ����  WpfApplication1$    WpfApplication1.NvConsultation   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  NvConsultation��5   	   $	��300��6    $	��550��6-    � 
ResizeMode$ CanMinimize=�69    � WindowStyle$
 None=�6R   .��5      � .+�5   
   �� -   5        $��ComponentsB��5   ,    �� Orientation$ Vertical=�6   .+�5      �� $ 
Horizontal=�6   .+�5	      �� -   5        $��Stack_title��5	      $��30��60   $	��500��6<   ���#0c4a60.+�5
      �� $��Consultation��6   $��18(�63   $
��Left=�6A   $��	219,0,0,0q�6\   $��SemiBold"�6o   $��30��6�   ���	#DDFFFFFF��6�   Z� 6�   5      �� 5      $ 
Horizontal=�6   $��50��66   ���#0c4a60.+�5      '  !MaterialDesignThemes.Wpf.PackIcon  -   5        $��ic_minimise��5   w   �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ WindowMinimize=�6.   
������� � Cursor$
 Hand��6W   $��12,5,0,0q�6e   6�     5      -   5        $��ic_close��5   n   $ Close=�6.   
�������$
 Hand��6O   $��5,5,0,0q�6]   6~   5      5      �� 5      $ 
Horizontal=�6   $��0,0,0,0q�62   .+�5      �� $��Nom :��6   $��15(�6(   $��10,0,0,0q�66   $��SemiBold"�6H   �� 5      -   5         ��Name$	 nom��5      $	��110��6'   $��10,0,0,0q�63   $��14(�6E    �� 
IsReadOnly . 6S   �� 5      $��Prenom :��6   $��10,0,0,0q�6+   $��15(�6=   $��SemiBold"�6K   �� 5      -   5        $��prenom��5      $��10,0,0,0q�6(   $	��110��6:   $��14(�6F    . 6T   �� 5      $��Age :��6   $��15(�6(   $��10,0,0,0q�66   $��SemiBold"�6H   �� 5      -   5        $	��age��5      $��80��6%   $��10,0,0,0q�60   $��14(�6B    . 6P   5      �� 5      $��Diagnostique :��6   $��22(�6-   $��	10,10,0,0q�6;   �� 5      -   5        $ diagno��5      $	��120��6&   $��	10,0,10,0q�63   $	��Top=�6F   $��14(�6e   �� 5      $ 
Horizontal=�6   $	��550��62   $��0,5,0,0q�6>   .+�5      �� -	   5        $��	buttenreg��5      $��Enregistrer��6*   $	��106��6@   $��	350,0,0,0q�6L   ���	#FFD54A42��6w   Z� ��6�   Z� 6�   �� 5      -
   5        $��buttannuler��5      $��Annuler��6,   $��86��6>   $��0,0,0,0q�6I   ���	#FFD54A42��6t   Z� ��6�   Z� 6�   5      5      �� 5    
   -   5        $��RecconfirmStack��5       $��Center=�6+   $��Center=�6H   $	��310��6c   $��60��6o   $��120,124,120,116q�6{   
�������$y�Hidden=�6�    J� Effect 5!      3  -System.Windows.Media.Effects.DropShadowEffect  5"      	   
BlurRadius$	 15Q�6#   
   	Direction$
 0Q�63      RenderingBias$ Quality=�6A      ShadowDepth$ 1Q�6Y      Color$ 	#FFBBBBBB��6i   5$      �� 5%   
   -   5        $��confirmStack��5%      $ Vertical=�6)   $��Center=�6@   $��120,124,120,118q�6\   
�������$y�Hidden=�5&   (   .+�5'      �� $ ��Voulez-vous bien quitter ?��6   $��Center=�69   �� 5(      $ 
Horizontal=�6   $��0,0,0,5q�62   .+�5)      �� -   5        $��buttoui��5)   T   $	��Oui��6   $��Center=�6'   $��80,0,0,0q�6B   ���	#FFD54A42��6�   Z� ��6�   Z� �� 5*      -   5        $��buttnon��5*   T   $	��Non��6   $��Center=�6'   $��30,0,0,0q�6B   ���	#FFD54A42��6�   Z� ��6�   Z� 5+      5,      �� 5-   
   -   5        $��RecStackriensaisi��5-      $��Center=�6-   $��Center=�6J   $	��206��6e   $��56��6q   $��172,124,172,120q�6}   
�������$y�Hidden=�6�    5.        5/      $	 15Q�6#   $
 0Q�63   $ Quality=�6A   $ 1Q�6Y   $ 	#FFBBBBBB��6i   51      �� 52   
   -   5        $��Stackriensaisi��52      $ Vertical=�6+   $��Center=�6B   $��Center=�6_   $y�Hidden=�6z   .+�53      �� $'��!Attention vous n'avez rien saisir��6   $��14(�6@   �� 54      $��Center=�6   .+�55      �� -   5        $��buttok��55      $��ok��6'   ���	#FFD54A42��64   Z� ��6J   Z� 6x   56      58      59      5:      